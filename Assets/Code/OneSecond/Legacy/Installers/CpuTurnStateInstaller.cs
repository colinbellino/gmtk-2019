using Zenject;

namespace OneSecond.Legacy.Installers
{
	public class CpuTurnStateInstaller : MonoInstaller<CpuTurnStateInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<IUnitBrain>().To<PlayerBrain>().AsSingle();
			Container.BindInterfacesAndSelfTo<TurnState>().AsSingle().WithArguments(BattleStates.PlayerTurn);
		}
	}
}
