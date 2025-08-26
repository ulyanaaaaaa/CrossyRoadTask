using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
   public override void InstallBindings()
   {
      Container.Bind<IInput>().To<DesktopInput>().AsSingle();
   }
}
