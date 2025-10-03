using Sergei_Lind.LS.Runtime.Utilities.Logging;
using Sergei_Lind.LS.Runtime.Utilities;
using Sergei_Lind.LS.Runtime.Input;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer.Unity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sergei_Lind.LS.Runtime.Core.Player
{
    [UsedImplicitly]
    public sealed class PlayerController : IDisposableLoadUnit, IFixedTickable
    {
        private readonly PlayerViewFactory _playerViewFactory;
        private readonly PlayerOrbitMovement _playerMovement;
        private readonly ConfigContainer _configContainer;
        private readonly Health _health;
        private readonly IInput _input;
        private PlayerView _playerView;
        
        private bool _canMove = true;

        public PlayerController(PlayerViewFactory playerViewFactory,
            ConfigContainer config,
            IInput input)
        {
            _playerViewFactory = playerViewFactory;
            _configContainer = config;
            var playerConfig = config.Core.Player;
            _input = input;
            
            _health = new Health(playerConfig.Health);
            _playerMovement = new PlayerOrbitMovement(playerConfig.Center,
                playerConfig.Radius,
                playerConfig.StartSpeed,
                playerConfig.StartDirection,
                playerConfig.StartAngleDeg);
        }
        
        public UniTask Load()
        {
            _playerView = _playerViewFactory.CreatePlayerView();

            _input.OnTap += HandleTap;
            _health.OnDead += HandlePlayerDead;
            _playerView.OnEnemyTriggerEnter += HandleEnemyTriggerEnter;
            
            return UniTask.CompletedTask;
        }

        public void FixedTick()
        {
            UpdatePosition();
        }

        public void Dispose()
        {
            _input.OnTap -= HandleTap;
            _health.OnDead -= HandlePlayerDead;
            _playerView.OnEnemyTriggerEnter -= HandleEnemyTriggerEnter;
            
            if (_playerView != null)
                Object.Destroy(_playerView);
        }
        
        public void EnableMovement() => _canMove = true;
        public void DisableMovement() => _canMove = false;
        
        private void UpdatePosition()
        {
            if (!_canMove) return;
            var position = _playerMovement.Tick(Time.fixedDeltaTime);
            _playerView.MoveTo(position);
        }
        
        private void HandleTap()
        {
            _playerMovement.ToggleDirection();
        }

        private void HandlePlayerDead()
        {
            Log.Core.D("Player Dead");
        }
        
        private void HandleEnemyTriggerEnter()
        {
            _health.TakeDamage(_configContainer.Core.Enemy.Damage);
        }
    }
}