using UnityEngine;

namespace Code.Actors.Hero
{
  public class HeroAudio : MonoBehaviour
  {
    [SerializeField] private HeroDirectionFinder _heroDirectionFinder;

    public void FootStep(int index)
    {
      // if (index == _heroDirectionFinder.GetDirection())
      //   FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Move/PlayerFootStep", GetComponent<Transform>().position);
    }

    public void Shoot()
    {
      // FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Attack/PlayerShoot", GetComponent<Transform>().position);
    }
  }
}