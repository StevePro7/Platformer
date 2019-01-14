using System;
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
		}

	}
}
