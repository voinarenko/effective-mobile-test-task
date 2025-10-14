using Code.Data;
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
    [SerializeField] private HeroLook _look;
    [SerializeField] private HeroShoot _attack;
    [SerializeField] private HeroAnimate _animator;

    private bool _isDead;
    private PlayerProgress _progress;

    public void Construct(PlayerProgress progress)
    {
      _progress = progress;
    }
    
    private void Start() =>
      _progress.HealthChanged += OnHealthChanged;

    private void OnDestroy() =>
      _progress.HealthChanged -= OnHealthChanged;

    private void OnHealthChanged()
    {
      if (!_isDead && _health.Current <= 0) 
        Die();
    }

    private void Die()
    {
      _isDead = true;
      _move.enabled = false;
      _look.enabled = false;
      _attack.enabled = false;
      tag = DeadTag;
      Happened?.Invoke(this);
    }

#pragma warning disable IDE0051
    private void OnDeath() =>
      _agent.isStopped = true;
#pragma warning restore IDE0051
  }
}