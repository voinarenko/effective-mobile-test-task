using Code.Infrastructure.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.Services.Async;
using Code.Services.Input;
using Code.Services.Progress;
using Code.Services.Random;
using Code.Services.StaticData;

namespace Code.Infrastructure.States.GameStates
{
  public class LevelLoadState : IPayloadedState<string>
  {
    private const string SceneNamePrefix = "Level";
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IProgressService _progress;
    private readonly IStaticDataService _staticData;
    private readonly IInputService _input;
    private readonly IRandomService _random;
    private readonly IAsyncService _async;

    private int _previousBlockType;

    public LevelLoadState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, ILoadingCurtain curtain,
      IGameFactory gameFactory, IProgressService progress, IStaticDataService staticData, IInputService input,
      IRandomService random, IAsyncService async)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _gameFactory = gameFactory;
      _progress = progress;
      _staticData = staticData;
      _input = input;
      _random = random;
      _async = async;
    }

    public void Enter(string payload)
    {
      _curtain.Show();
      _sceneLoader.Load(payload, OnLoaded).Forget();
    }

    public void Exit()
    {
      _curtain.Hide();
    }

    private void OnLoaded()
    {
      _stateMachine.Enter<LevelLoopState>();
    }
  }
}