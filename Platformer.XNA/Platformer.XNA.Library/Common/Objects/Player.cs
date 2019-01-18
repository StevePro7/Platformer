using System;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Objects
{
	public class Player
	{
		public void LoadContent(Byte x, Byte y, Vector2 position, Vector2 drawPosition)
		{
			TileX = x;
			TileY = y;
			Position = position;
			PositionX = position.X;
			PositionY = position.Y;

			Byte tileWide = MyGame.Manager.TileManager.TileWide;
			Byte tileHigh = MyGame.Manager.TileManager.TileHigh;

			Width = tileWide;
			Left = 0;
			Height = (int)(2 * tileHigh * 0.8);
			Top = 2 * tileHigh - Height;
			LocalBounds = new Rectangle(Left, Top, Width, Height);

			DrawPosition = drawPosition;
		}

		public void Update(Vector2 position)
		{
			Position = position;
		}

		public void Draw()
		{
			Engine.SpriteBatch.Draw(Assets.PlayerTexture, DrawPosition, Color.White);
		}

		public Vector2 DrawPosition { get; private set; }
		public Vector2 Position { get; private set; }
		public Single PositionX { get; private set; }
		public Single PositionY { get; private set; }
		public Byte TileX { get; private set; }
		public Byte TileY { get; private set; }

		public int Width { get; private set; }
		public int Left { get; private set; }
		public int Height { get; private set; }
		public int Top { get; private set; }
		public Rectangle LocalBounds { get; private set; }
	}
}
