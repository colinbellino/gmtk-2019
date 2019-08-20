namespace OneSecond
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
			Health = new Stat();
		}

		public void SetHealth(int baseHealth)
		{
			Health = new Stat(baseHealth);
		}
	}

	public enum Alliances
	{
		Ally,
		Foe
	}

	public enum Abilities
	{
		None,
		LightPunch,
		WenkPunch,
		LightHeal,
		StrongHeal
	}
}
