using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
  public class AssetProvider : IAssets
  {
    public GameObject Instantiate(string path)
    {
      var prefab = LoadPrefab(path);
      return Object.Instantiate(prefab);
    }

    public GameObject Instantiate(string path, Transform at)
    {
      var prefab = LoadPrefab(path);
      return Object.Instantiate(prefab, at);
    }

    public GameObject Instantiate(GameObject prefab) =>
      Object.Instantiate(prefab);

    public GameObject Instantiate(GameObject prefab, Transform at) =>
      Object.Instantiate(prefab, at);

    private GameObject LoadPrefab(string path)
    {
      var prefab = Resources.Load<GameObject>(path);
      return !prefab ? throw new System.Exception($"[AssetProvider] Prefab not found at path: {path}") : prefab;
    }
  }
}