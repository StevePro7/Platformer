using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platformer
{
	public class MyLevelManager
	{
		MyContentManager myContentManager;

		public MyLevelManager(MyContentManager myContentManager)
		{
			this.myContentManager = myContentManager;
		}

		public void Initialize()
		{
			collision_array = GetCollArray();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Vector2 pos = Vector2.Zero;
			for (byte row = 0; row < Constants.MAX_ROWS; row++)
			{
				for (byte col = 0; col < Constants.MAX_COLS; col++)
				{
					byte idx = (byte)(row * Constants.MAX_COLS + col);
					byte val = collision_array[idx];
					if (0 != val)
					{
						pos = new Vector2(col * Constants.TILE_WIDE, row * Constants.TILE_HIGH);
						spriteBatch.Draw(myContentManager.Tile1616, pos, Color.White);

						pos = new Vector2(col * Constants.TILE_WIDE + 0, row * Constants.TILE_HIGH);
						spriteBatch.Draw(myContentManager.Tile88, pos, Color.White);
						pos = new Vector2(col * Constants.TILE_WIDE + 8, row * Constants.TILE_HIGH);
						spriteBatch.Draw(myContentManager.Tile88, pos, Color.White);
					}
				}
			}
		}
		private Byte[] GetCollArray()
		{
			return new byte[Constants.MAX_ROWS * Constants.MAX_COLS]
			{
				0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
				1,1,1,1,1,0,0,0,0,0,0,1,1,1,1,1,
				0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
			};
		}

		private Byte[] collision_array { get; set; }
	}
}