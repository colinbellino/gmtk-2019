using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace OneSecond.Components
{
	public class BattleManager : MonoBehaviour
	{
		[Inject] private BattleStateManager _stateManager;

		public void Update()
		{
#if UNITY_EDITOR
			if (Input.GetKey(KeyCode.Return))
			{
				SceneManager.LoadScene("Battle");
			}
#endif
		}

		public void OnDestroy()
		{
			_stateManager.OnDestroy();
		}
	}
}
