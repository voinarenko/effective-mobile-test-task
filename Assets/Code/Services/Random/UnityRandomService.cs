using System.Collections.Generic;
using System.Linq;

namespace Code.Services.Random
{
  public class UnityRandomService : IRandomService
  {
    private static readonly List<WeightedValue> BlockValues = new()
    {
      new WeightedValue { Value = -2 }, // level bonus
      new WeightedValue { Value = -1 }, // common bonus
      new WeightedValue { Value = 0 }, // obstacle
      new WeightedValue { Value = 1 }, // fire
      new WeightedValue { Value = 2 }, // wind
      new WeightedValue { Value = 3 }, // earth
      new WeightedValue { Value = 4 }, // water
      new WeightedValue { Value = 5 } // kutu
    };

    public float Range(float inclusiveMin, float inclusiveMax) =>
      UnityEngine.Random.Range(inclusiveMin, inclusiveMax);

    public int Range(int inclusiveMin, int exclusiveMax) =>
      UnityEngine.Random.Range(inclusiveMin, exclusiveMax);

    public int WeightedRange(int inclusiveMin, int exclusiveMax)
    {
      var validValues = BlockValues
        .Where(v => v.Value >= inclusiveMin && v.Value < exclusiveMax)
        .ToList();

      var weightedPairs = validValues
        .Select(v => new { Value = v.Value, Weight = GetDynamicWeight(v.Value, exclusiveMax) })
        .ToList();

      var totalWeight = weightedPairs.Sum(v => v.Weight);
      var randomWeight = Range(0, totalWeight);

      foreach (var pair in weightedPairs)
      {
        if (randomWeight < pair.Weight)
          return pair.Value;
        randomWeight -= pair.Weight;
      }

      return weightedPairs[^1].Value;
    }
    
    private static int GetDynamicWeight(int value, int exclusiveMax)
    {
      if (value is -2 or -1)
        return 2;

      return exclusiveMax switch
      {
        2 => 28,
        3 => value == 0 ? 20 : 18,
        4 => 14,
        5 => value == 0 ? 12 : 11,
        6 => value == 0 ? 11 : 9,
        _ => 0
      };
    }
  }
}