using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Game/Enemy Settings")]
public class EnemySettings : ScriptableObject
{
    [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; } = 10;
    [field: SerializeField] public int Damage { get; private set; } = 1;
    [field: SerializeField] public float AttackInterval { get; private set; } = 1f;
    [field: SerializeField] public float MoveSpeed { get; private set; } = 2f;
}