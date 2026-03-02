public class EnemyCountDefeatCondition : IGameCondition
{
    private int _maxEnemies;

    public EnemyCountDefeatCondition(int maxEnemies)
    {
        _maxEnemies = maxEnemies;
    }

    public bool Check(IGameModeState state) => state.AliveEnemiesCount >= _maxEnemies;
}
