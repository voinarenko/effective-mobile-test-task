using Code.Actors.Hero;
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

    private void Update()
    {
      if (!HeroTransform) return;
      
      SetDestinationForAgent();
      CheckDistance();
    }

    public void Init() =>
      HeroDeath.Happened += HeroKilled;

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