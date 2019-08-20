using UnityEngine;
using UnityEngine.UI;

public class UIFacade : MonoBehaviour
{
	[SerializeField] private Image _timerImage;
	[SerializeField] private Sprite _timerAllySprite;
	[SerializeField] private Sprite _timerFoeSprite;
	[SerializeField] private Transform _currentUnitIndicator;

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

	public void UpdateCurrentUnitIndicator(UnitFacade unit)
	{
		_currentUnitIndicator.position = unit.transform.position;
	}
}
