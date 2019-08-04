using System.Collections;
using System.Threading.Tasks;
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
		_manager.AsyncGenerator.StartCoroutine(Sequence());
	}

	public IEnumerator Sequence()
	{
		var unit = _manager.Foes[_manager.GetNextFoeIndex()];
		_turn = new Turn(_manager, unit);
		_manager.UIFacade.SetTimerAlliance(Alliances.Foe);

		_endOfRoundTimestamp = Time.time + _roundDuration;

		var initiator = _manager.Foes[_manager.CurrentFoeIndex];
		var randomTarget = _manager.Allies[Random.Range(0, _manager.Allies.Count)];
		var target = randomTarget;
		var ability = Random.Range(0, 1) == 0 ? Abilities.LightPunch : Abilities.StrongHeal;

		Plan(initiator, target, ability);
		_manager.UIFacade.UpdateCurrentUnitIndicator(Alliances.Foe, _manager.CurrentFoeIndex);

		yield return new WaitForSeconds(_roundDuration / 2);

		Act();

		yield return new WaitForSeconds(_roundDuration / 2);

		_manager.CurrentFoeIndex = _manager.GetNextFoeIndex();
		EndRound();
	}

	public void Update()
	{
		_manager.UIFacade.UpdateTimer(_endOfRoundTimestamp - Time.time);
	}

	public void ExitState() { }
}
