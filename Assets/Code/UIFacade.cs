using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFacade : MonoBehaviour
{
	[SerializeField] private GameObject _timerContainer;
	[SerializeField] private Image _timerImage;
	[SerializeField] private Sprite _timerAllySprite;
	[SerializeField] private Sprite _timerFoeSprite;
	[SerializeField] private TextMeshProUGUI _roundText;

	public void UpdateTimer(float value)
	{
		_timerImage.fillAmount = value;
	}

	public void UpdateRound(Stat round)
	{
		_roundText.text = $"Round: {round.Current}/{round.Max}";
	}

	public void SetTimerAlliance(Alliances alliance)
	{
		_timerImage.sprite = alliance == Alliances.Ally ? _timerAllySprite : _timerFoeSprite;
		_timerImage.fillClockwise = alliance == Alliances.Foe;
	}
}
