using Code.Actors.Hero;
using Code.Data;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using System;

namespace Code.Services.Progress
{
  public class ProgressService : IProgressService, IDisposable
  {
    private HeroDeath _heroDeath;
    private readonly IGameStateMachine _stateMachine;
    public PlayerProgress Progress { get; set; }

    public ProgressService(IGameStateMachine stateMachine) =>
      _stateMachine = stateMachine;

    public void TrackHeroDeath(HeroDeath heroDeath)
    {
      _heroDeath = heroDeath;
      _heroDeath.Happened += GoToGameOverScreen;
    }

    public void Dispose() =>
      _heroDeath.Happened -= GoToGameOverScreen;

    private void GoToGameOverScreen()
    {
      _stateMachine.Enter<EndGameLoadState>();
    }
  }
}