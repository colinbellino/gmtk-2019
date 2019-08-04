using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateManager
{
	private Dictionary<BattleStates, IBattleState> _states;
	private BattleStates _currentState;
	private IBattleState _currentStateHandler;
	private AudioSource _audioSource;

	public Camera Camera;
	public UIFacade UIFacade;
	public List<UnitFacade> Units = new List<UnitFacade>();

	public List<UnitFacade> Allies => Units.Where(unit => unit.Data.Alliance == Alliances.Ally).ToList();
	public List<UnitFacade> Foes => Units.Where(unit => unit.Data.Alliance == Alliances.Foe).ToList();

	public BattleStateManager(UIFacade uiFacade, AudioSource audioSource)
	{
		UIFacade = uiFacade;
		_audioSource = audioSource;

		_states = new Dictionary<BattleStates, IBattleState>
		{ //
			{ BattleStates.Init, new InitBattleState(this) },
			{ BattleStates.PlayerTurn, new PlayerTurnState(this) },
			{ BattleStates.CPUTurn, new CPUTurnState(this) }
		};

		// TODO: Clean this up on destroy
		this.AddObserver(OnUnitDied, UnitFacade.DiedNotification);
	}

	public void PlayOneShot(AudioClip audioClip)
	{
		_audioSource.PlayOneShot(audioClip);
	}

	private void OnUnitDied(object sender, object args)
	{
		var unit = (UnitFacade) args;
		Units.Remove(unit);
		GameObject.Destroy(unit.gameObject);

		var clip = Resources.Load<AudioClip>($"Sounds/Death-3");
		PlayOneShot(clip);

		if (Allies.Count == 0)
		{
			Debug.LogError("Lose condition.");
		}
		else if (Foes.Count == 0)
		{
			Debug.LogWarning("Win condition.");
		}
	}

	public void Init()
	{
		Camera = Camera.main;
		ChangeState(BattleStates.Init);
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
	CPUTurn
}
