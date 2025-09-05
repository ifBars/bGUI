using UnityEngine;

namespace bGUI.Components.Animations
{
    /// <summary>
    /// Animation that pulses a UI element by scaling it up and down.
    /// </summary>
    public class PulseAnimation : BaseAnimation
    {
        private readonly float _pulseSpeed;
        private readonly float _pulseAmount;

        public PulseAnimation(PanelWrapper target, float pulseSpeed = 3f, float pulseAmount = 0.2f) : base(target)
        {
            _pulseSpeed = pulseSpeed;
            _pulseAmount = pulseAmount;
        }

        protected override void OnUpdate()
        {
            float pulseScale = 1.0f + _pulseAmount * Mathf.Sin(AnimationTime * _pulseSpeed);
            Target.RectTransform.localScale = new Vector3(pulseScale, pulseScale, 1);
        }

        public override void Reset()
        {
            base.Reset();
            Target.RectTransform.localScale = Vector3.one;
        }
    }
} 