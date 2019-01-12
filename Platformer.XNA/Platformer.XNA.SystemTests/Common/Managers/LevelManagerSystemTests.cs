﻿using NUnit.Framework;
using WindowsGame.Common;
using WindowsGame.Common.Managers;

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
			LevelManager.Initialize(CONTENT_ROOT);
		}

		[Test]
		public void LoadContentTest()
		{
			LevelManager.LoadLevel(0);

			Assert.AreEqual(16, LevelManager.Width);
			Assert.AreEqual(12, LevelManager.Height);
		}

	}
}
