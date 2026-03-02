using System;
using System.Collections.Generic;

public class GameMode : IGameModeState
{
    public event Action Win;
    public event Action Defeat;

    private LevelConfig _levelConfig;
    private IGameCondition _winCondition;
    private IGameCondition _defeatCondition;
    private EnemySpawner _enemySpawner;
    private Player _player;
    private List<Enemy> _spawnedEnemies = new();

    private bool _isRunning = false;
    private float _timeLastSpawnEnemies = 0;

    public GameMode(LevelConfig levelConfig, EnemySpawner enemySpawner, Player player)
    {
        _levelConfig = levelConfig;
        _enemySpawner = enemySpawner;
        _player = player;

        ConditionFactory conditionFactory = new ConditionFactory(_levelConfig);

        _winCondition = conditionFactory.CreateVictoryCondition();
        _defeatCondition = conditionFactory.CreateDefeatCondition();
    }

    public float Time {get; private set; } = 0;
    public int KillCount {get; private set; } = 0;
    public int AliveEnemiesCount => _spawnedEnemies.Count;
    public bool IsPlayerDestroyed => _player.IsDestroyed;

    public void Start()
    {
        ProcessSpawnEnemies(_levelConfig.EnemiesOnStart);

        KillCount = 0;
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

        Time += deltaTime;
    }

    private void ProcessSpawnEnemies(int countSpawnEnemies)
    {
        if(Time - _timeLastSpawnEnemies>= _levelConfig.EnemiesSpawnCooldown && _spawnedEnemies.Count < _levelConfig.CountEnemiesOnArenaToDefeat)
        {
            _timeLastSpawnEnemies = Time;

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
    
    private bool WinConditionCheck() => _winCondition.Check(this);

    private bool DefeatConditionCheck() => _defeatCondition.Check(this);

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
            KillCount++;
        }
    }
}
