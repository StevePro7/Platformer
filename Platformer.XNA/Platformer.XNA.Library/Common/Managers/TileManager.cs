using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface ITileManager
	{
		TileType GetTileType(Char tile);
	}

	public class TileManager : ITileManager
	{
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
