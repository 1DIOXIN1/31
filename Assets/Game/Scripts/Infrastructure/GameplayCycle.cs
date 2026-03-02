using System;
using UnityEngine;

public class GameplayCycle : IDisposable
{
    private GameMode _gameMode;

    public GameplayCycle(GameMode gameMode)
    {
        _gameMode = gameMode;

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
