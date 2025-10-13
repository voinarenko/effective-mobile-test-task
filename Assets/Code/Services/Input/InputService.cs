using System;
using UnityEngine.InputSystem;

namespace Code.Services.Input
{
  public class InputService : IInputService
  {
    private readonly PlayerInputActions _controls;
    private InputDevice _lastUsedDevice;

    public InputService()
    {
      _controls = new PlayerInputActions();
      _controls.Enable();
    }

    public PlayerInputActions GetActions() => _controls;
  }
}