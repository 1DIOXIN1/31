using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Transform[] _enemiesSpawnPoints;

    private CharactersFactory _charactersFactory;
    private PlayerProvider _playerProvider;
    private ControllersFactory _controllersFactory;
    private EnemiesFactory _enemiesFactory;
    private EnemySpawner _enemySpawner;
    private ControllersUpdateService _controllersUpdateService;
    private GameplayCycle _gameplayCycle;
    private GameMode _gameMode;

    private void Awake()
    {
        Initialization();
        StartGameplay();
    }

    private void Update()
    {
        _controllersUpdateService?.Update(Time.deltaTime);
        _gameplayCycle?.Update(Time.deltaTime);
    }

    private void Initialization()
    {
        _controllersUpdateService = new ControllersUpdateService();
        
        _controllersFactory = new ControllersFactory();
        _enemiesFactory = new EnemiesFactory(_controllersFactory, _controllersUpdateService);
        _charactersFactory = new CharactersFactory(_controllersFactory, _controllersUpdateService, _enemiesFactory);
        _playerProvider = new PlayerProvider(_charactersFactory);
        _enemySpawner = new EnemySpawner(_enemiesFactory, _enemiesSpawnPoints.Select(t => t.position).ToArray());
    }

    private void StartGameplay()
    {
        _charactersFactory.CreatePlayer(_playerConfig, _levelConfig.PlayerStartPosition);

        _gameMode = new GameMode(_levelConfig, _enemySpawner, _playerProvider.Player);
        _gameplayCycle = new GameplayCycle(_gameMode);
    }

    private void OnDestroy()
    {
        _gameplayCycle.Dispose();
    }
}
