using Code.Bullet;
using System.Collections;
using UnityEngine;

namespace Code.Actors.Bullet
{
  public class BulletMove : MonoBehaviour
  {
    private const string DoDisableMethodName = "DoDisable";
    private const float TimeToDestroy = 3;

    [SerializeField] private BulletTrailScriptableObject _trailConfig;
    [SerializeField] private Renderer _renderer;

    [SerializeField] private float _speed;

    private TrailRenderer _trail;


    private void OnEnable()
    {
      _renderer.enabled = true;
      ConfigureTrail();
      StartCoroutine(DestroyTimer());
    }

    private void Awake() =>
      _trail = GetComponent<TrailRenderer>();

    private void Update()
    {
      if (!transform) return;
      transform.position += transform.forward * _speed;
    }

    private void Disable()
    {
      CancelInvoke(DoDisableMethodName);
      _renderer.enabled = false;
      if (_trail && _trailConfig)
        Invoke(DoDisableMethodName, _trailConfig.Time);
      else
        DoDisable();
    }

    private void DoDisable()
    {
      if (_trail && _trailConfig)
        _trail.Clear();
      gameObject.SetActive(false);
    }

    private void ConfigureTrail()
    {
      if (_trail && _trailConfig)
        _trailConfig.SetupTrail(_trail);
    }

    private IEnumerator DestroyTimer()
    {
      yield return new WaitForSeconds(TimeToDestroy);
      DestroySelf();
    }

    private void DestroySelf() =>
      Destroy(gameObject);
  }
}