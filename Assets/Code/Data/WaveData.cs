using System;

namespace Code.Data
{
  [Serializable]
  public class WaveData
  {
    public event Action<int> WaveChanged;
    public event Action<int> EnemyChanged;

    public int CurrentWave = 1;

    private int _currentEnemies;

    public void NextWave()
    {
      CurrentWave++;
      WaveChanged?.Invoke(CurrentWave);
    }

    public void AddEnemy()
    {
      _currentEnemies++;
      EnemyChanged?.Invoke(_currentEnemies);
    }

    public void RemoveEnemy()
    {
      _currentEnemies--;
      EnemyChanged?.Invoke(_currentEnemies);
    }

    public int GetEnemies() => _currentEnemies;
  }
}