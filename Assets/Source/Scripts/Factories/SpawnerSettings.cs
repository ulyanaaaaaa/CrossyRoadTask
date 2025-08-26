using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerSettings", menuName = "Game/Spawner Settings")]
public class SpawnerSettings : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public MovingProduct Prefab { get; private set; }
    [field: SerializeField] public float SpawnInterval { get; private set; }
    [field: SerializeField] public int MaxActiveObjects { get; private set; }
    [field: SerializeField] public Vector3 SpawnPoint { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
}