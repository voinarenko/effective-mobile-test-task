using Code.StaticData;

namespace Code.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void LoadEnemies();
    void LoadHero();
    EnemyStaticData GetEnemy(EnemyTypeId type);
    HeroStaticData GetHero();
  }
}