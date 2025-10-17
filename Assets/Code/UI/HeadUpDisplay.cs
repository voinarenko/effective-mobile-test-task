using Code.Data;
using Code.UI.Elements;
using System;
using UnityEngine;

namespace Code.UI
{
  public class HeadUpDisplay : MonoBehaviour
  {
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Counter _wavesCounter;
    [SerializeField] private Counter _enemiesCounter;
    
    private PlayerProgress _progress;

    public void Construct(PlayerProgress progress) =>
      _progress = progress;

    private void OnDestroy()
    {
      _progress.HealthChanged -= UpdateHealth;
      _progress.WaveData.WaveChanged -= UpdateWavesCounter;
      _progress.WaveData.EnemyChanged -= UpdateEnemiesCounter;
    }

    public void Init()
    {
      UpdateHealth();
      _wavesCounter.UpdateCounter(_progress.WaveData.CurrentWave);
      _enemiesCounter.UpdateCounter(_progress.WaveData.CurrentEnemies);
      _progress.HealthChanged += UpdateHealth;
      _progress.WaveData.WaveChanged += UpdateWavesCounter;
      _progress.WaveData.EnemyChanged += UpdateEnemiesCounter;
    }

    private void UpdateHealth() =>
      _healthBar.SetValue(_progress.CurrentHealth, _progress.MaxHealth);

    private void UpdateEnemiesCounter(int count) =>
      _enemiesCounter.UpdateCounter(count);

    private void UpdateWavesCounter(int count) =>
      _wavesCounter.UpdateCounter(count);
  }
}