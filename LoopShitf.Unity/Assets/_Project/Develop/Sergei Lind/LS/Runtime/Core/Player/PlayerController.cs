using Sergei_Lind.LS.Runtime.Core.Enemy;
using Sergei_Lind.LS.Runtime.Utilities;
using Sergei_Lind.LS.Runtime.Input;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer.Unity;
using UniRx.Triggers;
using System;
using UniRx;
using Object = UnityEngine.Object;

namespace Sergei_Lind.LS.Runtime.Core.Player
{
    [UsedImplicitly]
    public sealed class PlayerController : IDisposableLoadUnit, IFixedTickable
    {
        public event Action OnPlayerDead;
        
        private readonly PlayerViewFactory _playerViewFactory;
        private readonly PlayerOrbitMovement _playerMovement;
        private readonly ConfigContainer _configContainer;
        private readonly Health _health;
        private readonly IInput _input;
        private PlayerView _playerView;
        
        private readonly CompositeDisposable _disposables = new();
        private bool _canMove;

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
            _health.IsDead.Where(dead => dead).Subscribe(_ => OnPlayerDead?.Invoke()).AddTo(_disposables);
            _playerView.OnTriggerEnter2DAsObservable()
                .Where(other => other.GetComponent<EnemyView>() != null)
                .Subscribe(_ => _health.TakeDamage(_configContainer.Core.Enemy.Damage))
                .AddTo(_disposables);
            
            return UniTask.CompletedTask;
        }

        public void FixedTick()
        {
            UpdatePosition();
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _input.OnTap -= HandleTap;
            
            if (_playerView != null)
                Object.Destroy(_playerView);
        }
        
        public void Start()
        {
            EnableMovement();
            _playerView.gameObject.SetActive(true);
        }

        public void Stop()
        {
            DisableMovement();
            _playerView.gameObject.SetActive(false);
        }
        
        public void EnableMovement() => _canMove = true;
        public void DisableMovement() => _canMove = false;

        public void Reset()
        {
            _playerMovement.ResetPosition();
            _health.Reset();
        }
        
        private void UpdatePosition()
        {
            if (!_canMove) return;
            var position = _playerMovement.Tick();
            _playerView.MoveTo(position);
        }
        
        private void HandleTap()
        {
            _playerMovement.ToggleDirection();
        }
    }
}