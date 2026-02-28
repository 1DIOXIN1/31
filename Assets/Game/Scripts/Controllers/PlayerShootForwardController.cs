using UnityEngine;

public class PlayerShootForwardController : Controller
{
    private IShooter _shooter;

    public PlayerShootForwardController(IShooter shooter)
    {
        _shooter = shooter;
    }

    public override void UpdateLogic(float deltaTime)
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
            _shooter.Shoot(_shooter.Transform.forward);
    }
}
