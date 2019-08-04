using UnityEngine;

public class Turn
{
	public BattleAction Action;
	private Alliances _alliance;

	public Turn(Alliances alliance)
	{
		Action = new BattleAction();
		_alliance = alliance;
	}

	public void EndRound()
	{
		Action = null;
	}

	public void Act()
	{
		switch (Action.Ability)
		{
			case Abilities.LightPunch:
				Action.Target.Damage(1);
				break;
			case Abilities.StrongPunch:
				Action.Target.Damage(2);
				break;
			case Abilities.LightHeal:
				Action.Target.Heal(1);
				break;
			case Abilities.StrongHeal:
				Action.Target.Heal(2);
				break;
		}

		var color = _alliance == Alliances.Ally ? "blue" : "red";
		Debug.Log($"<color={color}>---({Action.Ability})---> {Action.Target.Data.Name}</color>");
	}

	public bool IsValidAction()
	{
		if (Action == null || Action.Target == null)
		{
			return false;
		}

		return true;
	}

}
