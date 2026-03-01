using UnityEngine;

public class Bullet : MonoDestroyable
{
    private float _timeBeforeDestroyed = 2.5f;
    private int _damage;
    private float _moveSpeed;
    private Vector3 _direction;

    public void Initilize(Vector3 direction, float moveSpeed, int damage)
    {
        _direction = direction;
        _moveSpeed = moveSpeed;
        _damage = damage;
        
        Invoke(nameof(Destroy), _timeBeforeDestroyed);
    }

    private void Update()
    {
        Move(_direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(_damage);
            Destroy();
        }
    }

    private void Move(Vector3 direction)
    {
        transform.Translate(direction * _moveSpeed);
    }
}
