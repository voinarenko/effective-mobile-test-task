using Code.Actors.Hero;
using Code.Services.Async;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Actors.Enemies
{
  public class EnemyMove : MonoBehaviour
  {
    public event Action Completed;
    
    public Transform HeroTransform { get; set; }
    public HeroDeath HeroDeath { get; set; }

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyAttack _attack;

    private bool _isMoving;
    private IAsyncService _async;

    public void Construct(IAsyncService async) =>
      _async = async;

    private void Update()
    {
      if (!_isMoving) return;
      if (!HeroTransform) return;
      
      SetDestinationForAgent();
      CheckDistance();
    }

    public async UniTaskVoid Init()
    {
      HeroDeath.Happened += HeroKilled;
      await _async.NextFrame();
      _isMoving = true;
    }

    private void SetDestinationForAgent()
    {
      if (HeroTransform) _agent.destination = HeroTransform.position;
    }

    private void HeroKilled()
    {
      HeroDeath.Happened -= HeroKilled;
      HeroTransform = null;
    }

    private void CheckDistance()
    {
      var dist = _agent.remainingDistance;
      if (!float.IsPositiveInfinity(dist) &&
          _agent.remainingDistance <= _agent.stoppingDistance &&
          HeroTransform)
      {
        _attack.EnableAttack();
        Completed?.Invoke();
      }
      else 
        _attack.DisableAttack();
    }
  }
}