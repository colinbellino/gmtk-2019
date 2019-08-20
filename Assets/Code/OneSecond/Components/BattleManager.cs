using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneSecond.Components
{
	public class BattleManager : MonoBehaviour
	{
		private BattleStateManager _stateManager;

		[SerializeField] private UiFacade uiFacade;
		[SerializeField] private AudioSource audioSource;

		public void Start()
		{
			_stateManager = new BattleStateManager(this, uiFacade, audioSource);
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
}
