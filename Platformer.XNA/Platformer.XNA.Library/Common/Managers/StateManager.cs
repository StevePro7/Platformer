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
	}

	public class StateManager : IStateManager
	{
		public void Initialize(GameType gameType, UInt16 screenWide, UInt16 screenHigh)
		{
			GameType = gameType;
			ScreenWide = screenWide;
			ScreenHigh = screenHigh;
		}

		public GameType GameType { get; private set; }
		public UInt16 ScreenWide { get; private set; }
		public  UInt16 ScreenHigh { get; private set; }
	}
}
