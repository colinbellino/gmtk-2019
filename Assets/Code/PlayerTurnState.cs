using System;
using UnityEngine;

public class PlayerTurnState : IBattleState
{
	private BattleStateManager _manager;
	private float _roundDuration = 1f;
	private Turn _turn;

	private float _endOfRoundTimestamp;

	public PlayerTurnState(BattleStateManager manager)
	{
		_manager = manager;
	}

	public void EnterState()
	{
		_turn = new Turn(3);
		_manager.UIFacade.SetTimerVisibility(true);
		_manager.UIFacade.SetRoundVisibility(true);

		_endOfRoundTimestamp = Time.time + _roundDuration;
	}

	public void Update()
	{
		if (Time.time >= _endOfRoundTimestamp)
		{
			EndRound();
		}

		var activeUnit = GetUnitUnderMouseCursor();
		if (activeUnit)
		{
			if (Input.GetMouseButtonDown(0))
			{
				_turn.Act(activeUnit.Unit, 0);
			}
			else if (Input.GetMouseButtonDown(1))
			{
				_turn.Act(activeUnit.Unit, 1);
			}
			if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
			{
				_turn.Target(activeUnit.Unit);

				var action = _turn.GetValidAction();
				if (action != null)
				{
					Debug.Log($"Player acted: {action.Initiator.Name} ---({action.Ability})---> {action.Target.Name}");
					EndRound();
				}
			}
		}

		_manager.UIFacade.UpdateTimer(_endOfRoundTimestamp - Time.time);
		_manager.UIFacade.UpdateRound(_turn.Round);
	}

	private void EndRound()
	{
		_turn.EndRound();
		_endOfRoundTimestamp = Time.time + _roundDuration;

		if (_turn.IsOver)
		{
			_manager.ChangeState(BattleStates.CPUTurn);
		}
	}

	private UnitFacade GetUnitUnderMouseCursor()
	{
		RaycastHit hit;
		var ray = _manager.Camera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit))
		{
			Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
			return hit.collider.GetComponent<UnitFacade>();
		}

		return null;
	}

	public void ExitState() { }
}
