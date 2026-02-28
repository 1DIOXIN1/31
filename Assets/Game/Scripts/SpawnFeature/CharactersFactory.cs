using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class CharactersFactory
{
    private ControllersFactory _controllersFactory;
    private EnemiesFactory _enemiesFactory;

    public CharactersFactory(ControllersFactory controllersFactory, EnemiesFactory enemiesFactory)
    {
        _controllersFactory = controllersFactory;
        _enemiesFactory = enemiesFactory;
    }

    public Player CreatePlayer(PlayerConfig config, Vector3 spawnPosition, float moveSpeed)
    {
        Player player = Object.Instantiate(config.Prefab, spawnPosition, Quaternion.identity);

        CharacterController characterController = player.GetComponent<CharacterController>();

        if(characterController == null) 
            throw new InvalidOperationException("Not found mover component");

        DirectionMover mover = new CharacterControllerDirectionMover(characterController, moveSpeed);
        DirectionRotator rotator = new TransformDirectionRotator(player.transform, config.RotationSpeed);
        Shooter shooter = new Shooter(config.ShooterConfig, _enemiesFactory);

        Controller movableController = _controllersFactory.CreatePlayerDirectionMovableController(player);
        Controller rotatableController = _controllersFactory.CreatePlayerDirectionRotatableController(player);
        Controller shooterController = _controllersFactory.CreatePlayerShootForwardController(player);

        player.Initialize(mover, rotator, shooter);
        
        movableController.Enable();
        rotatableController.Enable();
        shooterController.Enable();
        
        return player;
    }
}

