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
using Microsoft.Xna.Framework.Input.Touch;

namespace Platformer
{
    /// <summary>
    /// Our fearless adventurer!
    /// </summary>
    class Player
    {
	    private static bool MovePlayer = true; 
	    private KeyboardState prevKeyboardState;
	    private const int deltaM = 1;
	    private bool shouldLog;
	    private Texture2D BoundImage;

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

        // Sounds
        private SoundEffect killedSound;
        private SoundEffect jumpSound;
        private SoundEffect fallSound;

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

	    private int moveX;
	    private int moveY;

        // Jumping state
        private bool isJumping;
        private bool wasJumping;
        private float jumpTime;

        //private Rectangle localBounds;
        /// <summary>
        /// Gets a rectangle which bounds this player in world space.
        /// </summary>
        public Rectangle BoundingRectangle
        {
            get
            {
	            int localBoundsWidth = 24;
	            int localBoundsHeight = 52;
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
			//int width = 24;//ORG=25;// (int)(idleAnimation.FrameWidth * 0.4);
			//int left = 20;//ORG=19;// (idleAnimation.FrameWidth - width) / 2;
			//int height = 52;//ORG=51;// (int)(idleAnimation.FrameWidth * 0.8);
			//int top = 12;//ORG=13;// idleAnimation.FrameHeight - height;
			//localBounds = new Rectangle(left, top, width, height);

            // Load sounds.            
            killedSound = Level.Content.Load<SoundEffect>("Sounds/PlayerKilled");
            jumpSound = Level.Content.Load<SoundEffect>("Sounds/PlayerJump");
            fallSound = Level.Content.Load<SoundEffect>("Sounds/PlayerFall");
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

	        moveX = 0;
	        moveY = 0;
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
	        int offset = 1;
			#region Movement
			if (keyboardState.IsKeyDown(Keys.Space))
	        {
		        offset = 2;
	        }
            // If any digital horizontal movement input is found, override the analog movement.
	        if (MovePlayer)
	        {
		        if (keyboardState.IsKeyDown(Keys.Left))
		        {
			        moveX = -deltaM * offset;
		        }
	        }
	        else
	        {
		        if (keyboardState.IsKeyDown(Keys.Left) && prevKeyboardState.IsKeyUp(Keys.Left))
		        {
			        moveX = -deltaM * offset;
		        }
	        }
	        if (MovePlayer)
	        {
		        if (keyboardState.IsKeyDown(Keys.Right))
		        {
			        moveX = deltaM * offset;
		        }
	        }
	        else
	        {
		        if (keyboardState.IsKeyDown(Keys.Right) && prevKeyboardState.IsKeyUp(Keys.Right))
		        {
			        moveX = deltaM * offset;
		        }
	        }
	        if (MovePlayer)
	        {
		        if (keyboardState.IsKeyDown(Keys.Up)) // && prevKeyboardState.IsKeyUp(Keys.Up))
		        {
			        moveY = -deltaM * offset;
		        }
	        }
			else
	        {
		        if (keyboardState.IsKeyDown(Keys.Up) && prevKeyboardState.IsKeyUp(Keys.Up))
		        {
			        moveY = -deltaM * offset;
		        }
	        }
	        if (MovePlayer)
	        {
		        if (keyboardState.IsKeyDown(Keys.Down)) // && prevKeyboardState.IsKeyUp(Keys.Down))
		        {
			        moveY = deltaM * offset;
		        }
	        }
	        else
	        {
		        if (keyboardState.IsKeyDown(Keys.Down) && prevKeyboardState.IsKeyUp(Keys.Down))
		        {
			        moveY = deltaM * offset;
		        }
			}
			#endregion
			// //if (keyboardState.IsKeyDown(Keys.Left) ||
			//    keyboardState.IsKeyDown(Keys.A))
			//{
			//    movement = -1.0f;
			//}
			//else if (keyboardState.IsKeyDown(Keys.Right) ||
			//         keyboardState.IsKeyDown(Keys.D))
			//{
			//    movement = 1.0f;
			//}

	        shouldLog = keyboardState.IsKeyDown(Keys.Enter) && prevKeyboardState.IsKeyUp(Keys.Enter);
            // Check if the player wants to jump.
	        isJumping = false;
                //gamePadState.IsButtonDown(JumpButton) ||
                //keyboardState.IsKeyDown(Keys.Space) ||
                //keyboardState.IsKeyDown(Keys.Up) ||
                //keyboardState.IsKeyDown(Keys.W);// ||
                //touchState.AnyTouch();

	        prevKeyboardState = keyboardState;
        }

        /// <summary>
        /// Updates the player's velocity and position based on input, gravity, etc.
        /// </summary>
        public void ApplyPhysics(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

			velocity = Vector2.Zero;
            Vector2 previousPosition = Position;
	        int prevPosY = (int) previousPosition.Y;
	        int prevPosX = (int) previousPosition.X;

            // Base velocity is a combination of horizontal movement control and
            // acceleration downward due to gravity.
            //velocity.X += movement * MoveAcceleration * elapsed;
	        velocity.X += moveX;
	        velocity.Y += moveY;
            //velocity.Y = MathHelper.Clamp(velocity.Y + GravityAcceleration * elapsed, -MaxFallSpeed, MaxFallSpeed);

            //velocity.Y = DoJump(velocity.Y, gameTime);
	        
            // Apply pseudo-drag horizontally.
			//if (IsOnGround)
			//    velocity.X *= GroundDragFactor;
			//else
			//    velocity.X *= AirDragFactor;
	        //velocity.X *= GroundDragFactor;
	        //velocity.X *= AirDragFactor;

            // Prevent the player from running faster than his top speed.            
            //velocity.X = MathHelper.Clamp(velocity.X, -MaxMoveSpeed, MaxMoveSpeed);

            // Apply velocity.
            //Position += velocity * elapsed;
	        Position += velocity;
            Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));

	        int currPosY = (int) Position.Y;
	        int currPosX = (int) Position.X;

	        if (0.0 != movement)
	        {
		        int velX = (int)(velocity.X);
		        int delta = currPosX - prevPosX;
		        string msg = String.Format("{0}\t\t{1}\t\t{2}\t\t{3}", velX, currPosX, prevPosX, delta);
		        Logger.Info(msg);
	        }

            // If the player is now colliding with the level, separate them.
            HandleCollisions();

            // If the collision stopped us from moving, reset the velocity to zero.
            if (Position.X == previousPosition.X)
                velocity.X = 0;

            if (Position.Y == previousPosition.Y)
                velocity.Y = 0;
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
        /// <param name="velocityY">
        /// The player's current velocity along the Y axis.
        /// </param>
        /// <returns>
        /// A new Y velocity if beginning or continuing a jump.
        /// Otherwise, the existing Y velocity.
        /// </returns>
        private float DoJump(float velocityY, GameTime gameTime)
        {
            // If the player wants to jump
            if (isJumping)
            {
                // Begin or continue a jump
                if ((!wasJumping && IsOnGround) || jumpTime > 0.0f)
                {
                    if (jumpTime == 0.0f)
                        jumpSound.Play();

                    jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    //sprite.PlayAnimation(jumpAnimation);
                }

                // If we are in the ascent of the jump
                if (0.0f < jumpTime && jumpTime <= MaxJumpTime)
                {
                    // Fully override the vertical velocity with a power curve that gives players more control over the top of the jump
                    velocityY = JumpLaunchVelocity * (1.0f - (float)Math.Pow(jumpTime / MaxJumpTime, JumpControlPower));
                }
                else
                {
                    // Reached the apex of the jump
                    jumpTime = 0.0f;
                }
            }
            else
            {
                // Continues not jumping or cancels a jump in progress
                jumpTime = 0.0f;
            }
            wasJumping = isJumping;

            return velocityY;
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
            int leftTile = (int)Math.Floor((float)bounds.Left / Tile.Width);
            int rightTile = (int)Math.Ceiling(((float)bounds.Right / Tile.Width)) - 1;
            int topTile = (int)Math.Floor((float)bounds.Top / Tile.Height);
            int bottomTile = (int)Math.Ceiling(((float)bounds.Bottom / Tile.Height)) - 1;

	        if (shouldLog)
	        {
		        String msg = String.Format("(X,Y)=({0},{1}), L:{2} R:{3} T:{4} B:{5}", (int) position.X, (int) position.Y, leftTile, rightTile, topTile, bottomTile);
		        //String msg = String.Format("BoundL:{0} BoundT:{1} BoundW:{2} BoundH:{3}", bounds.Left, bounds.Top, bounds.Width, bounds.Height);
		        Logger.Info(msg);
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

            if (killedBy != null)
                killedSound.Play();
            else
                fallSound.Play();

            //sprite.PlayAnimation(dieAnimation);
        }

        /// <summary>
        /// Called when this player reaches the level's exit.
        /// </summary>
        public void OnReachedExit()
        {
            //sprite.PlayAnimation(celebrateAnimation);
        }

        /// <summary>
        /// Draws the animated player.
        /// </summary>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Flip the sprite to face the way we are moving.
			//if (Velocity.X > 0)
			//    flip = SpriteEffects.FlipHorizontally;
			//else if (Velocity.X < 0)
			//    flip = SpriteEffects.None;

            // Draw that sprite.
            //sprite.Draw(gameTime, spriteBatch, Position, flip);

	        int localBoundsWidth = 24;
	        //int localBoundsHeight = 52;
	        int rendX = (int)position.X - localBoundsWidth / 2;
	        int rendY = (int)position.Y - 2 * (int)Tile.Size.Y;
	        Vector2 renderer = new Vector2(rendX, rendY);
	        sprite.Draw(spriteBatch, renderer);
			spriteBatch.Draw(BoundImage, new Vector2(BoundingRectangle.X, BoundingRectangle.Y), Color.White);
        }
    }
}
