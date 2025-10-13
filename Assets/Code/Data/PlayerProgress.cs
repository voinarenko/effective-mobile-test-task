using System;

namespace Code.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public int Level = 1;
    public float CurrentHealth;
    public float MaxHealth;

    public void Reset()
    {
      Level = 1;
    }
  }
}