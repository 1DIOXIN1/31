using System;

public enum VictoryType
{
    Time,
    KillCount
}

public enum DefeatType
{
    EnemyCount,
    PlayerDestroyed
}

public class ConditionFactory
{
    private LevelConfig _levelConfig;

    public ConditionFactory(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
    }
    
    public IGameCondition CreateVictoryCondition()
    {
        switch (_levelConfig.VictoryType)
        {
            case VictoryType.Time:
                return new TimeWinCondition(_levelConfig.TimeToWin);

            case VictoryType.KillCount:
                return new KillCountWinCondition(_levelConfig.CountKilledToWin);

            default:
                throw new ArgumentOutOfRangeException("Отсутствует нужное условие победы");
        }
    }

    public IGameCondition CreateDefeatCondition()
    {
        switch (_levelConfig.DefeatType)
        {
            case DefeatType.EnemyCount:
                return new EnemyCountDefeatCondition(_levelConfig.CountEnemiesOnArenaToDefeat);

            case DefeatType.PlayerDestroyed:
                return new PlayerDestroyedDefeatCondition();

            default:
                throw new ArgumentOutOfRangeException("Отсутствует нужное условие проигрыша");
        }
    }
}
