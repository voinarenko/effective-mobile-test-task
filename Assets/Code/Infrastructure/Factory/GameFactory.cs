using Code.Infrastructure.AssetManagement;
using Code.Services.Progress;
using Code.Services.Random;
using Code.Services.StaticData;
using Code.StaticData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public List<ISavedProgressReader> ProgressReaders { get; } = new();
    public List<ISavedProgress> ProgressWriters { get; } = new();
    public Transform HeroTransform { get; set; }
    public RectTransform UIRoot { get; set; }
    
    private readonly IAssets _assets;
    private readonly IRandomService _random;
    private readonly IProgressService _progress;
    private readonly IStaticDataService _staticData;
    private Scene _scene;

    public GameFactory(IAssets assets, IRandomService random, IProgressService progress, IStaticDataService staticData)
    {
      _assets = assets;
      _random = random;
      _progress = progress;
      _staticData = staticData;
    }

    public void SetScene(Scene scene) =>
      _scene = scene;

    public Scene GetScene() => _scene;

    public GameObject CreateHud() =>
      _assets.Instantiate(AssetPath.HUDPath, UIRoot);

    public GameObject CreateHero()
    {
      var go = _assets.Instantiate(AssetPath.HeroPath);
      HeroTransform = go.transform;
      return go;
    }

    public GameObject CreateEnemy(EnemyTypeId type)
    {
      var enemy = _staticData.GetEnemy(type);
      var go = Object.Instantiate(enemy.Prefab);
      return go;
    }

    public void CleanUp()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
    }
  }
}