﻿using System;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Screens
{
	public class PlayScreen : BaseScreen, IScreen
	{
		public Int32 Update(GameTime gameTime)
		{
			return (Int32)ScreenType.Play;
		}

		public void Draw()
		{
			//MyGame.Manager.LevelManager.DrawLevel();

			//MyGame.Manager.LevelManager.DrawColumn(0);
			MyGame.Manager.LevelManager.DrawColumn(1);
			MyGame.Manager.LevelManager.DrawColumn(2);
			MyGame.Manager.LevelManager.DrawColumn(3);
			MyGame.Manager.LevelManager.DrawColumn(4);


			//MyGame.Manager.TileManager.DrawBlockTypeLeft(BlockType.Blank, 0, 0);

			//MyGame.Manager.TileManager.DrawBlockTypeLeft(BlockType.Platform, 0, 1);
			//MyGame.Manager.TileManager.DrawBlockTypeRght(BlockType.Platform, 0, 1);

			//MyGame.Manager.TileManager.DrawBlockType(BlockType.Platform, 0, 2);
			//MyGame.Manager.TileManager.DrawBlockType(BlockType.Platform, 0, 0);
			//MyGame.Manager.TileManager.DrawBlockType(BlockType.BlockA1, 1, 0);
			//MyGame.Manager.TileManager.DrawBlockType(BlockType.Blank, 0, 0);
		}

	}
}
