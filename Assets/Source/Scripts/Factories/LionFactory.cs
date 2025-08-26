using UnityEngine;

public class LionFactory : Factory
{
    private ObjectPool _pool;
    
    public void Setup(ObjectPool pool)
    {
        _pool = pool;
    }

    public override IProduct Create(Vector3 position, float speed, int damage)
    {
        var pooled = _pool.GetPooledObject(position, Quaternion.identity);
        var product = pooled.GetComponent<MovingProduct>().Initialize(speed, damage);
        return product;
    }

}