using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IStateManager
	{
		void Initialize(GameType gameType, UInt16 screenWide, UInt16 screenHigh);

		GameType GameType { get; }
		UInt16 ScreenWide { get; }
		UInt16 ScreenHigh { get; }
		Byte GameOffset { get; }
		//Byte TileSize { get; }
	}

	public class StateManager : IStateManager
	{
		public void Initialize(GameType gameType, UInt16 screenWide, UInt16 screenHigh)
		{
			GameType = gameType;
			ScreenWide = screenWide;
			ScreenHigh = screenHigh;
			GameOffset = (Byte)((Byte)gameType * Constants.GAME_WIDE);
			//TileSize = (Byte)((Byte)gameType * Constants.TILE_WIDE);
		}

		public GameType GameType { get; private set; }
		public UInt16 ScreenWide { get; private set; }
		public UInt16 ScreenHigh { get; private set; }
		public Byte GameOffset { get; private set; }
		//public Byte TileSize { get; private set; }
	}
}
