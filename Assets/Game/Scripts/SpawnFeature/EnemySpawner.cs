using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    private EnemiesFactory _enemiesFactory;
    private Queue<Vector3> _spawnPositions = new();

    public EnemySpawner(EnemiesFactory enemiesFactory,  Vector3[] spawnPositions)
    {
        _enemiesFactory = enemiesFactory;

        foreach (var spawnPosition in spawnPositions)
            _spawnPositions.Enqueue(spawnPosition);
    }

    public List<Enemy> Spawn(EnemyConfig config, int count)
    {
        List<Enemy> enemies = new(count);

        for (int i = 0; i < count; i++)
        {
            Enemy enemy = _enemiesFactory.CreateStandartEnemy(config, GetNextPosition());
            enemies.Add(enemy);
        }

        return enemies;
    }

    private Vector3 GetNextPosition()
    {
        Vector3 nextPosition = _spawnPositions.Dequeue();
        _spawnPositions.Enqueue(nextPosition);
        return nextPosition;
    }
}
