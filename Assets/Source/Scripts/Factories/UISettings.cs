using UnityEngine;

[CreateAssetMenu(fileName = "UISettings", menuName = "Game/UI Settings")]
public class UISettings : ScriptableObject
{
    [field: SerializeField] public StartBattleZone StartBattleZone { get; private set; }
    [field: SerializeField] public HitButton HitButton { get; private set; }
    [field: SerializeField] public OpenButton OpenButton { get; private set; }
    [field: SerializeField] public BoxView BoxView { get; private set; }
    [field: SerializeField] public WinPanel WinPanel { get; private set; }
}