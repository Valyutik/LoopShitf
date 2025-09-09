using Sergei_Lind.LS.Runtime.Utilities.Logging;
using Sergei_Lind.LS.Runtime.Utilities;
using VContainer.Unity;
using System;

namespace Sergei_Lind.LS.Runtime.Core
{
    public sealed class CoreFlow : IStartable, IDisposable
    {
        private readonly LoadingService _loadingService;

        public CoreFlow(LoadingService loadingService)
        {
            _loadingService = loadingService;
        }

        public void Start()
        {
            Log.Core.D("CoreFlow.Start");
        }

        public void Dispose()
        {
            Log.Core.D("CoreFlow.Dispose()");
        }
    }
}