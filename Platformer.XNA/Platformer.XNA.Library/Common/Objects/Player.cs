using System;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Objects
{
	public class Player
	{
		public void LoadContent(Byte x, Byte y, Vector2 position)
		{
			TileX = x;
			TileY = y;
			Position = position;
			PositionX = position.X;
			PositionY = position.Y;
		}

		public void Update(Vector2 position)
		{
			Position = position;
		}

		public void Draw()
		{
			Engine.SpriteBatch.Draw(Assets.PlayerTexture, Position, Color.White);
		}

		public Vector2 Position { get; private set; }
		public Single PositionX { get; private set; }
		public Single PositionY { get; private set; }
		public Byte TileX { get; private set; }
		public Byte TileY { get; private set; }
	}
}
