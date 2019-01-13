using WindowsGame.Master.Managers;
using NUnit.Framework;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;
using Rhino.Mocks;

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
		public void GetBlockTypeTest()
		{
			RandomManager.Stub(rm => rm.Next(9)).Return(4);
			BlockType blockType = TileManager.GetBlockType(TileType.Block);
			Assert.That(BlockType.BlockA4, Is.EqualTo(blockType));
		}

		[Test]
		public void GetTileTypeTest()
		{
			TileType tileType = TileManager.GetTileType('#');
			Assert.That(TileType.Block, Is.EqualTo(tileType));
		}

	}
}
