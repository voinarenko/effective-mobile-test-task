using Code.Actors.Enemies.Spawn;
using Code.Services.Async;
using Code.Services.Progress;
using Code.Services.StaticData;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Code.Services.Wave
{
  public class WaveService : IWaveService, IDisposable
  {
    public List<SpawnPoint> SpawnPoints { get; set; }

    private readonly IProgressService _progress;
    private readonly IAsyncService _async;
    private readonly IStaticDataService _staticData;

    public WaveService(IProgressService progress, IAsyncService async, IStaticDataService staticData)
    {
      _progress = progress;
      _async = async;
      _staticData = staticData;
    }

    public void Init()
    {
      _progress.Progress.WaveData.EnemyChanged += CheckEnemies;
      SpawnEnemies().Forget();
    }

    public void Dispose() =>
      _progress.Progress.WaveData.EnemyChanged -= CheckEnemies;

    private async UniTaskVoid SpawnEnemies()
    {
      await _async.WaitForSeconds(_staticData.GetLevel().WavePause);
      for (var i = 0; i < _progress.Progress.WaveData.CurrentWave; i++)
        foreach (var point in SpawnPoints)
          point.Spawn();
    }

    private void CheckEnemies(int count)
    {
      if (count > 0) return;
      _progress.Progress.WaveData.NextWave();
      SpawnEnemies().Forget();
    }
  }
}