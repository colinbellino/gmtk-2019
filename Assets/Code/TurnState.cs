using UnityEngine;

public abstract class TurnState
{
	protected BattleStateManager _manager;
	protected float _roundDuration = 1f;
	protected Turn _turn;
	protected float _endOfRoundTimestamp;
	protected BattleStates _nextState;
	protected bool _acted;

	protected void Plan(UnitFacade initiator, UnitFacade target, Abilities ability)
	{
		_turn.Action = new BattleAction
		{
			Initiator = initiator,
			Target = target,
			Ability = ability
		};
	}

	protected void Act()
	{
		_turn.Act();
		_acted = true;
	}

	protected void EndRound()
	{
		_turn.EndRound();
		_acted = false;
		_endOfRoundTimestamp = Time.time + _roundDuration;

		_manager.ChangeState(_nextState);
	}

	protected UnitFacade GetUnitUnderMouseCursor()
	{
		var ray = _manager.Camera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
			return hit.collider.GetComponent<UnitFacade>();
		}

		return null;
	}
}
