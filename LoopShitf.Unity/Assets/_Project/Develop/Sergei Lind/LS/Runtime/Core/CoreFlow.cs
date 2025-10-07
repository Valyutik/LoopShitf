using Sergei_Lind.LS.Runtime.Utilities.Logging;
using Sergei_Lind.LS.Runtime.Core.Player.Ring;
using Sergei_Lind.LS.Runtime.Core.Player;
using Sergei_Lind.LS.Runtime.Core.Enemy;
using Sergei_Lind.LS.Runtime.Utilities;
using JetBrains.Annotations;
using VContainer.Unity;
using System;

namespace Sergei_Lind.LS.Runtime.Core
{
    [UsedImplicitly]
    public sealed class CoreFlow : IStartable, IDisposable
    {
        private readonly LoadingService _loadingService;
        
        private readonly PlayerViewFactory _playerViewFactory;
        private readonly PlayerController _playerController;
        private readonly RingFactory _ringFactory;

        private readonly EnemyFactory _enemyFactory;
        private readonly EnemySpawner _enemySpawner;
        
        private readonly GameSession _gameSession;

        public CoreFlow(LoadingService loadingService,
            PlayerViewFactory playerViewFactory,
            PlayerController playerController,
            RingFactory ringFactory,
            EnemyFactory enemyFactory,
            EnemySpawner enemySpawner,
            GameSession gameSession)
        {
            _loadingService = loadingService;
            _playerViewFactory = playerViewFactory;
            _playerController = playerController;
            _ringFactory = ringFactory;
            _enemyFactory = enemyFactory;
            _enemySpawner = enemySpawner;
            _gameSession = gameSession;
        }
        
        public async void Start()
        {
            await _loadingService.BeginLoading(_playerViewFactory);
            await _loadingService.BeginLoading(_playerController);
            await _loadingService.BeginLoading(_ringFactory);

            await _loadingService.BeginLoading(_enemyFactory);
            await _loadingService.BeginLoading(_enemySpawner);
            
            await _loadingService.BeginLoading(_gameSession);
            
            _gameSession.Start();
            
            Log.Core.D("CoreFlow.Start");
        }

        public void Dispose()
        {
            _loadingService.disposable.Dispose();
            Log.Core.D("CoreFlow.Dispose()");
        }
    }
}