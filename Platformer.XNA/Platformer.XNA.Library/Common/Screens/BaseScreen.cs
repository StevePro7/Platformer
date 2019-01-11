using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Screens
{
	public abstract class BaseScreen
	{
		protected UInt16 Timer { get; set; }
		protected ScreenType CurrScreen { get; set; }
		protected ScreenType NextScreen { get; set; }

		public virtual void Initialize()
		{
			String screenName = GetType().Name.ToLower();
			screenName = screenName.Replace("screen", String.Empty);
			CurrScreen = (ScreenType)Enum.Parse(typeof(ScreenType), screenName, true);
			NextScreen = CurrScreen;
		}

		public virtual void LoadContent()
		{
			Timer = 0;
		}

		protected void UpdateTimer(GameTime gameTime)
		{
			Timer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
		}

		protected void ResetTimer()
		{
			Timer = 0;
		}

	}
}