using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/PlayerConfig", fileName = "PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 9;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 900;
    [field: SerializeField] public Player Prefab { get; private set; }
    [field: SerializeField] public ShooterConfig ShooterConfig { get; private set; }
}
