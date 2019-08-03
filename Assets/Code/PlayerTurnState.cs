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
		// Debug.Log("Starting player turn.");

		_endOfRoundTimestamp = Time.time + _roundDuration;
		_turn = new Turn(3);
	}

	public void Update()
	{
		if (Time.time >= _endOfRoundTimestamp)
		{
			_turn.EndRound();
			_endOfRoundTimestamp = Time.time + _roundDuration;
			// Debug.Log($"Round timed out, {_turn.Round.Current} actions remaining.");
		}
		else
		{
			// TODO: Select unit
			// TODO: Select target

			if (Input.GetMouseButtonDown(0))
			{
				_turn.Act(_manager.Units[0], 0);
			}
			else if (Input.GetMouseButtonDown(1))
			{
				_turn.Act(_manager.Units[0], 1);
			}

			if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
			{
				_turn.Target(_manager.Units[1]);

				var action = _turn.GetValidAction();
				if (action != null)
				{
					_turn.EndRound();
					_endOfRoundTimestamp = Time.time + _roundDuration;
					Debug.Log($"Player acted: {action.Initiator} -({action.Ability})-> {action.Target}");
				}
			}
		}

		if (_turn.IsOver)
		{
			_manager.ChangeState(BattleStates.CPUTurn);
		}

		_manager.UIFacade.UpdateTimer(_endOfRoundTimestamp - Time.time);
		_manager.UIFacade.UpdateRound(_turn.Round);
	}

	public void ExitState() { }
}
