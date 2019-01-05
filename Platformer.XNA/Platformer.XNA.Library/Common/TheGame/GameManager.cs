using WindowsGame.Common.Managers;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.TheGame
{
	public interface IGameManager
	{
		IConfigManager ConfigManager { get; }
		IContentManager ContentManager { get; }
		IFileManager FileManager { get; }
		ILogger Logger { get; }
	}

	public class GameManager : IGameManager
	{
		public GameManager
		(
			IFileManager fileManager,
			ILogger logger
		)
		{
			FileManager = fileManager;
			Logger = logger;
		}

		public IConfigManager ConfigManager { get; private set; }
		public IContentManager ContentManager { get; private set; }
		public IFileManager FileManager { get; private set; }
		public ILogger Logger { get; private set; }
	}
}