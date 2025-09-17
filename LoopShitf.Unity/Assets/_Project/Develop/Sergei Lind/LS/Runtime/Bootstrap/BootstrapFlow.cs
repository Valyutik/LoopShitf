using Sergei_Lind.LS.Runtime.Utilities.Logging;
using Sergei_Lind.LS.Runtime.Utilities;
using Sergei_Lind.LS.Runtime.Input;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Sergei_Lind.LS.Runtime.Bootstrap
{
    public class BootstrapFlow : IStartable
    {
        private readonly LoadingService _loadingService;
        private readonly ConfigContainer _configContainer;
        private readonly InputService _inputService;

        public BootstrapFlow(LoadingService loadingService,
            ConfigContainer configContainer,
            InputService inputService)
        {
            _loadingService = loadingService;
            _configContainer = configContainer;
            _inputService = inputService;
        }

        public async void Start()
        {
            Log.Boot.D("BootstrapFlow.Start()");

            await _loadingService.BeginLoading(new ApplicationConfigurationLoadUnit());
            await _loadingService.BeginLoading(_configContainer);
            await _loadingService.BeginLoading(_inputService);
            SceneManager.LoadScene(RuntimeConstants.Scenes.Core);
        }
    }
}