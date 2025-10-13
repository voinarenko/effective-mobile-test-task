using Code.Infrastructure.AssetManagement;
using UnityEngine;

namespace Code.Actors.Hero
{
  public class HeroRotate : MonoBehaviour
  {
    private const string GroundMaskName = "Ground";
    private const float CamRayLength = Mathf.Infinity;
    private const float PointerPositionOffset = 0.15f;

    private static Canvas _pointer;
    [SerializeField] private GameObject _pointerPrefab;

    [SerializeField] private float _speed;
    private int _groundMask;
    private Camera _camera;

    public void Construct(Camera mainCamera, IAssets assets)
    {
      _camera = mainCamera;
    }
    
    private void Update()
    {
      var camRay = _camera.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(camRay, out var groundHit, CamRayLength, _groundMask))
        Rotate(groundHit);
    }

    private void Rotate(RaycastHit groundHit)
    {

      var playerToMouse = groundHit.point - transform.position;
      playerToMouse.y = 0f;
      transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(playerToMouse),
        _speed * Time.deltaTime);
      _pointer.transform.position = new Vector3(groundHit.point.x, PointerPositionOffset, groundHit.point.z);
    }

    public void OnStart()
    {
      _pointer = Instantiate(_pointerPrefab).GetComponent<Canvas>();
      Cursor.visible = false;
      _groundMask = LayerMask.GetMask(GroundMaskName);
    }

    public void SetSpeed(float speed) =>
      _speed = speed;
  }
}