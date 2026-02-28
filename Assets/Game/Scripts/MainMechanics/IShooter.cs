using UnityEngine;

public interface IShooter : ITransform
{
    public void Shoot(Vector3 direction);
}
