using Cinemachine;
using Code.Services;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    GameObject CreateEnemy(EnemyTypeId type, Transform at);
    Transform HeroTransform { get; set; }
    RectTransform UIRoot { get; set; }
    Transform StartPoint { get; set; }
    Camera MainCamera { get; set; }
    CinemachineVirtualCamera VirtualCamera { get; set; }
    Transform ProjectilesPool { get; set; }
    void CleanUp();
    GameObject CreateHud();
    GameObject CreateHero();
    GameObject GetBullet(Transform shootPoint);
    void PutBullet(GameObject bullet);
  }
}