using WindowsGame.Common.Managers;
using WindowsGame.Common.TheGame;
using WindowsGame.Common.Implementation;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.IoC;

namespace WindowsGame.Common.Static
{
	public static class Registration
	{
		public static void Initialize()
		{
			IoCContainer.Initialize<IGameManager, GameManager>();

			IoCContainer.Initialize<IFileProxy, ProdFileProxy>();
			IoCContainer.Initialize<IFileManager, FileManager>();
		}
	}
}