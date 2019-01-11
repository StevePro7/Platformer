using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame.Common.Screens;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Managers
{
	public interface IScreenManager 
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void Draw();
	}

	public class ScreenManager : IScreenManager 
	{
		private IDictionary<Int32, IScreen> screens;
		private Int32 currScreen = (Int32)ScreenType.Splash;
		private Int32 nextScreen = (Int32)ScreenType.Splash;
		private readonly Color color = Color.Black;

		public void Initialize()
		{
			screens = GetScreens();
			screens[(Int32)ScreenType.Splash].Initialize();
			screens[(Int32)ScreenType.Init].Initialize();
		}

		public void LoadContent()
		{
			foreach (var key in screens.Keys)
			{
				if ((Int32)ScreenType.Splash == key || (Int32)ScreenType.Init == key)
				{
					continue;
				}

				screens[key].Initialize();
			}
		}

		public void Update(GameTime gameTime)
		{
			if (currScreen != nextScreen)
			{
				currScreen = nextScreen;
				screens[currScreen].LoadContent();
			}

			nextScreen = screens[currScreen].Update(gameTime);
		}

		public void Draw()
		{
			MyGame.Manager.ResolutionManager.BeginDraw(color);
			Engine.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, MyGame.Manager.ResolutionManager.TransformationMatrix);
			screens[currScreen].Draw();
			Engine.SpriteBatch.End();
		}

		private static Dictionary<Int32, IScreen> GetScreens()
		{
			return new Dictionary<Int32, IScreen>
			{
				{(Int32)ScreenType.Splash, new SplashScreen()},
				{(Int32)ScreenType.Init, new InitScreen()},
				{(Int32)ScreenType.Load, new LoadScreen()},
				{(Int32)ScreenType.Play, new PlayScreen()},
			};
		}

	}
}
