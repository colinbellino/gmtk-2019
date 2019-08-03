using UnityEngine;

public class Turn
{
	public BattleAction Action;

	public Turn()
	{
		Action = new BattleAction();
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

		var color = Action.Target.Unit.Alliance == Alliances.Foe ? "blue" : "red";
		Debug.Log($"<color={color}>---({Action.Ability})---> {Action.Target.Unit.Name}</color>");
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
