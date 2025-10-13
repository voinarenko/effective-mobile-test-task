using System;

namespace Code.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public int Level = 1;

    public void Reset()
    {
      Level = 1;
    }
  }
}