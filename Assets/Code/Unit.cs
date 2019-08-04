using UnityEngine;

public class Unit
{
	public string Name;
	public Alliances Alliance;
	public UnitFacade Facade;
	public Stat Health
	{
		get => _health;
		private set => _health = value;
	}

	private Stat _health;

	public Unit(string name, Alliances alliance, int baseHealth)
	{
		Name = name;
		Alliance = alliance;
		_health = new Stat(baseHealth);
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
	StrongPunch,
	LightHeal,
	StrongHeal
}
