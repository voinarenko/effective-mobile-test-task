using Code.Data;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Services.Progress;
using Code.UI.Elements.Buttons;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI.Windows
{
  public class EndGameWindow : BaseWindow
  {
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private Button _restartButton;
    private IProgressService _progress;
    private IGameStateMachine _stateMachine;

    [Inject]
    public void Construct(IProgressService progress, IGameStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
      _progress = progress;
    }

    public void UpdateData() =>
      _score.text = $"{_progress.Progress.WaveData.CurrentWave - 1}";

    protected override void Initialize() =>
      _restartButton.Clicked += ProcessClick;

    protected override void Cleanup()
    {
      base.Cleanup();
      _restartButton.Clicked -= ProcessClick;
    }

    private void ProcessClick()
    {
      _restartButton.Clicked -= ProcessClick;
      _progress.Progress.Reset();
      _stateMachine.Enter<LevelLoadState, string>(Constants.LevelSceneName);
    }
  }
}