using Sergei_Lind.LS.Runtime.Core.Player;
using Sergei_Lind.LS.Runtime.Core.Enemy;
using Sergei_Lind.LS.Runtime.Utilities;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using System;

namespace Sergei_Lind.LS.Runtime.Core
{
    [UsedImplicitly]
    public sealed class GameSession : ILoadUnit, IDisposable
    {
        private readonly PlayerController _playerController;
        private readonly EnemySpawner _enemySpawner;

        public GameSession(PlayerController playerController, EnemySpawner enemySpawner)
        {
            _playerController = playerController;
            _enemySpawner = enemySpawner;
        }

        public UniTask Load()
        {
            _playerController.OnPlayerDead += HandleGameOver;
            return UniTask.CompletedTask;
        }
        
        public void Dispose()
        {
            _playerController.OnPlayerDead -= HandleGameOver;
        }

        public void Start()
        {
            
            _playerController.Start();
            _enemySpawner.Start();
        }

        private void HandleGameOver()
        {
            _playerController.DisableMovement();
            _enemySpawner.Stop();
        }
    }
}