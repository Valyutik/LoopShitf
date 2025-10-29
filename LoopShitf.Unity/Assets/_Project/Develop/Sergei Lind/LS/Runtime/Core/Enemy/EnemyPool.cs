using System.Collections.Generic;
using UnityEngine;

namespace Sergei_Lind.LS.Runtime.Core.Enemy
{
    public sealed class EnemyPool
    {
        private readonly EnemyView _prefab;
        private readonly Transform _parent;
        private readonly Queue<EnemyView> _pool = new();

        public EnemyPool(EnemyView prefab, Transform parent, int initialCount = 5)
        {
            _prefab = prefab;
            _parent = parent;
            
            for (var i = 0; i < initialCount; i++)
            {
                var enemy = CreateView();
                _pool.Enqueue(enemy);
            }
        }

        public EnemyView Get(Vector2 position)
        {
            EnemyView enemy;

            enemy = _pool.Count > 0 ? _pool.Dequeue() : CreateView();

            enemy.Resume();

            enemy.transform.position = position;
            return enemy;
        }

        public void Return(EnemyView enemy)
        {
            if (enemy == null) return;
            enemy.Reset();
            _pool.Enqueue(enemy);
        }
        
        private EnemyView CreateView()
        {
            var enemy = Object.Instantiate(_prefab, _parent);
            enemy.Reset();
            return enemy;
        }
    }
}