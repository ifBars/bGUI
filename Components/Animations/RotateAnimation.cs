using UnityEngine;

namespace bGUI.Components.Animations
{
    /// <summary>
    /// Animation that rotates a UI element continuously.
    /// </summary>
    public class RotateAnimation : BaseAnimation
    {
        private readonly float _rotationSpeed;

        public RotateAnimation(PanelWrapper target, float rotationSpeed = 60f) : base(target)
        {
            _rotationSpeed = rotationSpeed;
        }

        protected override void OnUpdate()
        {
            Target.RectTransform.localRotation = Quaternion.Euler(0, 0, AnimationTime * _rotationSpeed);
        }

        public override void Reset()
        {
            base.Reset();
            Target.RectTransform.localRotation = Quaternion.identity;
        }
    }
} 