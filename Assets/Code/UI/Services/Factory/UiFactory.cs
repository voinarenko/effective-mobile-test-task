using Code.Infrastructure.States.StateMachine;
using Code.Services.Progress;
using Code.Services.StaticData;
using Code.UI.Services.Windows;
using Code.UI.Windows;
using UnityEngine;

namespace Code.UI.Services.Factory
{
  public class UiFactory : IUiFactory
  {
    public EndGameWindow EndGameWindow { get; set; }
    public Transform HeroTransform { get; set; }
    public Transform UIRoot { get; set; }
    private readonly IStaticDataService _staticData;
    private readonly IProgressService _progress;

    public UiFactory(IStaticDataService staticData, IProgressService progress)
    {
      _staticData = staticData;
      _progress = progress;
    }

    public void CreatePause(IGameStateMachine stateMachine)
    {
      var config = _staticData.GetWindow(WindowId.Pause);
      var go = Object.Instantiate(config.Prefab, UIRoot);
      if (go.TryGetComponent<PauseWindow>(out var window))
      {
        window.Construct(_progress, stateMachine, _staticData, HeroTransform);
        window.Init();
      }
    }
  }
}