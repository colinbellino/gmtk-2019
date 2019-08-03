using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	private BattleStateManager _stateManager;
	[SerializeField] private GameObject _unitPrefab;

	private void Start()
	{
		_stateManager = new BattleStateManager(_unitPrefab);
		_stateManager.Init();
	}

	private void Update()
	{
		_stateManager.Tick();
	}
}
