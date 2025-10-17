using Code.Data;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.Services.Random;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class MenuLoadState : IState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IRandomService _random;

    public MenuLoadState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, ILoadingCurtain curtain,
      IGameFactory gameFactory, IRandomService random)
    {
      _random = random;
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _gameFactory = gameFactory;
    }

    public void Enter()
    {
      _curtain.Show();
      _gameFactory.CleanUp();
      _sceneLoader.Load(Constants.MenuSceneName, OnLoaded).Forget();
    }

    public void Exit()
    {
      _curtain.Hide();
    }

    private void OnLoaded() =>
      _stateMachine.Enter<MenuLoopState>();
  }
}