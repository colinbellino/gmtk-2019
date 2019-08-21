using OneSecond.Components;
using OneSecond.Unit;
using UnityEngine;

namespace OneSecond
{
	public abstract class TurnState
	{
		protected BattleStateManager Manager;
		protected const float RoundDuration = 1f;
		protected Turn Turn;
		protected float EndOfRoundTimestamp;
		protected BattleStates NextState;
		protected bool Acted;

		protected void Plan(UnitFacade initiator, UnitFacade target, Abilities ability)
		{
			Turn.Action = new BattleAction
			{
				Initiator = initiator,
				Target = target,
				Ability = ability
			};
		}

		protected void Act()
		{
			Turn.Act();
			Acted = true;
		}

		protected void EndRound()
		{
			Turn.EndRound();
			Acted = false;
			EndOfRoundTimestamp = Time.time + RoundDuration;

			Manager.ChangeState(NextState);
		}

		protected UnitFacade GetUnitUnderMouseCursor()
		{
			var ray = Manager.Camera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
				return hit.collider.GetComponent<UnitFacade>();
			}

			return null;
		}
	}
}
