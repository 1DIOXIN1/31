public class ControllersFactory
{
    public PlayerDirectionMovableController CreatePlayerDirectionMovableController(IDirectionMovable movable)
    {
        PlayerDirectionMovableController controller = new PlayerDirectionMovableController(movable);

        return controller;
    }

    public PlayerDirectionRotatableController CreatePlayerDirectionRotatableController(IDirectionRotatable rotatable)
    {
        PlayerDirectionRotatableController controller = new PlayerDirectionRotatableController(rotatable);

        return controller;
    }

    public PlayerShootForwardController CreatePlayerShootForwardController(IShooter shooter)
    {
        PlayerShootForwardController controller = new PlayerShootForwardController(shooter);

        return controller;
    }

    public RandomAIDirectionController CreateRandomAIDirectionController(IDirectionMovable movable, IDirectionRotatable rotatable, float timeToChangeDirection)
    {
        RandomAIDirectionController controller = new RandomAIDirectionController(movable, rotatable, timeToChangeDirection); 

        return controller;
    }
}
