using Code.Actors.Hero;
using Code.Infrastructure.AssetManagement;
using Code.Services.Input;
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
    public Transform StartPoint { get; set; }
    public Camera MainCamera { get; set; }

    private readonly IAssets _assets;
    private readonly IRandomService _random;
    private readonly IProgressService _progress;
    private readonly IStaticDataService _staticData;
    private readonly IInputService _input;
    private Scene _scene;

    public GameFactory(IAssets assets, IRandomService random, IProgressService progress, IStaticDataService staticData,
      IInputService input)
    {
      _assets = assets;
      _random = random;
      _progress = progress;
      _staticData = staticData;
      _input = input;
    }

    public void SetScene(Scene scene) =>
      _scene = scene;

    public Scene GetScene() => _scene;

    public GameObject CreateHud() =>
      _assets.Instantiate(AssetPath.HUDPath, UIRoot);

    public GameObject CreateHero()
    {
      var go = _assets.Instantiate(AssetPath.HeroPath, StartPoint);
      HeroTransform = go.transform;
      var data = _staticData.GetHero();
      if (go.TryGetComponent<HeroMove>(out var move))
      {
        move.Construct(_input);
        move.Speed = data.MoveSpeed;
      }
      if (go.TryGetComponent<HeroRotate>(out var rotate))
      {
        rotate.Construct(MainCamera, _input, _assets);
        rotate.Speed = data.RotateSpeed;
      }
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