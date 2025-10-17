using Code.Data;
using Code.Services.StaticData;
using Code.UI.Elements;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
  public class HeadUpDisplay : MonoBehaviour
  {
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Counter _wavesCounter;
    [SerializeField] private Counter _enemiesCounter;
    [SerializeField] private Image _waveTimer;

    private PlayerProgress _progress;
    private IStaticDataService _staticData;

    public void Construct(PlayerProgress progress, IStaticDataService staticData)
    {
      _progress = progress;
      _staticData = staticData;
    }

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
      ShowWaveTimer();
      _progress.HealthChanged += UpdateHealth;
      _progress.WaveData.WaveChanged += UpdateWavesCounter;
      _progress.WaveData.EnemyChanged += UpdateEnemiesCounter;
    }

    private void UpdateHealth() =>
      _healthBar.SetValue(_progress.CurrentHealth, _progress.MaxHealth);

    private void UpdateEnemiesCounter(int count) =>
      _enemiesCounter.UpdateCounter(count);

    private void UpdateWavesCounter(int count)
    {
      _wavesCounter.UpdateCounter(count);
      ShowWaveTimer();
    }

    private void ShowWaveTimer() =>
      _waveTimer
        .DOFillAmount(1, _staticData.GetLevel().WavePause)
        .SetEase(Ease.Linear)
        .OnComplete(() => _waveTimer.fillAmount = 0);
  }
}