using Code.Actors.Bullet;
using Code.Bullet;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Actors.Hero
{
  [RequireComponent(typeof(HeroAnimate))]
  public class HeroShoot : MonoBehaviour
  {
    public float Damage { get; set; }
    public float ShootDelay { get; set; }
    public float ReloadDelay { get; set; }

    [SerializeField] private GameObject _shootEffectPrefab;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPoint;

    [SerializeField] private HeroAudio _heroAudio;
    [SerializeField] private HeroAnimate _heroAnimate;
    private PlayerInputActions _controls;

    private float _shootTime = float.MinValue;
    private float _shoot;

    public void Init(float damage, float shootDelay, float reloadDelay)
    {
      Damage = damage;
      ShootDelay = shootDelay;
      ReloadDelay = reloadDelay;
      _controls.Player.Attack.performed += Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
      if (Time.time < _shootTime + ShootDelay) return;

      _shootTime = Time.time;
      Fire();
    }

    private void Fire()
    {
      if (_shootEffectPrefab != null)
      {
        var effect = Instantiate(_shootEffectPrefab, _shootPoint.position, _shootPoint.rotation);
      }

      if (_bulletPrefab != null)
      {
        var bullet = Instantiate(_bulletPrefab, _shootPoint.transform.position, transform.rotation);
        bullet.TryGetComponent<BulletDamage>(out var bulletData);
        bulletData.Sender = tag;
        bulletData.Damage = Damage;
      }

      OnFire();
    }

    private void OnFire()
    {
      _heroAnimate.Shoot();
      _heroAudio.Shoot();
    }
    
    #region Animation methods

#pragma warning disable IDE0051
    private void OnAttackStart()
    {
    }

    private void OnAttack()
    {
    }

    private void OnAttackEnded()
    {
    }

    private void OnHit()
    {
    }

    private void OnHitEnded()
    {
    }
#pragma warning restore IDE0051

    #endregion
  }
}