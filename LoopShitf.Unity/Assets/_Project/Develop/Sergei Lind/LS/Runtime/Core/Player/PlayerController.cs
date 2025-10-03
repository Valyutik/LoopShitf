using Sergei_Lind.LS.Runtime.Utilities.Logging;
using Sergei_Lind.LS.Runtime.Utilities;
using Sergei_Lind.LS.Runtime.Input;
using Cysharp.Threading.Tasks;
using VContainer.Unity;
using UnityEngine;
using System;
using JetBrains.Annotations;
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
        private readonly Health _health;
        private PlayerView _playerView;
        
        private readonly Vector2 _center;
        private readonly float _radius;
        private float _speed;
        private float _direction;
        private float _angleDeg;

        public PlayerController(PlayerFactory playerFactory,
            ConfigContainer config,
            IInput input)
        {
            _playerViewFactory = playerViewFactory;
            var playerConfig = config.Core.Player;
            _input = input;
            _health = new Health(_config.Core.Player.Health);

            _center = _config.Core.Player.Center;
            _speed = _config.Core.Player.StartSpeed;
            _direction = _config.Core.Player.StartDirection;
            _radius = _config.Core.Player.Radius;
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
            RotateByDegrees();
        }
        
        public void Dispose()
        {
            _input.OnTap -= HandleTap;
            _health.OnDead -= HandlePlayerDead;
            _playerView.OnEnemyTriggerEnter -= HandleEnemyTriggerEnter;
            
            if (_playerView != null)
                Object.Destroy(_playerView);
        }
        
        private void RotateByDegrees()
        {
            var deltaAngle = _speed * Time.fixedDeltaTime * _direction;
            _angleDeg += deltaAngle;
            if (_angleDeg >= 360f) _angleDeg -= 360f;
            if (_angleDeg < 0f) _angleDeg += 360f;
            MoveToAngle(_angleDeg);
        }
        
        private void MoveToAngle(float angleDeg)
        {
            var rad = angleDeg * Mathf.Deg2Rad;
            var targetPos = new Vector2(MathF.Cos(rad), MathF.Sin(rad)) * _radius + _center;
            
            _playerView.MoveTo(targetPos);
        }
        
        private void HandleTap()
        {
            _direction *= -1f;
        }

        private void HandlePlayerDead()
        {
            Log.Core.D("Player Dead");
        }
        
        private void HandleEnemyTriggerEnter()
        {
            _health.TakeDamage(_config.Core.Enemy.Damage);
        }
    }
}