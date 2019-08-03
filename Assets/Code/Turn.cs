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

	public void Act(Unit initiator, int abilityIndex)
	{
		_action = new BattleAction { Initiator = initiator, Ability = initiator.Abilities[abilityIndex] };
	}

	public void Target(Unit target)
	{
		if (_action == null) { return; }

		_action.Target = target;
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
