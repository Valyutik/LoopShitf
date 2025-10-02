using Sergei_Lind.LS.Runtime.Core.Enemy;
using UnityEngine;
using System;

namespace Sergei_Lind.LS.Runtime.Core.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public sealed class PlayerView : MonoBehaviour
    {
        public event Action OnEnemyTriggerEnter;
        
        private Rigidbody2D _rigidbody2D;
        private float _circleSize = 0.5f;

        public void Initialize(float circleSize)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            
            _circleSize = circleSize;
            
            transform.localScale = new Vector3(_circleSize, _circleSize, 1f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<EnemyView>(out _))
            {
                OnEnemyTriggerEnter?.Invoke();
            }
        }

        public void MoveTo(Vector2 position)
        {
            _rigidbody2D.MovePosition(position);
        }
    }
}