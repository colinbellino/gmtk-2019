using System;

namespace OneSecond.Unit
{
	public class Stat
	{
		public Stat(int max)
		{
			Current = Max = max;
		}

		private int _current;
		public int Current
		{
			get => _current;
			set => _current = Math.Min(Math.Max(value, 0), Max);
		}

		public readonly int Max;
	}
}
