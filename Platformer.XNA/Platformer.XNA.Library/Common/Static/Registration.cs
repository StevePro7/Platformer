using WindowsGame.Common.Managers;
using WindowsGame.Common.TheGame;
using WindowsGame.Master.Implementation;
using WindowsGame.Master.Interfaces;
using WindowsGame.Master.IoC;

namespace WindowsGame.Common.Static
{
	public static class Registration
	{
		public static void Initialize()
		{
			IoCContainer.Initialize<IGameManager, GameManager>();

			IoCContainer.Initialize<IFileProxy, ProdFileProxy>();
			IoCContainer.Initialize<IFileManager, FileManager>();

			IoCContainer.Initialize<ILogger, ProdLogger>();
		}
	}
}