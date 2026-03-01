using UnityEngine;

public class Enemy : MonoDestroyable, IDirectionMovable, IDirectionRotatable, IDamagable
{
    private EnemyConfig _config;
    private DirectionMover _mover;
    private DirectionRotator _rotator;
    private Health _health;

    public void Initialize(EnemyConfig config, DirectionMover mover, DirectionRotator rotator)
    {
        _config = config;
        _mover = mover;
        _rotator = rotator;

        _health = new Health(_config.StartHealth);
        _health.GetIsDead().Changed += Death;
    }

    private void Update()
    {
        _mover?.Update(Time.deltaTime);
        _rotator?.Update(Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out IDamagable damagable))
            damagable.TakeDamage(_config.ContactDamage);
    }

    public void SetMoveDirection(Vector3 direction) => _mover.SetMoveDirection(direction);

    public void SetRotationDirection(Vector3 direction) => _rotator.SetRotationDirection(direction);

    public void TakeDamage(int damage) => _health.TakeDamage(damage);

    private void Death(bool oldValue, bool currentValue)
    {
        _health.GetIsDead().Changed -= Death;
        
        Destroy();
    }
}
