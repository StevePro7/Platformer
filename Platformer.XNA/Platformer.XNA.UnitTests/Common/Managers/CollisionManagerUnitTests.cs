using NUnit.Framework;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;

namespace WindowsGame.UnitTests.Common.Managers
{
	[TestFixture]
	public class CollisionManagerUnitTests : BaseUnitTests
	{
		[SetUp]
		public new void SetUp()
		{
			// System under test.
			CollisionManager = new CollisionManager();
			base.SetUp();
		}

		[Test]
		public void GetCollisionTypeTest()
		{
			CollisionType collisionType = CollisionManager.GetCollisionType('#');
			Assert.That(CollisionType.Impassable, Is.EqualTo(collisionType));
		}

	}
}
