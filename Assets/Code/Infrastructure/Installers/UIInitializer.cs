using Code.Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class UIInitializer : MonoBehaviour, IInitializable
  {
    public RectTransform UIRoot;
    private IGameFactory _gameFactory;

    [Inject]
    private void Construct(IGameFactory gameFactory) =>
      _gameFactory = gameFactory;

    public void Initialize() =>
      _gameFactory.UIRoot = UIRoot;
  }
}