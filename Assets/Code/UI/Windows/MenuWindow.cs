using Code.Data;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.UI.Elements.Buttons;
using UnityEngine;
using Zenject;

namespace Code.UI.Windows
{
  public class MenuWindow : BaseWindow
  {
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    private IGameStateMachine _stateMachine;

    [Inject]
    public void Construct(IGameStateMachine stateMachine) =>
      _stateMachine = stateMachine;

    protected override void SubscribeUpdates()
    {
      base.SubscribeUpdates();
      _playButton.Clicked += Play;
      _quitButton.Clicked += Utils.Quit;
    }

    protected override void Cleanup()
    {
      base.Cleanup();
       _playButton.Clicked -= Play;
       _quitButton.Clicked -= Utils.Quit;
   }

    private void Play() =>
      _stateMachine.Enter<LevelLoadState, string>(Constants.LevelSceneName);
  }
}