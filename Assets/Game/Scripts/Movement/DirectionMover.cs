using UnityEngine;

public abstract class DirectionMover
{
    private float _moveSpeed;
    private Vector3 _currentDirection;

    public DirectionMover(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }
    public void SetMoveDirection(Vector3 direction) => _currentDirection = direction;

    public Vector3 CurrentVelocity => _currentDirection.normalized * _moveSpeed;

    public abstract void Update(float deltaTime);
}
