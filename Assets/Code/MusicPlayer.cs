using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
	[SerializeField] private AudioSource _audioSource;

	public static MusicPlayer instance;

	public void Start()
	{
		if (!instance)
		{
			instance = this;
		}
		else
		{
			Object.Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

		if (_audioSource.isPlaying == false)
		{
			_audioSource.time = 0f;
			_audioSource.Play();
		}
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "Lose" || scene.name == "Title")
		{
			_audioSource.Stop();
		}
	}
}
