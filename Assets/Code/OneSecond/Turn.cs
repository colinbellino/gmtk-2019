using OneSecond.Unit;
using UnityEngine;

namespace OneSecond
{
	public class Turn
	{
		public BattleAction Action;

		private readonly BattleStateManager _manager;

		public bool DidAct { get; private set; }

		public Turn(BattleStateManager manager)
		{
			_manager = manager;
		}

		public void Act()
		{
			switch (Action.Ability)
			{
				case Abilities.LightPunch:
					Action.Target.Damage(1);
					break;
				case Abilities.WenkPunch:
					Action.Target.Damage(1);
					break;
				case Abilities.LightHeal:
					Action.Target.Heal(1);
					break;
				case Abilities.StrongHeal:
					Action.Target.Heal(2);
					break;
			}

			Action.Initiator.Wiggle();

			var clip = Resources.Load<AudioClip>($"Sounds/{Action.Ability.ToString()}");
			_manager.PlayOneShot(clip);

			var color = Action.Initiator.Data.Alliance == Alliances.Ally ? "blue" : "red";
			Debug.Log($"<color={color}>---({Action.Ability.ToString()})---> {Action.Target.Data.Name}</color>");

			DidAct = true;
		}

		public void EndRound()
		{
			Action = null;
			DidAct = false;
		}
	}
}
