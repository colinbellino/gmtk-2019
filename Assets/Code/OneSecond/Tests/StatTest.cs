using NUnit.Framework;
using OneSecond.Unit;

namespace OneSecond.Tests
{
	public class StatTest
	{
		[Test]
		public void Constructor_InitializesCurrent()
		{
			var max = 3;
			var actual = new Stat(max);

			Assert.AreEqual(actual.Current, max);
		}

		[Test]
		public void Constructor_InitializesMax()
		{
			var max = 3;
			var actual = new Stat(max);

			Assert.AreEqual(actual.Max, max);
		}

		[Test]
		public void CurrentSetter_ClampValueBetween0AndMax()
		{
			var max = 3;
			var actual = new Stat(max);

			actual.Current = -1;
			Assert.AreEqual(actual.Current, 0);

			actual.Current = 99;
			Assert.AreEqual(actual.Current, max);
		}
	}
}
