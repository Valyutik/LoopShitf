using UnityEngine;

namespace Sergei_Lind.LS.Runtime.Core.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class EnemyView : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        public void Initialize(Vector2 velocity)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            _rigidbody2D.linearVelocity = velocity;
        }
    }
}