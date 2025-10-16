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
    [SerializeField] private MenuReturnButton _returnButton;
    private IProgressService _progress;
    private IUiFactory _uiFactory;

    [Inject]
    public void Construct(IProgressService progress, IUiFactory uiFactory)
    {
      _uiFactory = uiFactory;
      _progress = progress;
    }

    public void UpdateData() =>
      _score.text = $"{_progress.Progress.WaveData.CurrentWave - 1}";

    protected override void Initialize() =>
      _returnButton.Clicked += ProcessClick;

    protected override void Cleanup()
    {
      base.Cleanup();
      _returnButton.Clicked -= ProcessClick;
    }

    private void ProcessClick()
    {
      _returnButton.Clicked -= ProcessClick;
      // StateMachine.Enter<BootstrapState>();
    }
  }
}