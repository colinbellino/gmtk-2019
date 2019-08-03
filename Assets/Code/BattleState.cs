using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateManager
{
	private Dictionary<BattleStates, IBattleState> _states;
	private BattleStates _currentState;
	private IBattleState _currentStateHandler;

	public GameObject UnitPrefab;
	public UIFacade UIFacade;
	public Unit[] Units = new Unit[]
	{
		new Unit("Ally1", new string[] { "Light Punch", "Strong Punch" }, Alliances.Ally),
			new Unit("Ally2", new string[] { "Light Heal", "Strong Heal" }, Alliances.Ally),
			new Unit("Foe1", new string[] { "Light Punch", "Strong Punch" }, Alliances.Foe),
			new Unit("Foe2", new string[] { "Light Heal", "Strong Heal" }, Alliances.Foe)
	};

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
	}

	public void Init()
	{
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

public class InitBattleState : IBattleState
{
	private BattleStateManager _manager;
	private float _endOfTurnTimestamp;

	public InitBattleState(BattleStateManager manager)
	{
		_manager = manager;
	}

	public void EnterState()
	{
		Debug.Log("Initializing battle.");

		for (int i = 0; i < _manager.Units.Length; i++)
		{
			_manager.Units[i].GameObject = SpawnUnit(_manager.Units[i], i);
		}

		_manager.ChangeState(BattleStates.PlayerTurn);
	}

	private GameObject SpawnUnit(Unit unit, int index)
	{
		var instance = GameObject.Instantiate(_manager.UnitPrefab);
		instance.transform.position = new Vector3(2f * index, 0f, 0f);

		var facade = instance.GetComponent<UnitFacade>();
		if (facade == null)
		{
			throw new Exception("Missing UnitFacade on unit prefab.");
		}

		facade.InitView(unit);

		return instance;
	}

	public void Update() { }

	public void ExitState() { }
}

public class PlayerTurnState : IBattleState
{
	private BattleStateManager _manager;
	private float _roundDuration = 1f;
	private Turn _turn;

	private float _endOfRoundTimestamp;

	public PlayerTurnState(BattleStateManager manager)
	{
		_manager = manager;
	}

	public void EnterState()
	{
		Debug.Log("Starting player turn.");

		_endOfRoundTimestamp = Time.time + _roundDuration;
		_turn = new Turn(3);
	}

	public void Update()
	{
		if (Time.time >= _endOfRoundTimestamp)
		{
			_turn.EndRound();
			_endOfRoundTimestamp = Time.time + _roundDuration;
			Debug.Log($"Round timed out, {_turn.Round.Current} actions remaining.");
		}
		else
		{
			// TODO: Select unit
			// TODO: Select target

			if (Input.GetMouseButtonDown(0))
			{
				_turn.Act(_manager.Units[0], 0);
			}
			else if (Input.GetMouseButtonDown(1))
			{
				_turn.Act(_manager.Units[0], 1);
			}

			if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
			{
				_turn.Target(_manager.Units[1]);

				var action = _turn.GetValidAction();
				if (action != null)
				{
					_turn.EndRound();
					_endOfRoundTimestamp = Time.time + _roundDuration;
					Debug.Log($"Player acted: {action.Initiator} -({action.Ability})-> {action.Target}");
				}
			}
		}

		if (_turn.IsOver)
		{
			_manager.ChangeState(BattleStates.CPUTurn);
		}

		_manager.UIFacade.UpdateTimer(_endOfRoundTimestamp - Time.time);
		_manager.UIFacade.UpdateRound(_turn.Round);
	}

	public void ExitState() { }
}

public class CPUTurnState : IBattleState
{
	private BattleStateManager _manager;
	private float _turnDuration = 1f;

	private float _endOfTurnTimestamp;

	public CPUTurnState(BattleStateManager manager)
	{
		_manager = manager;
	}

	public void EnterState()
	{
		Debug.Log("Starting CPU turn");

		_endOfTurnTimestamp = Time.time + _turnDuration;
	}

	public void Update()
	{
		if (Time.time >= _endOfTurnTimestamp)
		{
			_manager.ChangeState(BattleStates.PlayerTurn);
		}
	}

	public void ExitState() { }
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
