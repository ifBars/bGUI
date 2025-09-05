using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Core.Abstractions
{
    /// <summary>
    /// Abstract base class for layout wrappers that provides common layout functionality.
    /// </summary>
    /// <typeparam name="TLayoutGroup">The type of Unity LayoutGroup this wrapper manages</typeparam>
    public abstract class LayoutBase<TLayoutGroup> where TLayoutGroup : LayoutGroup
    {
        protected readonly TLayoutGroup _layoutGroup;
        protected readonly GameObject _gameObject;

        /// <summary>
        /// Gets the underlying Unity LayoutGroup component.
        /// </summary>
        public TLayoutGroup Component => _layoutGroup;

        /// <summary>
        /// Gets the GameObject that owns this layout.
        /// </summary>
        public GameObject GameObject => _gameObject;

        /// <summary>
        /// Gets or sets the padding within the layout group.
        /// </summary>
        public virtual RectOffset Padding
        {
            get => _layoutGroup.padding;
            set => _layoutGroup.padding = value;
        }

        /// <summary>
        /// Gets or sets the alignment of child elements.
        /// </summary>
        public virtual TextAnchor ChildAlignment
        {
            get => _layoutGroup.childAlignment;
            set => _layoutGroup.childAlignment = value;
        }

        /// <summary>
        /// Initializes a new instance of the LayoutBase class.
        /// </summary>
        /// <param name="gameObject">The GameObject to add the layout component to</param>
        protected LayoutBase(GameObject gameObject)
        {
            _gameObject = gameObject;
            
            // Remove any existing layout groups to prevent conflicts
            var existingLayouts = gameObject.GetComponents<LayoutGroup>();
            foreach (var layout in existingLayouts)
            {
                Object.DestroyImmediate(layout);
            }
            
            // Add the new layout group
            _layoutGroup = gameObject.AddComponent<TLayoutGroup>();
            
            // Apply default settings
            SetDefaultSettings();
        }

        /// <summary>
        /// Sets the default settings for this layout. Override in derived classes.
        /// </summary>
        protected virtual void SetDefaultSettings()
        {
            _layoutGroup.padding = new RectOffset(5, 5, 5, 5);
            _layoutGroup.childAlignment = TextAnchor.UpperLeft;
        }

        /// <summary>
        /// Sets the padding within the layout group.
        /// </summary>
        /// <param name="left">Left padding</param>
        /// <param name="right">Right padding</param>
        /// <param name="top">Top padding</param>
        /// <param name="bottom">Bottom padding</param>
        /// <returns>This layout for method chaining</returns>
        public virtual LayoutBase<TLayoutGroup> SetPadding(int left, int right, int top, int bottom)
        {
            Padding = new RectOffset(left, right, top, bottom);
            return this;
        }

        /// <summary>
        /// Sets uniform padding within the layout group.
        /// </summary>
        /// <param name="padding">Uniform padding value</param>
        /// <returns>This layout for method chaining</returns>
        public virtual LayoutBase<TLayoutGroup> SetPadding(int padding)
        {
            return SetPadding(padding, padding, padding, padding);
        }

        /// <summary>
        /// Sets the alignment of child elements.
        /// </summary>
        /// <param name="alignment">The alignment to set</param>
        /// <returns>This layout for method chaining</returns>
        public virtual LayoutBase<TLayoutGroup> SetChildAlignment(TextAnchor alignment)
        {
            ChildAlignment = alignment;
            return this;
        }

        /// <summary>
        /// Enables or disables the layout group.
        /// </summary>
        /// <param name="enabled">Whether the layout should be enabled</param>
        /// <returns>This layout for method chaining</returns>
        public virtual LayoutBase<TLayoutGroup> SetEnabled(bool enabled)
        {
            _layoutGroup.enabled = enabled;
            return this;
        }
    }
} 