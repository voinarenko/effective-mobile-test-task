using Code.Services.Progress;
using Code.UI.Elements.Buttons;
using Code.UI.Services.Factory;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI.Windows
{
  public class EndGameWindow : BaseWindow
  {
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private RestartButton _restartButton;
    private IProgressService _progress;

    [Inject]
    public void Construct(IProgressService progress) =>
      _progress = progress;

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
      // StateMachine.Enter<BootstrapState>();
    }
  }
}