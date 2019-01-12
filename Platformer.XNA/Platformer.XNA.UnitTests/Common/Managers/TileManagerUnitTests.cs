using System;
using WindowsGame.Common.Static;
using NUnit.Framework;
using WindowsGame.Common;
using WindowsGame.Common.Managers;

namespace WindowsGame.UnitTests.Common.Managers
{
	[TestFixture]
	public class CollisionManagerUnitTests : BaseUnitTests
	{
		[SetUp]
		public new void SetUp()
		{
			// System under test.
			TileManager = new TileManager();
			base.SetUp();
		}

		[Test]
		public void GetTileTypeTest()
		{
			TileType tileType = TileManager.GetTileType('#');
			Assert.That(TileType.Block, Is.EqualTo(tileType));
		}

	}
}
