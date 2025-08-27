using System;
using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
    [Inject] private GameSettings _gameSettings;
    [Inject] private StartBattleZone _startBattleZone;
    [Inject] private PlayerHealth _playerHealth;
    [Inject] private HitButton _hitButton;

    private Action _onPlayerEnteredHandler;
    private bool _enemiesSpawned;
    
    private void OnEnable()
    {
        _onPlayerEnteredHandler = ActivateEnemies;
        _playerHealth.OnDeath += ResetEnemySpawnFlag;
        _startBattleZone.OnPlayerEntered += _onPlayerEnteredHandler;
        _hitButton.gameObject.SetActive(false);
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
            case CarFactory carFactory:
                carFactory.Setup(pool);
                break;
            case LionFactory lionFactory:
                lionFactory.Setup(pool);
                break;
            case GiraffeFactory giraffeFactory:
                giraffeFactory.Setup(pool);
                break;
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
        _hitButton.gameObject.SetActive(true);
        
        if (_enemiesSpawned) return;

        _enemiesSpawned = true;
        
        foreach (var settings in _gameSettings.EnemySettings)
        {
            Instantiate(settings.EnemyPrefab, settings.SpawnPoint, Quaternion.identity)
                .InitializeEnemy(_playerHealth, settings);
        }
    }
    
    private void ResetEnemySpawnFlag()
    {
        _hitButton.gameObject.SetActive(false);
        _enemiesSpawned = false;
    }

    private void OnDisable()
    {
        _hitButton.gameObject.SetActive(false);
        
        if (_onPlayerEnteredHandler != null)
            _startBattleZone.OnPlayerEntered -= _onPlayerEnteredHandler;
    }
}
