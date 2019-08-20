public class Unit
{
	public string Name;
	public Alliances Alliance;
	public UnitFacade Facade;
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
