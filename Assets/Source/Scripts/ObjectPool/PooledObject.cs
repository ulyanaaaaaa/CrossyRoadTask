using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool Pool { get; set; }
    
    protected void Release()
    {
        Pool.ReturnToPool(this);
    }
}