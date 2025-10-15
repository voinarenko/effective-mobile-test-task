using UnityEngine;

namespace Code.Actors.Enemies
{
  public class EnemyBehavior : MonoBehaviour
  {
    public EnemyMoveToHero Mover { get; private set; }
    public IHealth HeroHealth { get; set; }

    [SerializeField] private EnemyAttack _attacker;

    private void Start()
    {
      Mover = GetComponent<EnemyMoveToHero>();
    }

    private void OnDestroy()
    {
    }

    public void DoMove()
    {
      Mover.SetDestinationForAgent();
      _attacker.DisableAttack();
    }

    public void DoAttack() =>
      _attacker.EnableAttack();

    public void DoWait() =>
      _attacker.DisableAttack();
  }
}