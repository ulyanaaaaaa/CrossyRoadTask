using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Game/Enemy Settings")]
public class EnemySettings : ScriptableObject
{
    [field: SerializeField] public Enemy EnemyPrefab { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float AttackInterval { get; private set; } 
    [field: SerializeField] public float MoveSpeed { get; private set; } 
    [field: SerializeField] public Vector3 SpawnPoint { get; private set; } 
}