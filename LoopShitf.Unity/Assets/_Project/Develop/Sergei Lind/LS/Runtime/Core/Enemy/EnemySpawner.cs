using Sergei_Lind.LS.Runtime.Utilities;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer.Unity;
using UnityEngine;

namespace Sergei_Lind.LS.Runtime.Core.Enemy
{
    [UsedImplicitly]
    public sealed class EnemySpawner : IDisposableLoadUnit, ITickable
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly ConfigContainer _config;
        private readonly List<EnemyView> _activeEnemies = new();

        private float _timer;
        private bool _isActive;
        
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
            if (!_isActive) return;
            _timer += Time.deltaTime;

            if (!(_timer >= _config.Core.Enemy.SpawnInterval)) return;
            _timer = 0;
            SpawnEnemy();
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
        
        public void Start()
        {
            _isActive = true;
            foreach (var enemy in _activeEnemies)
                enemy.Resume();
        }

        public void Stop()
        {
            _isActive = false;
            foreach (var enemy in _activeEnemies)
                enemy.Stop();
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
        
        private void OnEnemyLifetimeEnded(EnemyView enemy)
        {
            _enemyFactory.Destroy(enemy);
            _activeEnemies.Remove(enemy);
            enemy.OnLifeTimeEndedEvent -= OnEnemyLifetimeEnded;
        }
    }
}