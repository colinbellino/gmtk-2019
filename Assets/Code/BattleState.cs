using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateManager
{
	private Dictionary<BattleStates, IBattleState> _states;
	private BattleStates _currentState;
	private IBattleState _currentStateHandler;
	private int _currentActionPoints;

	public Turn Turn;

	public BattleStateManager()
	{
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
		_manager.ChangeState(BattleStates.PlayerTurn);
	}

	public void Update() { }

	public void ExitState() { }
}

public class PlayerTurnState : IBattleState
{
	private BattleStateManager _manager;
	private float _roundDuration = 10f;
	private Unit[] _units = new Unit[]
	{
		new Unit("Ally1", new string[] { "Light Punch", "Strong Punch" }),
			new Unit("Ally2", new string[] { "Light Heal", "Strong Heal" }),
			new Unit("Foe1", new string[] { "Light Punch", "Strong Punch" }),
			new Unit("Foe2", new string[] { "Light Heal", "Strong Heal" })
	};

	private float _endOfRoundTimestamp;

	public PlayerTurnState(BattleStateManager manager)
	{
		_manager = manager;
	}

	public void EnterState()
	{
		Debug.Log("Starting player turn.");

		_endOfRoundTimestamp = Time.time + _roundDuration;
		_manager.Turn = new Turn(3);
	}

	public void Update()
	{

		if (Time.time >= _endOfRoundTimestamp)
		{
			_manager.Turn.EndRound();
			_endOfRoundTimestamp = Time.time + _roundDuration;
			Debug.Log($"Round timed out, {_manager.Turn.Round.Current} actions remaining.");
		}
		else
		{
			// TODO: Select unit
			// TODO: Select target

			if (Input.GetMouseButtonDown(0))
			{
				_manager.Turn.Act(_units[0], 0);
			}
			else if (Input.GetMouseButtonDown(1))
			{
				_manager.Turn.Act(_units[0], 1);
			}

			if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
			{
				_manager.Turn.Target(_units[1]);

				var action = _manager.Turn.GetValidAction();
				if (action != null)
				{
					_manager.Turn.EndRound();
					_endOfRoundTimestamp = Time.time + _roundDuration;
					Debug.Log($"Player acted: {action.Initiator} -({action.Ability})-> {action.Target}");
				}
			}
		}

		if (_manager.Turn.IsOver)
		{
			_manager.ChangeState(BattleStates.CPUTurn);
		}
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
