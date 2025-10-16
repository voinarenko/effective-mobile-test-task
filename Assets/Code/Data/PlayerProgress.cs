using System;

namespace Code.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public event Action HealthChanged;

    public WaveData WaveData = new();

    public float MaxHealth;
    public float CurrentHealth
    {
      get => _currentHealth;
      set
      {
        _currentHealth = value;
        HealthChanged?.Invoke();
      }
    }
    private float _currentHealth;

    public void Reset()
    {
    }
  }
}