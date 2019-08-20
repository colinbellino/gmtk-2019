using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneSecond
{
	public class BattleStateManager
	{
		private readonly Dictionary<BattleStates, IBattleState> _states;

		private BattleStates _currentState;
		private IBattleState _currentStateHandler;
		private readonly AudioSource _audioSource;

		public readonly MonoBehaviour AsyncGenerator;
		public Camera Camera;
		public readonly UiFacade UiFacade;
		public int CurrentAllyIndex;
		public int CurrentFoeIndex;
		public List<UnitFacade> Allies = new List<UnitFacade>();
		public List<UnitFacade> Foes = new List<UnitFacade>();

		public BattleStateManager(MonoBehaviour asyncGenerator, UiFacade uiFacade, AudioSource audioSource)
		{
			UiFacade = uiFacade;
			AsyncGenerator = asyncGenerator;
			_audioSource = audioSource;

			_states = new Dictionary<BattleStates, IBattleState>
			{
				{ BattleStates.Init, new InitBattleState(this) },
				{ BattleStates.PlayerTurn, new PlayerTurnState(this) },
				{ BattleStates.CpuTurn, new CpuTurnState(this) }
			};
		}

		public void Init()
		{
			Camera = Camera.main;

			this.AddObserver(OnUnitDied, UnitFacade.DiedNotification);

			ChangeState(BattleStates.Init);
		}

		public void OnDestroy()
		{
			this.RemoveObserver(OnUnitDied, UnitFacade.DiedNotification);
		}

		public int GetNextAllyIndex()
		{
			return (CurrentAllyIndex + 1) % Allies.Count;
		}

		public int GetNextFoeIndex()
		{
			return (CurrentFoeIndex + 1) % Foes.Count;
		}

		public void PlayOneShot(AudioClip audioClip)
		{
			_audioSource.PlayOneShot(audioClip);
		}

		private async void OnUnitDied(object sender, object args)
		{
			var unit = (UnitFacade)args;
			if (unit.Data.Alliance == Alliances.Ally)
			{
				Allies.Remove(unit);
				CurrentAllyIndex = 0;
			}
			else
			{
				Foes.Remove(unit);
				CurrentFoeIndex = 0;
			}
			Object.Destroy(unit.gameObject);

			var clip = Resources.Load<AudioClip>($"Sounds/Death-3");
			PlayOneShot(clip);

			if (Allies.Count == 0)
			{
				await Task.Delay(500);
				SceneManager.LoadScene("Lose");
			}
			else if (Foes.Count == 0)
			{
				await Task.Delay(500);
				SceneManager.LoadScene("Win");
			}
		}

		public void Tick() => _currentStateHandler.Update();

		public void ChangeState(BattleStates newState)
		{
			// Already in state.
			if (_currentState == newState) { return; }

			// Debug.Log($"View Changing state from {_currentState} to {newState}");

			_currentState = newState;

			if (_currentStateHandler != null)
			{
				_currentStateHandler.ExitState();
				_currentStateHandler = null;
			}

			_currentStateHandler = _states[newState];
			_currentStateHandler.EnterState();
		}
	}

	public interface IBattleState
	{
		void EnterState();
		void Update();
		void ExitState();
	}

	public enum BattleStates
	{
		None,
		Init,
		PlayerTurn,
		CpuTurn
	}
}
