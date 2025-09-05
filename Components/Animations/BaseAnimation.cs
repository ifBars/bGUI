namespace bGUI.Components.Animations
{
    /// <summary>
    /// Base class for all animations in the bGUI framework.
    /// </summary>
    public abstract class BaseAnimation
    {
        protected readonly PanelWrapper Target;
        protected bool IsEnabled = true;
        protected float AnimationTime = 0f;

        protected BaseAnimation(PanelWrapper target)
        {
            Target = target;
        }

        public virtual void Update(float deltaTime)
        {
            if (!IsEnabled) return;
            AnimationTime += deltaTime;
            OnUpdate();
        }

        protected abstract void OnUpdate();

        public virtual void Reset()
        {
            AnimationTime = 0f;
        }

        public virtual void SetEnabled(bool enabled)
        {
            IsEnabled = enabled;
            if (!enabled)
            {
                Reset();
            }
        }
    }
} 