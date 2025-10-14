using Cinemachine;
using Code.Services;
using Code.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    GameObject CreateEnemy(EnemyTypeId type);
    Transform HeroTransform { get; set; }
    RectTransform UIRoot { get; set; }
    Transform StartPoint { get; set; }
    Camera MainCamera { get; set; }
    CinemachineVirtualCamera VirtualCamera { get; set; }
    void CleanUp();
    GameObject CreateHud();
    GameObject CreateHero();
    void SetScene(Scene scene);
    Scene GetScene();
  }
}