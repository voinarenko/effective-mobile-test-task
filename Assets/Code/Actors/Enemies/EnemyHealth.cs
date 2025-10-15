using System;
using UnityEngine;

namespace Code.Actors.Enemies
{
  [RequireComponent(typeof(EnemyAnimator))]
  public class EnemyHealth : MonoBehaviour, IHealth
  {
    public event Action HealthChanged;

    [SerializeField] private EnemyAnimator _animator;

    public float Current { get; set; }
    public float Max { get; set; }

    public void TakeDamage(float damage)
    {
      Current -= damage;
      _animator.PlayHit();

      HealthChanged?.Invoke();
    }
  }
}