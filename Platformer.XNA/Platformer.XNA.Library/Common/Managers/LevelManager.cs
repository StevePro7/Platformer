using System;
using WindowsGame.Common.Static;
using System.Collections.Generic;

namespace WindowsGame.Common.Managers
{
	public interface ILevelManager 
	{
		void Initialize();
		void Initialize(String root);
		void LoadLevel(Byte levelNo);

		IList<String> LevelDataLines { get; }

		Byte[] Collision1D { get; }
		Byte[][] Collision2D { get; }
		Byte Width { get; }
		Byte Height { get; }
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
			levelRoot = String.Format("{0}{1}/{2}", root, Constants.CONTENT_DIRECTORY, LEVELS_DIRECTORY);
			Width = 0;
			Height = 0;
		}

		public void LoadLevel(Byte levelNo)
		{
			String file = String.Format("{0}/{1}.txt", levelRoot, levelNo);
			LevelDataLines = MyGame.Manager.FileManager.LoadTxt(file);

			Width = (Byte) LevelDataLines[0].Length;
			Height = (Byte)LevelDataLines.Count;

			Collision1D = new Byte[Height * Width];
			Collision2D = new Byte[Height][];

			for (Byte high = 0; high < Height; high++)
			{
				for (Byte wide = 0; wide < Width; wide++)
				{
					Collision2D[high] = new Byte[Width];
					String line = LevelDataLines[high];
					Char tile = line[wide];
					TileType tileType = MyGame.Manager.TileManager.GetTileType(tile);
				}
			}

		}

		public IList<String> LevelDataLines { get; private set; }
		public Byte[] Collision1D { get; private set; }
		public Byte[][] Collision2D { get; private set; }
		public Byte Width { get; private set; }
		public Byte Height { get; private set; }
	}
}
