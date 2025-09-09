using Sergei_Lind.LS.Runtime.Utilities;
using Unity.Plastic.Newtonsoft.Json;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sergei_Lind.LS.Runtime
{
    public sealed class ConfigContainer : ILoadUnit
    {
        public UniTask Load()
        {
            var asset = AssetService.R.Load<TextAsset>(RuntimeConstants.Configs.ConfigFileName);
            JsonConvert.PopulateObject(asset.text, this);
            return UniTask.CompletedTask;
        }
    }
}