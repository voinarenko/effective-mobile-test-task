using Code.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
    private HeroStaticData _hero;
    private LevelStaticData _level;

    public void LoadHero() => _hero = Resources.Load<HeroStaticData>("StaticData/HeroData");
    public void LoadLevel() => _level = Resources.Load<LevelStaticData>("StaticData/LevelData");

    public void LoadEnemies() =>
      _enemies = Resources
        .LoadAll<EnemyStaticData>("StaticData/Enemies")
        .ToDictionary(x => x.EnemyTypeId, x => x);

    public Dictionary<EnemyTypeId, EnemyStaticData> GetEnemies() => _enemies;
    
    public EnemyStaticData GetEnemy(EnemyTypeId type) => _enemies[type];

    public HeroStaticData GetHero() => _hero;
    
    public LevelStaticData GetLevel() => _level;
  }
}