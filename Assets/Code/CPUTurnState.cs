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
	}

	public void Update()
	{
		_manager.UIFacade.UpdateTimer(_endOfRoundTimestamp - Time.time);

		if (Time.time >= _endOfRoundTimestamp)
		{
			EndRound();
		}

		var allies = _manager.Units.Where(unit => unit.Alliance == Alliances.Ally).ToList();
		var foes = _manager.Units.Where(unit => unit.Alliance == Alliances.Foe).ToList();

		var initiator = foes[Random.Range(0, foes.Count)].Facade;
		var ability = initiator.Unit.Abilities[Random.Range(0, 1)];

		var randomTarget = allies[Random.Range(0, allies.Count)];
		var target = randomTarget.Facade;

		Plan(initiator, target, ability);
	}

	public void ExitState() { }
}
