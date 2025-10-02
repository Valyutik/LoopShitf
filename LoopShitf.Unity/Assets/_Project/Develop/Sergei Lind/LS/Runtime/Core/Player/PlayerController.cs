using Sergei_Lind.LS.Runtime.Utilities.Logging;
using Sergei_Lind.LS.Runtime.Utilities;
using Sergei_Lind.LS.Runtime.Input;
using Cysharp.Threading.Tasks;
using VContainer.Unity;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

namespace Sergei_Lind.LS.Runtime.Core.Player
{
    public sealed class PlayerController : IDisposableLoadUnit, IFixedTickable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly ConfigContainer _config;
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
            _playerFactory = playerFactory;
            _config = config;
            _input = input;
            _health = new Health(_config.Core.Player.Health);

            _center = _config.Core.Player.Center;
            _speed = _config.Core.Player.StartSpeed;
            _direction = _config.Core.Player.StartDirection;
            _radius = _config.Core.Player.Radius;
        }
        
        public UniTask Load()
        {
            _playerView = _playerFactory.CreatePlayerView();
            _speed = _config.Core.Player.StartSpeed;
            _direction = _config.Core.Player.StartDirection;

            _input.Tap += OnToggleDirection;
            _health.OnDead += OnPlayerDead;
            _playerView.OnEnemyTriggerEnter += OnEnemyTriggerEnter;
            
            return UniTask.CompletedTask;
        }

        public void FixedTick()
        {
            RotateByDegrees();
        }
        
        public void Dispose()
        {
            _input.Tap -= OnToggleDirection;
            _health.OnDead -= OnPlayerDead;
            _playerView.OnEnemyTriggerEnter -= OnEnemyTriggerEnter;
            
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
        
        private void OnToggleDirection()
        {
            _direction *= -1f;
        }

        private void OnPlayerDead()
        {
            Log.Core.D("Player Dead");
        }
        
        private void OnEnemyTriggerEnter()
        {
            _health.TakeDamage(_config.Core.Enemy.Damage);
        }
    }
}