using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool Pool { get; set; }
    
    public void Release()
    {
        Pool.ReturnToPool(this);
    }
}