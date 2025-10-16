using Code.StaticData;
using Code.StaticData.Windows;
using Code.UI.Services.Windows;
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
    void LoadWindows();
    WindowConfig GetWindow(WindowId windowId);
  }
}