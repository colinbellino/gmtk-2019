using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingMessageFacade : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _text;

	public void Init(string text, Color color)
	{
		_text.text = text;
		_text.color = color;
	}
}
