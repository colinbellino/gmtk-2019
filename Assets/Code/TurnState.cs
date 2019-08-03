using System;
using UnityEngine;

public abstract class TurnState
{
	protected BattleStateManager _manager;
	protected float _roundDuration = 1f;
	protected Turn _turn;
	protected float _endOfRoundTimestamp;
	protected BattleStates _nextState;
	protected bool _acted;

	protected void Plan(UnitFacade iniator, UnitFacade target, Abilities ability)
	{
		if (iniator && ability != Abilities.None)
		{
			_turn.SelectAbility(iniator, ability);
		}

		if (target)
		{
			_turn.SelectTarget(target);
		}

		// Act
		if (_turn.IsValidAction())
		{
			_turn.Act();
			_acted = true;
		}
	}

	protected void EndRound()
	{
		_turn.EndRound();
		_acted = false;
		_endOfRoundTimestamp = Time.time + _roundDuration;

		if (_turn.IsOver)
		{
			_manager.ChangeState(_nextState);
		}
	}

	protected UnitFacade GetUnitUnderMouseCursor()
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
}
