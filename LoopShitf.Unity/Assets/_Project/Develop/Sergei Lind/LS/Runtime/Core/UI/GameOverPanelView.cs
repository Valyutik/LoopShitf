using UnityEngine.UI;
using UnityEngine;
using System;

namespace Sergei_Lind.LS.Runtime.Core.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class GameOverPanelView : MonoBehaviour
    {
        public event Action OnRestartClicked;
        
        [SerializeField] private Button restartButton;
        
        private CanvasGroup _canvasGroup;
        
        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            Hide();
        }

        private void OnEnable()
        {
            restartButton.onClick.AddListener(HandleRestartClicked);
        }

        private void OnDisable()
        { 
            restartButton.onClick.RemoveListener(HandleRestartClicked);
        }

        public void Show()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
        
        private void HandleRestartClicked()
        {
            OnRestartClicked?.Invoke();
        }
    }
}