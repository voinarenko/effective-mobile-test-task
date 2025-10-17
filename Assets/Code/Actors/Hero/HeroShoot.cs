using Code.Actors.Interfaces;
using Code.Data;
using Code.Infrastructure.Factory;
using Code.Services.Async;
using Code.Services.Input;
using Code.Services.StaticData;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Actors.Hero
{
  public class HeroShoot : MonoBehaviour
  {
    public float Damage { get; set; }
    public float ShootDelay { get; set; }
    public float BulletSpeed { get; set; }

    public float ShotDistance { get; set; }

    [SerializeField] private Transform _shootPoint;
    [SerializeField] private HeroAudio _heroAudio;

    private IGameFactory _gameFactory;
    private IInputService _input;
    private IAsyncService _async;
    private IStaticDataService _staticData;

    private bool _isShooting;
    private bool _shootButtonHeld;
    private PlayerProgress _progress;
    private CancellationTokenSource _cts;

    public void Construct(IGameFactory gameFactory, IInputService input, IAsyncService async,
      IStaticDataService staticData, PlayerProgress progress)
    {
      _progress = progress;
      _staticData = staticData;
      _async = async;
      _input = input;
      _gameFactory = gameFactory;
      _progress.WaveData.WaveChanged += IncreaseDamage;
    }

    private void OnDestroy()
    {
      var attack = _input.GetActions().Player.Attack;
      attack.performed -= OnAttackPressed;
      attack.canceled -= OnAttackReleased;
      _progress.WaveData.WaveChanged -= IncreaseDamage;
      DOTween.KillAll();
    }

    public void Init()
    {
      var attack = _input.GetActions().Player.Attack;
      attack.performed += OnAttackPressed;
      attack.canceled += OnAttackReleased;
    }

    private void OnAttackPressed(InputAction.CallbackContext context)
    {
      _shootButtonHeld = true;

      if (!_isShooting)
        ShootLoopAsync().Forget();
    }

    private void OnAttackReleased(InputAction.CallbackContext context) =>
      _shootButtonHeld = false;

    private async UniTaskVoid ShootLoopAsync()
    {
      _isShooting = true;
      
      while (_shootButtonHeld)
      {
        Fire();
        await _async.WaitForSeconds(ShootDelay);
      }
      _isShooting = false;
    }

    private void Fire()
    {
      if (!_shootPoint) return;

      var origin = _shootPoint.position;
      var direction = _shootPoint.forward;
      Vector3 target;

      if (Physics.Raycast(origin, direction, out var hit, ShotDistance))
      {
        if (hit.transform.parent.TryGetComponent<IHealth>(out var health))
          health.TakeDamage(Damage);
        target = hit.point;
      }
      else
        target = origin + direction * ShotDistance;

      var bullet = _gameFactory.GetBullet(_shootPoint);
      bullet.transform
        .DOMove(target, BulletSpeed)
        .SetSpeedBased()
        .SetEase(Ease.Linear)
        .OnComplete(() => _gameFactory.PutBullet(bullet));

      _heroAudio.Shoot();
    }

    private void IncreaseDamage(int currentWave)
    {
      var heroData = _staticData.GetHero();
      var levelData = _staticData.GetLevel();
      Damage = heroData.Damage + heroData.Damage * levelData.HeroBoostFactor * (_progress.WaveData.CurrentWave - 1);
      BulletSpeed = heroData.ProjectileSpeed * (1 - levelData.HeroBoostFactor);
    }
  }
}