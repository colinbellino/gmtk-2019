using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneSecond.Components
{
	public class HelpScreen : MonoBehaviour
	{
		public void Update()
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
			{
				SceneManager.LoadScene("Battle");
			}

			if (Input.GetKey(KeyCode.Escape))
			{
				SceneManager.LoadScene("Title");
			}
		}
	}
}
