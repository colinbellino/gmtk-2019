using OneSecond.Components;
using UnityEngine;
using Zenject;

namespace OneSecond.Installers
{
	public class BattleInstaller : MonoInstaller<BattleInstaller>
	{
		public AudioSource AudioSource;

		public UiFacade UiFacade;
		public BattleState PlayerTurnState;

		public override void InstallBindings()
		{
			// TODO: Move to GameInstaller
			Container.Bind<AsyncGenerator>().FromNewComponentOnNewGameObject().AsSingle();
			Container.BindInstance(AudioSource);
			Container.BindInstance(Camera.main).AsSingle();

			Container.BindInstance(UiFacade);
			// TODO: Use a factory for this ?
			Container.Bind<IBattleState>().FromComponentInNewPrefab(PlayerTurnState).AsSingle();
			Container.BindInterfacesAndSelfTo<BattleStateManager>().AsSingle();
		}
	}
}
