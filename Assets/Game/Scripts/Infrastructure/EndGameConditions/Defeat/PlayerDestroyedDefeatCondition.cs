public class PlayerDestroyedDefeatCondition : IGameCondition
{
    public bool Check(IGameModeState state) => state.IsPlayerDestroyed;
}
