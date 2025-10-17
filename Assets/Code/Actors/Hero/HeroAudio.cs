using UnityEngine;

namespace Code.Actors.Hero
{
  public class HeroAudio : MonoBehaviour
  {
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _shoot;
    [SerializeField] private AudioClip _hit;
    [SerializeField] private AudioClip _dead;
    
    public void Shoot() =>
      _audioSource.PlayOneShot(_shoot);
    
    public void Hit() =>
      _audioSource.PlayOneShot(_hit);
    
    public void Dead() =>
      _audioSource.PlayOneShot(_dead);
  }
}