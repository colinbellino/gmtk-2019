using System;
using UnityEngine;

public class PlayerTurnState : TurnState, IBattleState
{
	public PlayerTurnState(BattleStateManager manager)
	{
		_manager = manager;
		_nextState = BattleStates.CPUTurn;
	}

	public void EnterState()
	{
		_turn = new Turn(1);

		_endOfRoundTimestamp = Time.time + _roundDuration;
	}

	public void Update()
	{
		_manager.UIFacade.UpdateTimer(_endOfRoundTimestamp - Time.time);

		if (Time.time >= _endOfRoundTimestamp)
		{
			EndRound();
		}

		if (_acted)
		{
			return;
		}

		UnitFacade initiator = null;
		UnitFacade target = null;
		var ability = Abilities.None;

		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
		{
			var unit = GetUnitUnderMouseCursor();
			if (unit && unit.Unit.Alliance == Alliances.Ally)
			{
				initiator = unit;
				var index = Input.GetMouseButtonDown(0) ? 0 : 1;
				ability = unit.Unit.Abilities[index];
			}
		}

		if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
		{
			var unit = GetUnitUnderMouseCursor();
			if (unit)
			{
				target = unit;
			}
		}

		Plan(initiator, target, ability);
	}

	public void ExitState() { }
}
