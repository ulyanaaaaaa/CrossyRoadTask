using UnityEngine;

[CreateAssetMenu(fileName = "BoxSettings", menuName = "Game/Box Settings")]
public class BoxSettings : ScriptableObject
{
    [field:SerializeField] public Box BoxPrefab { get;private set; }
    [field:SerializeField] public Vector3 SpawnPoint { get;private set; }
    [field:SerializeField] public Color[] PossibleColors { get;private set; }
    [field:SerializeField] public int RequiredKeys { get;private set; }  
    [field:SerializeField] public int GridSize { get;private set; }    
}