using Code.Actors.Enemies.Spawn;
using Code.Services.Progress;
using System.Collections.Generic;

namespace Code.Services.Wave
{
  public class WaveService : IWaveService
  {
    public List<SpawnPoint> SpawnPoints { get; set; }

    private readonly IProgressService _progressService;

    public WaveService(IProgressService progressService) =>
      _progressService = progressService;

    public void SpawnEnemies()
    {
      for (var i = 0; i < _progressService.Progress.WaveData.Encountered; i++)
        foreach (var point in SpawnPoints)
          point.Spawn();
    }
  }
}