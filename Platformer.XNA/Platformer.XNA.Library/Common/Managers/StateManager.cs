using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IStateManager
	{
		void LoadContent(GameType gameType);

		GameType GameType { get; }
	}

	public class StateManager : IStateManager
	{
		public void LoadContent(GameType gameType)
		{
			GameType = gameType;
		}

		public GameType GameType { get; private set; }
	}
}
