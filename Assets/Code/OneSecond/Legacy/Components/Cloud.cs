using UnityEngine;

namespace OneSecond.Legacy.Components
{
	public class Cloud : MonoBehaviour
	{
		[SerializeField] private Vector3 movement = new Vector3(0.002f, 0f, 0f);

		public void Update()
		{
			transform.position += movement;
		}
	}
}
