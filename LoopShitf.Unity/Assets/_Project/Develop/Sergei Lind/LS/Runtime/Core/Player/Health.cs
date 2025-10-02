using System;

namespace Sergei_Lind.LS.Runtime.Core.Player
{
    public sealed class Health
    {
        public event Action OnDead;

        private int _current;

        private bool IsDead => _current <= 0;

        public Health(int max)
        {
            _current = max;
        }

        public void TakeDamage(int amount)
        {
            _current -= amount;
            
            if (!IsDead) return;
            OnDead?.Invoke();
        }
    }
}