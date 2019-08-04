using System;
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

	public Unit(string name, Alliances alliance)
	{
		Name = name;
		Alliance = alliance;
		_health = new Stat();
	}

	public void SetHealth(int baseHealth)
	{
		_health = new Stat(baseHealth);
	}

	internal void SetHealth(object health)
	{
		throw new NotImplementedException();
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
