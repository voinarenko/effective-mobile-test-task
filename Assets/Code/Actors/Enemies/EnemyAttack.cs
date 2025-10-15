using Code.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Action = System.Action;

namespace Code.Actors.Enemies
{
  [RequireComponent(typeof(EnemyAnimate))]
  public class EnemyAttack : MonoBehaviour
  {
    public event Action Completed;
    public EnemyTypeId Type { get; set; }
    public float AttackCooldown { get; set; }
    public float Cleavage { get; set; }
    public float Damage { get; set; }

    private const string PlayerTag = "Player";
    private const float AttackTime = 0.1f;

    [SerializeField] private EnemyMove _move;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyAudio _audio;
    [SerializeField] private GameObject _shootEffectPrefab;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private EnemyAnimate _animate;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private List<Transform> _hitPoints;


    private Transform _playerTransform;
    private float _attackCooldown;
    private int _layerMask;
    private readonly Collider[] _hits = new Collider[1];

    private bool _isAttacking;
    private bool _attackIsActive;
    private float _savedSpeed;

    public void Construct(Transform playerTransform) =>
      _playerTransform = playerTransform;

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

    private void OnAttackStart()
    {
      _agent.speed = 0;
    }

    private void OnAttack()
    {
      if (Type == EnemyTypeId.Ranged)
      {
        if (_shootEffectPrefab)
          Instantiate(_shootEffectPrefab, _shootPoint.position, _shootPoint.rotation);
        if (_bulletPrefab)
        {
          var bullet = Instantiate(_bulletPrefab, _shootPoint.transform.position, transform.rotation);
        }

        _audio.Shoot();
      }
      else
      {
        foreach (var hitPoint in _hitPoints)
        {
          if (!Hit(out var hit, hitPoint)) continue;
          PhysicsDebug.DrawDebug(hitPoint.position, Cleavage, AttackTime);
          if (!hit.CompareTag(PlayerTag)) return;
          hit.transform.parent.GetComponent<IHealth>().TakeDamage(Damage);

          _audio.Attack();
        }
      }
    }

    private void OnAttackEnded()
    {
      _agent.speed = _savedSpeed;
      _attackCooldown = AttackCooldown;
      _isAttacking = false;
      if (Type == EnemyTypeId.Ranged) _audio.Reload();
      Completed?.Invoke();
    }

    private void OnHit() =>
      _agent.speed = 0;

    private void OnHitEnded()
    {
      _agent.speed = _savedSpeed;
      Completed?.Invoke();
    }

    public void EnableAttack() =>
      _attackIsActive = true;

    public void DisableAttack() =>
      _attackIsActive = false;

    private void StartAttack()
    {
      transform.LookAt(_playerTransform);
      if (Type == EnemyTypeId.Ranged)
        _animate.PlayShoot();
      else
        _animate.PlayAttack();
      _isAttacking = true;
    }

    private bool Hit(out Collider hit, Transform point)
    {
      var hitsCount = Physics.OverlapSphereNonAlloc(point.position, Cleavage, _hits, _layerMask);
      hit = _hits.FirstOrDefault();
      return hitsCount > 0;
    }

    private bool CooldownIsUp() =>
      _attackCooldown <= 0;

    private bool CanAttack() =>
      _attackIsActive && !_isAttacking && CooldownIsUp();

    private void UpdateCooldown()
    {
      if (!CooldownIsUp())
        _attackCooldown -= Time.deltaTime;
    }
  }
}