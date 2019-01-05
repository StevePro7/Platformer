using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using WindowsGame.Master.Interfaces;
using WindowsGame.Master.Managers;

namespace WindowsGame.Common.TheGame
{
	public interface IGameManager
	{
		IConfigManager ConfigManager { get; }
		IContentManager ContentManager { get; }
		IInputManager InputManager { get; }
		ILevelManager LevelManager { get; }
		IRandomManager RandomManager { get; }
		IResolutionManager ResolutionManager { get; }
		IFileManager FileManager { get; }
		ILogger Logger { get; }
	}

	public class GameManager : IGameManager
	{
		public GameManager
		(
			IConfigManager configManager,
			IContentManager contentManager,
			IInputManager inputManager,
			ILevelManager levelManager,
			IRandomManager randomManager,
			IResolutionManager resolutionManager,
			IFileManager fileManager,
			ILogger logger
		)
		{
			ConfigManager = configManager;
			ContentManager = contentManager;
			InputManager = inputManager;
			LevelManager = levelManager;
			RandomManager = randomManager;
			ResolutionManager = resolutionManager;
			FileManager = fileManager;
			Logger = logger;
		}

		public IConfigManager ConfigManager { get; private set; }
		public IContentManager ContentManager { get; private set; }
		public IInputManager InputManager { get; private set; }
		public ILevelManager LevelManager { get; private set; }
		public IRandomManager RandomManager { get; private set; }
		public IResolutionManager ResolutionManager { get; private set; }
		public IFileManager FileManager { get; private set; }
		public ILogger Logger { get; private set; }
	}
}