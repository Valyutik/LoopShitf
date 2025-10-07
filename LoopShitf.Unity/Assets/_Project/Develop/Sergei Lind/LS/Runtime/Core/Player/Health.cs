using UnityEngine;
using UniRx;

namespace Sergei_Lind.LS.Runtime.Core.Player
{
    public sealed class Health
    {
        public IReadOnlyReactiveProperty<int> Current => _current;
        public IReadOnlyReactiveProperty<bool> IsDead => _isDead;
        
        private readonly ReactiveProperty<int> _current;
        private readonly ReactiveProperty<bool> _isDead = new(false);
        
        private readonly int _max;

        public Health(int max)
        {
            _max = max;
            _current = new ReactiveProperty<int>(_max);
        }

        public void TakeDamage(int amount)
        {
            if (_isDead.Value)
                return;
            _current.Value = Mathf.Max(0, _current.Value - amount);

            if (_current.Value <= 0)
                _isDead.Value = true;
        }

        public void Reset()
        {
            _current.Value = _max;
            _isDead.Value = false;
        }
    }
}