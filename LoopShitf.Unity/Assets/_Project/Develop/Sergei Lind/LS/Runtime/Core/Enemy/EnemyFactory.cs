using Sergei_Lind.LS.Runtime.Utilities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sergei_Lind.LS.Runtime.Core.Enemy
{
    public sealed class EnemyFactory : ILoadUnit
    {
        private readonly RootTransform _root;
        private readonly ConfigContainer _config;
        private EnemyView  _enemyPrefab;

        public EnemyFactory(RootTransform root, ConfigContainer config)
        {
            _root = root;
            _config = config;
        }

        public UniTask Load()
        {
            _enemyPrefab = AssetService.R.Load<EnemyView>(RuntimeConstants.Enemy.Prefab);
            return UniTask.CompletedTask;
        }

        public EnemyView Create(Vector2 position)
        {
            var enemy = Object.Instantiate(_enemyPrefab,
                position,
                Quaternion.identity,
                _root.transform);
            enemy.Initialize(new Vector2(_config.Core.Enemy.Speed, 0));
            return enemy;
        }
    }
}