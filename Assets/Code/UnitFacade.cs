using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitFacade : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _renderer;
	[SerializeField] private Color _allianceAllyColor;
	[SerializeField] private Color _allianceFoeColor;
	[SerializeField] private LineRenderer _lineRenderer;

	[SerializeField] private TextMeshProUGUI _nameText;
	[SerializeField] private Transform _floatingMessageContainer;
	[SerializeField] private FloatingMessageFacade _floatingMessagePrefab;

	public Unit Unit => _unit;
	private Unit _unit;
	private List<FloatingMessageFacade> _messages = new List<FloatingMessageFacade>();

	public void Init(Unit unit)
	{
		_unit = unit;
		SetName(unit.Name);
		SetAlliance(unit.Alliance);
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
		var color = alliance == Alliances.Ally ? _allianceAllyColor : _allianceFoeColor;
		_renderer.color = color;
	}
}
