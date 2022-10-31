using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer
{
	public class MyPlayerManager
	{
		MyContentManager myContentManager;

		public MyPlayerManager(MyContentManager myContentManager)
		{
			this.myContentManager = myContentManager;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			//spriteBatch.Draw(myContentManager.Black2442, new Vector2(64, 128), Color.White);
			spriteBatch.Draw(myContentManager.Tile2432, new Vector2(64, 128), Color.White);
			//spriteBatch.Draw(myContentManager.Skater[2], new Vector2(72, 128), Color.White);
		}
	}

}