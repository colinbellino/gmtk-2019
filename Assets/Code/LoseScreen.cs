using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
	public void Update()
	{
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
		{
			SceneManager.LoadScene("Title");
		}

		if (Input.GetKey(KeyCode.Escape))
		{
			SceneManager.LoadScene("Title");
		}
	}
}
