using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface ICollisionManager 
	{
		CollisionType GetCollisionType(Char tile);
		CollisionType GetCollisionTile(SByte x, SByte y, Byte wide, Byte high, Byte[] collision1D, Byte[][] collision2D);
	}

	public class CollisionManager : ICollisionManager 
	{
		public CollisionType GetCollisionType(Char tile)
		{
			if ('#' == tile)
			{
				return CollisionType.Impassable;
			}
			if ('-' == tile)
			{
				return CollisionType.Platform;
			}

			// All other tile are passable.
			return CollisionType.Passable;
		}

		public CollisionType GetCollisionTile(SByte x, SByte y, Byte wide, Byte high, Byte[]collision1D, Byte[][] collision2D)
		{
			//// Prevent escaping past the level ends.
			//if (x < 0 || x >= wide)
			//    return CollisionType.Impassable;
			//// Allow jumping past the level top and falling through the bottom.
			//if (y < 0 || y >= high)
			//    return CollisionType.Passable;

			UInt16 index = (UInt16)(y * high + x);
			Byte collision1 = collision1D[index];
			Byte collision2 = collision2D[y][x];
			return (CollisionType)collision2;
		}

	}
}
