using Code.Infrastructure.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.Services.Wave;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class LevelLoadState : IPayloadedState<string>
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IWaveService _wave;

    private int _previousBlockType;

    public LevelLoadState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, ILoadingCurtain curtain,
      IGameFactory gameFactory, IWaveService wave)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _gameFactory = gameFactory;
      _wave = wave;
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
      Cursor.visible = false;
      _gameFactory.CreateHero();
      _gameFactory.CreateHud();
      _wave.Init();
      _stateMachine.Enter<LevelLoopState>();
    }
  }
}