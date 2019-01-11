using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Data
{
	public struct GlobalConfigData
	{
		public ScreenType ScreenType;
		public Byte LevelNo;
		public Byte FramesPerSecond;
		public Boolean LoadAudio;
		public Boolean PlayAudio;
		public Boolean QuitsToExit;
	}
}
