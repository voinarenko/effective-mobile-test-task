using UnityEngine;

namespace Code.Actors.Hero
{
  public class HeroLook : MonoBehaviour
  {
    public Transform ViewPoint;
    private CinemachinePovExtension _cinemachinePov;
    private Camera _mainCamera;

    private void OnDestroy() =>
      _cinemachinePov.LookChanged -= OnLookChanged;

    public void Init(Camera mainCamera, CinemachinePovExtension cinemachinePov)
    {
      _mainCamera = mainCamera;
      _cinemachinePov = cinemachinePov;
      _cinemachinePov.LookChanged += OnLookChanged;
    }

    private void OnLookChanged()
    {
      ViewPoint.rotation = Quaternion.Euler(
        _mainCamera.transform.eulerAngles.x,
        _mainCamera.transform.eulerAngles.y,
        _mainCamera.transform.eulerAngles.z);
    }
  }
}