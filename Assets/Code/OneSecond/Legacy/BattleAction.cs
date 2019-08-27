using OneSecond.Legacy.Components;
using OneSecond.Legacy.Unit;

namespace OneSecond.Legacy
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
