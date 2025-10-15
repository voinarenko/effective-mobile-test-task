using Code.Services;
using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
  public interface IAssets : IService
  {
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Transform at);
    GameObject Instantiate(GameObject prefab);
    GameObject Instantiate(GameObject prefab, Transform at);
  }
}