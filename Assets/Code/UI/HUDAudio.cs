using UnityEngine;

namespace Code.UI
{
  public class HUDAudio : MonoBehaviour
  {
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _waveSound;

    public void PlayNewWaveSound() => _audioSource.PlayOneShot(_waveSound);
  }
}