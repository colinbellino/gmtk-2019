using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
		{
			SceneManager.LoadScene("Title");
		}

		if (Input.GetKey("escape"))
		{
			SceneManager.LoadScene("Title");
		}
	}
}
