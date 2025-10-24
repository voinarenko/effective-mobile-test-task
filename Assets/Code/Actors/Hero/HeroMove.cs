using Code.Services.Input;
using Code.Services.Time;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Actors.Hero
{
  public class HeroMove : MonoBehaviour
  {
    public float Speed { get; set; }
    public Camera MainCamera { get; set; }

    [SerializeField] private NavMeshAgent _agent;

    private PlayerInputActions _controls;
    private bool _isMoving;
    private IInputService _input;
    private Vector2 _move;
    private ITimeService _time;

    public void Construct(IInputService input, ITimeService time)
    {
      _input = input;
      _time = time;
    }

    private void Update()
    {
      _move = _input.GetActions().Player.Move.ReadValue<Vector2>();
      _isMoving = _move.magnitude != 0;
      if (_isMoving) Move();
    }

    private void Move()
    {
      var move = new Vector3(_move.x, 0, _move.y);
      move = MainCamera.transform.forward * move.z + MainCamera.transform.right * move.x;
      move.y = 0;
      move.Normalize();

      _agent.Move(move * (Speed * _time.DeltaTime()));
    }
  }
}