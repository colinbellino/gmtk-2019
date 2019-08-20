using UnityEngine;

namespace OneSecond
{
	public class PlayerTurnState : TurnState, IBattleState
	{
		public PlayerTurnState(BattleStateManager manager)
		{
			Manager = manager;
			NextState = BattleStates.CpuTurn;
		}

		public void EnterState()
		{
			var unit = Manager.Allies[Manager.GetNextAllyIndex()];
			Turn = new Turn(Manager);
			Manager.UiFacade.SetTimerAlliance(Alliances.Ally);
			Manager.UiFacade.UpdateCurrentUnitIndicator(unit);

			EndOfRoundTimestamp = Time.time + RoundDuration;
		}

		public void Update()
		{
			Manager.UiFacade.UpdateTimer(EndOfRoundTimestamp - Time.time);

			if (Time.time >= EndOfRoundTimestamp)
			{
				Manager.CurrentAllyIndex = Manager.GetNextAllyIndex();
				EndRound();
			}

			if (Acted)
			{
				return;
			}

			if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
			{
				var unit = GetUnitUnderMouseCursor();
				if (unit)
				{
					var target = unit;
					var initiator = Manager.Allies[Manager.CurrentAllyIndex];
					var ability = Input.GetMouseButtonUp(0) ? Abilities.WenkPunch : Abilities.StrongHeal;
					Plan(initiator, target, ability);
					Act();
				}
			}
		}

		public void ExitState() { }
	}
}
