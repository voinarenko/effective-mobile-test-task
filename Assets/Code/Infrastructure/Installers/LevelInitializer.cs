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
    
    [Inject]
    private void Construct(IGameFactory windowFactory) =>
      _gameFactory = windowFactory;

    public void Initialize()
    {
      _gameFactory.StartPoint = _startPoint;
      _gameFactory.MainCamera = _mainCamera;
    }
  }
}