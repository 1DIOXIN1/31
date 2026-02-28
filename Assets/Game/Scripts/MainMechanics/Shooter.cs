using UnityEngine;

public class Shooter
{
    private EnemiesFactory _bulletCreator;
    private ShooterConfig _config;

    public Shooter(ShooterConfig config, EnemiesFactory bulletCreator)
    {
        _config = config;
        _bulletCreator = bulletCreator;
    }

    public void Shoot(Vector3 spawnPosition, Vector3 direction) => _bulletCreator.CreateBullet(_config, spawnPosition, direction);
}
