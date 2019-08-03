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
		// Debug.Log("Initializing battle.");

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
			throw new UnityException("Missing UnitFacade on unit prefab.");
		}

		facade.InitView(unit);

		return instance;
	}

	public void Update() { }

	public void ExitState() { }
}
