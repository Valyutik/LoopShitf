using Cysharp.Threading.Tasks;
using Sergei_Lind.LS.Runtime.Utilities;
using UnityEngine;

namespace Sergei_Lind.LS.Runtime.Core.Player.Ring
{
    public sealed class RingFactory : ILoadUnit
    {
        private readonly ConfigContainer _config;
        private readonly RootTransform _root;
        private RingView _ringView;

        public RingFactory(ConfigContainer config, RootTransform root)
        {
            _config = config;
            _root = root;
        }

        public UniTask Load()
        {
            CreateRingView();
            return UniTask.CompletedTask;
        }

        private void CreateRingView()
        {
            var gameObject = new GameObject("RingView");
            _ringView = Object.Instantiate(gameObject, _root.transform).AddComponent<RingView>();
            _ringView.Generate(_config.Core.Player.Radius,
                _config.Core.Player.CircleSize,
                _config.Core.Player.Ring.Offset,
                _config.Core.Player.Ring.SegmentsCount,
                _config.Core.Player.Ring.Color);
        }
    }
}