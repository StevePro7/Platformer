using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class SplashScreen : IScreen
	{
		private Boolean flag;

		public void Initialize()
		{
			flag = false;
		}

		public void LoadContent()
		{
		}

		public Int32 Update(GameTime gameTime)
		{
			return flag ? (Int32)ScreenType.Init : (Int32)ScreenType.Splash;
		}

		public void Draw()
		{
			flag = true;
		}

	}
}
