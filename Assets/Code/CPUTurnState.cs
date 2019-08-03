using System.Linq;
using UnityEngine;

public class CPUTurnState : TurnState, IBattleState
{
	public CPUTurnState(BattleStateManager manager)
	{
		_manager = manager;
		_nextState = BattleStates.PlayerTurn;
	}

	public void EnterState()
	{
		_turn = new Turn(1);
		_manager.UIFacade.SetTimerVisibility(false);
		_manager.UIFacade.SetRoundVisibility(false);
	}

	public void Update()
	{
		_manager.UIFacade.UpdateTimer(_endOfRoundTimestamp - Time.time);
		_manager.UIFacade.UpdateRound(_turn.Round);

		// FIXME: Change initiator at random ?
		UnitFacade initiator = _manager.Units[2].Facade;
		UnitFacade target = null;
		var ability = Abilities.None;

		var index = new System.Random().Next(0, 1);
		ability = initiator.Unit.Abilities[index];

		var allies = _manager.Units.Where(unit => unit.Alliance == Alliances.Ally).ToList();
		var randomIndex = new System.Random().Next(0, allies.Count);
		var randomTarget = allies[0];
		target = randomTarget.Facade;

		Plan(initiator, target, ability);
	}

	public void ExitState() { }
}
