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

	public void SelectAbility(Unit initiator, int abilityIndex)
	{
		_action = new BattleAction { Initiator = initiator.Facade, Ability = initiator.Abilities[abilityIndex] };
	}

	public void SelectTarget(Unit target)
	{
		if (_action == null) { return; }

		_action.Target = target.Facade;
	}

	public void Act()
	{
		switch (_action.Ability)
		{
			case Abilities.LightPunch:
				_action.Target.CreateMessage("10", Color.white);
				break;
			case Abilities.StrongPunch:
				_action.Target.CreateMessage("20", Color.white);
				break;
			case Abilities.LightHeal:
				_action.Target.CreateMessage("10", Color.green);
				break;
			case Abilities.StrongHeal:
				_action.Target.CreateMessage("20", Color.green);
				break;
		}
	}

	public BattleAction GetValidAction()
	{
		if (_action == null || _action.Initiator == null || _action.Initiator == null)
		{
			// Debug.Log("Invalid action." + _action);
			return null;
		}

		return _action;
	}

}
