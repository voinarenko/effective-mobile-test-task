using Code.Infrastructure.Factory;
using Code.Services.Progress;
using Code.Services.Random;
using UnityEngine;
using Zenject;

namespace Code.Actors.Enemies.Spawn
{
  public class SpawnPoint : MonoBehaviour
  {
    private IGameFactory _factory;
    private IRandomService _random;
    private IProgressService _progress;

    [Inject]
    public void Construct(IGameFactory factory, IRandomService random, IProgressService progress)
    {
      _factory = factory;
      _random = random;
      _progress = progress;
    }

    public void Spawn()
    {
      _factory.GetEnemy(_random.WeightedRange(), transform);
      _progress.Progress.WaveData.AddEnemy();
    }
  }
}