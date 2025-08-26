using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    private float _speed;
    private int _damage;
    private Factory _factory;
    private float _spawnInterval;
    private Transform _spawnPoint; 
    private Coroutine _spawnRoutine;

    public void Setup(float speed, Factory factory, float spawnInterval, Transform spawnPoint, int damage)
    {
        _speed =  speed;
        _factory = factory;
        _spawnInterval = spawnInterval;
        _spawnPoint = spawnPoint;
        _damage = damage;
        
        StartSpawning();
    }
    
    public void StartSpawning()
    {
        if (_spawnRoutine == null)
            _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            _factory.Create(_spawnPoint.position, _speed, _damage);
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}