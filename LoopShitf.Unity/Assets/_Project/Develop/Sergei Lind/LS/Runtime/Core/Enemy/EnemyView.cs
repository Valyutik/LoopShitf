using UnityEngine;
using System;

namespace Sergei_Lind.LS.Runtime.Core.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class EnemyView : MonoBehaviour
    {
        public event Action<EnemyView> OnLifeTimeEndedEvent;
        
        private Rigidbody2D _rigidbody2D;
        private float _lifeTimer;
        private float _lifeTimerMax;
        private bool _isActive = true;
        private float _cachedVelocity;

        public void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public void Initialize(float maxLifeTime, float velocity)
        {
            _lifeTimerMax = maxLifeTime;
            _cachedVelocity = velocity;
            SetVelocity(velocity);
        }

        private void OnEnable()
        {
            _lifeTimer = 0;
            _isActive = true;
        }

        private void Update()
        {
            if (!_isActive) return;
            
            _lifeTimer += Time.deltaTime;
            if (!(_lifeTimer >= _lifeTimerMax)) return;
            OnLifeTimeEndedEvent?.Invoke(this);
        }
        
        public void SetVelocity(float velocity)
        {
            _cachedVelocity = velocity;
            if (_isActive)
                _rigidbody2D.linearVelocityX = velocity;
            else
                _rigidbody2D.linearVelocityX = 0;
        }
        
        public void Stop()
        {
            _isActive = false;
            _rigidbody2D.linearVelocity = Vector2.zero;
        }

        public void Resume()
        {
            _isActive = true;
            _rigidbody2D.linearVelocityX = _cachedVelocity;
        }
    }
}