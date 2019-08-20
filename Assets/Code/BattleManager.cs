using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
	private BattleStateManager _stateManager;

	[SerializeField] private UIFacade _uiFacade;
	[SerializeField] private AudioSource _audioSource;

	public void Start()
	{
		_stateManager = new BattleStateManager(this, _uiFacade, _audioSource);
		_stateManager.Init();
	}

	public void Update()
	{
#if UNITY_EDITOR
		if (Input.GetKey(KeyCode.Return))
		{
			SceneManager.LoadScene("Battle");
		}
#endif

		_stateManager.Tick();
	}

	public void OnDestroy()
	{
		_stateManager.OnDestroy();
	}
}
