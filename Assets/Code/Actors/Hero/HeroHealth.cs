using Code.Actors.Interfaces;
using Code.Data;
using Code.Services.StaticData;
using DG.Tweening;
using UnityEngine;

namespace Code.Actors.Hero
{
  public class HeroHealth : MonoBehaviour, IHealth
  {
    public float Max
    {
      get => _progress.MaxHealth;
      set => _progress.MaxHealth = value;
    }

    public float Current
    {
      get => _progress.CurrentHealth;
      set => _progress.CurrentHealth = value;
    }

    private PlayerProgress _progress;
    private IStaticDataService _staticData;

    public void Construct(PlayerProgress progress, IStaticDataService staticData)
    {
      _staticData = staticData;
      _progress = progress;
      _progress.WaveData.WaveChanged += Heal;
    }

    private void OnDestroy() =>
      _progress.WaveData.WaveChanged -= Heal;

    public void TakeDamage(float damage)
    {
      if (Current <= 0) return;
      Current -= damage;
    }

    private void Heal(int currentWave) =>
      DOTween.To(() => Current, x => Current = x, Max, _staticData.GetLevel().WavePause);
  }
}