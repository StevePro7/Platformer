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

		Byte[] LevelMap1D { get; }
		Byte[] TileType1D { get; }
		Byte[] Collision1D { get; }

		Byte[][] LevelMap2D { get; }
		Byte[][] TileType2D { get; }
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

			LevelMap1D = new Byte[Height * Width];
			TileType1D = new Byte[Height * Width];
			Collision1D = new Byte[Height * Width];

			LevelMap2D = new Byte[Height][];
			TileType2D = new Byte[Height][];
			Collision2D = new Byte[Height][];

			for (Byte high = 0; high < Height; high++)
			{
				for (Byte wide = 0; wide < Width; wide++)
				{
					UInt16 index = (UInt16)(high * Height + wide);

					LevelMap2D[high] = new Byte[Width];
					TileType2D[high] = new Byte[Width];
					Collision2D[high] = new Byte[Width];

					String line = LevelDataLines[high];
					Char tile = line[wide];

					TileType tileType = MyGame.Manager.TileManager.GetTileType(tile);
					TileType1D[index] = (Byte) tileType;
					TileType2D[high][wide] = (Byte)tileType;

					CollisionType collisionType = MyGame.Manager.CollisionManager.GetCollisionType(tile);
					Collision1D[index] = (Byte) collisionType;
					Collision2D[high][wide] = (Byte)collisionType;

					BlockType blockType = MyGame.Manager.TileManager.GetBlockType(tileType);
					LevelMap1D[index] = (Byte) blockType;
					LevelMap2D[high][wide] = (Byte)blockType;
				}
			}

		}

		public IList<String> LevelDataLines { get; private set; }

		public Byte[] LevelMap1D { get; private set; }
		public Byte[] TileType1D { get; private set; }
		public Byte[] Collision1D { get; private set; }

		public Byte[][] LevelMap2D { get; private set; }
		public Byte[][] TileType2D { get; private set; }
		public Byte[][] Collision2D { get; private set; }

		public Byte Width { get; private set; }
		public Byte Height { get; private set; }
	}
}
