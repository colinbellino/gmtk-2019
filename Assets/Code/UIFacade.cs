using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFacade : MonoBehaviour
{
	[SerializeField] private GameObject _timerContainer;
	[SerializeField] private Image _timerImage;
	[SerializeField] private TextMeshProUGUI _roundText;

	public void UpdateTimer(float value)
	{
		_timerImage.fillAmount = value;
	}

	public void UpdateRound(Stat round)
	{
		_roundText.text = $"Round: {round.Current}/{round.Max}";
	}

	public void SetTimerVisibility(bool value)
	{
		_timerContainer.SetActive(value);
	}

	public void SetRoundVisibility(bool value)
	{
		_roundText.enabled = value;
	}
}
