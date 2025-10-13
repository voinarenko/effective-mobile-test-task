using Zenject;

namespace Code.Infrastructure.Installers
{
  public class MenuInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      // Container.BindInterfacesTo<MainMenu>().FromComponentInHierarchy().AsSingle();
    }
  }
}