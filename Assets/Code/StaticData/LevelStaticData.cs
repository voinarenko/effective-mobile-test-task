using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
  public class LevelStaticData : ScriptableObject
  {
    public float EnemyBoostFactor = 0.1f;
    public float HeroBoostFactor = 0.2f;
  }
}