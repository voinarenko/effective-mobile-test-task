using Cinemachine;
using Code.Actors.Enemies.Spawn;
using Code.Infrastructure.Factory;
using Code.Services.Wave;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class LevelInitializer : MonoBehaviour, IInitializable
  {
    private IGameFactory _gameFactory;
    private IWaveService _wave;

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _projectilesPool;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    [Inject]
    private void Construct(IGameFactory gameFactory, IWaveService wave)
    {
      _wave = wave;
      _gameFactory = gameFactory;
    }

    public void Initialize()
    {
      _gameFactory.StartPoint = _startPoint;
      _gameFactory.ProjectilesPool = _projectilesPool;
      _gameFactory.MainCamera = _mainCamera;
      _gameFactory.VirtualCamera = _virtualCamera;
      _wave.SpawnPoints = _spawnPoints;
    }
  }
}