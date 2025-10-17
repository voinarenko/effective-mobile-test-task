using Code.StaticData;
using System.Collections.Generic;

namespace Code.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void LoadEnemies();
    void LoadHero();
    EnemyStaticData GetEnemy(EnemyTypeId type);
    HeroStaticData GetHero();
    Dictionary<EnemyTypeId, EnemyStaticData> GetEnemies();
    void LoadLevel();
    LevelStaticData GetLevel();
  }
}