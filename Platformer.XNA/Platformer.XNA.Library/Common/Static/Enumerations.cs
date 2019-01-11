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
		Title,
		Diff,
		Long,
		Ready,
		Play,
		Quiz,
		Score,
		Over,
		Exit,
		Test,
	}

	public enum AnimationType
	{
		PlayerIdle,
		PlayerRun,
		PlayerJump,
		PlayerCelebrate,
		PlayerDie,
		MonsterAIdle,
		MonsterARun,
		MonsterADie,
		MonsterBIdle,
		MonsterBRun,
		MonsterBDie,
		MonsterCIdle,
		MonsterCRun,
		MonsterCDie,
		MonsterDIdle,
		MonsterDRun,
		MonsterDDie,
	}

	public enum SoundEffectType
	{
		ExitReached,
		GemCollected,
		MonsterKilled,
		PlayerFall,
		PlayerJump,
		PlayerKilled,
		Powerup,
	}

}