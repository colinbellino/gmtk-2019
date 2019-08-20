using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace OneSecond
{
	public class UiFacade : MonoBehaviour
	{
		[FormerlySerializedAs("_timerImage")] [SerializeField] private Image timerImage;
		[FormerlySerializedAs("_timerAllySprite")] [SerializeField] private Sprite timerAllySprite;
		[FormerlySerializedAs("_timerFoeSprite")] [SerializeField] private Sprite timerFoeSprite;
		[FormerlySerializedAs("_currentUnitIndicator")] [SerializeField] private Transform currentUnitIndicator;

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
