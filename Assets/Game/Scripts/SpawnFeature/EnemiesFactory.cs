using System;
using Object = UnityEngine.Object;
using UnityEngine;

public class EnemiesFactory
{
    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;
    public EnemiesFactory(ControllersFactory controllersFactory, ControllersUpdateService controllersUpdateService)
    {
        _controllersFactory = controllersFactory;
        _controllersUpdateService = controllersUpdateService;
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

        _controllersUpdateService.Add(controller, () => enemy.IsDestroyed);

        enemy.Initialize(config, mover, rotator);

        controller.Enable();

        return enemy;
    }
}
