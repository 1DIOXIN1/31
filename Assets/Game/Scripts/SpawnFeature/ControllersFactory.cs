public class ControllersFactory
{
    private ControllersUpdateService _controllersUpdateService;

    public ControllersFactory(ControllersUpdateService controllersUpdateService)
    {
        _controllersUpdateService = controllersUpdateService;
    }

    public PlayerDirectionMovableController CreatePlayerDirectionMovableController(IDirectionMovable movable)
    {
        PlayerDirectionMovableController controller = new PlayerDirectionMovableController(movable);

        _controllersUpdateService.Add(controller);

        return controller;
    }

    public PlayerDirectionRotatableController CreatePlayerDirectionRotatableController(IDirectionRotatable rotatable)
    {
        PlayerDirectionRotatableController controller = new PlayerDirectionRotatableController(rotatable);

        _controllersUpdateService.Add(controller);

        return controller;
    }

    public PlayerShootForwardController CreatePlayerShootForwardController(IShooter shooter)
    {
        PlayerShootForwardController controller = new PlayerShootForwardController(shooter);

        _controllersUpdateService.Add(controller);

        return controller;
    }

    public RandomAIDirectionController CreateRandomAIDirectionController(IDirectionMovable movable, IDirectionRotatable rotatable, float timeToChangeDirection)
    {
        RandomAIDirectionController controller = new RandomAIDirectionController(movable, rotatable, timeToChangeDirection);

        _controllersUpdateService.Add(controller);

        return controller;
    }
}
