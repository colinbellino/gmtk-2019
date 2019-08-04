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
		_turn = new Turn();
		_manager.UIFacade.SetTimerAlliance(Alliances.Foe);

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

		var allies = _manager.Units.Where(unit => unit.Alliance == Alliances.Ally).ToList();
		var foes = _manager.Units.Where(unit => unit.Alliance == Alliances.Foe).ToList();

		var randomTarget = allies[Random.Range(0, allies.Count)];
		var target = randomTarget.Facade;
		var ability = Random.Range(0, 1) == 0 ? Abilities.LightPunch : Abilities.StrongHeal;

		Plan(target, ability);
	}

	public void ExitState() { }
}
