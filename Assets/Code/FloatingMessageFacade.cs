using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace OneSecond
{
	public class FloatingMessageFacade : MonoBehaviour
	{
		[FormerlySerializedAs("_text")] [SerializeField] private TextMeshProUGUI text;

		public void Init(string textValue, Color color)
		{
			text.text = textValue;
			text.color = color;
		}

		public void Update()
		{
			transform.Translate(1f * Time.deltaTime * Vector3.up, Space.World);
		}
	}
}
