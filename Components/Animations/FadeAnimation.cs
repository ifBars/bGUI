using UnityEngine;

namespace bGUI.Components.Animations
{
    /// <summary>
    /// Animation that fades a UI element in and out using CanvasGroup.
    /// </summary>
    public class FadeAnimation : BaseAnimation
    {
        private readonly float _fadeSpeed;
        private readonly float _minAlpha;
        private readonly float _maxAlpha;
        private CanvasGroup _canvasGroup;

        public FadeAnimation(PanelWrapper target, float fadeSpeed = 2f, float minAlpha = 0.5f, float maxAlpha = 1f) : base(target)
        {
            _fadeSpeed = fadeSpeed;
            _minAlpha = minAlpha;
            _maxAlpha = maxAlpha;
            _canvasGroup = target.GameObject.GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
            {
                _canvasGroup = target.GameObject.AddComponent<CanvasGroup>();
            }
        }

        protected override void OnUpdate()
        {
            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = _minAlpha + (_maxAlpha - _minAlpha) * (0.5f + 0.5f * Mathf.Sin(AnimationTime * _fadeSpeed));
            }
        }

        public override void Reset()
        {
            base.Reset();
            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = _maxAlpha;
            }
        }
    }
} 