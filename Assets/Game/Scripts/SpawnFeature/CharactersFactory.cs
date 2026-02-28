using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class CharactersFactory
{
    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;
    private EnemiesFactory _enemiesFactory;

    public CharactersFactory(ControllersFactory controllersFactory, ControllersUpdateService controllersUpdateService, EnemiesFactory enemiesFactory)
    {
        _controllersFactory = controllersFactory;
        _controllersUpdateService = controllersUpdateService;
        _enemiesFactory = enemiesFactory;
    }

    public Player CreatePlayer(PlayerConfig config, Vector3 spawnPosition)
    {
        Player player = Object.Instantiate(config.Prefab, spawnPosition, Quaternion.identity);

        Rigidbody rigidbody = player.GetComponent<Rigidbody>();

        if(rigidbody == null) 
            throw new InvalidOperationException("Not found mover component");

        DirectionMover mover = new RigidbodyDirectionalMover(rigidbody, config.MoveSpeed);
        DirectionRotator rotator = new TransformDirectionRotator(player.transform, config.RotationSpeed);
        Shooter shooter = new Shooter(config.ShooterConfig, _enemiesFactory);

        Controller movableController = _controllersFactory.CreatePlayerDirectionMovableController(player);
        _controllersUpdateService.Add(movableController, () => player.IsDestroyed);
        
        Controller rotatableController = _controllersFactory.CreatePlayerDirectionRotatableController(player);
        _controllersUpdateService.Add(rotatableController, () => player.IsDestroyed);

        Controller shooterController = _controllersFactory.CreatePlayerShootForwardController(player);
        _controllersUpdateService.Add(shooterController, () => player.IsDestroyed);

        player.Initialize(config, mover, rotator, shooter);
        
        movableController.Enable();
        rotatableController.Enable();
        shooterController.Enable();
        
        return player;
    }
}

