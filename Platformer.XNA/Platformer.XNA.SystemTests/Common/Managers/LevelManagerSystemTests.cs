using NUnit.Framework;
using WindowsGame.Common;
using WindowsGame.Common.Static;

namespace WindowsGame.SystemTests.Common.Managers
{
	[TestFixture]
	public class LevelManagerSystemTests : BaseSystemTests
	{
		[SetUp]
		public void SetUp()
		{
			// System under test.
			LevelManager = MyGame.Manager.LevelManager;
			LevelManager.Initialize(GameType.Large, CONTENT_ROOT);
			MyGame.Manager.RandomManager.Initialize();
		}

		[Test]
		public void LoadContentTest()
		{
			LevelManager.LoadLevel(0);

			Assert.AreEqual(16, LevelManager.GameWidth);
			Assert.AreEqual(12, LevelManager.GameHeight);
		}

	}
}
