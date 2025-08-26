using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    public abstract IProduct Create(Vector3 position, float speed, int damage);
}
