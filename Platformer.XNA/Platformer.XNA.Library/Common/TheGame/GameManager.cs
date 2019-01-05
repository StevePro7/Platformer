using System;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;

namespace WindowsGame.Common.TheGame
{
	public interface IGameManager
	{
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

		public IFileManager FileManager { get; private set; }
		public ILogger Logger { get; private set; }
	}
}