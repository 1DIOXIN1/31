using System;
using UnityEngine;

public class GameplayCycle : IDisposable
{
    private GameMode _gameMode;
    private LevelConfig _levelConfig;
    private PlayerConfig _playerConfig;
    private EnemySpawner _enemySpawner;
    private CharactersFactory _characterFactory;

    public GameplayCycle(LevelConfig levelConfig, PlayerConfig playerConfig, EnemySpawner enemySpawner, CharactersFactory characterFactory)
    {
        _levelConfig = levelConfig;
        _playerConfig = playerConfig;
        _enemySpawner = enemySpawner;
        _characterFactory = characterFactory;

        Prepare();
    }

    public void Update(float deltaTime)
    {
        _gameMode?.Update(deltaTime);
    }

    public void Dispose()
    {
        OnGameModeEnded();
    }

    private void Prepare()
    {
        Player player = _characterFactory.CreatePlayer(_playerConfig, _levelConfig.PlayerStartPosition);

        _gameMode = new GameMode(_levelConfig, _enemySpawner, player);
        _gameMode.Start();

        _gameMode.Win += OnGameModeWin;
        _gameMode.Defeat += OnGameModeDefeat;
    }

    private void OnGameModeWin()
    {
        OnGameModeEnded();
        Debug.Log("Win");
    }

    private void OnGameModeDefeat()
    {
        OnGameModeEnded();
        Debug.Log("Defeat");

    }

    private void OnGameModeEnded()
    {
        if(_gameMode != null)
        {
            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;
        }
    }
}
