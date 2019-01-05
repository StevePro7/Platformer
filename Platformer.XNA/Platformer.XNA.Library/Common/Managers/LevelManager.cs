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

		private Vector2 levelNumPosition;
		private Vector2 levelOrbPosition;

		private const String LEVELS_DIRECTORY = "Levels";
		private const String LEVELS_NAMESFILE = "LevelNames";

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
