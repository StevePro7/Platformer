using System;
using WindowsGame.Common.Static;
using System.Collections.Generic;
using WindowsGame.Master;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface ILevelManager 
	{
		void Initialize(GameType gameType);
		void Initialize(GameType gameType, String root);
		void LoadLevel(Byte levelNo);
		void DrawLevel();
		void DrawColumn(Byte index);
		void DrawColumnLeft(Byte index);
		void DrawColumnRght(Byte index);

		IList<String> LevelDataLines { get; }

		Byte[] LevelMap1D { get; }
		Byte[] TileType1D { get; }
		Byte[] Collision1D { get; }
		Vector2[] Position1D { get; }

		Byte[][] LevelMap2D { get; }
		Byte[][] TileType2D { get; }
		Byte[][] Collision2D { get; }
		Vector2[][] Position2D { get; }

		Byte Width { get; }
		Byte Height { get; }
	}

	public class LevelManager : ILevelManager
	{
		private Byte size;
		private String levelRoot;

		private const String LEVELS_DIRECTORY = "Levels";

		public void Initialize(GameType gameType)
		{
			Initialize(gameType, String.Empty);
		}

		public void Initialize(GameType gameType, String root)
		{
			levelRoot = String.Format("{0}{1}/{2}", root, Constants.CONTENT_DIRECTORY, LEVELS_DIRECTORY);
			Width = 0;
			Height = 0;

			size = (Byte)((Byte)gameType * Constants.TILE_WIDE);
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
			Position1D = new Vector2[Height * Width];

			LevelMap2D = new Byte[Height][];
			TileType2D = new Byte[Height][];
			Collision2D = new Byte[Height][];
			Position2D = new Vector2[Height][];

			for (Byte high = 0; high < Height; high++)
			{
				String line = LevelDataLines[high];
				LevelMap2D[high] = new Byte[Width];
				TileType2D[high] = new Byte[Width];
				Collision2D[high] = new Byte[Width];
				Position2D[high] = new Vector2[Width];

				for (Byte wide = 0; wide < Width; wide++)
				{
					UInt16 index = (UInt16)(high * Height + wide);

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

					//Vector2 position = new Vector2(wide * size, high * size);
					//Position2D[high][wide] = position;
				}
			}
		}

		public void DrawLevel()
		{
			for (Byte high = 0; high < Height; high++)
			{
				for (Byte wide = 0; wide < Width; wide++)
				{
					DrawCol(high, wide);
				}
			}
		}

		public void DrawColumn(Byte index)
		{
			for (Byte high = 0; high < Height; high++)
			{
				DrawCol(high, index);
			}
		}
		public void DrawColumnLeft(Byte index)
		{
			for (Byte high = 0; high < Height; high++)
			{
				DrawColL(high, index);
			}
		}
		public void DrawColumnRght(Byte index)
		{
			for (Byte high = 0; high < Height; high++)
			{
				DrawColR(high, index);
			}
		}

		
		private void DrawCol(Byte high, Byte wide)
		{
			Byte block = LevelMap2D[high][wide];
			BlockType blockType = (BlockType)block;
			MyGame.Manager.TileManager.DrawBlockType(blockType, wide, high);
		}
		private void DrawColL(Byte high, Byte wide)
		{
			Byte block = LevelMap2D[high][wide];
			BlockType blockType = (BlockType)block;
			MyGame.Manager.TileManager.DrawBlockTypeLeft(blockType, wide, high);
		}
		private void DrawColR(Byte high, Byte wide)
		{
			Byte block = LevelMap2D[high][wide];
			BlockType blockType = (BlockType)block;
			MyGame.Manager.TileManager.DrawBlockTypeRght(blockType, wide, high);
		}

		public IList<String> LevelDataLines { get; private set; }

		public Byte[] LevelMap1D { get; private set; }
		public Byte[] TileType1D { get; private set; }
		public Byte[] Collision1D { get; private set; }
		public Vector2[] Position1D { get; private set; }

		public Byte[][] LevelMap2D { get; private set; }
		public Byte[][] TileType2D { get; private set; }
		public Byte[][] Collision2D { get; private set; }
		public Vector2[][] Position2D { get; private set; }

		public Byte Width { get; private set; }
		public Byte Height { get; private set; }
	}
}
