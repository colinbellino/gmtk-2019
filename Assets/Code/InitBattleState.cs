﻿using UnityEngine;

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
		for (int i = 0; i < _manager.Units.Length; i++)
		{
			_manager.Units[i].Facade = SpawnUnit(_manager.Units[i], i);
		}

		_manager.ChangeState(BattleStates.PlayerTurn);
	}

	private UnitFacade SpawnUnit(Unit unit, int index)
	{
		var instance = GameObject.Instantiate(_manager.UnitPrefab);
		instance.transform.position = new Vector3(3f * index, 0f, 0f);
		instance.name = unit.Name;

		var facade = instance.GetComponent<UnitFacade>();
		if (facade == null)
		{
			throw new UnityException("Missing UnitFacade on unit prefab.");
		}

		facade.Init(unit);

		return facade;
	}

	public void Update() { }

	public void ExitState() { }
}
