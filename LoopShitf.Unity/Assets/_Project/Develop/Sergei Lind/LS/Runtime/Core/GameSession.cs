using Sergei_Lind.LS.Runtime.Core.Player;
using Sergei_Lind.LS.Runtime.Core.Enemy;
using Sergei_Lind.LS.Runtime.Utilities;
using Sergei_Lind.LS.Runtime.Core.UI;
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
        private readonly GameUIController _gameUIController;

        public GameSession(PlayerController playerController,
            EnemySpawner enemySpawner,
            GameUIController gameUIController)
        {
            _playerController = playerController;
            _enemySpawner = enemySpawner;
            _gameUIController = gameUIController;
        }

        public UniTask Load()
        {
            _playerController.OnPlayerDead += HandleGameOver;
            _gameUIController.OnRestartClicked += HandleRestart;
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
            _gameUIController.ShowGameOver();
        }
        
        private void HandleRestart()
        {
            _gameUIController.HideGameOver();
            _enemySpawner.Dispose();
            _playerController.Reset();

            Start();
        }
    }
}