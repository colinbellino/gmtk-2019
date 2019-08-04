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
		_turn = new Turn();
		_manager.UIFacade.SetTimerAlliance(Alliances.Ally);

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

		if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
		{
			var unit = GetUnitUnderMouseCursor();
			if (unit)
			{
				var target = unit;
				var ability = Input.GetMouseButtonUp(0) ? Abilities.LightPunch : Abilities.StrongHeal;
				Plan(target, ability);
			}
		}
	}

	public void ExitState() { }
}
