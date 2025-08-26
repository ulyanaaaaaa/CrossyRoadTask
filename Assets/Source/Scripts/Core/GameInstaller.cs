using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
   [SerializeField] private GameSettings _gameSettings;
   
   public override void InstallBindings()
   {
      Container.Bind<IInput>().To<DesktopInput>().AsSingle();
      
      Container.Bind<PlayerHealth>()
         .FromInstance(new PlayerHealth(_gameSettings.PlayerSettings.MaxHealth))
         .AsSingle();
      
      Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle();
   }
}
