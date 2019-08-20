using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace OneSecond
{
	public class BattleManager : MonoBehaviour
	{
		private BattleStateManager _stateManager;

		[FormerlySerializedAs("_uiFacade")] [SerializeField] private UiFacade uiFacade;
		[FormerlySerializedAs("_audioSource")] [SerializeField] private AudioSource audioSource;

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
