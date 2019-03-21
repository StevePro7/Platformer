﻿using System;
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
			Clear();
		}

		public void Clear()
		{
			for (int y = 0; y < 12; y++)
			{
				for (int x = 0; x < 16; x++)
				{
					Tiles[x, y] = ".";
				}
			}
		}

		public void Load(string[] lines)
		{
			for (int y = 0; y < lines.Count(); y++)
			{
				string line = lines[y];
				for (int x = 0; x < line.Length; x++)
				{
					string tile = line[x].ToString();
					Tiles[x, y] = tile;
				}
			}
		}

		public void Update(int x, int y, String tile)
		{
			Update(x, y, tile, false);
		}

		public void Update(int x, int y, String tile, bool optional)
		{
			if (optional)
			{
				if ("A" == tile)
				{
					tile = "a";
				}
				if ("B" == tile)
				{
					tile = "b";
				}
				if ("C" == tile)
				{
					tile = "c";
				}
				if ("D" == tile)
				{
					tile = "d";
				}
			}

			Tiles[x, y] = tile;
		}

		public void Draw(SpriteBatch spriteBatch, string selector)
		{
			Texture2D texture;
			for (int y = 0; y < 12; y++)
			{
				for (int x = 0; x < 16; x++)
				{
					String tile = Tiles[x, y];
					; texture = GetTexture(tile);

					if (null != texture)
					{
						Color color = Color.White;
						Vector2 pos = new Vector2(x * 32, y * 32);
						if (Assets.BlankTexture == texture && "." == tile)
						{
							color = Color.CornflowerBlue;
						}
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

							if ("a" == tile ||"b" == tile ||"c" == tile ||"d" == tile)
							{
								color = Color.Black;
							}
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


			texture = GetTexture(selector);
			if (null != texture)
			{
				Color color = Color.White;
				Vector2 pos = new Vector2(512 + 16, 16);
				if (Assets.PlatformTexture == texture && "$" == selector)
				{
					color = Color.Gray;
				}
				if (Assets.GemTexture == texture && "G" == selector)
				{
					color = Color.Yellow;
				}
				if (Assets.GemTexture == texture && "P" == selector)
				{
					color = Color.Red;
				}

				if (Assets.EnemyATexture == texture || Assets.EnemyBTexture == texture || Assets.EnemyCTexture == texture || Assets.EnemyDTexture == texture)
				{
					if ("a" == selector || "b" == selector || "c" == selector || "d" == selector)
					{
						color = Color.Black;
					}
				}

				spriteBatch.Draw(texture, pos, color);
			}

			spriteBatch.Draw(Assets.HorzTexture, new Vector2(0, 383), Color.White);
			spriteBatch.Draw(Assets.VertTexture, new Vector2(511, 0), Color.White);

			int w = 32 * 16;
			int h = 32 * 3;
			spriteBatch.Draw(Assets.BlankTexture, new Vector2(w + 0 * 32, h ), Color.White);
			spriteBatch.Draw(Assets.BlockTexture, new Vector2(w + 1 * 32, h), Color.White);

			h = 32 * 4;
			spriteBatch.Draw(Assets.PlatformTexture, new Vector2(w + 0 * 32, h), Color.White);
			spriteBatch.Draw(Assets.PlatformTexture, new Vector2(w + 1 * 32, h), Color.Gray);

			h = 32 * 6;
			spriteBatch.Draw(Assets.ExitTexture, new Vector2(w + 0 * 32, h - 0), Color.White);
			spriteBatch.Draw(Assets.PlayerTexture, new Vector2(w + 1 * 32, h - 32), Color.White);

			h = 32 * 7;
			spriteBatch.Draw(Assets.EnemyATexture, new Vector2(w + 0 * 32, h), Color.White);
			spriteBatch.Draw(Assets.EnemyBTexture, new Vector2(w + 1 * 32, h), Color.White);
			h = 32 * 9;
			spriteBatch.Draw(Assets.EnemyCTexture, new Vector2(w + 0 * 32, h), Color.White);
			spriteBatch.Draw(Assets.EnemyDTexture, new Vector2(w + 1 * 32, h), Color.White);
			//h = 32 * 8;
			//spriteBatch.Draw(Assets.EnemyATexture, new Vector2(w + 0 * 32, h), Color.Black);
			//spriteBatch.Draw(Assets.EnemyBTexture, new Vector2(w + 1 * 32, h), Color.Black);
			//h = 32 * 9;
			//spriteBatch.Draw(Assets.EnemyCTexture, new Vector2(w + 0 * 32, h), Color.Black);
			//spriteBatch.Draw(Assets.EnemyDTexture, new Vector2(w + 1 * 32, h), Color.Black);
			//spriteBatch.Draw(Assets.BlockTexture, new Vector2(w, 96), Color.White);
			//spriteBatch.Draw(Assets.PlatformTexture, new Vector2(w + 32, 96), Color.White);

			//spriteBatch.Draw(Assets.GemTexture, new Vector2(w, 128), Color.Yellow);
			//spriteBatch.Draw(Assets.GemTexture, new Vector2(w + 32, 128), Color.Red);
		}

		public void RemovePrevious(int bx, int by, String selector)
		{
			for (int y = 0; y < 12; y++)
			{
				for (int x = 0; x < 16; x++)
				{
					String tile = Tiles[x, y];
					if (tile == selector)
					{
						if (x != bx || y != by)
						{
							Tiles[x, y] = ".";
						}
					}
				}
			}
		}

		public string[,] Tiles { get; private set; }


		private Texture2D GetTexture(String tile)
		{
			if ("." == tile)
			{
				return Assets.BlankTexture;
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
