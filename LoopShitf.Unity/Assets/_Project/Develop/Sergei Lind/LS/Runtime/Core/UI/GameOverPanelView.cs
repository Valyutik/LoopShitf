using UnityEngine.UI;
using UnityEngine;
using System;

namespace Sergei_Lind.LS.Runtime.Core.UI
{
    [RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
    public sealed class GameOverPanelView : MonoBehaviour
    {
        public event Action OnRestartClicked;
        public RectTransform PanelTransform { get; private set; }

        [SerializeField] private Button restartButton;
        
        private void Awake()
        {
            PanelTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            restartButton.onClick.AddListener(HandleRestartClicked);
        }

        private void OnDisable()
        { 
            restartButton.onClick.RemoveListener(HandleRestartClicked);
        }
        
        private void HandleRestartClicked()
        {
            OnRestartClicked?.Invoke();
        }
    }
}