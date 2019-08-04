using System.Threading.Tasks;
using UnityEngine;

public class CPUTurnState : TurnState, IBattleState
{
	public CPUTurnState(BattleStateManager manager)
	{
		_manager = manager;
		_nextState = BattleStates.PlayerTurn;
	}

	public async void EnterState()
	{
		_turn = new Turn(_manager, Alliances.Foe);
		_manager.UIFacade.SetTimerAlliance(Alliances.Foe);

		_endOfRoundTimestamp = Time.time + _roundDuration;

		var allies = _manager.Allies;
		var foes = _manager.Foes;

		var randomTarget = allies[Random.Range(0, allies.Count)];
		var target = randomTarget;
		var ability = Random.Range(0, 1) == 0 ? Abilities.LightPunch : Abilities.StrongHeal;

		Plan(target, ability);
		Act();

		await Task.Delay((int) _roundDuration * 1000);

		EndRound();
	}

	public void Update()
	{
		_manager.UIFacade.UpdateTimer(_endOfRoundTimestamp - Time.time);
	}

	public void ExitState() { }
}
