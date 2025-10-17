using Code.Data;
using Code.Infrastructure.Factory;
using Code.Services.Async;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Actors.Enemies
{
  [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimate), typeof(NavMeshAgent))]
  public class EnemyDeath : MonoBehaviour
  {
    private const float TimeToDestroy = 3;

    [SerializeField] private EnemyMove _move;
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private EnemyAnimate _animate;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyAttack _attack;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private EnemyAudio _audio;
    private IAsyncService _async;
    private IGameFactory _gameFactory;
    private PlayerProgress _progress;

    public void Construct(IGameFactory gameFactory, PlayerProgress progress, IAsyncService async)
    {
      _gameFactory = gameFactory;
      _progress = progress;
      _async = async;
    }

    private void Start() =>
      _health.HealthChanged += OnHealthChanged;

    private void OnDestroy() =>
      _health.HealthChanged -= OnHealthChanged;

    public void Reactivate()
    {
      _collider.enabled = true;
      _move.enabled = true;
      _agent.updatePosition = true;
      _agent.updateRotation = true;
      _attack.enabled = true;
    }

    private void OnHealthChanged()
    {
      if (_health.Current <= 0)
        Die().Forget();
    }

    private async UniTaskVoid Die()
    {
      UpdateGlobalData();
      _collider.enabled = false;
      _move.enabled = false;
      _agent.updatePosition = false;
      _agent.updateRotation = false;
      _agent.speed = 0;
      _attack.enabled = false;
      _animate.PlayDeath();
      _audio.Death();
      await _async.WaitForSeconds(TimeToDestroy);
      _gameFactory.PutEnemy(_attack.Type, gameObject);
    }

    private void UpdateGlobalData() =>
      _progress.WaveData.RemoveEnemy();

    private void OnDeath() =>
      _agent.isStopped = true;
  }
}