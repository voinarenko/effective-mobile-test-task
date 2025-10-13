using UnityEngine;

namespace Code.Actors.Bullet
{
  public class BulletDamage : MonoBehaviour
  {
    public float Damage { get; set; }
    public string Sender { get; set; }

    private const string PlayerTag = "Player";
    private const string EnemyTag = "Enemy";
    private const string WallTag = "Wall";

    [SerializeField] private GameObject _hitFxPrefab;

    private bool _collided;

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag(WallTag)) DestroySelf();
      if (other.CompareTag(Sender)) return;
      if (!other.transform.CompareTag(EnemyTag) && !other.transform.CompareTag(PlayerTag)) return;
      if (_collided) return;
      _collided = true;
      if (other.transform.parent.TryGetComponent<IHealth>(out var health))
        health.TakeDamage(Damage);
      Hit();
      DestroySelf();
    }

    private void Hit()
    {
      var effect = Instantiate(_hitFxPrefab, transform.position, Quaternion.identity);
    }

    private void DestroySelf() =>
      Destroy(gameObject);
  }
}