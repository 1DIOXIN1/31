using UnityEngine;

public class Player : MonoBehaviour, IDirectionMovable, IDirectionRotatable, IShooter
{
    [SerializeField] private Transform _bulletSpawnPoint;
    
    private DirectionMover _mover;
    private DirectionRotator _rotator;
    private Shooter _shooter;

    public Transform Transform => transform;

    public void Initialize(DirectionMover mover, DirectionRotator rotator, Shooter shooter)
    {
        _mover = mover;
        _rotator = rotator;
        _shooter = shooter;
    }

    private void Update()
    {
        _mover?.Update(Time.deltaTime);
        _rotator?.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 direction) => _mover.SetMoveDirection(direction);
    public void SetRotationDirection(Vector3 direction) => _rotator.SetRotationDirection(direction);

    public void Shoot(Vector3 direction) => _shooter.Shoot(_bulletSpawnPoint.position, direction);
}