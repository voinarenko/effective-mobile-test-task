using Code.Actors.Hero;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Actors.Enemies
{
  public class EnemyMoveToHero : MonoBehaviour
  {
    public bool PlayerNearby { get; set; }
    public NavMeshAgent Agent { get; private set; }
    public event Action Completed;


    private Transform _heroTransform;
    private EnemyBehavior _behavior;
    private EnemyAttack _attack;

    private void Start()
    {
      Agent = GetComponent<NavMeshAgent>();
      _behavior = GetComponent<EnemyBehavior>();
      _attack = GetComponent<EnemyAttack>();
    }

    private void Update()
    {
      if (!_heroTransform)
      {
        var player = FindTarget();
        if (player)
          InitTarget(player);
      }
      else
      {
        SetDestinationForAgent();
        if (PlayerNearby) CheckDistance();
      }
    }

    public void InitTarget(GameObject player)
    {
      _heroTransform = player.GetComponent<HeroMove>().transform;
      player.GetComponent<HeroDeath>().Happened += HeroKilled;
      _behavior.HeroHealth = player.GetComponent<HeroHealth>();
      _attack.Construct(_heroTransform);
    }

    public void SetDestinationForAgent()
    {
      if (_heroTransform)
        Agent.destination = _heroTransform.position;
    }
    
    private GameObject FindTarget()
    {
      GameObject target = null;
      var targets = GameObject.FindGameObjectsWithTag("Player").ToList();
      if (targets.Count > 0)
      {
        target = targets.FirstOrDefault()!.gameObject;
        if (targets.Count > 1 && target)
        {
          Agent.destination = target.transform.position;
          var distance = Agent.remainingDistance;
          foreach (var t in targets)
          {
            Agent.destination = t.transform.position;
            var newDistance = Agent.remainingDistance;
            if (newDistance < distance) target = t.gameObject;
          }
        }
      }
      return target;
    }

    private void HeroKilled(HeroDeath heroDeath)
    {
      heroDeath.Happened -= HeroKilled;
      _heroTransform = null;
    }

    private void CheckDistance()
    {
      var dist = Agent.remainingDistance;
      if (!float.IsPositiveInfinity(dist) && Agent.remainingDistance <= Agent.stoppingDistance &&
          _heroTransform)
        Completed?.Invoke();
    }
  }
}