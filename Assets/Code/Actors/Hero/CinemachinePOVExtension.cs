using UnityEngine;
using Cinemachine;
using Code.Services.Input;
using Code.Services.Time;
using System;
using Zenject;

namespace Code.Actors.Hero
{
  public class CinemachinePovExtension : CinemachineExtension
  {
    public event Action LookChanged;
    public float Speed { get; set; }

    private const float ClampAngle = 80f;

    private IInputService _input;
    private ITimeService _time;
    private Vector3 _startRotation;


    [Inject]
    public void Construct(IInputService input, ITimeService time)
    {
      _input = input;
      _time = time;
    }

    public void ResetDirection(Vector3 heroDir) =>
      _startRotation = heroDir;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage,
      ref CameraState state, float deltaTime)
    {
      if (vcam.Follow)
      {
        if (stage == CinemachineCore.Stage.Aim)
        {
          var deltaInput = _input.GetActions().Player.Look.ReadValue<Vector2>();
          _startRotation.x += deltaInput.x * Speed * _time.DeltaTime();
          _startRotation.y += deltaInput.y * Speed * _time.DeltaTime();
          _startRotation.y = Mathf.Clamp(_startRotation.y, -ClampAngle, ClampAngle);
          state.RawOrientation = Quaternion.Euler(-_startRotation.y, _startRotation.x, 0f);
          LookChanged?.Invoke();
        }
      }
    }
  }
}