using Code.StaticData;
using System.Collections.Generic;

namespace Code.Services.Random
{
  public interface IRandomService
  {
    float Range(float inclusiveMin, float inclusiveMax);
    int Range(int inclusiveMin, int exclusiveMax);
    void FillWeights(Dictionary<EnemyTypeId, EnemyStaticData> data);
    EnemyTypeId WeightedRange();
  }
}