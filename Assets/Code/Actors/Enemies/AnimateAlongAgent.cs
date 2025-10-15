using UnityEngine;
using UnityEngine.AI;

namespace Code.Actors.Enemies
{
  [RequireComponent(typeof(NavMeshAgent))]
  [RequireComponent(typeof(EnemyAnimate))]
  public class AnimateAlongAgent : MonoBehaviour
  {
    private const float MinimalVelocity = 0.1f;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyAnimate animate;

    private void Update()
    {
      if (ShouldMove())
        animate.Move();
      else
        animate.StopMoving();
    }

    private bool ShouldMove() =>
      _agent.velocity.magnitude > MinimalVelocity && _agent.remainingDistance > _agent.radius;
  }
}