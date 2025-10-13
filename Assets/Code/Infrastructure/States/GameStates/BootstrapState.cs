using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.Services.StaticData;
using DG.Tweening;

namespace Code.Infrastructure.States.GameStates
{
  public class BootstrapState : IState
  {
    private const string BootSceneName = "Boot";
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly IStaticDataService _staticData;
    private readonly ILoadingCurtain _curtain;

    public BootstrapState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IStaticDataService staticData, ILoadingCurtain curtain)
    {
      _curtain = curtain;
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _staticData = staticData;
    }

    public void Enter()
    {
      _curtain.Show();
      _sceneLoader.Load(BootSceneName, OnLoad).Forget();
    }

    public void Exit()
    {
    }

    private void OnLoad()
    {
      LoadStaticData();
      _stateMachine.Enter<ProgressLoadState>();
    }

    private void LoadStaticData()
    {
      _staticData.LoadEnemies();
      _staticData.LoadHero();
    }
  }
}