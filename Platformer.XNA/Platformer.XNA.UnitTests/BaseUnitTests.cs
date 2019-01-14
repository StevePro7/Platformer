using NUnit.Framework;
using Rhino.Mocks;
using WindowsGame.Common;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using WindowsGame.Common.TheGame;
using WindowsGame.Master.Interfaces;
using WindowsGame.Master.Managers;

namespace WindowsGame.UnitTests
{
	public abstract class BaseUnitTests
	{
		protected IAnimationManager AnimationManager;
		protected ICollisionManager CollisionManager;
		protected IConfigManager ConfigManager;
		protected IContentManager ContentManager;
		protected IEnemysManager EnemysManager;
		protected IInputManager InputManager;
		protected ILevelManager LevelManager;
		protected IPlayerManager PlayerManager;
		protected IPhysicsManager PhysicsManager;
		protected IRandomManager RandomManager;
		protected IResolutionManager ResolutionManager;
		protected IScreenManager ScreenManager;
		protected ISoundManager SoundManager;
		protected IScrollManager ScrollManager;
		protected IStateManager StateManager;
		protected ITileManager TileManager;
		protected IFileManager FileManager;
		protected ILogger Logger;

#pragma warning disable 618
		[TestFixtureSetUp]
#pragma warning restore 618
		public void TestFixtureSetUp()
		{
			AnimationManager = MockRepository.GenerateStub<IAnimationManager>();
			CollisionManager = MockRepository.GenerateStub<ICollisionManager>();
			ConfigManager = MockRepository.GenerateStub<IConfigManager>();
			ContentManager = MockRepository.GenerateStub<IContentManager>();
			EnemysManager = MockRepository.GenerateStub<IEnemysManager>();
			InputManager = MockRepository.GenerateStub<IInputManager>();
			LevelManager = MockRepository.GenerateStub<ILevelManager>();
			PlayerManager = MockRepository.GenerateStub<IPlayerManager>();
			PhysicsManager = MockRepository.GenerateStub<IPhysicsManager>();
			RandomManager = MockRepository.GenerateStub<IRandomManager>();
			ResolutionManager = MockRepository.GenerateStub<IResolutionManager>();
			ScreenManager = MockRepository.GenerateStub<IScreenManager>();
			SoundManager = MockRepository.GenerateStub<ISoundManager>();
			ScrollManager = MockRepository.GenerateStub<IScrollManager>();
			StateManager = MockRepository.GenerateStub<IStateManager>();
			TileManager = MockRepository.GenerateStub<ITileManager>();
			FileManager = MockRepository.GenerateStub<IFileManager>();
			Logger = MockRepository.GenerateStub<ILogger>();
		}

		protected void SetUp()
		{
			IGameManager manager = new GameManager
			(
				AnimationManager,
				CollisionManager,
				ConfigManager,
				ContentManager,
				EnemysManager,
				InputManager,
				LevelManager,
				PlayerManager,
				PhysicsManager,
				RandomManager,
				ResolutionManager,
				ScreenManager,
				SoundManager,
				ScrollManager,
				StateManager,
				TileManager,
				FileManager,
				Logger
			);

			MyGame.Construct(manager);
		}

#pragma warning disable 618
		[TestFixtureTearDown]
#pragma warning restore 618
		public void TestFixtureTearDown()
		{
			AnimationManager = null;
			CollisionManager = null;
			ConfigManager = null;
			ContentManager = null;
			EnemysManager = null;
			InputManager = null;
			LevelManager = null;
			PlayerManager = null;
			PhysicsManager = null;
			RandomManager = null;
			ResolutionManager = null;
			ScreenManager = null;
			SoundManager = null;
			ScrollManager = null;
			StateManager = null;
			TileManager = null;
			FileManager = null;
			Logger = null;
		}

	}
}
