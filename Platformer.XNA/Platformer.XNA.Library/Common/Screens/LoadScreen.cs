using System;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class LoadScreen : BaseScreen, IScreen
	{
		public override void LoadContent()
		{
			Byte levelNo = MyGame.Manager.ConfigManager.GlobalConfigData.LevelNo;
			MyGame.Manager.LevelManager.LoadLevel(levelNo);
		}

		public Int32 Update(GameTime gameTime)
		{
			return (Int32) ScreenType.Play;
		}

		public void Draw()
		{
		}

	}
}
