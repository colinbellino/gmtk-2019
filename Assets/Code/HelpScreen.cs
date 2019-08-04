using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpScreen : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
		{
			SceneManager.LoadScene("Battle");
		}

		if (Input.GetKey("escape"))
		{
			SceneManager.LoadScene("Title");
		}
	}
}
