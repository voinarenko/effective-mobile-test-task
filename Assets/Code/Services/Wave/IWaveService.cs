using Code.Actors.Enemies.Spawn;
using System.Collections.Generic;

namespace Code.Services.Wave
{
  public interface IWaveService : IService
  {
    List<SpawnPoint> SpawnPoints { get; set; }
    void Init();
  }
}