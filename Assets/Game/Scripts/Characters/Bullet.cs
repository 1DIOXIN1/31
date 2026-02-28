using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damage;
    private float _moveSpeed;
    private Vector3 _direction;

    public void Initilize(Vector3 direction, float moveSpeed, int damage)
    {
        _direction = direction;
        _moveSpeed = moveSpeed;
        _damage = damage;
    }

    private void Update()
    {
        Move(_direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
            damagable.TakeDamage(_damage);
    }

    private void Move(Vector3 direction)
    {
        transform.Translate(direction * _moveSpeed);
    }
}
