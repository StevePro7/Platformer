using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class AnGame : Game
	{
		private readonly GraphicsDeviceManager graphics;

		public AnGame()
		{
			graphics = new GraphicsDeviceManager(this) { SupportedOrientations = DisplayOrientation.LandscapeLeft };
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			base.LoadContent();
		}

		protected override void UnloadContent()
		{
			base.UnloadContent();
		}

		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
		}

		protected override void OnActivated(object sender, EventArgs args)
		{
			base.OnActivated(sender, args);
		}

		protected override void OnDeactivated(object sender, EventArgs args)
		{
			base.OnDeactivated(sender, args);
		}

		protected override void OnExiting(object sender, EventArgs args)
		{
			base.OnExiting(sender, args);
		}
	}
}
