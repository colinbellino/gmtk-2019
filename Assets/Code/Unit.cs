using UnityEngine;

public class Unit
{
	public string Name;
	public Abilities[] Abilities;
	public Alliances Alliance;
	public UnitFacade Facade;

	public Unit(string name, Abilities[] abilities, Alliances alliance)
	{
		Name = name;
		Abilities = abilities;
		Alliance = alliance;
	}
}

public enum Alliances
{
	Ally,
	Foe
}

public enum Abilities
{
	LightPunch,
	StrongPunch,
	LightHeal,
	StrongHeal
}
