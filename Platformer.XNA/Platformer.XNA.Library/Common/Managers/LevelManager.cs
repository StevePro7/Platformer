using System;
using System.Collections.Generic;
using WindowsGame.Common.Objects;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

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

		Rectangle GetBounds(Byte x, Byte y);
		Vector2 GetBottomCenter(Rectangle rect);

		IList<String> LevelDataLines { get; }

		Byte[] LevelMap1D { get; }
		Byte[] TileType1D { get; }
		Byte[] Collision1D { get; }
		//Vector2[] Position1D { get; }

		Byte[][] LevelMap2D { get; }
		Byte[][] TileType2D { get; }
		Byte[][] Collision2D { get; }
		//Vector2[][] Position2D { get; }

		Byte LevelWide { get; }
		Byte LevelHigh { get; }
		UInt16 PlayerSpot { get; }
		IList<UInt16> EnemysSpots { get; }
		IList<EnemysType> EnemysTypes { get; }
	}

	public class LevelManager : ILevelManager
	{
		private String levelRoot;

		private const String LEVELS_DIRECTORY = "Levels";

		public void Initialize(GameType gameType)
		{
			Initialize(gameType, String.Empty);
		}

		public void Initialize(GameType gameType, String root)
		{
			levelRoot = String.Format("{0}{1}/{2}", root, Constants.CONTENT_DIRECTORY, LEVELS_DIRECTORY);
			LevelWide = 0;
			LevelHigh = 0;
			PlayerSpot = 0;
			EnemysSpots = new List<UInt16>();
			EnemysTypes = new List<EnemysType>();
		}

		public void LoadLevel(Byte levelNo)
		{
			String file = String.Format("{0}/{1}.txt", levelRoot, levelNo);
			LevelDataLines = MyGame.Manager.FileManager.LoadTxt(file);

			LevelWide = (Byte)LevelDataLines[0].Length;
			LevelHigh = (Byte)LevelDataLines.Count;

			LevelMap1D = new Byte[LevelHigh * LevelWide];
			TileType1D = new Byte[LevelHigh * LevelWide];
			Collision1D = new Byte[LevelHigh * LevelWide];
			//Position1D = new Vector2[LevelHigh * LevelWide];

			LevelMap2D = new Byte[LevelHigh][];
			TileType2D = new Byte[LevelHigh][];
			Collision2D = new Byte[LevelHigh][];
			//Position2D = new Vector2[LevelHigh][];

			PlayerSpot = 0;
			EnemysSpots.Clear();
			EnemysTypes.Clear();

			for (Byte high = 0; high < LevelHigh; high++)
			{
				String line = LevelDataLines[high];
				LevelMap2D[high] = new Byte[LevelWide];
				TileType2D[high] = new Byte[LevelWide];
				Collision2D[high] = new Byte[LevelWide];
				//Position2D[high] = new Vector2[LevelWide];

				for (Byte wide = 0; wide < LevelWide; wide++)
				{
					UInt16 index = (UInt16)(high * LevelHigh + wide);

					Char tile = line[wide];
					TileType tileType = MyGame.Manager.TileManager.GetTileType(tile);
					TileType1D[index] = (Byte) tileType;
					TileType2D[high][wide] = (Byte)tileType;

					if (tileType == TileType.Player)
					{
						PlayerSpot = (UInt16) (high * LevelHigh + wide);
					}
					if (tileType == TileType.EnemyA || tileType == TileType.EnemyB || tileType == TileType.EnemyC || tileType == TileType.EnemyD)
					{
						EnemysType enemysType = MyGame.Manager.TileManager.GetEnemysType(tileType);
						UInt16 enemysSpot = (UInt16)(high * LevelHigh + wide);
						EnemysTypes.Add(enemysType);
						EnemysSpots.Add(enemysSpot);
					}

					CollisionType collisionType = MyGame.Manager.CollisionManager.GetCollisionType(tile);
					Collision1D[index] = (Byte) collisionType;
					Collision2D[high][wide] = (Byte)collisionType;

					BlockType blockType = MyGame.Manager.TileManager.GetBlockType(tileType);
					LevelMap1D[index] = (Byte) blockType;
					LevelMap2D[high][wide] = (Byte)blockType;
				}
			}
		}

		public void DrawLevel()
		{
			for (Byte high = 0; high < LevelHigh; high++)
			{
				for (Byte wide = 0; wide < LevelWide; wide++)
				{
					DrawCol(high, wide);
				}
			}
		}

		public void DrawColumn(Byte index)
		{
			for (Byte high = 0; high < LevelHigh; high++)
			{
				DrawCol(high, index);
			}
		}
		public void DrawColumnLeft(Byte index)
		{
			for (Byte high = 0; high < LevelHigh; high++)
			{
				DrawColL(high, index);
			}
		}
		public void DrawColumnRght(Byte index)
		{
			for (Byte high = 0; high < LevelHigh; high++)
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

		public Rectangle GetBounds(Byte x, Byte y)
		{
			Byte tileWide = MyGame.Manager.TileManager.TileWide;
			Byte tileHigh = MyGame.Manager.TileManager.TileHigh;
			Rectangle rectangle = new Rectangle(x * tileWide, y * tileHigh, tileWide, tileHigh);
			return rectangle;
		}

		public Vector2 GetBottomCenter(Rectangle bounds)
		{
			return new Vector2(bounds.X + bounds.Width / 2.0f, bounds.Bottom);
		}

		public IList<String> LevelDataLines { get; private set; }

		public Byte[] LevelMap1D { get; private set; }
		public Byte[] TileType1D { get; private set; }
		public Byte[] Collision1D { get; private set; }
		//public Vector2[] Position1D { get; private set; }

		public Byte[][] LevelMap2D { get; private set; }
		public Byte[][] TileType2D { get; private set; }
		public Byte[][] Collision2D { get; private set; }
		//public Vector2[][] Position2D { get; private set; }

		public Byte LevelWide { get; private set; }
		public Byte LevelHigh { get; private set; }
		public UInt16 PlayerSpot { get; private set; }
		public IList<UInt16> EnemysSpots { get; private set; }
		public IList<EnemysType> EnemysTypes { get; private set; }
	}
}
