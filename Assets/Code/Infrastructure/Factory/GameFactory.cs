using Cinemachine;
using Code.Actors.Enemies;
using Code.Actors.Hero;
using Code.Infrastructure.AssetManagement;
using Code.Services.Async;
using Code.Services.Input;
using Code.Services.Progress;
using Code.Services.Random;
using Code.Services.StaticData;
using Code.Services.Time;
using Code.StaticData;
using Code.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private HeroDeath _heroDeath;

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

    public GameObject CreateHud()
    {
      var go = _assets.Instantiate(AssetPath.HUDPath, UIRoot);
      if (go.TryGetComponent<HeadUpDisplay>(out var display))
      {
        display.Construct(_progress.Progress, _staticData);
        display.Init();
      }
      return go;
    }

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
      {
        health.Construct(_progress.Progress, _staticData);
        health.Current = data.Health;
        health.Max = data.Health;
      }
      if (go.TryGetComponent<HeroDeath>(out var death))
      {
        death.Construct(_progress.Progress, _async);
        _heroDeath = death;
        _progress.TrackHeroDeath(death);
      }
      if (go.TryGetComponent<HeroShoot>(out var shoot))
      {
        shoot.Construct(this, _input, _async, _staticData, _progress.Progress);
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

      var enemyGo = pool.Dequeue();
      enemyGo.transform.position = at.position;
      enemyGo.transform.SetParent(at);
      var enemy = _staticData.GetEnemy(type);
      var level = _staticData.GetLevel();
      RefreshEnemyHealth(enemyGo, enemy, level);
      if (enemyGo.TryGetComponent<EnemyAttack>(out var attack))
        attack.Damage = enemy.Damage +
                        enemy.Damage * level.EnemyBoostFactor * (_progress.Progress.WaveData.CurrentWave - 1);
      if (enemyGo.TryGetComponent<EnemyDeath>(out var death))
        death.Reactivate();
      enemyGo.SetActive(true);
      return enemyGo;
    }

    public void PutEnemy(EnemyTypeId type, GameObject enemy)
    {
      var pool = SelectEnemyPool(type);
      pool.Enqueue(enemy);
      enemy.SetActive(false);
    }

    public void CleanUp()
    {
      _bulletsPool.Clear();
      _enemySmallPool.Clear();
      _enemyBigPool.Clear();
      _enemyRangedPool.Clear();
    }

    private GameObject CreateEnemy(EnemyTypeId type, Transform at)
    {
      var enemy = _staticData.GetEnemy(type);
      var level = _staticData.GetLevel();
      var go = _assets.Instantiate(enemy.Prefab, at);

      if (go.TryGetComponent<EnemyMove>(out var move))
      {
        move.HeroTransform = HeroTransform;
        move.HeroDeath = _heroDeath;
        move.Construct(_async);
        move.Init().Forget();
      }
      RefreshEnemyHealth(go, enemy, level);
      if (go.TryGetComponent<NavMeshAgent>(out var agent))
      {
        agent.speed = enemy.MoveSpeed;
        agent.stoppingDistance = enemy.StoppingDistance;
        agent.angularSpeed = enemy.RotateSpeed;
        agent.acceleration = enemy.Acceleration;
        agent.destination = HeroTransform.position;
      }
      if (go.TryGetComponent<EnemyAttack>(out var attack))
      {
        attack.Construct(this, _time, HeroTransform);
        attack.Type = enemy.EnemyTypeId;
        attack.Damage = enemy.Damage +
                        enemy.Damage * level.EnemyBoostFactor * (_progress.Progress.WaveData.CurrentWave - 1);
        attack.Cleavage = enemy.Cleavage;
        attack.AttackCooldown = enemy.AttackCooldown;
        attack.BulletSpeed = enemy.BulletSpeed;
        attack.ShotDistance = enemy.ShotDistance;
        attack.UpdateSpecificData();
      }
      if (go.TryGetComponent<EnemyDeath>(out var death))
        death.Construct(this, _progress.Progress, _async);
      return go;
    }

    private void RefreshEnemyHealth(GameObject go, EnemyStaticData enemy, LevelStaticData level)
    {
      if (go.TryGetComponent<EnemyHealth>(out var health))
      {
        health.Max = enemy.Health +
                     enemy.Health * level.EnemyBoostFactor * (_progress.Progress.WaveData.CurrentWave - 1);
        health.Current = health.Max;
      }
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