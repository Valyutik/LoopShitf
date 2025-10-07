using Sergei_Lind.LS.Runtime.Utilities;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using System;

namespace Sergei_Lind.LS.Runtime.Core.UI
{
    [UsedImplicitly]
    public sealed class GameUIController : ILoadUnit, IDisposable
    {
        public event Action OnRestartClicked;
        
        private readonly GameOverPanelView _gameOverPanel;

        public GameUIController(GameOverPanelView gameOverPanel)
        {
            _gameOverPanel = gameOverPanel;
        }
        
        public UniTask Load()
        {
            _gameOverPanel.OnRestartClicked += HandleRestartClicked;
            return UniTask.CompletedTask;
        }

        public void Dispose()
        {
            _gameOverPanel.OnRestartClicked -= HandleRestartClicked;
        }

        public void ShowGameOver()
        {
            _gameOverPanel.Show();
        }
        
        public void HideGameOver()
        {
            _gameOverPanel.Hide();
        }
        
        private void HandleRestartClicked()
        {
            OnRestartClicked?.Invoke();
        }
    }
}