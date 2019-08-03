using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFacade : MonoBehaviour
{
	[SerializeField] private Slider _timeSlider;
	[SerializeField] private TextMeshProUGUI _roundText;

	public void UpdateTimer(float value) => _timeSlider.value = value;
	public void UpdateRound(Stat round) => _roundText.text = $"Round: {round.Current}/{round.Max}";
}
