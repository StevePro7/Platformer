using System;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Managers
{
	public interface ITileManager
	{
		void Initialize(GameType gameType);

		BlockType GetBlockType(TileType tileType);
		TileType GetTileType(Char tile);

		void DrawBlockType(BlockType blockType, Byte x, Byte y);
		void DrawBlockTypeLeft(BlockType blockType, Byte x, Byte y);
		void DrawBlockTypeRght(BlockType blockType, Byte x, Byte y);
	}

	public class TileManager : ITileManager
	{
		private Byte size, wide, high;

		private Rectangle leftRect;
		private Rectangle rghtRect;

		public void Initialize(GameType gameType)
		{
			size = (Byte) ((Byte) gameType * Constants.TILE_WIDTH);
			high = size;
			wide = (Byte) (high / 2);

			leftRect = new Rectangle(0, 0, wide, high);
			rghtRect = new Rectangle(wide, 0, wide, high);
		}

		public BlockType GetBlockType(TileType tileType)
		{
			if (TileType.Blank == tileType)
			{
				return BlockType.Blank;
			}
			if (TileType.Block == tileType)
			{
				Int32 next = MyGame.Manager.RandomManager.Next(9);
				next += 1;
				BlockType blockType = (BlockType) next;
				return blockType;
			}
			if (TileType.Platform == tileType)
			{
				return BlockType.Platform;
			}
			if (TileType.Exit == tileType)
			{
				return BlockType.Exit;
			}
			if (TileType.Gem == tileType)
			{
				return BlockType.Gem;
			}

			// Default block type blank.
			return BlockType.Blank;
		}

		public TileType GetTileType(Char tile)
		{
			if ('.' == tile)
			{
				return TileType.Blank;
			}
			if ('#' == tile)
			{
				return TileType.Block;
			}
			if ('-' == tile)
			{
				return TileType.Platform;
			}
			if ('1' == tile)
			{
				return TileType.Player;
			}
			if ('A' == tile)
			{
				return TileType.EnemyA;
			}
			if ('B' == tile)
			{
				return TileType.EnemyB;
			}
			if ('C' == tile)
			{
				return TileType.EnemyC;
			}
			if ('D' == tile)
			{
				return TileType.EnemyD;
			}
			if ('X' == tile)
			{
				return TileType.Exit;
			}
			if ('G' == tile)
			{
				return TileType.Gem;
			}

			return TileType.Unknown;
		}

		public void DrawBlockType(BlockType blockType, Byte x, Byte y)
		{
			DrawBlock(null, blockType, 0, x, y);
			//Texture2D image = Assets.BlocksTexture[(Byte) blockType];
			//Vector2 position = new Vector2(x * size, y * size);
			////Engine.SpriteBatch.Draw(image, position, Color.White);
			//Engine.SpriteBatch.Draw(image, position, null, Color.White);
		}

		public void DrawBlockTypeLeft(BlockType blockType, Byte x, Byte y)
		{
			DrawBlock(leftRect, blockType, 0, x, y);
			//Texture2D image = Assets.BlocksTexture[(Byte)blockType];
			//Vector2 position = new Vector2(x * size, y * size);
			//Engine.SpriteBatch.Draw(image, new Vector2(x, y), leftRect, Color.White);
			//Engine.SpriteBatch.Draw(image, position, leftRect, Color.White);
		}

		public void DrawBlockTypeRght(BlockType blockType, Byte x, Byte y)
		{
			DrawBlock(rghtRect, blockType, wide, x, y);
			//Texture2D image = Assets.BlocksTexture[(Byte)blockType];
			//Vector2 position = new Vector2(x * size, y * size);

			////Engine.SpriteBatch.Draw(image, new Vector2(x, y), rghtRect, Color.White);
			//Engine.SpriteBatch.Draw(image, position, rghtRect, Color.White);
		}

		private void DrawBlock(Rectangle? rectangle, BlockType blockType, Byte s, Byte x, Byte y)
		{
			Texture2D image = Assets.BlocksTexture[(Byte)blockType];
			Vector2 position = new Vector2(x * size + s, y * size);
			Engine.SpriteBatch.Draw(image, position, rectangle, Color.White);
		}

	}
}
