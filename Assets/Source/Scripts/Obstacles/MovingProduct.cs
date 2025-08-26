using UnityEngine;

public class MovingProduct : PooledObject, IProduct
{
    private float _speed;
    private int _damage;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidbody.linearVelocity = Vector3.forward * _speed;
    }

    public IProduct Initialize(float speed, int damage)
    {
        _speed = speed;
        _damage = damage;
        return this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage(_damage);
        }

        if (other.GetComponent<Border>())
        {
            Release();
        }
    }
}