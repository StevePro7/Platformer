using System;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;
using WindowsGame.Common.TheGame;
using WindowsGame.Master;

namespace WindowsGame.Common
{
	public static class MyGame
	{
		public static void Construct(IGameManager manager)
		{
			Manager = manager;
		}

		public static void Initialize()
		{
			Manager.Logger.Initialize();

			Manager.ConfigManager.Initialize();
			Manager.ConfigManager.LoadContent();

			Manager.ContentManager.Initialize();

			Manager.LevelManager.Initialize();
			Manager.RandomManager.Initialize();
			
			Manager.ResolutionManager.Initialize();
			Manager.SoundManager.Initialize();
			Manager.ScreenManager.Initialize();
		}

		public static void LoadContent(GameType gameType , UInt16 screenWide, UInt16 screenHigh)
		{
			Byte framesPerSecond = Manager.ConfigManager.GlobalConfigData.FramesPerSecond;
			Engine.Game.IsFixedTimeStep = Constants.IsFixedTimeStep;
			Engine.Game.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / framesPerSecond);
			Engine.Game.IsMouseVisible = Constants.IsMouseVisible;

			Manager.ResolutionManager.LoadContent(Constants.IsFullScreen, screenWide, screenHigh);
			Manager.StateManager.LoadContent(gameType);
		}

		public static void LoadContentAsync()
		{
			// Load all the content first!
			Manager.ContentManager.LoadContent();
		}

		public static void UnloadContent()
		{
			Engine.Game.Content.Unload();
		}

		public static void Update(GameTime gameTime)
		{
			// 50fps = 20ms = 20 / 1000 = 0.02
			// Single delta = (Single) gameTime.ElapsedGameTime.TotalSeconds;

			Manager.InputManager.Update(gameTime);

#if WINDOWS && DEBUG
			Boolean escape = Manager.InputManager.Escape();
			if (escape)
			{
				if (Manager.ConfigManager.GlobalConfigData.QuitsToExit)
				{
					Engine.Game.Exit();
					return;
				}
			}
#endif

			Manager.ScreenManager.Update(gameTime);
		}

		public static void Draw()
		{
			Manager.ScreenManager.Draw();
		}

		public static void OnActivated()
		{
		}

		public static void OnDeactivated()
		{
			
		}

		public static void OnExiting()
		{
		}

		public static IGameManager Manager { get; private set; }
	}
}
