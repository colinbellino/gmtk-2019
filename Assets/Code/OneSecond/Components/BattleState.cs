using UnityEngine;
using Zenject;

namespace OneSecond.Components
{
	public class BattleState : MonoBehaviour, IBattleState
	{
		[Inject] private IBattleState _state;

		public void EnterState() => _state.EnterState();
		public void Update() => _state.Update();
		public void ExitState() => _state.ExitState();
	}
}
