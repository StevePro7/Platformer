using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface ILevelManager 
	{
		void Initialize();
		void Initialize(String root);
		void LoadLevel(Byte levelNo);
	}

	public class LevelManager : ILevelManager 
	{
		private String levelRoot;

		private const String LEVELS_DIRECTORY = "Levels";

		public void Initialize()
		{
			Initialize(String.Empty);
		}

		public void Initialize(String root)
		{
			levelRoot = String.Format("{0}{1}/{2}", root, Constants.CONTENT_DIRECTORY,LEVELS_DIRECTORY);
		}

		public void LoadLevel(Byte levelNo)
		{
		}

	}
}
