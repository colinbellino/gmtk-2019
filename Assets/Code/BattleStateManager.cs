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

	public Camera Camera;
	public GameObject UnitPrefab;
	public UIFacade UIFacade;
	public List<UnitFacade> Units = new List<UnitFacade>();

	public List<UnitFacade> Allies => Units.Where(unit => unit.Data.Alliance == Alliances.Ally).ToList();
	public List<UnitFacade> Foes => Units.Where(unit => unit.Data.Alliance == Alliances.Foe).ToList();

	public BattleStateManager(GameObject unitPrefab, UIFacade uiFacade)
	{
		UnitPrefab = unitPrefab;
		UIFacade = uiFacade;

		_states = new Dictionary<BattleStates, IBattleState>
		{ //
			{ BattleStates.Init, new InitBattleState(this) },
			{ BattleStates.PlayerTurn, new PlayerTurnState(this) },
			{ BattleStates.CPUTurn, new CPUTurnState(this) }
		};

		// TODO: Clean this up on destroy
		this.AddObserver(OnUnitDied, UnitFacade.DiedNotification);
	}

	private void OnUnitDied(object sender, object args)
	{
		var unit = (UnitFacade) args;
		Units.Remove(unit);
		GameObject.Destroy(unit.gameObject);

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
