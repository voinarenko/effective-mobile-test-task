using UnityEngine;

namespace Code.Actors.Bullet
{
  public class BulletMove : MonoBehaviour
  {
    [SerializeField] private BulletTrailScriptableObject _trailConfig;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TrailRenderer _trail;

    [SerializeField] private float _speed;


    private void OnEnable() =>
      ConfigureTrail();

    private void OnDisable() =>
      ClearTrail();

    private void ClearTrail()
    {
      if (_trail && _trailConfig)
        _trail.Clear();
    }

    private void ConfigureTrail()
    {
      if (_trail && _trailConfig)
        _trailConfig.SetupTrail(_trail);
    }
  }
}