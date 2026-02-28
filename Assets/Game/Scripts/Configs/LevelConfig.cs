using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfig", fileName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField] public float SpawnCooldown { get; private set;} = 3;
}