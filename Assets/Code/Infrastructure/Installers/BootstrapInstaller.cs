using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Services.Async;
using Code.Services.Input;
using Code.Services.Progress;
using Code.Services.Random;
using Code.Services.StaticData;
using Code.Services.Time;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    public override void InstallBindings()
    {
      BindProgressServices();
      BindStateMachine();
      BindInputService();
      BindInfrastructureServices();
      BindCommonServices();
      BindGameplayServices();
      BindGameFactory();
      BindStateFactory();
      BindGameStates();
    }

    private void BindInputService() =>
      Container.Bind<IInputService>().To<InputService>().AsSingle();

    private void BindInfrastructureServices() =>
      Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();

    private void BindCommonServices()
    {
      Container.Bind<ITimeService>().To<TimeService>().AsSingle();
      Container.Bind<IRandomService>().To<RandomService>().AsSingle();
      Container.Bind<IAsyncService>().To<AsyncService>().AsSingle();
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
      Container.Bind<ILoadingCurtain>()
        .To<LoadingCurtain>()
        .FromComponentInNewPrefabResource("Curtain")
        .AsSingle();
    }

    private void BindGameplayServices() =>
      Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();

    private void BindProgressServices() =>
      Container.Bind<IProgressService>().To<ProgressService>().AsSingle();

    private void BindGameFactory()
    {
      Container.Bind<IAssets>().To<AssetProvider>().AsSingle();
      Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
    }

    private void BindStateMachine() =>
      Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();

    private void BindStateFactory() =>
      Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
      Container.BindInterfacesAndSelfTo<ProgressLoadState>().AsSingle();
      Container.BindInterfacesAndSelfTo<MenuLoadState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LevelLoadState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LevelLoopState>().AsSingle();
      Container.BindInterfacesAndSelfTo<MenuLoopState>().AsSingle();
    }

    public void Initialize() =>
      Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
  }
}