using UnityEngine;

public class CPUTurnState : IBattleState
{
	private BattleStateManager _manager;
	private float _turnDuration = 1f;

	private float _endOfTurnTimestamp;

	public CPUTurnState(BattleStateManager manager)
	{
		_manager = manager;
	}

	public void EnterState()
	{
		_manager.UIFacade.SetTimerVisibility(false);
		_manager.UIFacade.SetRoundVisibility(false);

		_endOfTurnTimestamp = Time.time + _turnDuration;
	}

	public void Update()
	{
		if (Time.time >= _endOfTurnTimestamp)
		{
			_manager.ChangeState(BattleStates.PlayerTurn);
		}
	}

	public void ExitState() { }
}
