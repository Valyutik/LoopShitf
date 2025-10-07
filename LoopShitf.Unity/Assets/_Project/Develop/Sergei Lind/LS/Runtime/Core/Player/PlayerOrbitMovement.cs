using UnityEngine;

namespace Sergei_Lind.LS.Runtime.Core.Player
{
    public sealed class PlayerOrbitMovement
    {
        private readonly Vector2 _center;
        private readonly float _radius;
        private readonly float _speed;
        private readonly float _startAngleDeg;
        
        private float _direction;
        private float _currentAngleDeg;

        public PlayerOrbitMovement(Vector2 center,
            float radius,
            float startSpeed,
            float startDirection,
            float startAngleDeg)
        {
            _center = center;
            _radius = radius;
            _speed = startSpeed;
            _direction = startDirection;
            _currentAngleDeg = startAngleDeg;
            _startAngleDeg = startAngleDeg;
        }

        public Vector2 Tick(float deltaTime)
        {
            var deltaAngle = _speed * deltaTime * _direction;
            _currentAngleDeg += deltaAngle;
            if (_currentAngleDeg >= 360f) _currentAngleDeg -= 360f;
            if (_currentAngleDeg < 0f) _currentAngleDeg += 360f;

            return CalculatePosition(_currentAngleDeg);
        }
        
        public void ToggleDirection() => _direction *= -1f;

        public void ResetPosition() => _currentAngleDeg = _startAngleDeg;

        private Vector2 CalculatePosition(float angleDeg)
        {
            var rad = angleDeg * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * _radius + _center;
        }
    }
}