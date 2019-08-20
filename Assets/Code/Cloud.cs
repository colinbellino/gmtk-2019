using UnityEngine;

public class Cloud : MonoBehaviour
{
	[SerializeField] private Vector3 _movement = new Vector3(0.002f, 0f, 0f);

	public void Update()
	{
		transform.position += _movement;
	}
}
