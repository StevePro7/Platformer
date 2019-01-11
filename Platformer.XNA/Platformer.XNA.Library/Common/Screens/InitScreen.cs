using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class InitScreen : IScreen
	{
		public void Initialize()
		{
		}

		public void LoadContent()
		{
			MyGame.LoadContentAsync();
		}

		public Int32 Update(GameTime gameTime)
		{
			ScreenType screenType = MyGame.Manager.ConfigManager.GlobalConfigData.ScreenType;
			if (ScreenType.Splash == screenType || ScreenType.Init == screenType)
			{
				screenType = ScreenType.Load;
			}

			return (Int32)screenType;
		}

		public void Draw()
		{
		}

	}
}
