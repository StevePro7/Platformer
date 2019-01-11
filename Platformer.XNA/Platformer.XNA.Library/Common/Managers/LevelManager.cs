using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Managers
{
	public interface ILevelManager 
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();
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
			levelRoot = String.Format("{0}{1}/{2}/{3}", root, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY, LEVELS_DIRECTORY);

		}

		public void LoadContent()
		{
			
		}

		
	}
}
