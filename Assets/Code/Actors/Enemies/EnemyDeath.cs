using Code.Data;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Action = System.Action;

namespace Code.Actors.Enemies
{
  [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimate), typeof(NavMeshAgent))]
  public class EnemyDeath : MonoBehaviour
  {
    public event Action Happened;
    private const float TimeToDestroy = 3;

    private PlayerProgress _progress;
    private EnemyMove _mover;
    private EnemyHealth _health;
    private EnemyAnimate _animate;
    private NavMeshAgent _agent;
    private EnemyAttack _attack;
    private BoxCollider _collider;

    public void Construct(PlayerProgress progress)
    {
      _progress = progress;
    }

    private void Start()
    {
      _mover = GetComponent<EnemyMove>();
      _health = GetComponent<EnemyHealth>();
      _animate = GetComponent<EnemyAnimate>();
      _agent = GetComponent<NavMeshAgent>();
      _attack = GetComponent<EnemyAttack>();
      _collider = GetComponentInChildren<BoxCollider>();

      _health.HealthChanged += HealthChanged;
    }

    private void HealthChanged()
    {
      if (_health.Current <= 0)
        Die();
    }

    private void Die()
    {
      UpdateGlobalData();
      _collider.enabled = false;
      _health.HealthChanged -= HealthChanged;
      _mover.enabled = false;
      _agent.updatePosition = false;
      _agent.updateRotation = false;
      _agent.speed = 0;
      _attack.enabled = false;
      _animate.PlayDeath();
      Happened?.Invoke();
      Destroy(gameObject, TimeToDestroy);
    }

    private void UpdateGlobalData() =>
      _progress.WaveData.RemoveEnemy();

    private void OnDeath() =>
      _agent.isStopped = true;
  }
}