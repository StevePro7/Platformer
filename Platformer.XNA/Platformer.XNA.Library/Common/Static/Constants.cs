using System;

namespace WindowsGame.Common.Static
{
	public static class Constants
	{
		public const String CONTENT_DIRECTORY = "Content";

		public const String DATA_DIRECTORY = "Data";

		// Global data.
		public const Boolean IsFixedTimeStep = true;

		public const Boolean IsFullScreen = false;
		public const Boolean IsMouseVisible = true;
		public const UInt16 ScreenWide = 256;
		public const UInt16 ScreenHigh = 192;

		public const Byte NUM_TILES = 14;
		public const Byte GAME_WIDE = 0;	//TODO revert back to 8
		public const Byte TILE_WIDE = 16;
	}
}