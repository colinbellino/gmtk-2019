using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
	[SerializeField] private Vector3 _movement = new Vector3(0.002f, 0f, 0f);

	private void Update()
	{
		transform.position += _movement;
	}

	// private float _min;
	// private float _max;

	// private void Start()
	// {
	// 	_min = transform.position.y + 0f;
	// 	_max = transform.position.y + 0.02f;
	// }

	// private void Update()
	// {
	// 	transform.position = new Vector3(
	// 		transform.position.z,
	// 		Mathf.PingPong(Time.time * 0.002f, _max - _min) + _min,
	// 		transform.position.z
	// 	);
	// }
}
