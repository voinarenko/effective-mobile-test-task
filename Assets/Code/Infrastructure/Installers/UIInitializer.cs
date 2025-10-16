using Code.Infrastructure.Factory;
using Code.UI.Services.Factory;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class UIInitializer : MonoBehaviour, IInitializable
  {
    public RectTransform UIRoot;
    private IGameFactory _gameFactory;
    private IUiFactory _uiFactory;

    [Inject]
    private void Construct(IGameFactory gameFactory, IUiFactory uiFactory)
    {
      _uiFactory = uiFactory;
      _gameFactory = gameFactory;
    }

    public void Initialize()
    {
      _gameFactory.UIRoot = UIRoot;
      _uiFactory.UIRoot = UIRoot;
    }
  }
}