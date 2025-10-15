using Code.Data;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Services.Progress;
using UnityEngine;

namespace Code.Services.Wave
{
  public class WaveChanger : MonoBehaviour
  {
    private IGameStateMachine _stateMachine;
    private IWaveService _waveService;
    private ISceneLoader _sceneLoader;

    private WaveData _waveData;

    public void Construct(IProgressService progressService, IGameStateMachine stateMachine, IWaveService waveService,
      ISceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _waveService = waveService;
      _waveData = progressService.Progress.WaveData;
      _waveData.EnemyRemoved += CheckEnemies;
      _sceneLoader = sceneLoader;
      DontDestroyOnLoad(this);
    }

    private void OnDestroy()
    {
      if (_waveData != null)
        _waveData.EnemyRemoved -= CheckEnemies;
    }

    private void CheckEnemies()
    {
      if (_waveData.GetEnemies() > 0) return;
      _waveData.NextWave();
      _waveService.SpawnEnemies();
    }
  }
}