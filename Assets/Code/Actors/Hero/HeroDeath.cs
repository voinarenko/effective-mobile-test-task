using Code.Data;
using Code.Services.Async;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Actors.Hero
{
  public class HeroDeath : MonoBehaviour
  {
    public event Action Happened;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private HeroHealth _health;
    [SerializeField] private HeroMove _move;
    [SerializeField] private HeroLook _look;
    [SerializeField] private HeroShoot _attack;
    [SerializeField] private HeroAudio _audio;

    private bool _isDead;
    private PlayerProgress _progress;
    private IAsyncService _async;

    public void Construct(PlayerProgress progress, IAsyncService async)
    {
      _progress = progress;
      _async = async;
    }
    
    private void Start() =>
      _progress.HealthChanged += OnHealthChanged;

    private void OnDestroy() =>
      _progress.HealthChanged -= OnHealthChanged;

    private void OnHealthChanged()
    {
      if (!_isDead && _health.Current <= 0) 
        Die().Forget();
    }

    private async UniTaskVoid Die()
    {
      _isDead = true;
      _move.enabled = false;
      _look.enabled = false;
      _attack.enabled = false;
      _audio.Dead();
      await _async.WaitForSeconds(Constants.DeathDuration);
      Happened?.Invoke();
    }

    private void OnDeath() =>
      _agent.isStopped = true;
  }
}