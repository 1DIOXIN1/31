using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner
{
    private EnemiesFactory _enemiesFactory;
    private Queue<Vector3> _spawnPositions = new();

    public EnemiesSpawner(EnemiesFactory enemiesFactory,  Transform[] spawnPoints)
    {
        _enemiesFactory = enemiesFactory;

        foreach (var spawnPoint in spawnPoints)
            _spawnPositions.Enqueue(spawnPoint.position);
    }

    public void Spawn(EnemyConfig config)
    {
        _enemiesFactory.CreateStandartEnemy(config, GetNextPosition());
    }

    private Vector3 GetNextPosition()
    {
        Vector3 nextPosition = _spawnPositions.Dequeue();
        _spawnPositions.Enqueue(nextPosition);
        return nextPosition;
    }
}
