using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	private BattleStateManager _stateManager;

	private void Start()
	{
		_stateManager = new BattleStateManager();
		_stateManager.Init();
	}

	private void Update()
	{
		_stateManager.Tick();
	}
}
