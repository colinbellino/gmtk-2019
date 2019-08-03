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
			var unitFacade = GetUnitUnderMouseCursor();
			if (unitFacade)
			{
				if (Input.GetMouseButtonDown(0))
				{
					_turn.Act(unitFacade.Unit, 0);
				}
				else if (Input.GetMouseButtonDown(1))
				{
					_turn.Act(unitFacade.Unit, 1);
				}

				if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
				{
					_turn.Target(unitFacade.Unit);

					var action = _turn.GetValidAction();
					if (action != null)
					{
						_turn.EndRound();
						_endOfRoundTimestamp = Time.time + _roundDuration;
						Debug.Log($"Player acted: {action.Initiator.Name} ---({action.Ability})---> {action.Target.Name}");
					}
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
