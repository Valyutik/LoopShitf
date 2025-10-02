using Sergei_Lind.LS.Runtime.Utilities;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using VContainer.Unity;
using UnityEngine;

namespace Sergei_Lind.LS.Runtime.Core.Enemy
{
    public sealed class EnemySpawner : IDisposableLoadUnit, ITickable
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly ConfigContainer _config;
        private readonly List<EnemyView> _activeEnemies = new();

        private float _timer;
        
        public EnemySpawner(EnemyFactory enemyFactory, ConfigContainer config)
        {
            _enemyFactory = enemyFactory;
            _config = config;
        }

        public UniTask Load()
        {
            _timer = 0;
            return UniTask.CompletedTask;
        }

        public void Tick()
        {
            _timer += Time.deltaTime;

            if (!(_timer >= _config.Core.Enemy.SpawnInterval)) return;
            _timer = 0;
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            var angleRad = Random.Range(0f, 2 * Mathf.PI);
            var radius = _config.Core.Player.Radius;

            if (Camera.main == null) return;
            var screenLeft = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
            var spawnX = screenLeft - _config.Core.Enemy.SpawnOffset;
            var spawnY = Mathf.Sin(angleRad) * radius;
            
            var position = new Vector2(spawnX, spawnY);
            var enemy = _enemyFactory.Create(position);
            enemy.OnLifeTimeEndedEvent += OnEnemyLifetimeEnded;
            _activeEnemies.Add(enemy);
        }

        public void Dispose()
        {
            foreach (var enemy in _activeEnemies)
            {
                enemy.OnLifeTimeEndedEvent -= OnEnemyLifetimeEnded;
                _enemyFactory.Destroy(enemy);
            }
            _activeEnemies.Clear();
        }
        
        private void OnEnemyLifetimeEnded(EnemyView enemy)
        {
            _enemyFactory.Destroy(enemy);
            _activeEnemies.Remove(enemy);
            enemy.OnLifeTimeEndedEvent -= OnEnemyLifetimeEnded;
        }
    }
}