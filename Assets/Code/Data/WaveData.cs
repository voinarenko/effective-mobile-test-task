using System;

namespace Code.Data
{
  [Serializable]
  public class WaveData
  {
    public event Action<int> WaveChanged;
    public event Action<int> EnemyChanged;

    public int CurrentWave { get; set; } = 1;
    public int CurrentEnemies { get; set; }

    public void NextWave()
    {
      CurrentWave++;
      WaveChanged?.Invoke(CurrentWave);
    }

    public void AddEnemy()
    {
      CurrentEnemies++;
      EnemyChanged?.Invoke(CurrentEnemies);
    }

    public void RemoveEnemy()
    {
      CurrentEnemies--;
      EnemyChanged?.Invoke(CurrentEnemies);
    }
  }
}