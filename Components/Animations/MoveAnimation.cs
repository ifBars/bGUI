using UnityEngine;

namespace bGUI.Components.Animations
{
    /// <summary>
    /// Animation that moves a UI element up and down.
    /// </summary>
    public class MoveAnimation : BaseAnimation
    {
        private readonly float _moveSpeed;
        private readonly float _moveAmount;
        private readonly RectTransform _rectTransform;
        private Vector3 _offsetVec = Vector3.zero;

        public MoveAnimation(PanelWrapper target, float moveSpeed = 2f, float moveAmount = 20f) : base(target)
        {
            _moveSpeed = moveSpeed;
            _moveAmount = moveAmount;
            _rectTransform = target.RectTransform;
        }

        protected override void OnUpdate()
        {
            // First reset any previous offset
            _rectTransform.localPosition -= _offsetVec;
            
            // Calculate new offset
            float yOffset = _moveAmount * Mathf.Sin(AnimationTime * _moveSpeed);
            _offsetVec = new Vector3(0f, yOffset, 0f);
            
            // Apply the new offset without disrupting layout
            _rectTransform.localPosition += _offsetVec;
        }

        public override void Reset()
        {
            base.Reset();
            
            // Remove any offset we've added
            _rectTransform.localPosition -= _offsetVec;
            _offsetVec = Vector3.zero;
        }
    }
} 