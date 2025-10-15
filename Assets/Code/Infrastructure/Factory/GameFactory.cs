using Cinemachine;
using Code.Actors.Hero;
using Code.Infrastructure.AssetManagement;
using Code.Services.Async;
using Code.Services.Input;
using Code.Services.Progress;
using Code.Services.Random;
using Code.Services.StaticData;
using Code.Services.Time;
using Code.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public Transform HeroTransform { get; set; }
    public RectTransform UIRoot { get; set; }
    public Transform StartPoint { get; set; }
    public Transform ProjectilesPool { get; set; }
    public Camera MainCamera { get; set; }
    public CinemachineVirtualCamera VirtualCamera { get; set; }

    private readonly IAssets _assets;
    private readonly IRandomService _random;
    private readonly IProgressService _progress;
    private readonly IStaticDataService _staticData;
    private readonly IInputService _input;
    private readonly ITimeService _time;
    private readonly IAsyncService _async;

    private readonly Queue<GameObject> _bulletsPool = new();
    private readonly Queue<GameObject> _enemySmallPool = new();
    private readonly Queue<GameObject> _enemyBigPool = new();
    private readonly Queue<GameObject> _enemyRangedPool = new();

    public GameFactory(IAssets assets, IRandomService random, IProgressService progress, IStaticDataService staticData,
      IInputService input, ITimeService time, IAsyncService async)
    {
      _assets = assets;
      _random = random;
      _progress = progress;
      _staticData = staticData;
      _input = input;
      _time = time;
      _async = async;
    }

    public GameObject GetBullet(Transform shootPoint)
    {
      if (_bulletsPool.Count == 0)
        return CreateBullet(shootPoint.position);

      var bullet = _bulletsPool.Dequeue();
      bullet.transform.position = shootPoint.position;
      bullet.SetActive(true);
      return bullet;
    }

    public void PutBullet(GameObject bullet)
    {
      bullet.SetActive(false);
      _bulletsPool.Enqueue(bullet);
    }

    public GameObject CreateHud() =>
      _assets.Instantiate(AssetPath.HUDPath, UIRoot);

    public GameObject CreateHero()
    {
      var go = _assets.Instantiate(AssetPath.HeroPath, StartPoint);
      HeroTransform = go.transform;
      var data = _staticData.GetHero();
      if (go.TryGetComponent<HeroMove>(out var move))
      {
        move.Construct(_input, _time);
        move.Speed = data.MoveSpeed;
        move.MainCamera = MainCamera;
      }
      if (go.TryGetComponent<HeroLook>(out var look))
      {
        VirtualCamera.Follow = look.ViewPoint;
        if (VirtualCamera.TryGetComponent(out CinemachinePovExtension pov))
        {
          pov.Speed = data.LookSpeed;
          pov.ResetDirection(HeroTransform.forward);
          look.Init(MainCamera, pov);
        }
      }
      if (go.TryGetComponent<HeroHealth>(out var health))
        health.Construct(_progress.Progress);
      if (go.TryGetComponent<HeroDeath>(out var death))
        death.Construct(_progress.Progress);
      if (go.TryGetComponent<HeroShoot>(out var shoot))
      {
        shoot.Construct(this, _input, _async);
        shoot.Damage = data.Damage;
        shoot.ShootDelay = data.AttackCooldown;
        shoot.BulletSpeed = data.ProjectileSpeed;
        shoot.ShotDistance = data.AttackDistance;
        shoot.Init();
      }
      return go;
    }
    
    public GameObject GetEnemy(EnemyTypeId type, Transform at)
    {
      var pool = SelectEnemyPool(type);
      if (pool.Count == 0)
        return CreateEnemy(type, at);

      var enemy = pool.Dequeue();
      enemy.transform.position = at.position;
      enemy.transform.SetParent(at);
      enemy.SetActive(true);
      return enemy;
    }

    public void PutEnemy(EnemyTypeId type, GameObject enemy)
    {
      var pool = SelectEnemyPool(type);
      pool.Enqueue(enemy);
      enemy.SetActive(false);
    }

    public void CleanUp()
    {
    }

    private GameObject CreateEnemy(EnemyTypeId type, Transform at)
    {
      var enemy = _staticData.GetEnemy(type);
      var go = _assets.Instantiate(enemy.Prefab, at);
      return go;
    }

    private GameObject CreateBullet(Vector3 at)
    {
      var go = _assets.Instantiate(AssetPath.BulletPath);
      go.transform.position = at;
      go.transform.SetParent(ProjectilesPool);
      return go;
    }

    private Queue<GameObject> SelectEnemyPool(EnemyTypeId type) =>
      type switch
      {
        EnemyTypeId.SmallMelee => _enemySmallPool,
        EnemyTypeId.BigMelee => _enemyBigPool,
        EnemyTypeId.Ranged => _enemyRangedPool,
        _ => new Queue<GameObject>()
      };
  }
}