using WindowsGame.Common.Managers;
using WindowsGame.Common.TheGame;
using WindowsGame.Master.Implementation;
using WindowsGame.Master.Interfaces;
using WindowsGame.Master.IoC;
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


		}
	}
}