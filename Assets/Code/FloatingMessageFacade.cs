using TMPro;
using UnityEngine;

public class FloatingMessageFacade : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _text;

	public void Init(string text, Color color)
	{
		_text.text = text;
		_text.color = color;
	}

	public void Update()
	{
		transform.Translate(Vector3.up * Time.deltaTime * 1f, Space.World);
	}
}
