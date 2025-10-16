using Code.Infrastructure.States.StateMachine;
using Code.Services;

namespace Code.UI.Services.Windows
{
  public interface IWindowService : IService
  {
    void Open(WindowId windowId, IGameStateMachine stateMachine);
  }
}