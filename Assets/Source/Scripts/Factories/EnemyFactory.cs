using UnityEngine;

public class EnemyFactory : Factory
{
    private ObjectPool _pool;
    private EnemySettings _settings;

    public void Setup(ObjectPool pool, EnemySettings settings)
    {
        _pool = pool;
        _settings = settings;
    }

    public override IProduct Create(Vector3 position, float unused1 = 0, int unused2 = 0)
    {
        var pooled = _pool.GetPooledObject(position, Quaternion.identity);
        var enemy = pooled.GetComponent<Enemy>();
        enemy.Setup(_settings);  
        return enemy;
    }
}