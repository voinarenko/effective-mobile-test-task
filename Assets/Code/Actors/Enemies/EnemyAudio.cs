using Code.StaticData;
using UnityEngine;

namespace Code.Actors.Enemies
{
  public class EnemyAudio : MonoBehaviour
  {
    [SerializeField] private EnemyAttack _enemyAttack;

    public void FootStep()
    {
      switch (_enemyAttack.Type)
      {
        case EnemyTypeId.SmallMelee:
          
          break;
        case EnemyTypeId.BigMelee:
          
          break;
        case EnemyTypeId.Ranged:
          
          break;
      }
    }

    public void Attack()
    {
      switch (_enemyAttack.Type)
      {
        case EnemyTypeId.SmallMelee:
          
          break;
        case EnemyTypeId.BigMelee:
          
          break;
      }
    }

    public void Shoot()
    {
    }

    public void PlayReload()
    {
    }
  }
}