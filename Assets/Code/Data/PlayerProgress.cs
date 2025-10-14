using System;

namespace Code.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public event Action HealthChanged;

    public int Level = 1;

    public float CurrentHealth
    {
      get => _currentHealth;
      set
      {
        _currentHealth = value;
        HealthChanged?.Invoke();
      }
    }

    public float MaxHealth;

    private float _currentHealth;


    public void Reset()
    {
      Level = 1;
    }
  }
}