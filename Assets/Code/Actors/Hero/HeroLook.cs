using Code.Infrastructure.AssetManagement;
using Code.Services.Input;
using UnityEngine;

namespace Code.Actors.Hero
{
  public class HeroLook : MonoBehaviour
  {
    public float Speed { get; set; }
    public Transform ViewPoint;

    private const float CamRayLength = Mathf.Infinity;
    private const float PointerPositionOffset = 0.15f;

    private static Canvas _pointer;
    private int _groundMask;
    private Camera _camera;
    private IInputService _input;
    private IAssets _assets;

    public void Construct(Camera mainCamera, IInputService input, IAssets assets)
    {
      _assets = assets;
      _input = input;
      _camera = mainCamera;
    }
    
    private void Update()
    {
    }

    private void Look()
    {
      
    }
  }
}