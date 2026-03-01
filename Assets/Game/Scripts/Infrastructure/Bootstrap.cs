using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Transform[] _enemiesSpawnPoints;

    private CharactersFactory _charactersFactory;
    private ControllersFactory _controllersFactory;
    private EnemiesFactory _enemiesFactory;
    private EnemySpawner _enemySpawner;
    private ControllersUpdateService _controllersUpdateService;
    private GameplayCycle _gameplayCycle;

    private void Awake()
    {
        Initialization();
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
        _enemySpawner = new EnemySpawner(_enemiesFactory, _enemiesSpawnPoints.Select(t => t.position).ToArray());

        _gameplayCycle = new GameplayCycle(_levelConfig, _playerConfig, _enemySpawner, _charactersFactory);
    }

    private void OnDestroy()
    {
        _gameplayCycle.Dispose();
    }
}
