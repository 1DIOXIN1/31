public interface IGameModeState
{
    float Time { get; }
    int KillCount { get; }
    int AliveEnemiesCount { get; }
    bool IsPlayerDestroyed { get; }
}