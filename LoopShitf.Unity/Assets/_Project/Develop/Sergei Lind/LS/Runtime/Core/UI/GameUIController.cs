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
        private GameOverPanelAnimator _gameOverPanelAnimator;

        public GameUIController(GameOverPanelView gameOverPanel)
        {
            _gameOverPanel = gameOverPanel;
        }
        
        public UniTask Load()
        {
            _gameOverPanelAnimator = new GameOverPanelAnimator(_gameOverPanel.PanelTransform);
            _gameOverPanel.OnRestartClicked += HandleRestartClicked;
            return UniTask.CompletedTask;
        }

        public void Dispose()
        {
            _gameOverPanel.OnRestartClicked -= HandleRestartClicked;
        }

        public void ShowGameOver() => _gameOverPanelAnimator.Show();

        public void HideGameOver() => _gameOverPanelAnimator.Hide();
        
        private void HandleRestartClicked()
        {
            OnRestartClicked?.Invoke();
        }
    }
}