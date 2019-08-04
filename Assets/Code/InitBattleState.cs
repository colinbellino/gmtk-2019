using System.Collections.Generic;
using UnityEngine;

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
		_manager.Units = new List<UnitFacade>
		{
			SpawnUnit(new Unit("Ally1", Alliances.Ally, 5), new Vector3(2f, 2f, 0f)),
			SpawnUnit(new Unit("Ally2", Alliances.Ally, 5), new Vector3(2f, 0f, 0f)),
			SpawnUnit(new Unit("Foe1", Alliances.Foe, 5), new Vector3(7f, 2f, 0f)),
			SpawnUnit(new Unit("Foe2", Alliances.Foe, 5), new Vector3(7f, 0f, 0f))
		};

		_manager.ChangeState(BattleStates.PlayerTurn);
	}

	private UnitFacade SpawnUnit(Unit unit, Vector2 position)
	{
		var instance = GameObject.Instantiate(_manager.UnitPrefab);
		instance.transform.position = position;
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
