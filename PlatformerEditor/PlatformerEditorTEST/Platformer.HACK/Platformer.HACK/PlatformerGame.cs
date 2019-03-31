using System;
using System.Configuration;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class PlatformerGame : Microsoft.Xna.Framework.Game
	{
		// mklink /D C:\PlatformerEditor C:\Github\StevePro7\Platformer\PlatformerEditor\
		// mklink /D C:\PlatformerEditor D:\Github\StevePro7\Platformer\PlatformerEditor\
		// mklink /D C:\PlatformerEditor E:\Github\StevePro7\Platformer\PlatformerEditor\
		private const string root = @"C:\PlatformerEditor\";
		//private const string root = @"E:\Github\StevePro7\Platformer\PlatformerEditor\";
		private const string srce = @"PlatformerEditorXXXX\Platformer.HACK\Platformer.HACKContent\Levels\";
		private const string dest = @"PlatformerEditorXXXX\Platformer.HACK\Platformer.HACK\bin\x86\Debug\Content\Levels\";
		//const string dest = @"C:\Github\StevePro7\Platformer\PlatformerEditor\PlatformerEditorPLAY\Platformer.HACK\Platformer.HACK\bin\x86\Debug\Content\Levels\";

		private const string file = "0.txt";

		// Resources for drawing.
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private Config config;

		private BoardManager boardManager;
		private LoadManager loadManager;

		// Global content.
		private SpriteFont hudFont;

		//private Texture2D winOverlay;
		//private Texture2D loseOverlay;
		//private Texture2D diedOverlay;

		// Meta-level game state.
		private int levelIndex = 0;
		//private Level level;
		//private bool wasContinuePressed;

		// When the time remaining is less than the warning time, it blinks on the hud
		private static readonly TimeSpan WarningTime = TimeSpan.FromSeconds(30);

		// We store our input states so that we only poll once per frame, 
		// then we use the same input state wherever needed
		private KeyboardState currKeyboardState, prevKeyboardState;
		private MouseState mouseState;
		private bool valid;
		// The number of levels in the Levels directory of our content. We assume that
		// levels in our content are 0-based and that all numbers under this constant
		// have a level file present. This allows us to not need to check for the file
		// or handle exceptions, both of which can add unnecessary time to level loading.
		private const int numberOfLevels = 3;

		private string selector = ".";
		private int fullWide = 18 * 32;
		private int tileWide = 16 * 32;
		private int tileHigh = 12 * 32;

		public PlatformerGame()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = fullWide; //32 * 18;//640;
			graphics.PreferredBackBufferHeight = tileHigh; //32 * 12;//480;
			Content.RootDirectory = "Content";
			Logger.Initialize();

			boardManager = new BoardManager();
			loadManager = new LoadManager();
			valid = true;
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			IsMouseVisible = true;

			//Byte framesPerSecond = 50;
			Byte framesPerSecond = Convert.ToByte(ConfigurationManager.AppSettings["FramesPerSecond"]);
			Byte configLevelNext = Convert.ToByte(ConfigurationManager.AppSettings["BeginStartLevel"]);

			Boolean invincibility = Convert.ToBoolean(ConfigurationManager.AppSettings["Invincibility"]);
			Boolean optionalBlock = Convert.ToBoolean(ConfigurationManager.AppSettings["OptionalBlock"]);
			config = new Config(invincibility, optionalBlock);

			levelIndex = configLevelNext - 1;
			IsFixedTimeStep = true;
			TargetElapsedTime = TimeSpan.FromSeconds(1.0f / framesPerSecond);


			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load fonts
			hudFont = Content.Load<SpriteFont>("Fonts/Hud");

			// Load overlay textures
			//winOverlay = Content.Load<Texture2D>("Overlays/you_win");
			//loseOverlay = Content.Load<Texture2D>("Overlays/you_lose");
			//diedOverlay = Content.Load<Texture2D>("Overlays/you_died");

			loadManager.Load(Content);
			//LoadNextLevel();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// get all of our input states
			currKeyboardState = Keyboard.GetState();

			Getselector();

			bool optional = currKeyboardState.IsKeyDown(Keys.O);
			if (optional)
			{
				//Logger.Info("O pressed");
			}

			bool clear = currKeyboardState.IsKeyDown(Keys.Delete) && prevKeyboardState.IsKeyUp(Keys.Delete);
			{
				if (clear)
				{
					valid = true;
					boardManager.Clear();
				}
			}

			bool back = currKeyboardState.IsKeyDown(Keys.Back) && prevKeyboardState.IsKeyUp(Keys.Back);
			if (back)
			{
				valid = true;
			}

			bool load = currKeyboardState.IsKeyDown(Keys.L) && prevKeyboardState.IsKeyUp(Keys.L);
			if (load)
			{
				const string path = "Content/Levels/" + file;
				boardManager.Load(path);
				valid = true;
			}
			bool save = currKeyboardState.IsKeyDown(Keys.S) && prevKeyboardState.IsKeyUp(Keys.S);
			if (save)
			{
				valid = boardManager.ValidateBoard();
				if (valid)
				{
					//const string root = "Content/Levels/";
					//const string dest = @"\..\..\..\..\..\..\PlatformerEditorPLAY\Platformer.HACK\Platformer.HACK\bin\x86\Debug\Content\Levels\";
					//const string dest = @"\..\PlatformerEditorPLAY\Platformer.HACK\Platformer.HACK\bin\x86\Debug\Content\Levels\";
					
					boardManager.Save(root, srce, dest, file);
				}
			}

			mouseState = Mouse.GetState();
			ButtonState buttonState;
			buttonState = mouseState.LeftButton;
			if (buttonState == ButtonState.Pressed)
			{
				int mx = mouseState.X;
				int my = mouseState.Y;
				if (mx >= 32 && mx <= tileWide && my >= 32 && my <= tileHigh)
				{
					int bx = mx / 32;
					int by = my / 32;

					string pos = String.Format("({0},{1}), ", bx, by);
					//Logger.Info(pos);

					bool update = ValidateTile(bx, by, selector);
					if (update)
					{
						boardManager.Update(bx, by, selector, optional);
					}

					if ("X" == selector || "1" == selector)
					{
						boardManager.RemovePrevious(bx, by, selector);
					}
				}

				if (mx >= tileWide && mx <= fullWide && my >= 0 && my <= tileHigh)
				{
					int bx = mx / 32;
					int by = my / 32;

					Getselector2(bx, by, optional);
				}
			}

			buttonState = mouseState.RightButton;
			if (buttonState == ButtonState.Pressed)
			{
				int mx = mouseState.X;
				int my = mouseState.Y;
				if (mx >= 0 && mx <= tileWide && my >= 0 && my <= tileHigh)
				{
					int bx = mx / 32;
					int by = my / 32;

					string pos = String.Format("({0},{1}), ", bx, by);
					//Logger.Info(pos);
					boardManager.Update(bx, by, ".");
				}
			}

			if (currKeyboardState.IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			prevKeyboardState = currKeyboardState;
			base.Update(gameTime);
		}

		/// <summary>
		/// Draws the game from background to foreground.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);


			spriteBatch.Begin();

			boardManager.Draw(spriteBatch, selector);
			if (!valid)
			{
				spriteBatch.Draw(Assets.ErrorTexture, new Vector2((tileWide - Assets.ErrorTexture.Width) / 2, (tileHigh - Assets.ErrorTexture.Height) / 2), Color.White);
			}
			spriteBatch.End();

			base.Draw(gameTime);
		}

		private bool ValidateTile(int bx, int by, string selector)
		{
			if (0 == by || 11 == by || 10 == by)
			{
				if ("A" == selector || "B" == selector || "C" == selector || "D" == selector ||
				    "a" == selector || "b" == selector || "c" == selector || "d" == selector ||
				    "1" == selector)
				{
					return false;
				}
			}
			if (11 == by)
			{
				if ("X" == selector || "G" == selector || "P" == selector)
				{
					return false;
				}
			}

			return true;
		}
		private void Getselector2(int bx, int by, bool optional)
		{
			if (optional)
			{
				if (16 == bx && 7 == by || 16 == bx && 8 == by)
				{
					selector = "a";
				}
				else if (17 == bx && 7 == by || 17 == bx && 8 == by)
				{
					selector = "b";
				}
				else if (16 == bx && 9 == by || 16 == bx && 10 == by)
				{
					selector = "c";
				}
				else if (17 == bx && 9 == by || 17 == bx && 10 == by)
				{
					selector = "d";
				}

				return;
			}

			if (16 == bx && 3 == by)
			{
				selector = ".";
			}
			else if (17 == bx && 3 == by)
			{
				selector = "#";
			}
			else if (16 == bx && 4 == by)
			{
				selector = "@";
			}
			else if (17 == bx && 4 == by)
			{
				selector = "$";
			}
			else if (16 == bx && 5 == by)
			{
				selector = "G";
			}
			else if (17 == bx && 5 == by)
			{
				selector = "P";
			}
			else if (16 == bx && 6 == by || 16 == bx && 7 == by)
			{
				selector = "X";
			}
			else if (17 == bx && 6 == by || 17 == bx && 7 == by)
			{
				selector = "1";
			}
			else if (16 == bx && 8 == by || 16 == bx && 9 == by)
			{
				selector = "A";
			}
			else if (17 == bx && 8 == by || 17 == bx && 9 == by)
			{
				selector = "B";
			}
			else if (16 == bx && 10 == by || 16 == bx && 11 == by)
			{
				selector = "C";
			}
			else if (17 == bx && 10 == by || 17 == bx && 11 == by)
			{
				selector = "D";
			}
		}

		private void Getselector()
		{
			if (currKeyboardState.IsKeyDown(Keys.D1))
			{
				selector = ".";
			}
			if (currKeyboardState.IsKeyDown(Keys.D2))
			{
				selector = "#";
			}
			if (currKeyboardState.IsKeyDown(Keys.D3))
			{
				selector = "@";
			}
			if (currKeyboardState.IsKeyDown(Keys.D4))
			{
				selector = "$";
			}
			if (currKeyboardState.IsKeyDown(Keys.D5))
			{
				selector = "G";
			}
			if (currKeyboardState.IsKeyDown(Keys.D6))
			{
				selector = "P";
			}
			if (currKeyboardState.IsKeyDown(Keys.D7))
			{
				selector = "X";
			}
			if (currKeyboardState.IsKeyDown(Keys.D8))
			{
				selector = "1";
			}
			//if (currKeyboardState.IsKeyDown(Keys.A))
			//{
			//    selector = "A";
			//}
			//if (currKeyboardState.IsKeyDown(Keys.S))
			//{
			//    selector = "B";
			//}
			//if (currKeyboardState.IsKeyDown(Keys.D))
			//{
			//    selector = "C";
			//}
			//if (currKeyboardState.IsKeyDown(Keys.F))
			//{
			//    selector = "D";
			//}
			//if (currKeyboardState.IsKeyDown(Keys.Z))
			//{
			//    selector = "a";
			//}
			//if (currKeyboardState.IsKeyDown(Keys.X))
			//{
			//    selector = "b";
			//}
			//if (currKeyboardState.IsKeyDown(Keys.C))
			//{
			//    selector = "c";
			//}
			//if (currKeyboardState.IsKeyDown(Keys.V))
			//{
			//    selector = "d";
			//}
		}

	}
}
