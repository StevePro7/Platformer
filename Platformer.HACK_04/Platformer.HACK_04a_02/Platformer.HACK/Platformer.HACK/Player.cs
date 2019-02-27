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

	    private static bool MovePlayer = true; 
	    private KeyboardState prevKeyboardState;
	    private const int deltaM = 1;
	    private bool shouldLog;
	    private Texture2D BoundImage;

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

        public bool IsOnGround
        {
            get { return isOnGround; }
        }
        bool isOnGround;

        private float movement;

	    private int moveX;
	    private int moveY;

        // Jumping state
		//private bool isJumping;
		//private bool wasJumping;
		//private float jumpTime;

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

        public Player(Level level, Vector2 position)
        {
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
            //isJumping = false;

	        moveX = 0;
	        moveY = 0;
        }

        private void GetInput(KeyboardState keyboardState)
        {
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

	        shouldLog = keyboardState.IsKeyDown(Keys.Enter) && prevKeyboardState.IsKeyUp(Keys.Enter);
            // Check if the player wants to jump.

	        prevKeyboardState = keyboardState;
        }

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
                velocity.Y = 0;
        }

		#region DoJump
		//private float DoJump(float velocityY, GameTime gameTime)
		//{
		//    // If the player wants to jump
		//    if (isJumping)
		//    {
		//        // Begin or continue a jump
		//        if ((!wasJumping && IsOnGround) || jumpTime > 0.0f)
		//        {
		//            //if (jumpTime == 0.0f)
		//            //    jumpSound.Play();

		//            jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
		//            //sprite.PlayAnimation(jumpAnimation);
		//        }

		//        // If we are in the ascent of the jump
		//        if (0.0f < jumpTime && jumpTime <= MaxJumpTime)
		//        {
		//            // Fully override the vertical velocity with a power curve that gives players more control over the top of the jump
		//            velocityY = JumpLaunchVelocity * (1.0f - (float)Math.Pow(jumpTime / MaxJumpTime, JumpControlPower));
		//        }
		//        else
		//        {
		//            // Reached the apex of the jump
		//            jumpTime = 0.0f;
		//        }
		//    }
		//    else
		//    {
		//        // Continues not jumping or cancels a jump in progress
		//        jumpTime = 0.0f;
		//    }
		//
		//    wasJumping = isJumping;
		//    return velocityY;
		//}
		#endregion

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
	        spriteBatch.Draw(BoundImage, new Vector2(BoundingRectangle.X, BoundingRectangle.Y), Color.White);
        }
    }
}
