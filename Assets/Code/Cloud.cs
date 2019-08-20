using UnityEngine;
using UnityEngine.Serialization;

namespace OneSecond
{
	public class Cloud : MonoBehaviour
	{
		[FormerlySerializedAs("_movement")] [SerializeField] private Vector3 movement = new Vector3(0.002f, 0f, 0f);

		public void Update()
		{
			transform.position += movement;
		}
	}
}
