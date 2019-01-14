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
		void DrawStrips();
		void DrawStripLeft();
		void DrawStripRght();
	}

	public class TileManager : ITileManager
	{
		private Byte size, wide, high;

		private Rectangle leftRect;
		private Rectangle rghtRect;
		private Vector2 rghtPosn;

		public void Initialize(GameType gameType)
		{
			size = (Byte) ((Byte) gameType * Constants.TILE_WIDE);
			high = size;
			wide = (Byte) (high / 2);

			leftRect = new Rectangle(0, 0, wide, high);
			rghtRect = new Rectangle(wide, 0, wide, high);
			rghtPosn = new Vector2(MyGame.Manager.StateManager.ScreenWide - wide, 0);
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
		}

		public void DrawBlockTypeLeft(BlockType blockType, Byte x, Byte y)
		{
			DrawBlock(leftRect, blockType, 0, x, y);
		}

		public void DrawBlockTypeRght(BlockType blockType, Byte x, Byte y)
		{
			DrawBlock(rghtRect, blockType, wide, x, y);
		}

		public void DrawStrips()
		{
			DrawStripLeft();
			DrawStripRght();
		}
		public void DrawStripLeft()
		{
			DrawStrip(Vector2.Zero);
		}
		public void DrawStripRght()
		{
			DrawStrip(rghtPosn);
		}
		private void DrawStrip(Vector2 position)
		{
			Texture2D image = Assets.BlocksTexture[(Byte)BlockType.Strip];
			Engine.SpriteBatch.Draw(image, position, Color.White);
		}

		private void DrawBlock(Rectangle? rectangle, BlockType blockType, Byte s, Byte x, Byte y)
		{
			Texture2D image = Assets.BlocksTexture[(Byte)blockType];
			Vector2 position = new Vector2(x * size + s, y * size);
			Engine.SpriteBatch.Draw(image, position, rectangle, Color.White);
		}

	}
}
