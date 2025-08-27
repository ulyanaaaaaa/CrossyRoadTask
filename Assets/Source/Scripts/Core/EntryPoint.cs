// EntryPoint.cs
using System;
using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _parentZone;

    private GameSettings _gameSettings;
    private StartBattleZone _startBattleZone;
    private PlayerHealth _playerHealth;
    private HitButton _hitButton;
    private EnemyController _enemyController;
    private OpenButton _openButton;
    private BoxView _boxView;
    private WinPanel _winPanel;

    private Box _box;
    private bool _enemiesSpawned;
    private Action _onPlayerEnteredHandler;

    [Inject]
    public void Init(GameSettings gameSettings,
        PlayerHealth playerHealth,
        EnemyController enemyController,
        HitButton hitButton,
        OpenButton openButton,
        BoxView boxView,
        WinPanel winPanel)
    {
        _gameSettings = gameSettings;
        _playerHealth = playerHealth;
        _enemyController = enemyController;

        _hitButton = hitButton;
        _openButton = openButton;
        _boxView = boxView;
        _winPanel = winPanel;
    }

    private void Awake()
    {
        _startBattleZone = Instantiate(_gameSettings.UISettings.StartBattleZone, _parentZone.transform);

        _hitButton.Close();
        _winPanel.Close();
        _openButton.Hide();
    }

    private void OnEnable()
    {
        _enemiesSpawned = false;
        _onPlayerEnteredHandler = ActivateEnemies;

        _playerHealth.OnDeath += Reset;
        _enemyController.OnAllEnemiesDefeated += CreateBox;

        _startBattleZone.OnPlayerEntered += _onPlayerEnteredHandler;
    }

    private void Start()
    {
        CreateSpawner(_gameSettings.CarSettings, _gameSettings.FactorySettings.CarFactory);
        CreateSpawner(_gameSettings.LionSettings, _gameSettings.FactorySettings.LionFactory);
        CreateSpawner(_gameSettings.GiraffeSettings, _gameSettings.FactorySettings.GiraffeFactory);
    }

    private void CreateSpawner(SpawnerSettings settings, Factory factory)
    {
        GameObject poolObj = new GameObject($"{settings.Prefab.name} Pool");
        poolObj.transform.parent = transform;
        ObjectPool pool = poolObj.AddComponent<ObjectPool>();
        pool.Setup(settings.MaxActiveObjects, settings.Prefab.GetComponent<PooledObject>());

        switch (factory)
        {
            case CarFactory carFactory: carFactory.Setup(pool); break;
            case LionFactory lionFactory: lionFactory.Setup(pool); break;
            case GiraffeFactory giraffeFactory: giraffeFactory.Setup(pool); break;
        }

        GameObject spawnerObj = new GameObject($"{settings.Prefab.name} Spawner");
        spawnerObj.transform.parent = transform;
        var spawner = spawnerObj.AddComponent<Spawner>();

        GameObject spawnPointObj = new GameObject($"{settings.Prefab.name} SpawnPoint");
        spawnPointObj.transform.position = settings.SpawnPoint;
        spawnPointObj.transform.parent = spawnerObj.transform;

        spawner.Setup(settings.Speed, factory, settings.SpawnInterval, spawnPointObj.transform, settings.Damage);
        spawner.StartSpawning();
    }

    private void ActivateEnemies()
    {
        if (_enemiesSpawned) return;

        _enemiesSpawned = true;
        _hitButton.Open();

        foreach (var settings in _gameSettings.EnemySettings)
        {
            var enemy = Instantiate(settings.EnemyPrefab, settings.SpawnPoint, Quaternion.identity)
                .InitializeEnemy(_playerHealth, settings);

            _enemyController.RegisterEnemy(enemy);
        }
    }

    private void CreateBox()
    {
        if (!_enemiesSpawned) 
            return;
        
        _box = Instantiate(_gameSettings.BoxSettings.BoxPrefab, _gameSettings.BoxSettings.SpawnPoint, Quaternion.identity)
            .Setup(_openButton);

        _hitButton.Close();
        _openButton.Open();
    }

    private void Reset()
    {
        _hitButton.Close();
        _enemiesSpawned = false;

        if (_box) Destroy(_box.gameObject);
    }

    private void OnDisable()
    {
        _hitButton?.Close();
        _startBattleZone.OnPlayerEntered -= _onPlayerEnteredHandler;
        _enemyController.OnAllEnemiesDefeated -= CreateBox;
    }
}
