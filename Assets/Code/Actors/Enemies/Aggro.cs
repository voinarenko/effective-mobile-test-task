using Code.Actors.Hero;
using UnityEngine;

namespace Code.Actors.Enemies
{
  public class Aggro : MonoBehaviour
  {
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private EnemyMoveToHero _follow;

    private void Start()
    {
      _triggerObserver.TriggerEnter += TriggerEnter;
      _triggerObserver.TriggerExit += TriggerExit;

      SwitchFollow(true);
    }

    private void OnDestroy()
    {
      _triggerObserver.TriggerEnter -= TriggerEnter;
      _triggerObserver.TriggerExit -= TriggerExit;
    }

    private void TriggerEnter(Collider other)
    {
      _follow.InitTarget(other.GetComponentInParent<HeroHealth>().gameObject);
      _follow.PlayerNearby = true;
    }

    private void TriggerExit(Collider other) =>
      _follow.PlayerNearby = false;

    private void SwitchFollow(bool value) =>
      _follow.enabled = value;
  }
}