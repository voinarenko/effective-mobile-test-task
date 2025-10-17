using Code.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string StaticDataHeroPath = "StaticData/HeroData";
    private const string StaticDataLevelPath = "StaticData/LevelData";
    private const string StaticDataEnemiesPath = "StaticData/Enemies";
    private const string StaticDataWindowsPath = "StaticData/WindowStaticData";
    private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
    private HeroStaticData _hero;
    private LevelStaticData _level;

    public void LoadHero() =>
      _hero = Resources.Load<HeroStaticData>(StaticDataHeroPath);

    public void LoadLevel() => _level = Resources.Load<LevelStaticData>(StaticDataLevelPath);

    public void LoadEnemies() =>
      _enemies = Resources
        .LoadAll<EnemyStaticData>(StaticDataEnemiesPath)
        .ToDictionary(x => x.EnemyTypeId, x => x);

    public Dictionary<EnemyTypeId, EnemyStaticData> GetEnemies() => _enemies;
    
    public EnemyStaticData GetEnemy(EnemyTypeId type) => _enemies[type];

    public HeroStaticData GetHero() => _hero;
    
    public LevelStaticData GetLevel() => _level;
  }
}