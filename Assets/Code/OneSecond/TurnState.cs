using OneSecond.Unit;
using UnityEngine;
using Zenject;

namespace OneSecond
{
	public interface ITurnState
	{
		void Plan();
		void Act();
	}

	public class TurnState : IBattleState, ITurnState
	{
		[Inject] private BattleStateManager Manager;
		[Inject] private BattleStates _nextState;
		[Inject] private IUnitBrain _brain;

		protected const float RoundDuration = 1f;
		protected Turn Turn;
		protected float EndOfRoundTimestamp;
		protected bool Acted;

		public void EnterState()
		{
			Turn = new Turn(Manager);
			var unit = Manager.Allies[Manager.GetNextAllyIndex()];
			Turn = new Turn(Manager);
			Manager.UiFacade.SetTimerAlliance(Alliances.Ally);
			Manager.UiFacade.UpdateCurrentUnitIndicator(unit);

			EndOfRoundTimestamp = Time.time + RoundDuration;
		}

		public void Update()
		{
			Manager.UiFacade.UpdateTimer(EndOfRoundTimestamp - Time.time);

			if (!Turn.DidAct)
			{
				if (Time.time >= EndOfRoundTimestamp)
				{
					EndRound();
				}

				if (_brain.IsTryingToAct())
				{
					Plan();
					Act();
				}
			}
		}

		public void ExitState() { }

		public void Plan()
		{
			Turn.Action = _brain.Plan();
		}

		public void Act()
		{
			Turn.Act();
		}

		private void EndRound()
		{
			Manager.CurrentAllyIndex = Manager.GetNextAllyIndex();
			Turn.EndRound();

			EndOfRoundTimestamp = Time.time + RoundDuration;

			Manager.ChangeState(_nextState);
		}
	}
}
