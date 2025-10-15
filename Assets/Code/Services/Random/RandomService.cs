using Code.StaticData;
using System.Collections.Generic;
using System.Linq;

namespace Code.Services.Random
{
  public class RandomService : IRandomService
  {
    private readonly List<WeightedValue> _enemyValues = new();

    public float Range(float inclusiveMin, float inclusiveMax) =>
      UnityEngine.Random.Range(inclusiveMin, inclusiveMax);

    public int Range(int inclusiveMin, int exclusiveMax) =>
      UnityEngine.Random.Range(inclusiveMin, exclusiveMax);

    public void FillWeights(Dictionary<EnemyTypeId, EnemyStaticData> data)
    {
      foreach (var pair in data)
        _enemyValues.Add(new WeightedValue { Value = pair.Key, Weight = pair.Value.SpawnWeight });
    }

    public EnemyTypeId WeightedRange()
    {
      var totalWeight = _enemyValues.Sum(item => item.Weight);
      var randomWeight = Range(0, totalWeight);

      foreach (var item in _enemyValues)
      {
        if (randomWeight < item.Weight)
          return item.Value;
        randomWeight -= item.Weight;
      }

      return _enemyValues[^1].Value;
    }
  }
}