using Code.Infrastructure.States.StateMachine;
using Code.UI.Services.Factory;

namespace Code.UI.Services.Windows
{
  public class WindowService : IWindowService
  {
    private readonly IUiFactory _uiFactory;

    public WindowService(IUiFactory uiFactory) =>
      _uiFactory = uiFactory;

    public void Open(WindowId windowId, IGameStateMachine stateMachine)
    {
      switch (windowId)
      {
        case WindowId.Unknown:
          break;
        case WindowId.Pause:
          _uiFactory.CreatePause(stateMachine);
          break;
      }
    }
  }
}