using UnityEngine;

public class Unit
{
	public string Name;
	public string[] Abilities;
	public Alliances Alliance;
	public UnitFacade Facade;

	public Unit(string name, string[] abilities, Alliances alliance)
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
