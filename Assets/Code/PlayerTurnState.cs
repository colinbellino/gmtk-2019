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
		var unit = _manager.Allies[_manager.GetNextAllyIndex()];
		_turn = new Turn(_manager, unit);
		_manager.UIFacade.SetTimerAlliance(Alliances.Ally);
		_manager.UIFacade.UpdateCurrentUnitIndicator(unit);

		_endOfRoundTimestamp = Time.time + _roundDuration;
	}

	public void Update()
	{
		_manager.UIFacade.UpdateTimer(_endOfRoundTimestamp - Time.time);

		if (Time.time >= _endOfRoundTimestamp)
		{
			_manager.CurrentAllyIndex = _manager.GetNextAllyIndex();
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
				var initiator = _manager.Allies[_manager.CurrentAllyIndex];
				var ability = Input.GetMouseButtonUp(0) ? Abilities.WenkPunch : Abilities.StrongHeal;
				Plan(initiator, target, ability);
				Act();
			}
		}
	}

	public void ExitState() { }
}
