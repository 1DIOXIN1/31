public class KillCountWinCondition : IGameCondition
{
    private int _requiredKills;

    public KillCountWinCondition(int requiredKills)
    {
        _requiredKills = requiredKills;
    }

    public bool Check(IGameModeState state) => state.KillCount >= _requiredKills;
}
