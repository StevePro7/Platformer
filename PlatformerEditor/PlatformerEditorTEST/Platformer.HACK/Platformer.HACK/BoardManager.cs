using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
	public class BoardManager
	{
		public BoardManager()
		{
			Tiles = new string[16,12];
			Reset();
		}

		public void Reset()
		{
			for (int y = 0; y < 12; y++)
			{
				for (int x = 0; x < 16; x++)
				{
					Tiles[x, y] = "1";
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int y = 0; y < 12; y++)
			{
				for (int x = 0; x < 16; x++)
				{
					String tile = Tiles[x, y];
					Texture2D texture = GetTexture(tile);

					if (null != texture)
					{
						Color color = Color.White;
						Vector2 pos = new Vector2(x * 32, y * 32);
						if (Assets.PlatformTexture == texture && "$" == tile)
						{
							color = Color.Gray;
						}
						if (Assets.GemTexture == texture && "G" == tile)
						{
							color = Color.Yellow;
						}
						if (Assets.GemTexture == texture && "P" == tile)
						{
							color = Color.Red;
						}

						if (Assets.PlayerTexture == texture || Assets.EnemyATexture == texture ||
						    Assets.EnemyBTexture == texture || Assets.EnemyCTexture == texture ||
						    Assets.EnemyDTexture == texture)
						{
							pos.Y -= 32;
						}

						spriteBatch.Draw(texture, pos, color);
					}
				}
			}

			for (int y = 0; y < 12; y++)
			{
				spriteBatch.Draw(Assets.HorzTexture, new Vector2(0, y * 32), Color.White);
			}
			for (int x = 0; x < 16; x++)
			{
				spriteBatch.Draw(Assets.VertTexture, new Vector2(x * 32, 0), Color.White);
			}

			spriteBatch.Draw(Assets.HorzTexture, new Vector2(0, 383), Color.White);
			spriteBatch.Draw(Assets.VertTexture, new Vector2(511, 0), Color.White);
		}

		public string[,] Tiles { get; private set; }

		private Texture2D GetTexture(String tile)
		{
			if ("." == tile)
			{
				return null;
			}
			if ("X" == tile)
			{
				return Assets.ExitTexture;
			}
			if ("#" == tile)
			{
				return Assets.BlockTexture;
			}
			if ("$" == tile || "@" == tile)
			{
				return Assets.PlatformTexture;
			}
			if ("G" == tile)
			{
				return Assets.GemTexture;
			}
			if ("P" == tile)
			{
				return Assets.GemTexture;
			}
			if ("1" == tile)
			{
				return Assets.PlayerTexture;
			}
			if ("A" == tile || "a" == tile)
			{
				return Assets.EnemyATexture;
			}
			if ("B" == tile || "b" == tile)
			{
				return Assets.EnemyBTexture;
			}
			if ("C" == tile || "c" == tile)
			{
				return Assets.EnemyCTexture;
			}
			if ("D" == tile || "d" == tile)
			{
				return Assets.EnemyDTexture;
			}
			
			return null;
		}
	}
}
