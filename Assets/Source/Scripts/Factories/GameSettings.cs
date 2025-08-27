using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Car Settings")]
    [field: SerializeField] public SpawnerSettings CarSettings { get; private set; }
    
    [Header("Lion Settings")]
    [field: SerializeField] public SpawnerSettings LionSettings { get; private set; }
    
    [Header("Giraffe Settings")]
    [field: SerializeField] public SpawnerSettings GiraffeSettings { get; private set; }
    
    [Header("Player Settings")]
    [field: SerializeField] public PlayerSettings PlayerSettings { get; private set; }
    
    [Header("Enemy Settings")]
    [field: SerializeField] public EnemySettings[] EnemySettings { get; private set; }
    
    [Header("Box Settings")] 
    [field: SerializeField] public BoxSettings BoxSettings { get; private set; } 
    
    [Header("UI Settings")]
    [field: SerializeField] public UISettings UISettings { get; private set; } 
    
    [Header("Factories")]
    [field: SerializeField] public FactorySettings FactorySettings { get; private set; }
}
