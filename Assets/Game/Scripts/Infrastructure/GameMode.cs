using System;
using System.Collections.Generic;

public enum VictoryType
{
    Time,
    KillCount
}

public enum DefeatType
{
    EnemyCount,
    PlayerDestroyed
}

public class GameMode
{
    public event Action Win;
    public event Action Defeat;

    private LevelConfig _levelConfig;
    private VictoryType _victoryType;
    private DefeatType _defeatType;
    private EnemySpawner _enemySpawner;
    private Player _player;
    private List<Enemy> _spawnedEnemies = new();

    private int _killCount = 0;
    private bool _isRunning = false;
    private float _time = 0;
    private float _timeLastSpawnEnemies = 0;

    public GameMode(LevelConfig levelConfig, EnemySpawner enemySpawner, Player player)
    {
        _levelConfig = levelConfig;
        _enemySpawner = enemySpawner;
        _player = player;

        _victoryType = levelConfig.VictoryType;
        _defeatType = levelConfig.DefeatType;
    }

    public void Start()
    {
        ProcessSpawnEnemies(_levelConfig.EnemiesOnStart);

        _killCount = 0;
        _isRunning = true;
    }

    public void Update(float deltaTime)
    {
        if(_isRunning == false)
            return;

        if(WinConditionCheck())
            ProcessWin();

        if(DefeatConditionCheck())
            ProcessDefeat();

        ProcessSpawnEnemies(_levelConfig.CountEnemiesToSpawnInCycle);

        _time += deltaTime;
    }

    private void ProcessSpawnEnemies(int countSpawnEnemies)
    {
        if(_time - _timeLastSpawnEnemies>= _levelConfig.EnemiesSpawnCooldown && _spawnedEnemies.Count < _levelConfig.CountEnemiesOnArenaToDefeat)
        {
            _timeLastSpawnEnemies = _time;

            List<Enemy> newSpawnedEnemies = _enemySpawner.Spawn(_levelConfig.EnemyConfig, countSpawnEnemies);

            foreach (var enemy in newSpawnedEnemies)
                enemy.Destroyed += OnEnemyDestroyed;

            _spawnedEnemies.AddRange(newSpawnedEnemies);
        }
    }

    private void ProcessEndGame()
    {
        _isRunning = false;

        foreach(var enemy in _spawnedEnemies.ToArray())
            enemy.Destroy();
    }

    private void ProcessWin()
    {
        ProcessEndGame();
        Win?.Invoke();
    }
    
    private bool WinConditionCheck()
    {
        switch (_victoryType)
        {
            case VictoryType.Time:
                return _time >= _levelConfig.TimeToWin;
            case VictoryType.KillCount:
                return _killCount >= _levelConfig.CountKilledToWin;
            default:
                return false;
        }
    }

    private bool DefeatConditionCheck()
    {
        switch (_defeatType)
        {
            case DefeatType.EnemyCount:
                return _spawnedEnemies.Count >= _levelConfig.CountEnemiesOnArenaToDefeat;
            case DefeatType.PlayerDestroyed:
                return _player.IsDestroyed;
            default:
                return false;
        }
    }

    private void ProcessDefeat()
    {
        ProcessEndGame();
        Defeat?.Invoke();
    }

    private void OnEnemyDestroyed(MonoDestroyable destroyable)
    {
        if(_isRunning == false)
            return;

        if(destroyable is Enemy enemy)
        {
            enemy.Destroyed -= OnEnemyDestroyed;
            _spawnedEnemies.Remove(enemy);
            _killCount++;
        }
    }
}
