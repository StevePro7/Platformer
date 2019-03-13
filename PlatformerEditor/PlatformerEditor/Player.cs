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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    class Player
    {
	    private Config config;

	    // (32 - 48) / 2		32=tileWidth	48=playerWidth
	    private const int drawOffsetX = -8;

	    private readonly int[] ltArray = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 };
	    private readonly int[] rtArray = { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
	    private readonly int[] ttArray = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
	    private readonly int[] btArray = { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };

	    const int localBoundsWidth = 24;
	    const int localBoundsHeight = 52;
	    const int halfBoundsWidth = localBoundsWidth / 2;//12;

	    const int rectAWidth = localBoundsWidth;
	    const int rectAHeight = localBoundsHeight;
	    const int rectBWidth = Tile.Width;
	    const int rectBHeight = Tile.Height;

	    const int halfWidthA = rectAWidth / 2;
	    const int halfHeightA = rectAHeight / 2;
	    const int halfWidthB = rectBWidth / 2;
	    const int halfHeightB = rectBHeight / 2;

	    private int depthX;
	    private int depthY;

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

        // Animations
        private Animation idleAnimation;
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
        //private const float MoveAcceleration = 13000.0f;
        private const float MaxMoveSpeed = 1750.0f;
        //private const float GroundDragFactor = 0.48f;
        //private const float AirDragFactor = 0.58f;

        public bool IsOnGround
        {
            get { return isOnGround; }
        }
        bool isOnGround;

        private float movement;

        // Jumping state
        private bool isJumping;
        private bool wasJumping;

        public Rectangle BoundingRectangle
        {
	        get
	        {
		        int localBoundsWidth = 24;
		        int localBoundsHeight = 52;

		        int halfBoundsWidth = localBoundsWidth / 2;//12;
		        int left = (int)Position.X - halfBoundsWidth;		//=8
		        int top = (int)Position.Y - localBoundsHeight;		//=12

		        Rectangle br = new Rectangle(left, top, localBoundsWidth, localBoundsHeight);
		        return br;
	        }
        }

        public Player(Level level, Vector2 position, Config config)
        {
	        this.config = config;
            this.level = level;

            LoadContent();

            Reset(position);
        }

        public void LoadContent()
        {
            // Load animated textures.
	        BoundImage = Level.Content.Load<Texture2D>("Sprites/Player/Rect");
            idleAnimation = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/Idle"), 0.1f, true);
        }

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

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            GetInput(keyboardState);

            ApplyPhysics(gameTime);

            if (IsAlive && IsOnGround)
            {
				sprite.PlayAnimation(idleAnimation);
            }

            // Clear input.
            movement = 0.0f;
            isJumping = false;
        }

        private void GetInput(KeyboardState keyboardState)
        {
            // Get analog horizontal movement.
	        shouldLog = false;

			if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
			{
				movement = -1.0f;
				if (enum_move_type.move_type_left != player_move_type)
				{
					player_idxX = 0;
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
					deltaX = GetVelocityX(player_idxX);
				}
				player_move_type = enum_move_type.move_type_left;
			}
			else if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
			{
				movement = 1.0f;
				if (enum_move_type.move_type_rght != player_move_type)
				{
					player_idxX = 0;
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
					deltaX = GetVelocityX(player_idxX);
				}
			}

	        shouldLog = keyboardState.IsKeyDown(Keys.Enter) && prevKeyboardState.IsKeyUp(Keys.Enter);
            // Check if the player wants to jump.
	        isJumping = keyboardState.IsKeyDown(Keys.Space);

	        prevKeyboardState = keyboardState;
        }

	    private int GetVelocityX(int index)
	    {
		    return isOnGround ? velocityXgnd[index] : velocityXair[index];
	    }

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
		        velocity.X = (int) (player_move_type - 1) * deltaX * 2;			// IMPORTANT must multiply by 2 as pre-calc's for 16px
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
			velocity.Y = deltaY * 2;											// IMPORTANT must multiply by 2 as pre-calc's for 16px
			//velocity.Y = MathHelper.Clamp(velocity.Y + GravityAcceleration * elapsed, -MaxFallSpeed, MaxFallSpeed);

            velocity.Y = DoJump(velocity.Y, gameTime);

            // Prevent the player from running faster than his top speed.            
            velocity.X = MathHelper.Clamp(velocity.X, -MaxMoveSpeed, MaxMoveSpeed);

            // Apply velocity.
	        //var bob = velocity * elapsed;
	        var bobX = velocity.X;// * elapsed;		// IMPORTANT pre-calc'd so don't multiply by game tile delta elapsed
			var bobY = velocity.Y;// * elapsed;		// IMPORTANT pre-calc'd so don't multiply by game tile delta elapsed
	        var bobPos = Position;
	        bobPos.X += bobX;

			// Boundaries.
	        if (bobPos.X <= 48.0f)
	        {
		        bobPos.X = 48.0f;
	        }
	        if (bobPos.X >= 496.0f)
	        {
		        bobPos.X = 496.0f;
	        }

	        bobPos.Y += bobY;
	        Position = bobPos;
            Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));

	        int currPosY = (int) Position.Y;
	        int currPosX = (int) Position.X;

	        if (0.0 != movement)
	        {
				//int velX = (int)(velocity.X);
				//int delta = currPosX - prevPosX;
				//string msg = String.Format("{0}\t\t{1}\t\t{2}\t\t{3}", velX, currPosX, prevPosX, delta);
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

        private float DoJump(float inpVelocityY, GameTime gameTime)
        {
	        if (!isJumping && jumpFrame > 0 || isJumping && jumpFrame >= COUNT)
	        {
				player_grav = 0;
		        inpVelocityY = 0;
	        }
            // If the player wants to jump
            if (isJumping)
            {
                // Begin or continue a jump
	            if ((!wasJumping && IsOnGround) || jumpFrame > 0)
                {
	                jumpFrame++;

	                //sprite.PlayAnimation(jumpAnimation);
                }

                // If we are in the ascent of the jump
	            if (0 < jumpFrame && jumpFrame <= COUNT)
                {
                    // Fully override the vertical velocity with a power curve that gives players more control over the top of the jump
					deltaY = velocityY[player_idxY];
	                inpVelocityY = deltaY * 2;							// IMPORTANT must multiply by 2 as pre-calc's for 16px

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
	                jumpFrame = 0;
                }
            }
            else
            {
                // Continues not jumping or cancels a jump in progress
	            player_idxY = 0;
	            jumpFrame = 0;
            }

            wasJumping = isJumping;
			return inpVelocityY;
        }

        private void HandleCollisions()
        {
			// Get the player's bounding rectangle and find neighboring tiles.
	        //Rectangle bounds = BoundingRectangle;

	        int boundsLeft = (int)Position.X - halfBoundsWidth;
	        int boundsTop = (int)Position.Y - localBoundsHeight;
	        //int boundsRight = boundsLeft + localBoundsWidth;
	        //int boundsBottom = boundsTop + localBoundsHeight;

	        //int left = (int)Position.X - halfBoundsWidth;		//=8
	        //int top = (int)Position.Y - localBoundsHeight;		//=12

	        //Rectangle bounds2 = new Rectangle(left, top, localBoundsWidth, localBoundsHeight);
	        //int boundsLeft = bounds2.Left;
	        //int boundsRight= bounds2.Right;
	        //int boundsTop = bounds2.Top;
	        //int boundsBottom = bounds2.Bottom;

			int leftTile = 0;
			int rightTile = 0;
			int topTile = 0;
			int bottomTile = 0;

	        //Vector2 drawPosn = GetDrawPosn();
	        Vector2 collPosn = GetCollPosn();

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

	        if (1 == bottomTile - topTile)
	        {
				//Logger.Info("falling in between tiles due gravity");
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
						int tileBoundsLeft = x * Tile.Width;
						int tileBoundsTop = y * Tile.Height;

						Process(boundsLeft, boundsTop, tileBoundsLeft, tileBoundsTop);
						if (depthX != 0 || depthY != 0)
						{
							float absDepthX = Math.Abs(depthX);
							float absDepthY = Math.Abs(depthY);

							// Resolve the collision along the shallow axis.
							if (absDepthY < absDepthX || collision == TileCollision.Platform)
							{
								// If we crossed the top of a tile, we are on the ground.
								if (previousBottom <= tileBoundsTop)
								{
									isOnGround = true;
								}

								// Ignore platforms, unless we are on the ground.
								if (collision == TileCollision.Impassable || IsOnGround)
								{
									// Resolve the collision along the Y axis.
									Position = new Vector2(Position.X, Position.Y + depthY);

									// Perform further collisions with the new bounds.
									//bounds = BoundingRectangle;
									boundsLeft = (int)Position.X - halfBoundsWidth;
									boundsTop = (int)Position.Y - localBoundsHeight;
								}
							}
							else if (collision == TileCollision.Impassable) // Ignore platforms.
							{
								// Resolve the collision along the X axis.
								Position = new Vector2(Position.X + depthX, Position.Y);

								// Perform further collisions with the new bounds.
								//bounds = BoundingRectangle;
								boundsLeft = (int)Position.X - halfBoundsWidth;
								boundsTop = (int)Position.Y - localBoundsHeight;
							}

						}
					}
				}
			}

			// Save the new bounds bottom.
			boundsLeft = (int)Position.X - halfBoundsWidth;
			boundsTop = (int)Position.Y - localBoundsHeight;
			int boundsBottom = boundsTop + localBoundsHeight;
			previousBottom = boundsBottom;
			//previousBottom = bounds.Bottom;
		}

	    //private void Process(int boundsLeft, int boundsTop, int tileBoundsLeft, int tileBoundsTop)
	    private void Process(int rectALeft, int rectATop, int rectBLeft, int rectBTop)
	    {
		    // Calculate half sizes.	DONE

		    // Calculate centers.
		    int centerAX = rectALeft + halfWidthA;
		    int centerAY = rectATop + halfHeightA;
		    int centerBX = rectBLeft + halfWidthB;
		    int centerBY = rectBTop + halfHeightB;

		    // Calculate current and minimum-non-intersecting distances between centers.
		    int distanceX = centerAX - centerBX;
		    int distanceY = centerAY - centerBY;

		    int minDistanceX = halfWidthA + halfWidthB;
		    int minDistanceY = halfHeightA + halfHeightB;

		    depthX = 0;
		    depthY = 0;

		    // If we are not intersecting at all, return (0, 0).
		    if (Math.Abs(distanceX) >= minDistanceX || Math.Abs(distanceY) >= minDistanceY)
		    {
			    return;
		    }

		    // Calculate and return intersection depths.
		    depthX = distanceX > 0 ? minDistanceX - distanceX : -minDistanceX - distanceX;
		    depthY = distanceY > 0 ? minDistanceY - distanceY : -minDistanceY - distanceY;
	    }

        public void OnKilled(Enemy killedBy)
        {
			if (config.Invincibility)
	        {
		        return;
	        }

            isAlive = false;
            //sprite.PlayAnimation(dieAnimation);
        }

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
			//spriteBatch.Draw(BoundImage, new Vector2(BoundingRectangle.X, BoundingRectangle.Y), Color.White);
        }
    }
}
