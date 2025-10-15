using System;
using UnityEngine;

namespace Code.Actors.Enemies
{
  [RequireComponent(typeof(EnemyAnimate))]
  public class EnemyHealth : MonoBehaviour, IHealth
  {
    public event Action HealthChanged;

    [SerializeField] private EnemyAnimate animate;

    public float Current { get; set; }
    public float Max { get; set; }

    public void TakeDamage(float damage)
    {
      Current -= damage;
      animate.PlayHit();

      HealthChanged?.Invoke();
    }
  }
}