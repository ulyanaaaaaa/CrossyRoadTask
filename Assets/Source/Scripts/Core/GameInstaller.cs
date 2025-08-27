using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private Canvas _canvas;

    public override void InstallBindings()
    {
        Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle();
        Container.Bind<IInput>().To<MobileInput>().AsSingle();
        Container.Bind<EnemyController>().AsSingle();
        Container.Bind<SceneLoader>().AsSingle();
        Container.Bind<BoxController>().AsSingle();

        Container.Bind<PlayerHealth>()
            .FromInstance(new PlayerHealth(_gameSettings.PlayerSettings.MaxHealth))
            .AsSingle();

        Container.Bind<PlayerHit>()
            .FromInstance(new PlayerHit(_gameSettings.PlayerSettings.Damage))
            .AsSingle();

        Container.Bind<BoxSettings>()
            .FromInstance(_gameSettings.BoxSettings)
            .AsSingle();

        Container.Bind<HitButton>()
            .FromComponentInNewPrefab(_gameSettings.UISettings.HitButton)
            .AsSingle()
            .OnInstantiated<HitButton>((ctx, btn) => { btn.transform.SetParent(_canvas.transform, false); });

        Container.Bind<OpenButton>()
            .FromComponentInNewPrefab(_gameSettings.UISettings.OpenButton)
            .AsSingle()
            .OnInstantiated<OpenButton>((ctx, btn) => { btn.transform.SetParent(_canvas.transform, false); });

        Container.Bind<BoxView>()
            .FromComponentInNewPrefab(_gameSettings.UISettings.BoxView)
            .AsSingle()
            .OnInstantiated<BoxView>((ctx, btn) => { btn.transform.SetParent(_canvas.transform, false); });

        Container.Bind<WinPanel>()
            .FromComponentInNewPrefab(_gameSettings.UISettings.WinPanel)
            .AsSingle()
            .OnInstantiated<WinPanel>((ctx, btn) => { btn.transform.SetParent(_canvas.transform, false); });
    }
}