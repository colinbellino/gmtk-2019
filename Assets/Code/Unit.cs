using UnityEngine;

public class Unit
{
	public string Name;
	public Abilities[] Abilities;
	public Alliances Alliance;
	public UnitFacade Facade;
	public Stat Health;

	public Unit(string name, Abilities[] abilities, Alliances alliance, int health)
	{
		Name = name;
		Abilities = abilities;
		Alliance = alliance;
		Health = new Stat(health);
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
