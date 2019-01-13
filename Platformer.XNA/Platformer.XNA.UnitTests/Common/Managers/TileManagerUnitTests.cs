using NUnit.Framework;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;

namespace WindowsGame.UnitTests.Common.Managers
{
	[TestFixture]
	public class TileManagerUnitTests : BaseUnitTests
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
