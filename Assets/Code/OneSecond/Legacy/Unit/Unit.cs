namespace OneSecond.Legacy.Unit
{
	public class Unit
	{
		public readonly string Name;
		public readonly Alliances Alliance;

		public Stat Health { get; private set; }

		public Unit(string name, Alliances alliance)
		{
			Name = name;
			Alliance = alliance;
			SetHealth(1);
		}

		public void SetHealth(int baseHealth)
		{
			Health = new Stat(baseHealth);
		}
	}
}
