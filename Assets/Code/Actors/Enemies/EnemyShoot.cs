using Code.Actors.Enemies.Interfaces;
using Code.Actors.Interfaces;
using Code.Infrastructure.Factory;
using DG.Tweening;
using UnityEngine;

namespace Code.Actors.Enemies
{
  public class EnemyShoot : MonoBehaviour, IShootAttack
  {
    [SerializeField] private Transform _shootPoint;

    private float _damage;
    private float _bulletSpeed;
    private float _shotDistance;

    [SerializeField] private EnemyAudio _audio;
    private IGameFactory _gameFactory;

    public void Construct(IGameFactory gameFactory, float damage, float bulletSpeed,
      float shotDistance)
    {
      _gameFactory = gameFactory;
      _damage = damage;
      _bulletSpeed = bulletSpeed;
      _shotDistance = shotDistance;
    }

    public void Perform()
    {
      var origin = _shootPoint.position;
      var direction = _shootPoint.forward;
      Vector3 target;

      if (Physics.Raycast(origin, direction, out var hit, _shotDistance))
      {
        var health = hit.collider.GetComponentInParent<IHealth>();
        health?.TakeDamage(_damage);
        target = hit.point;
      }
      else
        target = origin + direction * _shotDistance;

      var bullet = _gameFactory.GetBullet(_shootPoint);
      bullet.transform
        .DOMove(target, _bulletSpeed)
        .SetSpeedBased()
        .SetEase(Ease.Linear)
        .SetLink(bullet, LinkBehaviour.KillOnDisable)
        .OnComplete(() => _gameFactory.PutBullet(bullet));

      _audio.Shoot();
    }
  }
}