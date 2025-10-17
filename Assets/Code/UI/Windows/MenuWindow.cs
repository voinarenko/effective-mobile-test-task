using Code.Data;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.UI.Elements.Buttons;
using UnityEngine;
using Zenject;

namespace Code.UI.Windows
{
  public class MenuWindow : MonoBehaviour
  {
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    private IGameStateMachine _stateMachine;

    [Inject]
    public void Construct(IGameStateMachine stateMachine) =>
      _stateMachine = stateMachine;

    private void Start()
    {
      _playButton.Clicked += Play;
      _quitButton.Clicked += Utils.Quit;
      Cursor.visible = true;
    }

    private void OnDestroy()
    {
      _playButton.Clicked -= Play;
      _quitButton.Clicked -= Utils.Quit;
    }

    private void Play() =>
      _stateMachine.Enter<LevelLoadState, string>(Constants.LevelSceneName);
  }
}