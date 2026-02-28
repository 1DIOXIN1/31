using UnityEngine;

public class Enemy : MonoBehaviour, IDirectionMovable, IDirectionRotatable, IDamagable
{
    private DirectionMover _mover;
    private DirectionRotator _rotator;
    private Health _health;

    public void Initialize(EnemyConfig config, DirectionMover mover, DirectionRotator rotator)
    {
        _mover = mover;
        _rotator = rotator;

        _health = new Health(config.StartHealth);
    }

    private void Update()
    {
        _mover?.Update(Time.deltaTime);
        _rotator?.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 direction) => _mover.SetMoveDirection(direction);

    public void SetRotationDirection(Vector3 direction) => _rotator.SetRotationDirection(direction);

    public void TakeDamage(int damage) => _health.TakeDamage(damage);
}
