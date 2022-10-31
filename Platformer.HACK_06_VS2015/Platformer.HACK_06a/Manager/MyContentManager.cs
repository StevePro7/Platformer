using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer
{
	public class MyContentManager
	{
		public void Initialize(ContentManager content)
		{
			content.RootDirectory = Constants.CONTENT_DIRECTORY;
		}

		public void LoadContent(ContentManager content)
		{
			Black2442 = content.Load<Texture2D>("black_24x32");
			Tile88 = content.Load<Texture2D>("tile_8x8");
			Tile1616 = content.Load<Texture2D>("tile_16x16");
			Tile248 = content.Load<Texture2D>("tile_24x8");
			Tile2432 = content.Load<Texture2D>("tile_24x32");

			Skater = new Texture2D[3];
			Skater[0] = content.Load<Texture2D>("skate01");
			Skater[1] = content.Load<Texture2D>("skate02");
			Skater[2] = content.Load<Texture2D>("skate03");
		}

		public void UnLoadContent(ContentManager content)
		{
			content.Unload();
		}

		public Texture2D Black2442 { get; private set; }
		public Texture2D Tile88 { get; private set; }
		public Texture2D Tile1616 { get; private set; }
		public Texture2D Tile248{ get; private set; }
		public Texture2D Tile2432 { get; private set; }
		public Texture2D[] Skater { get; private set; }
	}
}