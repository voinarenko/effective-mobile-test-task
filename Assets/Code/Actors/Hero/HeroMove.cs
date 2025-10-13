using Code.Services.Input;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Actors.Hero
{
  public class HeroMove : MonoBehaviour
  {
    public float Speed { get; set; }

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private HeroAnimate _animator;

    private PlayerInputActions _controls;
    private bool _isMoving;
    private IInputService _inputService;
    private Vector2 _move;

    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void Update()
    {
      _move = _inputService.GetActions().Player.Move.ReadValue<Vector2>();
      _isMoving = _move.magnitude != 0;
      if (_isMoving) Move();
    }

    private void Move()
    {
      var pos = transform.position;
      var dir = new Vector3(_move.x, 0, _move.y);
      dir.Normalize();

      pos += new Vector3(_move.x * Speed, 0, _move.y * Speed);
      if (transform) transform.position = pos;

      _animator.Move(dir);
    }

    public void SetSpeed(float speed) =>
      Speed = speed;
  }
}