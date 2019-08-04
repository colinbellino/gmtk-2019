using UnityEngine;

public class BattleManager : MonoBehaviour
{
	private BattleStateManager _stateManager;

	[SerializeField] private UIFacade _uiFacade;

	private void Start()
	{
		_stateManager = new BattleStateManager(_uiFacade);
		_stateManager.Init();
	}

	private void Update() => _stateManager.Tick();
}
