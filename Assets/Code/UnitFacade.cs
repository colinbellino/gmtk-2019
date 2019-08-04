using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitFacade : MonoBehaviour
{
	public const string DiedNotification = "Unit.Died";

	[SerializeField] private SpriteRenderer _renderer;
	[SerializeField] private Sprite _allianceAllySprite;
	[SerializeField] private Sprite _allianceFoeSprite;
	[SerializeField] private LineRenderer _lineRenderer;

	[SerializeField] private TextMeshProUGUI _nameText;
	[SerializeField] private Transform _floatingMessageContainer;
	[SerializeField] private FloatingMessageFacade _floatingMessagePrefab;
	[SerializeField] private Slider _healthSlider;

	public Unit Data => _data;
	private Unit _data;
	private List<FloatingMessageFacade> _messages = new List<FloatingMessageFacade>();

	public void Init(Unit unit)
	{
		_data = unit;
		SetName(unit.Name);
		SetAlliance(unit.Alliance);
	}

	private void Update()
	{
		_healthSlider.value = (float) _data.Health.Current / (float) _data.Health.Max;
	}

	public void DrawLineTo(Vector3 endPoint)
	{
		_lineRenderer.SetPosition(0, Vector3.zero);
		_lineRenderer.SetPosition(1, endPoint);
	}

	public void CreateMessage(string text, Color color)
	{
		var instance = GameObject.Instantiate(_floatingMessagePrefab, _floatingMessageContainer);
		var message = instance.GetComponent<FloatingMessageFacade>();
		message.Init(text, color);

		_messages.Add(message);
		StartCoroutine(RemoveMessage(message));
	}

	public void Damage(int value)
	{
		_data.Health.Current = _data.Health.Current - value;
		CreateMessage(value.ToString(), Color.white);

		if (_data.Health.Current <= 0)
		{
			OnDeath();
		}
	}

	public void Heal(int value)
	{
		_data.Health.Current = _data.Health.Current + value;
		CreateMessage(value.ToString(), Color.green);
	}

	private IEnumerator RemoveMessage(FloatingMessageFacade message)
	{
		yield return new WaitForSeconds(0.5f);

		GameObject.Destroy(message.gameObject);
		_messages.Remove(message);
	}

	private void SetName(string name)
	{
		_nameText.text = name;
	}

	private void SetAlliance(Alliances alliance)
	{
		var sprite = alliance == Alliances.Ally ? _allianceAllySprite : _allianceFoeSprite;
		_renderer.sprite = sprite;
	}

	public void OnDeath()
	{
		var color = Data.Alliance == Alliances.Ally ? "blue" : "red";
		Debug.Log($"<color={color}>{Data.Name} died.</color>");
		this.PostNotification(DiedNotification, this);
	}
}
