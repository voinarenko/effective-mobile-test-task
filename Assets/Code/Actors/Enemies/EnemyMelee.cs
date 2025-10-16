using Code.Actors.Enemies.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Actors.Enemies
{
  public class EnemyMelee : MonoBehaviour, IMeleeAttack
  {
    private const string PlayerLayerMask = "Player";

    [SerializeField] private List<Transform> _hitPoints;
    private const string PlayerTag = "Player";
    private const float AttackTime = 0.1f;
    private int _layerMask;
    private readonly Collider[] _hits = new Collider[1];

    [SerializeField] private EnemyAudio _audio;
    private float _cleavage;
    private float _damage;

    public void Construct(float cleavage, float damage)
    {
      _cleavage = cleavage;
      _damage = damage;
    }

    private void Start() =>
      _layerMask = LayerMask.GetMask(PlayerLayerMask);

    public void Perform()
    {
      foreach (var hitPoint in _hitPoints)
      {
        if (!Hit(out var hit, hitPoint)) continue;
        PhysicsDebug.DrawDebug(hitPoint.position, _cleavage, AttackTime);
        if (!hit.CompareTag(PlayerTag)) return;
        if (hit.transform.parent.TryGetComponent<IHealth>(out var health)) 
          health.TakeDamage(_damage);

        _audio.Attack();
      }
    }

    private bool Hit(out Collider hit, Transform point)
    {
      var hitsCount = Physics.OverlapSphereNonAlloc(point.position, _cleavage, _hits, _layerMask);
      hit = _hits.FirstOrDefault();
      return hitsCount > 0;
    }
  }
}