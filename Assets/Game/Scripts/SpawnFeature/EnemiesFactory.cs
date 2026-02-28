using System;
using Object = UnityEngine.Object;
using UnityEngine;

public class EnemiesFactory
{
    private ControllersFactory _controllersFactory;
    public EnemiesFactory(ControllersFactory controllersFactory)
    {
        _controllersFactory = controllersFactory;
    }

    public Bullet CreateBullet(ShooterConfig config, Vector3 spawnPosition, Vector3 direction)
    {
        Bullet bullet = Object.Instantiate(config.Prefab, spawnPosition, Quaternion.identity);
        bullet.Initilize(direction, config.MoveSpeed, config.Damage);
        
        return bullet;
    }

    public Enemy CreateStandartEnemy(EnemyConfig config, Vector3 spawnPosition)
    {
        Enemy enemy = Object.Instantiate(config.Prefab, spawnPosition, Quaternion.identity);

        CharacterController characterController = enemy.GetComponent<CharacterController>();

        if(characterController == null) 
            throw new InvalidOperationException("Not found mover component");
            
        DirectionMover mover = new CharacterControllerDirectionMover(characterController, config.MoveSpeed);
        DirectionRotator rotator = new TransformDirectionRotator(enemy.transform, config.RotationSpeed);
        RandomAIDirectionController controller = _controllersFactory.CreateRandomAIDirectionController(enemy, enemy, 3);

        enemy.Initialize(config, mover, rotator);

        controller.Enable();

        return enemy;
    }
}
