using Sergei_Lind.LS.Runtime.Utilities;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace Sergei_Lind.LS.Runtime.Core.Player
{
    [UsedImplicitly]
    public sealed class PlayerViewFactory : ILoadUnit
    {
        private readonly ConfigContainer _config;
        private readonly RootTransform _rootTransform;
        private PlayerView _playerView;

        public PlayerViewFactory(ConfigContainer config, RootTransform rootTransform)
        {
            _config = config;
            _rootTransform = rootTransform;
        }

        public UniTask Load()
        {
            _playerView = AssetService.R.Load<PlayerView>(RuntimeConstants.Player.Prefab);
            return UniTask.CompletedTask;
        }

        public PlayerView CreatePlayerView()
        {
            var playerView = Object.Instantiate(_playerView, _rootTransform.transform);
            playerView.gameObject.SetActive(false);
            playerView.Initialize(_config.Core.Player.CircleSize);
            return playerView;
        }
    }
}