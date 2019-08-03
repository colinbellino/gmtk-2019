using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
	private BattleStateManager _stateManager;

	[SerializeField] private GameObject _unitPrefab;
	[SerializeField] private UIFacade _uiFacade;

	private void Start()
	{
		_stateManager = new BattleStateManager(_unitPrefab, _uiFacade);
		_stateManager.Init();
	}

	private void Update()
	{
		_stateManager.Tick();
	}
}
