using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using WindowsGame.Master.Interfaces;
using WindowsGame.Master.Managers;

namespace WindowsGame.Common.TheGame
{
	public interface IGameManager
	{
		IAnimationManager AnimationManager { get; }
		ICollisionManager CollisionManager { get; }
		IConfigManager ConfigManager { get; }
		IContentManager ContentManager { get; }
		IEnemyManager EnemyManager { get; }
		IInputManager InputManager { get; }
		ILevelManager LevelManager { get; }
		IPhysicsManager PhysicsManager { get; }
		IRandomManager RandomManager { get; }
		IResolutionManager ResolutionManager { get; }
		ISoundManager SoundManager { get; }
		IScreenManager ScreenManager { get; }
		IScrollManager ScrollManager { get; }
		ITileManager TileManager { get; }
		IFileManager FileManager { get; }
		ILogger Logger { get; }
	}

	public class GameManager : IGameManager
	{
		public GameManager
		(
			IAnimationManager animationManager,
			ICollisionManager collisionManager,
			IConfigManager configManager,
			IContentManager contentManager,
			IEnemyManager enemyManager,
			IInputManager inputManager,
			ILevelManager levelManager,
			IPhysicsManager physicsManager,
			IRandomManager randomManager,
			IResolutionManager resolutionManager,
			IScreenManager screenManager,
			ISoundManager soundManager,
			IScrollManager scrollManager,
			ITileManager tileManager,
			IFileManager fileManager,
			ILogger logger
		)
		{
			AnimationManager = animationManager;
			CollisionManager = collisionManager;
			ConfigManager = configManager;
			ContentManager = contentManager;
			EnemyManager = enemyManager;
			InputManager = inputManager;
			LevelManager = levelManager;
			PhysicsManager = physicsManager;
			RandomManager = randomManager;
			ResolutionManager = resolutionManager;
			SoundManager = soundManager;
			ScreenManager = screenManager;
			ScrollManager = scrollManager;
			TileManager = tileManager;
			FileManager = fileManager;
			Logger = logger;
		}


		public IAnimationManager AnimationManager { get; private set; }
		public ICollisionManager CollisionManager { get; private set; }
		public IConfigManager ConfigManager { get; private set; }
		public IContentManager ContentManager { get; private set; }
		public IEnemyManager EnemyManager { get; private set; }
		public IInputManager InputManager { get; private set; }
		public ILevelManager LevelManager { get; private set; }
		public IPhysicsManager PhysicsManager { get; private set; }
		public IRandomManager RandomManager { get; private set; }
		public IResolutionManager ResolutionManager { get; private set; }
		public ISoundManager SoundManager { get; private set; }
		public IScreenManager ScreenManager { get; private set; }
		public IScrollManager ScrollManager { get; private set; }
		public ITileManager TileManager { get; private set; }
		public IFileManager FileManager { get; private set; }
		public ILogger Logger { get; private set; }
		
	}
}