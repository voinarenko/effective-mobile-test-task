using Code.Actors.Enemies.Interfaces;
using Code.Infrastructure.Factory;
using Code.Services.Time;
using Code.StaticData;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Actors.Enemies
{
  [RequireComponent(typeof(EnemyAnimate))]
  public class EnemyAttack : MonoBehaviour
  {
    public EnemyTypeId Type { get; set; }
    public float AttackCooldown { get; set; }
    public float Cleavage { get; set; }
    public float Damage { get; set; }
    public float ShotDistance { get; set; }
    public float BulletSpeed { get; set; }

    [SerializeField] private EnemyMove _move;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyAudio _audio;
    [SerializeField] private EnemyAnimate _animate;
    private ISpecificAttack _specificAttack;

    private Transform _playerTransform;
    private float _attackCooldown;

    private bool _isAttacking;
    private bool _attackIsActive;
    private float _savedSpeed;
    private ITimeService _time;
    private IGameFactory _gameFactory;

    public void Construct(IGameFactory gameFactory, ITimeService time, Transform playerTransform)
    {
      _gameFactory = gameFactory;
      _time = time;
      _playerTransform = playerTransform;
    }

    private void OnEnable() =>
      _move.Completed += EnableAttack;

    private void Start() =>
      _savedSpeed = _agent.speed;

    private void Update()
    {
      UpdateCooldown();
      if (CanAttack())
        StartAttack();
    }

    private void OnDisable() =>
      _move.Completed -= EnableAttack;

    public void UpdateSpecificData()
    {
      TryGetComponent(out _specificAttack);
      switch (_specificAttack)
      {
        case IShootAttack shoot:
          shoot.Construct(_gameFactory, Damage, BulletSpeed, ShotDistance);
          break;

        case IMeleeAttack melee:
          melee.Construct(Cleavage, Damage);
          break;
      }
    }
    
    private void OnAttackStart() =>
      _agent.speed = 0;

    private void OnAttack() =>
      _specificAttack.Perform();

    private void OnAttackEnded()
    {
      _agent.speed = _savedSpeed;
      _attackCooldown = AttackCooldown;
      _isAttacking = false;
      if (_specificAttack is IShootAttack) _audio.Reload();
    }

    private void OnHit() =>
      _agent.speed = 0;

    private void OnHitEnded() =>
      _agent.speed = _savedSpeed;

    public void EnableAttack() =>
      _attackIsActive = true;

    public void DisableAttack() =>
      _attackIsActive = false;

    private void StartAttack()
    {
      transform.LookAt(_playerTransform);
      switch (_specificAttack)
      {
        case IShootAttack:
          _animate.PlayShoot();
          break;

        case IMeleeAttack:
          _animate.PlayAttack();
          break;
      }

      _isAttacking = true;
    }

    private bool CooldownIsUp() =>
      _attackCooldown <= 0;

    private bool CanAttack() =>
      _attackIsActive && !_isAttacking && CooldownIsUp();

    private void UpdateCooldown()
    {
      if (!CooldownIsUp())
        _attackCooldown -= _time.DeltaTime();
    }
  }
}