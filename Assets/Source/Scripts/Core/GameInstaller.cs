using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
   [SerializeField] private GameSettings _gameSettings;
   [SerializeField] private StartBattleZone _startBattleZone;
   [SerializeField] private HitButton _hitButton;
   
   public override void InstallBindings()
   {
      Container.Bind<IInput>().To<DesktopInput>().AsSingle();

      Container.Bind<PlayerHealth>()
         .FromInstance(new PlayerHealth(_gameSettings.PlayerSettings.MaxHealth))
         .AsSingle();

      Container.Bind<PlayerHit>()
         .FromInstance(new PlayerHit(_gameSettings.PlayerSettings.Damage))
         .AsSingle();

      Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle();
      Container.Bind<StartBattleZone>().FromInstance(_startBattleZone).AsSingle();
      Container.Bind<HitButton>().FromInstance(_hitButton).AsSingle();
   }
}
