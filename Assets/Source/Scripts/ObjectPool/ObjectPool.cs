using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private int _initPoolSize;    
    private PooledObject _objectToPool;

    public void Setup(int initPoolSize, PooledObject objectToPool)
    {
        _initPoolSize = initPoolSize;
        _objectToPool = objectToPool;
    }

    private Stack<PooledObject> stack;

    private void Awake()
    {
        SetupPool();
    }
    
    private void SetupPool()
    {
        stack = new Stack<PooledObject>();

        for (int i = 0; i < _initPoolSize; i++)
        {
            CreateNewInstance();
        }
    }

    private PooledObject CreateNewInstance()
    {
        var instance = Instantiate(_objectToPool, transform);
        instance.Pool = this;
        instance.gameObject.SetActive(false);
        stack.Push(instance);
        return instance;
    }
    
    public PooledObject GetPooledObject(Vector3 position, Quaternion rotation)
    {
        if (stack.Count == 0)
            CreateNewInstance();

        var obj = stack.Pop();
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        stack.Push(pooledObject);
    }
}