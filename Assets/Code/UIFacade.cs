using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFacade : MonoBehaviour
{
	[SerializeField] private GameObject _timerContainer;
	[SerializeField] private Image _timerImage;
	[SerializeField] private Sprite _timerAllySprite;
	[SerializeField] private Sprite _timerFoeSprite;

	public void UpdateTimer(float value)
	{
		_timerImage.fillAmount = value;
	}

	public void SetTimerAlliance(Alliances alliance)
	{
		if (_timerImage == null)
		{
			return;
		}

		_timerImage.sprite = alliance == Alliances.Ally ? _timerAllySprite : _timerFoeSprite;
		_timerImage.fillClockwise = alliance == Alliances.Foe;
	}
}
