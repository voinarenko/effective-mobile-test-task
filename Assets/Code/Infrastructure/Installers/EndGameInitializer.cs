using Code.Infrastructure.Factory;
using Code.UI.Services.Factory;
using Code.UI.Windows;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class EndGameInitializer : MonoBehaviour, IInitializable
  {
    [SerializeField] private EndGameWindow _endGameWindow;
    private IUiFactory _uiFactory;

    [Inject]
    private void Construct(IUiFactory uiFactory) =>
      _uiFactory = uiFactory;

    public void Initialize()
    {
      _uiFactory.EndGameWindow = _endGameWindow;
    }
  }
}