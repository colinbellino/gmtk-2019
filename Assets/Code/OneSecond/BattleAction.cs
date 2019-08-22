using OneSecond.Components;
using OneSecond.Unit;

namespace OneSecond
{
	public class BattleAction
	{
		public UnitFacade Initiator;
		public UnitFacade Target;
		public Abilities Ability;

		public BattleAction(UnitFacade initiator, UnitFacade target, Abilities ability) {
			Initiator = initiator;
			Target = target;
			Ability = ability;
		}
	}
}
