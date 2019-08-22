using Zenject;

namespace OneSecond.Installers
{
	public class PlayerTurnStateInstaller : MonoInstaller<PlayerTurnStateInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<IUnitBrain>().To<PlayerBrain>().AsSingle();
			Container.BindInterfacesAndSelfTo<TurnState>().AsSingle().WithArguments(BattleStates.PlayerTurn);
		}
	}
}
