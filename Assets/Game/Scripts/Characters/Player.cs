using UnityEngine;

public class Player : MonoDestroyable, IDirectionMovable, IDirectionRotatable, IShooter, IDamagable
{
    [SerializeField] private Transform _bulletSpawnPoint;
    
    private PlayerConfig _config;
    private DirectionMover _mover;
    private DirectionRotator _rotator;
    private Shooter _shooter;
    private Health _health;

    public Transform Transform => transform;

    public void Initialize(PlayerConfig config, DirectionMover mover, DirectionRotator rotator, Shooter shooter)
    {
        _config = config;
        _mover = mover;
        _rotator = rotator;
        _shooter = shooter;

        _health = new Health(_config.StartHealth);
        _health.GetIsDead().Changed += Death;
    }

    private void Update()
    {
        _mover?.Update(Time.deltaTime);
        _rotator?.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 direction) => _mover.SetMoveDirection(direction);
    
    public void SetRotationDirection(Vector3 direction) => _rotator.SetRotationDirection(direction);

    public void Shoot(Vector3 direction) => _shooter.Shoot(_bulletSpawnPoint.position, direction);

    public void TakeDamage(int damage) => _health.TakeDamage(damage);

    private void Death(bool oldValue, bool currentValue)
    {
        _health.GetIsDead().Changed -= Death;
        
        Destroy(gameObject);
    }
}