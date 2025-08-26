using UnityEngine;

[CreateAssetMenu(fileName = "FactorySettings", menuName = "Game/Factory Settings")]
public class FactorySettings : ScriptableObject
{
    [field: SerializeField] public Factory CarFactory { get; private set; }
    [field: SerializeField] public Factory LionFactory { get; private set; }
    [field: SerializeField] public Factory GiraffeFactory { get; private set; }
    [field: SerializeField] public EnemyFactory EnemyFactory { get; private set; }
}
