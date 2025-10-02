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

        public void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public void Initialize(float maxLifeTime, float speed)
        {
            _lifeTimerMax = maxLifeTime;
            SetVelocity(speed);
        }

        private void OnEnable()
        {
            _lifeTimer = 0;
        }

        public void SetVelocity(float velocity)
        {
            _rigidbody2D.linearVelocityX = velocity;
        }

        private void Update()
        {
            if (!gameObject.activeSelf) return;
            _lifeTimer += Time.deltaTime;
            if (!(_lifeTimer >= _lifeTimerMax)) return;
            OnLifeTimeEndedEvent?.Invoke(this);
        }
    }
}