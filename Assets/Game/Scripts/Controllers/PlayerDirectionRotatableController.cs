using UnityEngine;

public class PlayerDirectionRotatableController : Controller
{
    private IDirectionRotatable _rotatable;

    public PlayerDirectionRotatableController(IDirectionRotatable rotatable)
    {
        _rotatable = rotatable;
    }
    
    public override void UpdateLogic(float deltaTime)
    {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        _rotatable.SetRotationDirection(inputDirection);
    }
}
