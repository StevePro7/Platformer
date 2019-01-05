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

			IoCContainer.Initialize<IConfigManager, ConfigManager>();
			IoCContainer.Initialize<IContentManager, ContentManager>();
			IoCContainer.Initialize<ILevelManager, LevelManager>();
			IoCContainer.Initialize<ISoundManager, SoundManager>();

			IoCContainer.Initialize<IInputManager, InputManager>();
			IoCContainer.Initialize<ILogger, ProdLogger>();
		}
	}
}