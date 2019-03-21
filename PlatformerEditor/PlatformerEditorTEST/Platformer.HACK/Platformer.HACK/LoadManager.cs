using System;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
	public class LoadManager
	{
		public void Load(Microsoft.Xna.Framework.Content.ContentManager content)
		{
			Assets.ExitTexture = content.Load<Texture2D>("Tiles/Exit");
			Assets.BlockTexture = content.Load<Texture2D>("Tiles/BlockA0");
			Assets.PlatformTexture = content.Load<Texture2D>("Tiles/Platform");
			Assets.GemTexture = content.Load<Texture2D>("Sprites/Gem");
			Assets.PlayerTexture = content.Load<Texture2D>("Sprites/Player/Idle");
			Assets.EnemyATexture = content.Load<Texture2D>("Sprites/MonsterA/Idle");
			Assets.EnemyBTexture = content.Load<Texture2D>("Sprites/MonsterB/Idle");
			Assets.EnemyCTexture = content.Load<Texture2D>("Sprites/MonsterC/Idle");
			Assets.EnemyDTexture = content.Load<Texture2D>("Sprites/MonsterD/Idle");
			Assets.HorzTexture = content.Load<Texture2D>("Tiles/StripHorz");
			Assets.VertTexture = content.Load<Texture2D>("Tiles/StripVert");
		}

		public void Draw()
		{

		}

	}
}
