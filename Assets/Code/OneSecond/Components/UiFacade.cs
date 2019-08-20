using UnityEngine;
using UnityEngine.UI;

namespace OneSecond.Components
{
	public class UiFacade : MonoBehaviour
	{
		[SerializeField] private Image timerImage;
		[SerializeField] private Sprite timerAllySprite;
		[SerializeField] private Sprite timerFoeSprite;
		[SerializeField] private Transform currentUnitIndicator;

		public void UpdateTimer(float value)
		{
			timerImage.fillAmount = value;
		}

		public void SetTimerAlliance(Alliances alliance)
		{
			if (!timerImage)
			{
				return;
			}

			timerImage.sprite = alliance == Alliances.Ally ? timerAllySprite : timerFoeSprite;
			timerImage.fillClockwise = alliance == Alliances.Foe;
		}

		public void UpdateCurrentUnitIndicator(UnitFacade unit)
		{
			currentUnitIndicator.position = unit.transform.position;
		}
	}
}
