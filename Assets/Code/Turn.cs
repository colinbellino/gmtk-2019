using UnityEngine;

public class Turn
{
	public BattleAction Action;

	private BattleStateManager _manager;
	private Alliances _alliance;

	public Turn(BattleStateManager manager, Alliances alliance)
	{
		_manager = manager;
		_alliance = alliance;

		Action = new BattleAction();
	}

	public void EndRound()
	{
		Action = null;
	}

	public void Act()
	{
		switch (Action.Ability)
		{
			case Abilities.LightPunch:
				Action.Target.Damage(1);
				break;
			case Abilities.StrongPunch:
				Action.Target.Damage(2);
				break;
			case Abilities.LightHeal:
				Action.Target.Heal(1);
				break;
			case Abilities.StrongHeal:
				Action.Target.Heal(2);
				break;
		}

		var clip = Resources.Load<AudioClip>($"Sounds/{Action.Ability.ToString()}");
		Action.Target.PlayOneShot(clip);

		var color = _alliance == Alliances.Ally ? "blue" : "red";
		Debug.Log($"<color={color}>---({Action.Ability})---> {Action.Target.Data.Name}</color>");
	}

	public bool IsValidAction()
	{
		if (Action == null || Action.Target == null)
		{
			return false;
		}

		return true;
	}

}
