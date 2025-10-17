namespace Code.Services.Input
{
  public interface IInputService : IService
  {
    PlayerInputActions GetActions();
  }
}