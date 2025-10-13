using Code.Data;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.Services.Progress;

namespace Code.Infrastructure.States.GameStates
{
  public class ProgressLoadState : IState
  {
    private const string LevelName = "Level";
    private readonly IGameStateMachine _stateMachine;
    private readonly IProgressService _progress;

    public ProgressLoadState(IGameStateMachine stateMachine, IProgressService progress)
    {
      _stateMachine = stateMachine;
      _progress = progress;
    }

    public void Enter()
    {
      InitProgress();
      _stateMachine.Enter<LevelLoadState, string>(LevelName);
    }

    public void Exit()
    {

    }

    private void InitProgress() =>
      _progress.Progress = new PlayerProgress();
  }
}