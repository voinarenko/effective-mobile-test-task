using UnityEngine;
using UnityEngine.AI;

namespace Code.Actors.Enemies
{
  [RequireComponent(typeof(NavMeshAgent))]
  [RequireComponent(typeof(EnemyAnimator))]
  public class AnimateAlongAgent : MonoBehaviour
  {
    private const float MinimalVelocity = 0.1f;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyAnimator _animator;

    private void Update()
    {
      if (ShouldMove())
        _animator.Move();
      else
        _animator.StopMoving();
    }

    private bool ShouldMove() =>
      _agent.velocity.magnitude > MinimalVelocity && _agent.remainingDistance > _agent.radius;
  }
}