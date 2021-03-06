using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			OnExit();
		}
	}

	public void OnStart()
	{
		SceneManager.LoadScene("Help");
	}

	public void OnHelp()
	{
		Debug.Log("OnHelp");
	}

	public void Twitter()
	{
		Application.OpenURL("https://twitter.com/colinbellino");
	}

	public void OnExit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
		Application.OpenURL(webplayerQuitURL);
#else
		Application.Quit();
#endif
	}
}
