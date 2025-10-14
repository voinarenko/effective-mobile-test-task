using Cinemachine;
using Code.Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class LevelInitializer : MonoBehaviour, IInitializable
  {
    private IGameFactory _gameFactory;

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    
    [Inject]
    private void Construct(IGameFactory gameFactory) =>
      _gameFactory = gameFactory;

    public void Initialize()
    {
      _gameFactory.StartPoint = _startPoint;
      _gameFactory.MainCamera = _mainCamera;
      _gameFactory.VirtualCamera = _virtualCamera;
    }
  }
}