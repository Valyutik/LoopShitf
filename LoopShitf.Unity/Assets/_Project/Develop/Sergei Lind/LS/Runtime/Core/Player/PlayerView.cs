using UnityEngine;

namespace Sergei_Lind.LS.Runtime.Core.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerView : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private float _circleSize = 0.5f;

        public void Initialize(float circleSize)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            
            _circleSize = circleSize;
            
            transform.localScale = new Vector3(_circleSize, _circleSize, 1f);
        }

        public void MoveTo(Vector2 position)
        {
            _rigidbody2D.MovePosition(position);
        }
    }
}