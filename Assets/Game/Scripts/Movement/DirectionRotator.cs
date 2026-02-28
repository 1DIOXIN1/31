using UnityEngine;

public abstract class DirectionRotator
{
    private float _rotationSpeed;
    private Vector3 _currentDirection;
    private Transform _transform;

    public DirectionRotator(Transform transform, float rotationSpeed)
    {
        _transform = transform;
        _rotationSpeed = rotationSpeed;
    }

    public virtual void SetRotationDirection(Vector3 direction) => _currentDirection = direction;

    public void Update(float deltaTime)
    {
        if (_currentDirection.magnitude < 0.05f)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(_currentDirection.normalized);

        float step = _rotationSpeed * deltaTime;

        ApplyRotation(Quaternion.RotateTowards(_transform.rotation, lookRotation, step));
    }

    protected abstract void ApplyRotation(Quaternion quaternion);
}
