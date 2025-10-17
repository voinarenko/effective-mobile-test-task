using Code.StaticData;
using UnityEngine;

namespace Code.Actors.Enemies
{
  public class EnemyAudio : MonoBehaviour
  {
    [SerializeField] private EnemyAttack _enemyAttack;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _smallMelee;
    [SerializeField] private AudioClip _bigMelee;
    [SerializeField] private AudioClip _shoot;
    [SerializeField] private AudioClip _reload;
    [SerializeField] private AudioClip _death;

    public void FootStep()
    {
    }

    public void Melee()
    {
      switch (_enemyAttack.Type)
      {
        case EnemyTypeId.SmallMelee:
          _audioSource.PlayOneShot(_smallMelee);
          break;
        case EnemyTypeId.BigMelee:
          _audioSource.PlayOneShot(_bigMelee);
          break;
      }
    }

    public void Shoot()
    {
      _audioSource.PlayOneShot(_shoot);
    }

    public void Reload()
    {
      _audioSource.PlayOneShot(_reload);
    }

    public void Death()
    {
      _audioSource.PlayOneShot(_death);
    }
  }
}