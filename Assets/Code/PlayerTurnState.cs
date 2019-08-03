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

		_manager.UIFacade.UpdateTimer(_endOfRoundTimestamp - Time.time);
		_manager.UIFacade.UpdateRound(_turn.Round);

		Plan(initiator, target, ability);
	}

	public void ExitState() { }
}

public abstract class TurnState
{
	protected BattleStateManager _manager;
	protected float _roundDuration = 1f;
	protected Turn _turn;
	protected float _endOfRoundTimestamp;
	protected BattleStates _nextState;

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
			Debug.Log($"{_turn.Action.Initiator.Unit.Name} ---({_turn.Action.Ability})---> {_turn.Action.Target.Unit.Name}");
			_turn.Act();
			EndRound();
		}
	}

	protected void EndRound()
	{
		_turn.EndRound();
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
