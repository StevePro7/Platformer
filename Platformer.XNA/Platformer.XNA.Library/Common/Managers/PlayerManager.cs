using System;
using WindowsGame.Common.Objects;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IPlayerManager
	{
		void Initialize();
		void LoadContent(UInt16 playerSpot);
		void LoadContent(UInt16 playerSpot, Byte gameWidth, Byte gameHeight, Byte theGameOffset, Byte theTileSize);

		void DrawPlayer();

		Player Player { get; }
	}

	public class PlayerManager : IPlayerManager
	{
		private Byte gameOffset;
		private Byte tileSize;

		public void Initialize()
		{
			Player = new Player();

			gameOffset = MyGame.Manager.StateManager.GameOffset;
			tileSize = MyGame.Manager.StateManager.TileSize;
		}

		public void LoadContent(UInt16 playerSpot)
		{
			Byte gameWidth = MyGame.Manager.LevelManager.GameWidth;
			Byte gameHeight = MyGame.Manager.LevelManager.GameHeight;
			
			LoadContent(playerSpot, gameWidth, gameHeight, gameOffset, tileSize);
		}

		public void LoadContent(UInt16 playerSpot, Byte gameWidth, Byte gameHeight, Byte theGameOffset, Byte theTileSize)
		{
			Byte y = (Byte) (playerSpot / gameHeight);
			Byte x = (Byte) (playerSpot % gameWidth);
			Vector2 position = new Vector2(x * theTileSize + theGameOffset, (y - 1) * tileSize);
			Player.LoadContent(x, y, position);
		}

		public void DrawPlayer()
		{
			Player.Draw();
		}

		public Player Player { get; private set; }
	}
}
