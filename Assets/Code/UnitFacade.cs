using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitFacade : MonoBehaviour
{
	public const string DiedNotification = "Unit.Died";

	[SerializeField] private int _health = 3;

	[SerializeField] private SpriteRenderer _renderer;
	[SerializeField] private Animator _animator;
	[SerializeField] private Transform _floatingMessageContainer;
	[SerializeField] private FloatingMessageFacade _floatingMessagePrefab;
	[SerializeField] private Slider _healthSlider;
	[SerializeField] private AudioSource _audioSource;

	public Unit Data => _data;
	private Unit _data;
	private List<FloatingMessageFacade> _messages = new List<FloatingMessageFacade>();

	public void Init(Unit unit)
	{
		_data = unit;
		_data.SetHealth(_health);
	}

	private void Update()
	{
		_healthSlider.value = (float) _data.Health.Current / (float) _data.Health.Max;
	}

	public void CreateMessage(string text, Color color)
	{
		var instance = GameObject.Instantiate(_floatingMessagePrefab, _floatingMessageContainer);
		var message = instance.GetComponent<FloatingMessageFacade>();
		message.Init(text, color);

		_messages.Add(message);
		StartCoroutine(RemoveMessage(message));
	}

	public void Wiggle()
	{
		if (_animator == null)
		{
			return;
		}
		_animator.Play("Wiggle");
	}

	public void Damage(int value)
	{
		var color = value < 0 ? Color.green : Color.white;
		var isCrit = UnityEngine.Random.Range(0, 100) < 10;
		var multiplier = isCrit ? 2 : 1;
		var suffix = isCrit ? "!" : "";
		var modifiedValue = value * multiplier;

		_data.Health.Current = _data.Health.Current - modifiedValue;
		CreateMessage(Math.Abs(modifiedValue).ToString() + suffix, color);

		if (_data.Health.Current <= 0)
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

		GameObject.Destroy(message.gameObject);
		_messages.Remove(message);
	}

	public void OnDeath()
	{
		var color = Data.Alliance == Alliances.Ally ? "blue" : "red";
		Debug.Log($"<color={color}>{Data.Name} died.</color>");

		this.PostNotification(DiedNotification, this);
	}

	public void PlayOneShot(AudioClip audioClip)
	{
		_audioSource.PlayOneShot(audioClip);
	}
}
