using Sergei_Lind.LS.Runtime.Utilities.Logging;
using Sergei_Lind.LS.Runtime.Utilities;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Sergei_Lind.LS.Runtime.Bootstrap
{
    public class BootstrapFlow : IStartable
    {
        private readonly LoadingService _loadingService;
        private readonly ConfigContainer _configContainer;

        public BootstrapFlow(LoadingService loadingService, ConfigContainer configContainer)
        {
            _loadingService = loadingService;
            _configContainer = configContainer;
        }

        public async void Start()
        {
            Log.Boot.D("BootstrapFlow.Start()");

            await _loadingService.BeginLoading(new ApplicationConfigurationLoadUnit());
            await _loadingService.BeginLoading(_configContainer);
            SceneManager.LoadScene(RuntimeConstants.Scenes.Core);
        }
    }
}