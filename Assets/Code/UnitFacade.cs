using System;
using TMPro;
using UnityEngine;

public class UnitFacade : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _renderer;
	[SerializeField] private TextMeshProUGUI _nameText;
	[SerializeField] private Color _allianceAllyColor;
	[SerializeField] private Color _allianceFoeColor;
	[SerializeField] private LineRenderer _lineRenderer;

	public Unit Unit => _unit;
	private Unit _unit;

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
