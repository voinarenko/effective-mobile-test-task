using Code.Data;
using System;
using UnityEngine.InputSystem;

namespace Code.Services.Input
{
  public class InputService : IInputService, IDisposable
  {
    private readonly PlayerInputActions _controls;
    private InputDevice _lastUsedDevice;

    public InputService()
    {
      _controls = new PlayerInputActions();
      _controls.Enable();
      _controls.UI.Cancel.performed += OnCancel;
    }

    public PlayerInputActions GetActions() => _controls;

    public void Dispose()
    {
      _controls.UI.Cancel.performed -= OnCancel;
      _controls.Disable();
    }

    private void OnCancel(InputAction.CallbackContext context) =>
      Utils.Quit();
  }
}