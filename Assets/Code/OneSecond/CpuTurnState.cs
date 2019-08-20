using System.Collections;
using UnityEngine;

namespace OneSecond
{
	public class CpuTurnState : TurnState, IBattleState
	{
		public CpuTurnState(BattleStateManager manager)
		{
			Manager = manager;
			NextState = BattleStates.PlayerTurn;
		}

		public void EnterState()
		{
			Manager.AsyncGenerator.StartCoroutine(Sequence());
		}

		private IEnumerator Sequence()
		{
			var unit = Manager.Foes[Manager.GetNextFoeIndex()];
			Turn = new Turn(Manager);
			Manager.UiFacade.SetTimerAlliance(Alliances.Foe);

			EndOfRoundTimestamp = Time.time + RoundDuration;

			var initiator = Manager.Foes[Manager.CurrentFoeIndex];
			var randomTarget = Manager.Allies[Random.Range(0, Manager.Allies.Count)];
			var target = randomTarget;
			var ability = Random.Range(0, 1) == 0 ? Abilities.LightPunch : Abilities.StrongHeal;

			Plan(initiator, target, ability);
			Manager.UiFacade.UpdateCurrentUnitIndicator(unit);

			yield return new WaitForSeconds(RoundDuration / 2);

			Act();

			yield return new WaitForSeconds(RoundDuration / 2);

			Manager.CurrentFoeIndex = Manager.GetNextFoeIndex();
			EndRound();
		}

		public void Update()
		{
			Manager.UiFacade.UpdateTimer(EndOfRoundTimestamp - Time.time);
		}

		public void ExitState() { }
	}
}
