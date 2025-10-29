using DG.Tweening;
using UnityEngine;

namespace Sergei_Lind.LS.Runtime.Core.UI
{
    public sealed class GameOverPanelAnimator
    {
        private readonly RectTransform _panelTransform;
        private readonly Vector3 _offscreenPos;
        private readonly Vector3 _onscreenPos;

        public GameOverPanelAnimator(RectTransform panelTransform)
        {
            _panelTransform = panelTransform;
            _onscreenPos = panelTransform.anchoredPosition;
            _offscreenPos = _onscreenPos + new Vector3(0, -Screen.height, 0);
            _panelTransform.anchoredPosition = _offscreenPos;
        }

        public void Show(float duration = 1f)
        {
            _panelTransform.gameObject.SetActive(true);
            _panelTransform.DOAnchorPos(_onscreenPos, duration).SetEase(Ease.OutCubic);
        }

        public void Hide(float duration = 0.5f)
        {
            _panelTransform.DOAnchorPos(_offscreenPos, duration)
                 .SetEase(Ease.InCubic)
                 .OnComplete(() => _panelTransform.gameObject.SetActive(false));
        }
    }
}