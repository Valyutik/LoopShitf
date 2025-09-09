using Cysharp.Threading.Tasks;
using Sergei_Lind.LS.Runtime.Utilities;
using UnityEngine;

namespace Sergei_Lind.LS.Runtime.Bootstrap
{
    public class ApplicationConfigurationLoadUnit : ILoadUnit
    {
        public UniTask Load()
        {
            Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
            return UniTask.CompletedTask;
        }
    }
}