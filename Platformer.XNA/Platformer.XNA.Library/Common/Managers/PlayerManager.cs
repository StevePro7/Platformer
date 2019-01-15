using System;
using WindowsGame.Common.Objects;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IPlayerManager
	{
		void Initialize();
		void LoadContent(UInt16 playerSpot);
		void LoadContent(UInt16 playerSpot, Byte gameWidth, Byte gameHeight, Byte theGameOffset, Byte theTileSize);

		void Update(GameTime gameTime);
		void UpdateControls(Boolean left, Boolean rght, Boolean jump);
		void UpdatePhysics(GameTime gameTime);

		void Draw();

		Player Player { get; }
		Vector2 Velocity { get; }
		Boolean IsOnGround { get; }
		Boolean IsAlive { get; }
	}

	public class PlayerManager : IPlayerManager
	{
		private Byte gameOffset;
		private Byte tileSize;

		private float movement;
		private bool isJumping;
		//private bool wasJumping;
		//private float jumpTime;

		// Constants for controling horizontal movement
		private const float MoveAcceleration = 13000.0f;
		private const float MaxMoveSpeed = 1750.0f;
		private const float GroundDragFactor = 0.48f;
		private const float AirDragFactor = 0.58f;

		public void Initialize()
		{
			Player = new Player();

			Velocity = Vector2.Zero;
			IsOnGround = true;
			IsAlive = false;

			gameOffset = MyGame.Manager.StateManager.GameOffset;
			tileSize = MyGame.Manager.StateManager.TileSize;
			movement = 0.0f;
		}

		public void LoadContent(UInt16 playerSpot)
		{
			Byte gameWidth = MyGame.Manager.LevelManager.GameWidth;
			Byte gameHeight = MyGame.Manager.LevelManager.GameHeight;
			
			LoadContent(playerSpot, gameWidth, gameHeight, gameOffset, tileSize);
		}
		public void LoadContent(UInt16 playerSpot, Byte gameWidth, Byte gameHeight, Byte theGameOffset, Byte theTileSize)
		{
			Byte y = (Byte) (playerSpot / gameHeight);
			Byte x = (Byte) (playerSpot % gameWidth);
			Vector2 position = new Vector2(x * theTileSize + theGameOffset, (y - 1) * tileSize);
			Player.LoadContent(x, y, position);
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

		public void UpdatePhysics(GameTime gameTime)
		{
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

			Vector2 previousPosition = Player.Position;

			// Base velocity is a combination of horizontal movement control and
			// acceleration downward due to gravity.
			Vector2 velocity = Velocity;
			velocity.X += movement * MoveAcceleration * elapsed;


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
			Player.Update(position);
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
