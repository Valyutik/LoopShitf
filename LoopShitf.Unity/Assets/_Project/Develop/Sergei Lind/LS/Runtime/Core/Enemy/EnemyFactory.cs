using Sergei_Lind.LS.Runtime.Utilities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sergei_Lind.LS.Runtime.Core.Enemy
{
    public sealed class EnemyFactory : ILoadUnit
    {
        private readonly EnemyRootTransform _root;
        private readonly ConfigContainer _config;
        private EnemyPool  _pool;
        private EnemyView  _enemyPrefab;

        public EnemyFactory(EnemyRootTransform root, ConfigContainer config)
        {
            _root = root;
            _config = config;
        }

        public UniTask Load()
        {
            _enemyPrefab = AssetService.R.Load<EnemyView>(RuntimeConstants.Enemy.Prefab);
            _pool = new EnemyPool(_enemyPrefab, _root.transform, _config.Core.Enemy.InitialCount);
            return UniTask.CompletedTask;
        }

        public EnemyView Create(Vector2 position)
        {
            var enemy = _pool.Get(position);
            enemy.Initialize(_config.Core.Enemy.LifeTime, _config.Core.Enemy.Speed);
            return enemy;
        }

        public void Destroy(EnemyView enemy)
        {
            enemy.SetVelocity(0);
            _pool.Return(enemy);
        }
    }
}