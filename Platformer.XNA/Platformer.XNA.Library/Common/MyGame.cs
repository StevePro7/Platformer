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
		}

		public static void LoadContent()
		{
			//Byte framesPerSecond = Manager.ConfigManager.GlobalConfigData.FramesPerSecond;
			//Engine.Game.IsFixedTimeStep = Constants.IsFixedTimeStep;
			//Engine.Game.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / framesPerSecond);
			//Engine.Game.IsMouseVisible = Constants.IsMouseVisible;
			//Manager.ResolutionManager.LoadContent(Constants.IsFullScreen, Constants.ScreenWide, Constants.ScreenHigh, Constants.UseExposed, Constants.ExposeWide, Constants.ExposeHigh);
			//Manager.InputManager.LoadContent();
		}

		public static void UnloadContent()
		{
			Engine.Game.Content.Unload();
		}

		public static void Update(GameTime gameTime)
		{
		}

		public static void Draw()
		{
			//Manager.ScreenManager.Draw();
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
