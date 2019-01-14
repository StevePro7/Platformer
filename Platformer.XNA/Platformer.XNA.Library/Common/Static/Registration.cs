using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using WindowsGame.Common.TheGame;
using WindowsGame.Managers.Inputs;
using WindowsGame.Master.Implementation;
using WindowsGame.Master.IoC;
using WindowsGame.Master.Interfaces;
using TheRegistration = WindowsGame.Master.Static.Registration;

namespace WindowsGame.Common.Static
{
	public static class Registration
	{
		public static void Initialize()
		{
			// Initialize engine first.
			TheRegistration.Initialize();

			IoCContainer.Initialize<IGameManager, GameManager>();

			IoCContainer.Initialize<IAnimationManager, AnimationManager>();
			IoCContainer.Initialize<ICollisionManager, CollisionManager>();
			IoCContainer.Initialize<IConfigManager, ConfigManager>();
			IoCContainer.Initialize<IContentManager, ContentManager>();
			IoCContainer.Initialize<IEnemyManager, EnemyManager>();
			IoCContainer.Initialize<ILevelManager, LevelManager>();
			IoCContainer.Initialize<IPhysicsManager, PhysicsManager>();
			IoCContainer.Initialize<ISoundManager, SoundManager>();
			IoCContainer.Initialize<IScreenManager, ScreenManager>();
			IoCContainer.Initialize<IScrollManager, ScrollManager>();
			IoCContainer.Initialize<IStateManager, StateManager>();
			IoCContainer.Initialize<ITileManager, TileManager>();

			IoCContainer.Initialize<IInputManager, InputManager>();
			IoCContainer.Initialize<ILogger, ProdLogger>();
		}
	}
}