using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface ITileManager
	{
		BlockType GetBlockType(TileType tileType);
		TileType GetTileType(Char tile);
	}

	public class TileManager : ITileManager
	{
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
			if (TileType.Exit == tileType)
			{
				return BlockType.Exit;
			}
			if (TileType.Gem == tileType)
			{
				return BlockType.Gem;
			}

			return BlockType.None;
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
}
}
