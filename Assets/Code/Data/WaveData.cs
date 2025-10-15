using System;

namespace Code.Data
{
  [Serializable]
  public class WaveData
  {
    public event Action<int> WaveChanged;
    
    public int CurrentWave = 1;
    public event Action EnemyRemoved;

    private int _currentEnemies;

    public void NextWave()
    {
      CurrentWave++;
      WaveChanged?.Invoke(CurrentWave);
    }

    public void AddEnemy() =>
      _currentEnemies++;

    public void RemoveEnemy()
    {
      _currentEnemies--;
      EnemyRemoved?.Invoke();
    }

    public int GetEnemies() => _currentEnemies;
  }
}