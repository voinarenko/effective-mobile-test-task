using System;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Actors.Hero
{
  public class HeroDeath : MonoBehaviour
  {
    public event Action<HeroDeath> Happened;

    private const string DeadTag = "Dead";

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private HeroHealth _health;
    [SerializeField] private HeroMove _move;
    [SerializeField] private HeroRotate _rotate;
    [SerializeField] private HeroShoot _attack;
    [SerializeField] private HeroAnimate _animator;
    [SerializeField] private GameObject _deathFx;

    private bool _isDead;

    private void Start() =>
      _health.HealthChanged += HealthChanged;

    private void OnDestroy() =>
      _health.HealthChanged -= HealthChanged;

    private void HealthChanged()
    {
      if (!_isDead && _health.Current <= 0) Die();
    }

    private void Die()
    {
      _isDead = true;
      _move.enabled = false;
      _rotate.enabled = false;
      _attack.enabled = false;
      _animator.PlayDeath();
      tag = DeadTag;
      Happened?.Invoke(this);
    }

#pragma warning disable IDE0051
    private void OnDeath() =>
      _agent.isStopped = true;
#pragma warning restore IDE0051
  }
}