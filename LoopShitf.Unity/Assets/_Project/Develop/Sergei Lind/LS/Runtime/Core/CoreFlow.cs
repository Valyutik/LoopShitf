using Sergei_Lind.LS.Runtime.Utilities.Logging;
using Sergei_Lind.LS.Runtime.Core.Player.Ring;
using Sergei_Lind.LS.Runtime.Core.Player;
using Sergei_Lind.LS.Runtime.Core.Enemy;
using Sergei_Lind.LS.Runtime.Utilities;
using VContainer.Unity;
using System;

namespace Sergei_Lind.LS.Runtime.Core
{
    public sealed class CoreFlow : IStartable, IDisposable
    {
        private readonly LoadingService _loadingService;
        
        private readonly PlayerFactory _playerFactory;
        private readonly PlayerController _playerController;
        private readonly RingFactory _ringFactory;

        private readonly EnemyFactory _enemyFactory;
        private readonly EnemySpawner _enemySpawner;

        public CoreFlow(LoadingService loadingService,
            PlayerFactory playerFactory,
            PlayerController playerController,
            RingFactory ringFactory,
            EnemyFactory enemyFactory,
            EnemySpawner enemySpawner)
        {
            _loadingService = loadingService;
            _playerFactory = playerFactory;
            _playerController = playerController;
            _ringFactory = ringFactory;
            _enemyFactory = enemyFactory;
            _enemySpawner = enemySpawner;
        }
        
        public async void Start()
        {
            await _loadingService.BeginLoading(_playerFactory);
            await _loadingService.BeginLoading(_playerController);
            await _loadingService.BeginLoading(_ringFactory);

            await _loadingService.BeginLoading(_enemyFactory);
            await _loadingService.BeginLoading(_enemySpawner);
            
            Log.Core.D("CoreFlow.Start");
        }

        public void Dispose()
        {
            _loadingService.Disposable.Dispose();
            Log.Core.D("CoreFlow.Dispose()");
        }
    }
}