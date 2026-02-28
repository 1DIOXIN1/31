using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private EnemyConfig _enemyConfig;

    private CharactersFactory _charactersFactory;
    private ControllersFactory _controllersFactory;
    private EnemiesFactory _enemiesFactory;
    private ControllersUpdateService _controllersUpdateService;

    private void Awake()
    {
        Initialization();
    }

    private void Update()
    {
        _controllersUpdateService?.Update(Time.deltaTime);
    }

    private void Initialization()
    {
        _controllersUpdateService = new ControllersUpdateService();
        _controllersFactory = new ControllersFactory(_controllersUpdateService);
        _enemiesFactory = new EnemiesFactory(_controllersFactory);
        _charactersFactory = new CharactersFactory(_controllersFactory, _enemiesFactory);

        // _charactersFactory.CreatePlayer(_playerConfig, _playerSpawnPoint.position, _playerConfig.MoveSpeed);
        _enemiesFactory.CreateStandartEnemy(_enemyConfig, _playerSpawnPoint.position);
    }
}
