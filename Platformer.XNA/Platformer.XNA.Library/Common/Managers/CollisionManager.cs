using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface ICollisionManager 
	{
		CollisionType GetCollisionType(Char tile);
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

	}
}
