public class TimeWinCondition : IGameCondition
{
    private float _requiredTime;

    public TimeWinCondition(float requiredTime)
    {
        _requiredTime = requiredTime;
    }

    public bool Check(IGameModeState state) => state.Time >= _requiredTime;
}
