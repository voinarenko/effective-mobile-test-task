using Code.Infrastructure;
using System;
using UnityEngine;

namespace Code.Data
{
  [Serializable]
  public class WaveData
  {
    public event Action<int> WaveChanged;
    
    public int Encountered;
    public event Action EnemyRemoved;

    // private const string WaveChangerTag = "WaveChanger";
    private int _currentEnemies;

    public void NextWave()
    {
      Encountered++;
      WaveChanged?.Invoke(Encountered);
      // if (Encountered > 1) UpdateHudData();
    }

    public void AddEnemy() =>
      _currentEnemies++;

    public void RemoveEnemy()
    {
      _currentEnemies--;
      EnemyRemoved?.Invoke();
    }

    public int GetEnemies() => _currentEnemies;

    // private void UpdateHudData()
    // {
    //     var hudConnectors = GameObject.FindWithTag(WaveChangerTag).GetComponent<PlayersWatcher>().GetConnectors();
    //     foreach (var hudConnector in hudConnectors) 
    //         hudConnector.WaveNumber = Encountered;
    // }
  }
}