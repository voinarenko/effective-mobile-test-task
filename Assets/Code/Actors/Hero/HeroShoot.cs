using Code.Infrastructure.Factory;
using Code.Services.Async;
using Code.Services.Input;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Actors.Hero
{
  [RequireComponent(typeof(HeroAnimate))]
  public class HeroShoot : MonoBehaviour
  {
    public float Damage { get; set; }
    public float ShootDelay { get; set; }
    public float BulletSpeed { get; set; }

    public float ShotDistance { get; set; }

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private HeroAudio _heroAudio;

    private IGameFactory _gameFactory;
    private IInputService _input;
    private IAsyncService _async;

    private bool _shootButtonHeld;

    public void Construct(IGameFactory gameFactory, IInputService input, IAsyncService async)
    {
      _async = async;
      _input = input;
      _gameFactory = gameFactory;
    }

    public void Init()
    {
      var attack = _input.GetActions().Player.Attack;
      attack.performed += OnAttackPressed;
      attack.canceled += OnAttackReleased;
    }

    private void OnAttackPressed(InputAction.CallbackContext context)
    {
      if (_shootButtonHeld) return;

      _shootButtonHeld = true;
      ShootLoopAsync().Forget();
    }

    private void OnAttackReleased(InputAction.CallbackContext context) =>
      _shootButtonHeld = false;

    private async UniTaskVoid ShootLoopAsync()
    {
      while (_shootButtonHeld)
      {
        Fire();
        await _async.WaitForSeconds(ShootDelay);
      }
    }

    private void Fire()
    {
      var origin = _shootPoint.position;
      var direction = _shootPoint.forward;
      Vector3 target;

      if (Physics.Raycast(origin, direction, out var hit, ShotDistance))
      {
        // var health = hit.collider.GetComponentInParent<IHealth>();
        // health?.TakeDamage(Damage);
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
        .OnComplete(() => _gameFactory.PutBullet(bullet));

      OnFire();
    }

    private void OnFire() =>
      _heroAudio.Shoot();
  }
}