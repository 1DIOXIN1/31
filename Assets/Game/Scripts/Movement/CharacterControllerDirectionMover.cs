using UnityEngine;

public class CharacterControllerDirectionMover : DirectionMover
{
    private CharacterController _characterController;

    public CharacterControllerDirectionMover(CharacterController characterController, float moveSpeed) : base(moveSpeed)
    {
        _characterController = characterController;
    }

    public override void Update(float deltaTime) => _characterController.Move(CurrentVelocity * deltaTime);
}
