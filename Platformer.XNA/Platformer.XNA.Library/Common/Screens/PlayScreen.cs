using System;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class PlayScreen : BaseScreen, IScreen
	{
		public Int32 Update(GameTime gameTime)
		{
			return (Int32)CurrScreen;
		}

		public void Draw()
		{
		}

	}
}
