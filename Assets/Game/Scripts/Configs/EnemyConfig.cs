using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/EnemyConfig", fileName = "EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 900;
    [field: SerializeField] public int StartHealth { get; private set; } = 100;
    [field: SerializeField] public Enemy Prefab { get; private set; }
}
