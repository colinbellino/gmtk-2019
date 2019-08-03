using System;
using TMPro;
using UnityEngine;

public class UnitFacade : MonoBehaviour
{
	[SerializeField] private Renderer _renderer;
	[SerializeField] private TextMeshProUGUI _nameText;
	[SerializeField] private Material _allianceAllyMaterial;
	[SerializeField] private Material _allianceFoeMaterial;

	public void InitView(Unit unit)
	{
		SetNameText(unit.Name);
		SetAllianceTexture(unit.Alliance);
	}

	private void SetNameText(string name)
	{
		_nameText.text = name;
	}

	private void SetAllianceTexture(Alliances alliance)
	{
		var material = alliance == Alliances.Ally ? _allianceAllyMaterial : _allianceFoeMaterial;
		_renderer.material = material;
	}
}
