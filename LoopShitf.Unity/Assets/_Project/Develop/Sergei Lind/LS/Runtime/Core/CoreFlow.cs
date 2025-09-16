using Sergei_Lind.LS.Runtime.Utilities.Logging;
using Sergei_Lind.LS.Runtime.Core.Player;
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

        public CoreFlow(LoadingService loadingService,
            PlayerFactory playerFactory,
            PlayerController playerController)
        {
            _loadingService = loadingService;
            _playerFactory = playerFactory;
            _playerController = playerController;
        }
        
        public async void Start()
        {
            await _loadingService.BeginLoading(_playerFactory);
            await _loadingService.BeginLoading(_playerController);
            Log.Core.D("CoreFlow.Start");
        }

        public void Dispose()
        {
            Log.Core.D("CoreFlow.Dispose()");
        }
    }
}