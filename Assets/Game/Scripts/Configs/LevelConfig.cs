using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfig", fileName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField] public EnemyConfig EnemyConfig { get; private set;}
    [field: SerializeField] public int EnemiesOnStart { get; private set;} = 3;
    [field: SerializeField] public float EnemiesSpawnCooldown { get; private set;} = 3;
    [field: SerializeField] public float TimeToWin { get; private set; } = 15;
    [field: SerializeField] public int CountKilledToWin { get; private set;} = 5;
    [field: SerializeField] public int CountEnemiesOnArenaToDefeat { get; private set;} = 8;
    [field: SerializeField] public int CountEnemiesToSpawnInCycle { get; private set;} = 1;
    [field: SerializeField] public Vector3 PlayerStartPosition { get; private set; }
    [field: SerializeField] public Vector3[] EnemiesStartPositions { get; private set; }
    [field: SerializeField] public VictoryType VictoryType { get; private set; } = VictoryType.KillCount;
    [field: SerializeField] public DefeatType DefeatType { get; private set; } = DefeatType.PlayerDestroyed;

    [ContextMenu("UpdateStartPositions")]
    private void UpdateStartPositions()
    {
        PlayerStartPosition = GameObject.FindGameObjectWithTag("StartPlayerPosition").transform.position;
        EnemiesStartPositions = GameObject.FindGameObjectsWithTag("SpawnEnemyPosition").Select(p => p.transform.position).ToArray();
    }
}