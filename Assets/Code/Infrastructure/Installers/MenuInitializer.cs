using Code.Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class MenuInitializer : MonoBehaviour, IInitializable
  {
    // [SerializeField] private MainMenu _mainMenu;
    private IGameFactory _gameFactory;

    [Inject]
    private void Construct(IGameFactory gameFactory) =>
      _gameFactory = gameFactory;

    public void Initialize()
    {
      // _gameFactory.MainMenu = _mainMenu;
    }
  }
}