using Code.Infrastructure.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.UI.Services.Factory;
using Code.UI.Windows;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class EndGameLoadState : IState
  {
    private const string EndGameSceneName = "EndGame";
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IUiFactory _uiFactory;

    public EndGameLoadState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, ILoadingCurtain curtain,
      IGameFactory gameFactory, IUiFactory uiFactory)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _gameFactory = gameFactory;
      _uiFactory = uiFactory;
    }

    public void Enter()
    {
      _curtain.Show();
      _gameFactory.CleanUp();
      _sceneLoader.Load(EndGameSceneName, OnLoaded).Forget();
    }

    public void Exit()
    {
      _curtain.Hide();
    }

    private void OnLoaded()
    {
      Cursor.visible = true;
      _uiFactory.EndGameWindow.UpdateData();
      _stateMachine.Enter<EndGameLoopState>();
    }
  }
}