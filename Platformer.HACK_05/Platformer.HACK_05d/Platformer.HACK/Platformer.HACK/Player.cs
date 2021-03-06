#region File Description
//-----------------------------------------------------------------------------
// Player.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    /// <summary>
    /// Our fearless adventurer!
    /// </summary>
    class Player
    {
	    // (16 - 24) / 2		16=tileWidth	24=playerWidth
	    private const int drawOffsetX = -4;

		private readonly int[] ltArray = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 };
	    private readonly int[] rtArray = { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
	    private readonly int[] ttArray = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 };
	    private readonly int[] btArray = { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };

	    const int COUNT = 17;
	    const int MAX_X = 10;
	    private readonly int[] velocityXgnd = { 1, 2, 2, 2, 2, 2, 2, 2, 3, 3 };
	    private readonly int[] velocityXair = { 1, 2, 3, 3, 3, 3, 3, 3, 3, 3 };
		private readonly int[] velocityY = { -11, -9, -7, -6, -6, -5, -4, -4, -3, -3, -2, -2, -2, -1, -1, -1, -1 };
	    private readonly int[] gravityZ = { 1, 1, 2, 2, 3, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
	    private int player_idxX, player_idxY, player_grav;
	    private int jumpFrame;
	    private int deltaX, deltaY;

	    //private static bool MovePlayer = true; 
	    private KeyboardState prevKeyboardState;
	    private const int deltaM = 1;
	    private bool shouldLog;
	    private Texture2D BoundImage;
	    private enum_move_type player_move_type;

		// Position deltas.
	    private int[] posDeltaAirX = new[] { 0, 1, 2, 3, 4, 5 };
	    private int[] posDeltaGndX = new[] { 0, 1, 2, 3, 4, 5 };
	    private int[] posDeltaY = new[] { 0, 1, 2, 3, 4, 5 };

        // Animations
        private Animation idleAnimation;
		//private Animation runAnimation;
		//private Animation jumpAnimation;
		//private Animation celebrateAnimation;
		//private Animation dieAnimation;
        //private SpriteEffects flip = SpriteEffects.None;
        private AnimationPlayer sprite;

        public Level Level
        {
            get { return level; }
        }
        Level level;

        public bool IsAlive
        {
			get { return isAlive; }
        }
        bool isAlive;

        // Physics state
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        Vector2 position;

        private float previousBottom;

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        Vector2 velocity;

        // Constants for controling horizontal movement
        private const float MoveAcceleration = 13000.0f;
        private const float MaxMoveSpeed = 1750.0f;
        private const float GroundDragFactor = 0.48f;
        private const float AirDragFactor = 0.58f;

        // Constants for controlling vertical movement
        private const float MaxJumpTime = 0.35f;
        private const float JumpLaunchVelocity = -3500.0f;
        private const float GravityAcceleration = 3400.0f;
        private const float MaxFallSpeed = 550.0f;
        private const float JumpControlPower = 0.14f; 

        // Input configuration
        private const float MoveStickScale = 1.0f;
        private const float AccelerometerScale = 1.5f;
        private const Buttons JumpButton = Buttons.A;

        /// <summary>
        /// Gets whether or not the player's feet are on the ground.
        /// </summary>
        public bool IsOnGround
        {
            get { return isOnGround; }
        }
        bool isOnGround;

        /// <summary>
        /// Current user movement input.
        /// </summary>
        private float movement;

        // Jumping state
        private bool isJumping;
        private bool wasJumping;
        //private float jumpTime;

        //private Rectangle localBounds;
        /// <summary>
        /// Gets a rectangle which bounds this player in world space.
        /// </summary>
        public Rectangle BoundingRectangle
        {
			get
			{
				int localBoundsWidth = 12;//24;		//IMP
				int localBoundsHeight = 26;//52;	//IMP
				//int localBoundsX = 20;//localBounds.X;
				//int localBoundsY = 12;//localBounds.Y;

				int halfBoundsWidth = localBoundsWidth / 2;//12;
				int left = (int)Position.X - halfBoundsWidth;		//=8
				int top = (int)Position.Y - localBoundsHeight;		//=12

				Rectangle br = new Rectangle(left, top, localBoundsWidth, localBoundsHeight);
				return br;
				//return new Rectangle(left, top, localBounds.Width, localBounds.Height);
			}
        }

        /// <summary>
        /// Constructors a new player.
        /// </summary>
        public Player(Level level, Vector2 position)
        {
            this.level = level;

            LoadContent();

            Reset(position);
        }

        /// <summary>
        /// Loads the player sprite sheet and sounds.
        /// </summary>
        public void LoadContent()
        {
            // Load animated textures.
	        BoundImage = Level.Content.Load<Texture2D>("Sprites/Player/Rect");
            idleAnimation = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/Idle"), 0.1f, true);
            //runAnimation = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/Run"), 0.1f, true);
            //jumpAnimation = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/Jump"), 0.1f, false);
            //celebrateAnimation = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/Celebrate"), 0.1f, false);
            //dieAnimation = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/Die"), 0.1f, false);

            // Calculate bounds within texture size.            
			//int width = 24;//ORG=25;// (int)(idleAnimation.FrameWidth * 0.4);		//NEW=24;
			//int left = 4;//ORG=19;// (idleAnimation.FrameWidth - width) / 2;		//ORG=20;
			//int height = 52;//ORG=51;// (int)(idleAnimation.FrameWidth * 0.8);
			//int top = 12;//ORG=13;// idleAnimation.FrameHeight - height;
			//localBounds = new Rectangle(left, top, width, height);
        }

        /// <summary>
        /// Resets the player to life.
        /// </summary>
        /// <param name="position">The position to come to life at.</param>
        public void Reset(Vector2 position)
        {
            Position = position;
            Velocity = Vector2.Zero;
            isAlive = true;
            sprite.PlayAnimation(idleAnimation);
	        player_move_type = enum_move_type.move_type_idle;
	        player_idxX = 0;
	        player_idxY = 0;
	        player_grav = 0;
	        jumpFrame = 0;
        }

        /// <summary>
        /// Handles input, performs physics, and animates the player sprite.
        /// </summary>
        /// <remarks>
        /// We pass in all of the input states so that our game is only polling the hardware
        /// once per frame. We also pass the game's orientation because when using the accelerometer,
        /// we need to reverse our motion when the orientation is in the LandscapeRight orientation.
        /// </remarks>
        public void Update(
            GameTime gameTime, 
            KeyboardState keyboardState)
        {
            GetInput(keyboardState);

            ApplyPhysics(gameTime);

            if (IsAlive && IsOnGround)
            {
                //if (Math.Abs(Velocity.X) - 0.02f > 0)
                //{
                //    sprite.PlayAnimation(runAnimation);
                //}
                //else
                //{
                    sprite.PlayAnimation(idleAnimation);
                //}
            }

            // Clear input.
            movement = 0.0f;
            isJumping = false;
        }

        /// <summary>
        /// Gets player horizontal movement and jump commands from input.
        /// </summary>
        private void GetInput(KeyboardState keyboardState)
        {
            // Get analog horizontal movement.
            //movement = gamePadState.ThumbSticks.Left.X * MoveStickScale;

            // Ignore small movements to prevent running in place.
            //if (Math.Abs(movement) < 0.5f)
            //    movement = 0.0f;

            // Move the player with accelerometer
			//if (Math.Abs(accelState.Acceleration.Y) > 0.10f)
			//{
			//    // set our movement speed
			//    movement = MathHelper.Clamp(-accelState.Acceleration.Y * AccelerometerScale, -1f, 1f);

			//    // if we're in the LandscapeLeft orientation, we must reverse our movement
			//    if (orientation == DisplayOrientation.LandscapeRight)
			//        movement = -movement;
			//}
	        shouldLog = false;

			if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
			{
				movement = -1.0f;
				if (enum_move_type.move_type_left != player_move_type)
				{
					player_idxX = 0;
					//deltaX = velocityX[player_idxX];
					deltaX = GetVelocityX(player_idxX);
					player_move_type = enum_move_type.move_type_left;
				}
				else if (enum_move_type.move_type_left == player_move_type)
				{
					player_idxX++;
					if (player_idxX > MAX_X - 1)
					{
						player_idxX = MAX_X - 1;
					}
					//deltaX = velocityX[player_idxX];
					deltaX = GetVelocityX(player_idxX);
					//player_move_type = enum_move_type.move_type_left;
				}
				player_move_type = enum_move_type.move_type_left;
			}
			else if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
			{
				movement = 1.0f;
				if (enum_move_type.move_type_rght != player_move_type)
				{
					player_idxX = 0;
					//deltaX = velocityX[player_idxX];
					deltaX = GetVelocityX(player_idxX);
					player_move_type = enum_move_type.move_type_rght;
				}
				else if (enum_move_type.move_type_rght == player_move_type)
				{
					player_idxX++;
					if (player_idxX > MAX_X - 1)
					{
						player_idxX = MAX_X - 1;
					}
					//deltaX = velocityX[player_idxX];
					deltaX = GetVelocityX(player_idxX);
					//player_move_type = move_type_rght;
				}
			}

	        shouldLog = keyboardState.IsKeyDown(Keys.Enter) && prevKeyboardState.IsKeyUp(Keys.Enter);
            // Check if the player wants to jump.
	        isJumping =
                //gamePadState.IsButtonDown(JumpButton) ||
                keyboardState.IsKeyDown(Keys.Space); // ||
                //keyboardState.IsKeyDown(Keys.Up) ||
                //keyboardState.IsKeyDown(Keys.W);// ||
                //touchState.AnyTouch();

	        prevKeyboardState = keyboardState;
        }

	    private int GetVelocityX(int index)
	    {
		    //int tempDeltaX = velocityXgnd[index];
		    int tempDeltaX = 0;
		    if (isOnGround)
		    {

			    tempDeltaX = velocityXgnd[index];
		    }
		    else
		    {
			    tempDeltaX = velocityXair[index];
		    }

		    return tempDeltaX;
	    }

        /// <summary>
        /// Updates the player's velocity and position based on input, gravity, etc.
        /// </summary>
        public void ApplyPhysics(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 previousPosition = Position;
	        int prevPosY = (int) previousPosition.Y;
	        int prevPosX = (int) previousPosition.X;

            // Base velocity is a combination of horizontal movement control and
            // acceleration downward due to gravity.
	        if (0 == movement)
	        {
		        player_move_type = enum_move_type.move_type_idle;
		        velocity.X = 0;
	        }
	        if (enum_move_type.move_type_idle != player_move_type)
	        {
		        velocity.X = (int) (player_move_type - 1) * deltaX * 1;			// IMPORTANT must multiply by 2 as pre-calc's for 16px
	        }

            //velocity.X += movement * MoveAcceleration * elapsed;

	        // TODO stevepro - this is the problem line:
	        // once hit the apex of the jump the DoJump() method will reset velocityY to 0 so as not harshly fall.
	        if (!isOnGround)
	        {
		        player_grav++;
		        if (player_grav > COUNT - 1)
		        {
			        player_grav = COUNT - 1;
		        }
	        }
	        else
	        {
		        player_grav = 0;
	        }
	        deltaY = gravityZ[player_grav];
	        velocity.Y = deltaY * 1;											// IMPORTANT must multiply by 2 as pre-calc's for 16px
	        //velocity.Y = MathHelper.Clamp(velocity.Y + GravityAcceleration * elapsed, -MaxFallSpeed, MaxFallSpeed);

            velocity.Y = DoJump(velocity.Y, gameTime);

			// Apply pseudo-drag horizontally.
	        //if (IsOnGround)
	        //    velocity.X *= GroundDragFactor;
	        //else
	        //    velocity.X *= AirDragFactor;
	        //velocity.X *= GroundDragFactor;
	        //velocity.X *= AirDragFactor;

			// Prevent the player from running faster than his top speed.            
	        velocity.X = MathHelper.Clamp(velocity.X, -MaxMoveSpeed, MaxMoveSpeed);

			// Apply velocity.
	        //var bob = velocity * elapsed;
	        var bobX = velocity.X;// * elapsed;		// IMPORTANT pre-calc'd so don't multiply by game tile delta elapsed
			var bobY = velocity.Y;// * elapsed;		// IMPORTANT pre-calc'd so don't multiply by game tile delta elapsed
	        //velocity.X *= elapsed;
	        //velocity.Y *= elapsed;

	        //Position += velocity * elapsed;
	        //Position += velocity;
	        //Position += bob;
	        var bobPos = Position;
	        bobPos.X += bobX;
	        bobPos.Y += bobY;
	        Position = bobPos;
	        Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));

	        int currPosY = (int) Position.Y;
	        int currPosX = (int) Position.X;

	        if (0.0 != movement)
	        {
		        int velX = (int)(velocity.X);
		        int delta = currPosX - prevPosX;
		        string msg = String.Format("{0}\t\t{1}\t\t{2}\t\t{3}", velX, currPosX, prevPosX, delta);
		        //Logger.Info(msg);
	        }

            // If the player is now colliding with the level, separate them.
            HandleCollisions();

            // If the collision stopped us from moving, reset the velocity to zero.
            if (Position.X == previousPosition.X)
                velocity.X = 0;

			if (Position.Y == previousPosition.Y)
			{
				velocity.Y = 0;
				player_grav = 0;
				jumpFrame = 0;
			}
        }

        /// <summary>
        /// Calculates the Y velocity accounting for jumping and
        /// animates accordingly.
        /// </summary>
        /// <remarks>
        /// During the accent of a jump, the Y velocity is completely
        /// overridden by a power curve. During the decent, gravity takes
        /// over. The jump velocity is controlled by the jumpTime field
        /// which measures time into the accent of the current jump.
        /// </remarks>
		/// <param name="inpVelocityY">
        /// The player's current velocity along the Y axis.
        /// </param>
        /// <returns>
        /// A new Y velocity if beginning or continuing a jump.
        /// Otherwise, the existing Y velocity.
        /// </returns>
		private float DoJump(float inpVelocityY, GameTime gameTime)
        {
			//if (isJumping && jumpFrame >= COUNT)
	        //if (!isJumping && jumpFrame > 0)
	        if (!isJumping && jumpFrame > 0 || isJumping && jumpFrame >= COUNT)
	        {
		        player_grav = 0;
		        inpVelocityY = 0;
	        }
            // If the player wants to jump
            if (isJumping)
            {
				// Begin or continue a jump
	            //if ((!wasJumping && IsOnGround) || jumpTime > 0.0f)
	            if ((!wasJumping && IsOnGround) || jumpFrame > 0)
                {
	                //if (jumpTime == 0.0f)
	                //    jumpSound.Play();

					//jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
	                jumpFrame++;

                    //sprite.PlayAnimation(jumpAnimation);
                }

				// If we are in the ascent of the jump
	            //if (0.0f < jumpTime && jumpTime <= MaxJumpTime)
	            if (0 < jumpFrame && jumpFrame <= COUNT)
	            {
                    // Fully override the vertical velocity with a power curve that gives players more control over the top of the jump
                    //velocityY = JumpLaunchVelocity * (1.0f - (float)Math.Pow(jumpTime / MaxJumpTime, JumpControlPower));

		            deltaY = velocityY[player_idxY];
		            inpVelocityY = deltaY * 1;							// IMPORTANT must multiply by 2 as pre-calc's for 16px

		            player_idxY++;
		            if (player_idxY > COUNT - 1)
		            {
			            player_idxY = COUNT - 1;
		            }
                }
                else
                {
					// Reached the apex of the jump
	                player_idxY = 0;
	                //player_grav = 0;
	                jumpFrame = 0;
                }
            }
            else
            {
				// Continues not jumping or cancels a jump in progress
	            //jumpTime = 0.0f;
	            player_idxY = 0;
	            //player_grav = 0;
	            jumpFrame = 0;
            }

            wasJumping = isJumping;
			return inpVelocityY;
        }

        /// <summary>
        /// Detects and resolves all collisions between the player and his neighboring
        /// tiles. When a collision is detected, the player is pushed away along one
        /// axis to prevent overlapping. There is some special logic for the Y axis to
        /// handle platforms which behave differently depending on direction of movement.
        /// </summary>
        private void HandleCollisions()
        {
            // Get the player's bounding rectangle and find neighboring tiles.
            Rectangle bounds = BoundingRectangle;

	        int[] ltArray = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 };
			int[] rtArray = { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
			int[] ttArray = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
			int[] btArray = { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };

	        int leftTile = 0;
	        int rightTile = 0;
			int topTile = 0;
	        int bottomTile = 0;

	        //Vector2 drawPosn = GetDrawPosn();
	        Vector2 collPosn = GetCollPosn();
			//if (drawPosn.X < 0)
			//{
			//    leftTile = drawPosn.X < -4 ? -1 : 0;
			//}
			//else
			//{
			int idxX = (int)collPosn.X;
				int quoX = (int)(idxX / Tile.Size.X);
				int remX = (int)(idxX % Tile.Size.X);
				if (remX < 0)
				{
					remX = 0;
				}
				int idxLeftTile = ltArray[remX];
				int idxRightTile = rtArray[remX];

				leftTile = idxLeftTile + quoX;
				rightTile = idxRightTile + quoX;
	        //}

			//if (drawPosn.Y < 0)
			//{
			//    topTile = drawPosn.Y < -12 ? -1 : 0;
			//}
			//else
			//{
				int idxY = (int)collPosn.Y;
				int quoY = (int)(idxY / Tile.Size.Y);
				int remY = (int)(idxY % Tile.Size.Y);
			// this won't crash at least but will go off the sides
	        if (remY < 0)
	        {
		        remY = 0;
			}
				int idxTopTile = ttArray[remY];
				int idxBottomTile = btArray[remY];

				topTile = idxTopTile + quoY;
				bottomTile = idxBottomTile + quoY;
	        //}

			int leftTile2 = (int)Math.Floor((float)bounds.Left / Tile.Width);
			int rightTile2 = (int)Math.Ceiling(((float)bounds.Right / Tile.Width)) - 1;
			int topTile2 = (int)Math.Floor((float)bounds.Top / Tile.Height);
			int bottomTile2 = (int)Math.Ceiling(((float)bounds.Bottom / Tile.Height)) - 1;

	        if (leftTile != leftTile2 || rightTile != rightTile2 || topTile != topTile2 || bottomTile != bottomTile2)
	        {
		        //String msg = String.Format("(X,Y)=({0},{1}), L:{2} R:{3} T:{4} B:{5}", (int)position.X, (int)position.Y, leftTile, rightTile, topTile, bottomTile);
		        //String msg = String.Format("BoundL:{0} BoundT:{1} BoundW:{2} BoundH:{3}", bounds.Left, bounds.Top, bounds.Width, bounds.Height);
		        //Logger.Info(msg);
	        }

	        if (shouldLog)
	        {
		        //String msg = String.Format("(X,Y)=({0},{1}), L:{2} R:{3} T:{4} B:{5}", (int) position.X, (int) position.Y, leftTile, rightTile, topTile, bottomTile);
		        //String msg = String.Format("BoundL:{0} BoundT:{1} BoundW:{2} BoundH:{3}", bounds.Left, bounds.Top, bounds.Width, bounds.Height);
		        //Logger.Info(msg);
	        }

	        // Reset flag to search for ground collision.
            isOnGround = false;

            // For each potentially colliding tile,
            for (int y = topTile; y <= bottomTile; ++y)
            {
                for (int x = leftTile; x <= rightTile; ++x)
                {
                    // If this tile is collidable,
                    TileCollision collision = Level.GetCollision(x, y);
                    if (collision != TileCollision.Passable)
                    {
                        // Determine collision depth (with direction) and magnitude.
                        Rectangle tileBounds = Level.GetBounds(x, y);
                        Vector2 depth = RectangleExtensions.GetIntersectionDepth(bounds, tileBounds);
                        if (depth != Vector2.Zero)
                        {
                            float absDepthX = Math.Abs(depth.X);
                            float absDepthY = Math.Abs(depth.Y);

                            // Resolve the collision along the shallow axis.
                            if (absDepthY < absDepthX || collision == TileCollision.Platform)
                            {
                                // If we crossed the top of a tile, we are on the ground.
                                if (previousBottom <= tileBounds.Top)
                                    isOnGround = true;

                                // Ignore platforms, unless we are on the ground.
                                if (collision == TileCollision.Impassable || IsOnGround)
                                {
                                    // Resolve the collision along the Y axis.
                                    Position = new Vector2(Position.X, Position.Y + depth.Y);

                                    // Perform further collisions with the new bounds.
                                    bounds = BoundingRectangle;
                                }
                            }
                            else if (collision == TileCollision.Impassable) // Ignore platforms.
                            {
                                // Resolve the collision along the X axis.
                                Position = new Vector2(Position.X + depth.X, Position.Y);

                                // Perform further collisions with the new bounds.
                                bounds = BoundingRectangle;
                            }
                        }
                    }
                }
            }

            // Save the new bounds bottom.
            previousBottom = bounds.Bottom;
        }

        /// <summary>
        /// Called when the player has been killed.
        /// </summary>
        /// <param name="killedBy">
        /// The enemy who killed the player. This parameter is null if the player was
        /// not killed by an enemy (fell into a hole).
        /// </param>
        public void OnKilled(Enemy killedBy)
        {
            isAlive = false;
            //sprite.PlayAnimation(dieAnimation);
        }

        /// <summary>
        /// Called when this player reaches the level's exit.
        /// </summary>
        public void OnReachedExit()
        {
            //sprite.PlayAnimation(celebrateAnimation);
        }

		private Vector2 GetCollPosn()
	    {
		    return GetCommonPosn((int)position.X, (int)position.Y, 0);
	    }
	    private Vector2 GetDrawPosn()
	    {
		    return GetCommonPosn((int)position.X, (int)position.Y, drawOffsetX);
	    }
	    private static Vector2 GetCommonPosn(int posX, int posY, int offsetX)
	    {
		    int halfTileSizeX = ((int)Tile.Size.X / 2);
		    int twiceTileSizeY = 2 * (int)Tile.Size.Y;

		    int commX = (int)posX - halfTileSizeX + offsetX;
		    int commY = (int)posY - twiceTileSizeY;
		    Vector2 commPosn = new Vector2(commX, commY);
		    return commPosn;
	    }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
	        Vector2 drawPosn = GetDrawPosn();
			sprite.Draw(spriteBatch, drawPosn);
	        spriteBatch.Draw(BoundImage, new Vector2(BoundingRectangle.X, BoundingRectangle.Y), Color.White);
        }
    }
}
