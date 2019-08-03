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
		_manager.Units[0].Facade = SpawnUnit(_manager.Units[0], new Vector3(2f, 0f, 0f));
		_manager.Units[1].Facade = SpawnUnit(_manager.Units[1], new Vector3(2f, 2f, 0f));
		_manager.Units[2].Facade = SpawnUnit(_manager.Units[2], new Vector3(7f, 0f, 0f));
		_manager.Units[3].Facade = SpawnUnit(_manager.Units[3], new Vector3(7f, 2f, 0f));

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
