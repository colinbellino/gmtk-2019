using NUnit.Framework;
using OneSecond.Legacy.Unit;

namespace OneSecond.Legacy.Tests
{
	public class UnitTest
	{
		[Test]
		public void Constructor_InitializesName()
		{
			var actual = new Unit.Unit("Anri", Alliances.Ally);

			Assert.AreEqual(actual.Name, "Anri");
		}

		[Test]
		public void Constructor_InitializesAlliance()
		{
			var actual = new Unit.Unit("Anri", Alliances.Foe);

			Assert.AreEqual(actual.Alliance, Alliances.Foe);
		}

		[Test]
		public void Constructor_InitializesHealthTo1()
		{
			var actual = new Unit.Unit("Anri", Alliances.Ally);

			Assert.AreEqual(actual.Health.Current, 1);
		}

		[Test]
		public void SetHealth_SetsHealthTo3()
		{
			var actual = new Unit.Unit("Anri", Alliances.Ally);
			actual.SetHealth(3);

			Assert.AreEqual(actual.Health.Current, 3);
		}
	}
}
