using UnityEngine;

public class TransformDirectionRotator : DirectionRotator
{
    private Transform _transform;

    public TransformDirectionRotator(Transform transform, float rotationSpeed) : base(transform, rotationSpeed)
    {
        _transform = transform;
    }

    protected override void ApplyRotation(Quaternion quaternion) => _transform.rotation = quaternion;
}
