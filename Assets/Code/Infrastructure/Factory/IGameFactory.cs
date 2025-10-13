using Code.Services;
using Code.Services.Progress;
using Code.StaticData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    GameObject CreateEnemy(EnemyTypeId type);
    Transform HeroTransform { get; set; }
    RectTransform UIRoot { get; set; }
    void CleanUp();
    GameObject CreateHud();
    GameObject CreateHero();
    void SetScene(Scene scene);
    Scene GetScene();
  }
}