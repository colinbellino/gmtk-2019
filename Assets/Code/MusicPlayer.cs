using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace OneSecond
{
	public class MusicPlayer : MonoBehaviour
	{
		[FormerlySerializedAs("_audioSource")] [SerializeField] private AudioSource audioSource;

		private static MusicPlayer _instance;

		public void Start()
		{
			if (!_instance)
			{
				_instance = this;
			}
			else
			{
				Destroy(gameObject);
			}

			DontDestroyOnLoad(gameObject);

			if (!audioSource.isPlaying)
			{
				audioSource.time = 0f;
				audioSource.Play();
			}
		}

		public void OnEnable()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		public void OnDisable()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (scene.name == "Lose" || scene.name == "Title")
			{
				audioSource.Stop();
			}
		}
	}
}
