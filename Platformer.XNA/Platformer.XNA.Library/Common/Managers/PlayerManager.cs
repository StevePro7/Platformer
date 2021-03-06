﻿using System;
using WindowsGame.Common.Objects;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IPlayerManager
	{
		void Initialize(GameType gameType);
		void LoadContent(UInt16 playerSpot);
		void LoadContent(UInt16 playerSpot, Byte gameHeight, Byte theGameOffset, Byte theTileSize);
		void Reset();

		void Update(GameTime gameTime);
		void UpdateControls(Boolean left, Boolean rght, Boolean jump);
		void UpdatePhysics();
		void UpdatePhysics(GameTime gameTime);

		void Draw();

		Player Player { get; }
		Vector2 Velocity { get; }
		Boolean IsOnGround { get; }
		Boolean IsAlive { get; }
	}

	public class PlayerManager : IPlayerManager
	{
		private GameType gameType;
		private Byte gameOffset;
		private Byte tileWide;

		private float movement;
		private bool isJumping;
		//private bool wasJumping;
		//private float jumpTime;

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

		public void Initialize(GameType gameType)
		{
			this.gameType = gameType;
			Player = new Player();

			Velocity = Vector2.Zero;
			IsOnGround = true;
			IsAlive = false;

			gameOffset = MyGame.Manager.StateManager.GameOffset;
			tileWide = MyGame.Manager.TileManager.TileWide;
			movement = 0.0f;
		}

		public void LoadContent(UInt16 playerSpot)
		{
			Byte levelHigh = MyGame.Manager.LevelManager.LevelHigh;
			LoadContent(playerSpot, levelHigh, gameOffset, tileWide);
		}
		public void LoadContent(UInt16 playerSpot, Byte gameHeight, Byte theGameOffset, Byte theTileSize)
		{
			Byte y = (Byte) (playerSpot / gameHeight);
			Byte x = (Byte) (playerSpot % gameHeight);

			Rectangle bounds = MyGame.Manager.LevelManager.GetBounds(x, y);
			Vector2 bottom = MyGame.Manager.LevelManager.GetBottomCenter(bounds);

			Vector2 drawPosition = new Vector2(x * theTileSize + theGameOffset, (y - 1) * tileWide);
			Vector2 tilePosition = bottom;

			drawPosition.X -= (int)gameType * 2;
			Player.LoadContent(x, y, tilePosition, drawPosition);
		}

		public void Update(GameTime gameTime)
		{
		}

		public void UpdateControls(Boolean left, Boolean rght, Boolean jump)
		{
			if (left)
			{
				movement = -1.0f;
			}
			else if (rght)
			{
				movement = 1.0f;
			}
			else if (jump)
			{
				isJumping = true;
			}
		}

		//public void UpdatePhysicsX(GameTime gameTime)
		//{
		//    Vector2 position = Player.Position;
		//    position.X += movement;
		//    Player.Update(position);
		//}

		public void UpdatePhysics()
		{
			float delta = movement;
			//Vector2 previousPosition = Player.Position;
			
			// Apply velocity.
			Vector2 position = Player.Position;
			Vector2 drawPosition = Player.DrawPosition;

			position.X += delta;
			drawPosition.X += delta;

			Player.Update(position, drawPosition);
		}

		public void UpdatePhysics(GameTime gameTime)
		{
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

			Vector2 previousPosition = Player.Position;

			// Base velocity is a combination of horizontal movement control and
			// acceleration downward due to gravity.
			Vector2 velocity = Velocity;
			velocity.X += movement * MoveAcceleration * elapsed;
			//velocity.Y = MathHelper.Clamp(velocity.Y + GravityAcceleration * elapsed, -MaxFallSpeed, MaxFallSpeed);	//TODO

			// Apply pseudo-drag horizontally.
			if (IsOnGround)
				velocity.X *= GroundDragFactor;
			else
				velocity.X *= AirDragFactor;

			// Apply velocity.
			Vector2 position = Player.Position;
			position += velocity * elapsed;

			// If the collision stopped us from moving, reset the velocity to zero.
			if (position.X == previousPosition.X)
				velocity.X = 0;

			if (position.Y == previousPosition.Y)
				velocity.Y = 0;

			Velocity = velocity;
			Player.Update(position, position);
		}

		public void Reset()
		{
			// Clear input.
			movement = 0.0f;
			isJumping = false;
		}

		public void Draw()
		{
			Player.Draw();
		}

		public Player Player { get; private set; }
		public Vector2 Velocity { get; private set; }
		public Boolean IsOnGround { get; private set; }
		public Boolean IsAlive { get; private set; }
	}
}
