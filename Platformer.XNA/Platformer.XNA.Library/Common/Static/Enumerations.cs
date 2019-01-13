namespace WindowsGame.Common.Static
{
	public enum GameType
	{
		Incog = 0,
		Small = 1, 
		Large = 2,
	}

	public enum ScreenType
	{
		Splash,
		Init,
		Load,
		Play,
	}

	public enum AnimationType
	{
		PlayerIdle,
		PlayerMove,
		PlayerJump,
		PlayerExit,
		PlayerDead,
		EnemyAIdle,
		EnemyAMove,
		EnemyBIdle,
		EnemyBMove,
		EnemyCIdle,
		EnemyCMove,
		EnemyDIdle,
		EnemyDMove,
	}

	public enum TileType
	{
		Unknown = -1,
		Blank = 0,
		Block = 1,
		Platform = 2,
		Player = 3,
		EnemyA,
		EnemyB,
		EnemyC,
		EnemyD,
		Exit,
		Gem,
	}

	public enum BlockType
	{
		None = -1,
		Blank = 0,
		BlockA0 = 1,
		BlockA1 = 2,
		BlockA2 = 3,
		BlockA3 = 4,
		BlockA4 = 5,
		BlockA5 = 6,
		BlockA6 = 7,
		BlockB0 = 8,
		BlockB1 = 9,
		Platform = 10,
		Exit = 11,
		Gem = 12
	}

	public enum CollisionType
	{
		Passable = 0,
		Impassable = 1,
		Platform = 2,
	}

	public enum SoundEffectType
	{
	}

}