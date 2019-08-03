using UnityEngine;

public class Turn
{
	public Stat Round;
	public BattleAction Action => _action;

	private BattleAction _action;

	public Turn(int round)
	{
		Round = new Stat(round);
	}

	public void EndRound()
	{
		Round.Current = Round.Current - 1;
		_action = null;
	}

	public bool IsOver => Round.Current == 0;

	public void SelectAbility(UnitFacade initiator, Abilities ability)
	{
		_action = new BattleAction { Initiator = initiator, Ability = ability };
	}

	public void SelectTarget(UnitFacade target)
	{
		if (_action == null) { return; }

		_action.Target = target;
	}

	public void Act()
	{
		switch (_action.Ability)
		{
			case Abilities.LightPunch:
				_action.Target.Damage(1);
				break;
			case Abilities.StrongPunch:
				_action.Target.Damage(2);
				break;
			case Abilities.LightHeal:
				_action.Target.Heal(1);
				break;
			case Abilities.StrongHeal:
				_action.Target.Heal(2);
				break;
		}

		var color = _action.Initiator.Unit.Alliance == Alliances.Ally ? "blue" : "red";
		Debug.Log($"<color={color}>{_action.Initiator.Unit.Name} ---({_action.Ability})---> {_action.Target.Unit.Name}</color>");
	}

	public bool IsValidAction()
	{
		if (_action == null || _action.Initiator == null || _action.Target == null)
		{
			return false;
		}

		return true;
	}

}
