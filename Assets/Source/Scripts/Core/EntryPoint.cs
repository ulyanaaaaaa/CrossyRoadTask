using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
    [Inject] private GameSettings _gameSettings;

    private void Start()
    {
        CreateSpawner(_gameSettings.CarSettings, _gameSettings.FactorySettings.CarFactory);
        CreateSpawner(_gameSettings.LionSettings, _gameSettings.FactorySettings.LionFactory);
        CreateSpawner(_gameSettings.GiraffeSettings, _gameSettings.FactorySettings.GiraffeFactory);

        SpawnEnemiesAtFinish(_gameSettings.FactorySettings.EnemyFactory, new Vector3(0f, 0f, 0f));
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
        Spawner spawner = spawnerObj.AddComponent<Spawner>();
        
        GameObject spawnPointObj = new GameObject($"{settings.Prefab.name} SpawnPoint");
        spawnPointObj.transform.position = settings.SpawnPoint;
        spawnPointObj.transform.parent = spawnerObj.transform;

        spawner.Setup(settings.Speed, factory, settings.SpawnInterval, spawnPointObj.transform, settings.Damage);
    }

    private void SpawnEnemiesAtFinish(EnemyFactory factory, Vector3 spawnPosition)
    {
        for (int i = 0; i < _gameSettings.EnemySettings.Length; i++)
        {
            EnemySettings settings = _gameSettings.EnemySettings[i];

            GameObject poolObj = new GameObject($"{settings.EnemyPrefab.name} Pool");
            poolObj.transform.parent = transform;
            ObjectPool pool = poolObj.AddComponent<ObjectPool>();
            pool.Setup(_gameSettings.EnemySettings.Length, settings.EnemyPrefab.GetComponent<PooledObject>());

            factory.Setup(pool, settings);

            for (int j = 0; j < _gameSettings.EnemySettings.Length; j++)
            {
                Vector3 offset = new Vector3(j * 1.5f, 0f, i * 2f); 
                factory.Create(spawnPosition + offset);
            }
        }
    }

}
