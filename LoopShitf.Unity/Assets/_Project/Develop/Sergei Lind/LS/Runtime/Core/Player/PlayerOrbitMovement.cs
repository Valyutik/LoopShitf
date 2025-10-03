using UnityEngine;

namespace Sergei_Lind.LS.Runtime.Core.Player
{
    public sealed class PlayerOrbitMovement
    {
        private readonly Vector2 _center;
        private readonly float _radius;
        private readonly float _speed;
        
        private float _direction;
        private float _angleDeg;

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
            _angleDeg = startAngleDeg;
        }

        public Vector2 Tick(float deltaTime)
        {
            var deltaAngle = _speed * deltaTime * _direction;
            _angleDeg += deltaAngle;
            if (_angleDeg >= 360f) _angleDeg -= 360f;
            if (_angleDeg < 0f) _angleDeg += 360f;

            return CalculatePosition(_angleDeg);
        }

        private Vector2 CalculatePosition(float angleDeg)
        {
            var rad = angleDeg * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * _radius + _center;
        }

        public void ToggleDirection() => _direction *= -1f;
    }
}