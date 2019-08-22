using OneSecond.Components;
using OneSecond.Unit;
using UnityEngine;
using Zenject;

namespace OneSecond
{
	public interface IUnitBrain
	{
		BattleAction Plan();
		bool IsTryingToAct();
	}

	public class PlayerBrain : IUnitBrain
	{
		/* [Inject] */
		private UnitFacade _owner;
		[Inject] private Camera _camera;

		public BattleAction Plan()
		{
			var target = GetUnitUnderMouseCursor();
			var ability = Input.GetMouseButtonUp(0) ? Abilities.WenkPunch : Abilities.StrongHeal;
			return new BattleAction(_owner, target, ability);
		}

		public bool IsTryingToAct()
		{
			// TODO: Use interface for Input
			return Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1);
		}

		private UnitFacade GetUnitUnderMouseCursor()
		{
			var ray = _camera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
				return hit.collider.GetComponent<UnitFacade>();
			}

			return null;
		}
	}
}
