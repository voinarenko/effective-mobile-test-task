using Code.Actors.Enemies.Spawn;
using Code.Services.Progress;
using System;
using System.Collections.Generic;

namespace Code.Services.Wave
{
  public class WaveService : IWaveService, IDisposable
  {
    public List<SpawnPoint> SpawnPoints { get; set; }

    private readonly IProgressService _progress;

    public WaveService(IProgressService progress) =>
      _progress = progress;

    public void Init()
    {
      _progress.Progress.WaveData.EnemyChanged += CheckEnemies;
      SpawnEnemies();
    }

    public void Dispose() =>
      _progress.Progress.WaveData.EnemyChanged -= CheckEnemies;

    private void SpawnEnemies()
    {
      for (var i = 0; i < _progress.Progress.WaveData.CurrentWave; i++)
        foreach (var point in SpawnPoints)
          point.Spawn();
    }

    private void CheckEnemies(int count)
    {
      if (count > 0) return;
      _progress.Progress.WaveData.NextWave();
      SpawnEnemies();
    }
  }
}