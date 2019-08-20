using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace OneSecond
{
	public class UnitFacade : MonoBehaviour
	{
		public const string DiedNotification = "Unit.Died";

		[FormerlySerializedAs("_health")] [SerializeField] private int health = 3;
		[FormerlySerializedAs("_animator")] [SerializeField] private Animator animator;
		[FormerlySerializedAs("_floatingMessageContainer")] [SerializeField] private Transform floatingMessageContainer;
		[FormerlySerializedAs("_floatingMessagePrefab")] [SerializeField] private FloatingMessageFacade floatingMessagePrefab;
		[FormerlySerializedAs("_healthSlider")] [SerializeField] private Slider healthSlider;
		[FormerlySerializedAs("_audioSource")] [SerializeField] private AudioSource audioSource;

		public Unit Data { get; private set; }

		public void Init(Unit unit)
		{
			Data = unit;
			Data.SetHealth(health);
		}

		public void Update()
		{
			healthSlider.value = (float)Data.Health.Current / Data.Health.Max;
		}

		public void CreateMessage(string text, Color color)
		{
			var instance = GameObject.Instantiate(floatingMessagePrefab, floatingMessageContainer);
			var message = instance.GetComponent<FloatingMessageFacade>();
			message.Init(text, color);

			StartCoroutine(RemoveMessage(message));
		}

		public void Wiggle()
		{
			if (!animator) { return; }

			animator.Play("Wiggle");
		}

		public void Damage(int value)
		{
			var color = value < 0 ? Color.green : Color.white;
			var isCritical = UnityEngine.Random.Range(0, 100) < 10;
			var multiplier = isCritical ? 2 : 1;
			var suffix = isCritical ? "!" : "";
			var modifiedValue = value * multiplier;

			Data.Health.Current = Data.Health.Current - modifiedValue;
			CreateMessage(Math.Abs(modifiedValue).ToString() + suffix, color);

			if (Data.Health.Current <= 0)
			{
				OnDeath();
			}
		}

		public void Heal(int value)
		{
			Damage(-value);
		}

		private IEnumerator RemoveMessage(FloatingMessageFacade message)
		{
			yield return new WaitForSeconds(0.5f);

			Destroy(message.gameObject);
		}

		public void OnDeath()
		{
			var color = Data.Alliance == Alliances.Ally ? "blue" : "red";
			Debug.Log($"<color={color}>{Data.Name} died.</color>");

			this.PostNotification(DiedNotification, this);
		}

		public void PlayOneShot(AudioClip audioClip)
		{
			audioSource.PlayOneShot(audioClip);
		}
	}
}
