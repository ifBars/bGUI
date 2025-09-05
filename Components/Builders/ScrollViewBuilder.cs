using bGUI.Core.Components;
using bGUI.Core.Factory;
using UnityEngine;

namespace bGUI.Components.Builders
{
    /// <summary>
    /// Builder for creating scroll view UI elements.
    /// </summary>
    public class ScrollViewBuilder
    {
        private readonly Transform? _parent;
        private readonly bool _usePooling;
        private string _name = "ScrollView";
        private bool _horizontalScrolling = false;
        private bool _verticalScrolling = true;

        /// <summary>
        /// Creates a new ScrollViewBuilder.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="usePooling">Whether to use object pooling</param>
        public ScrollViewBuilder(Transform? parent = null, bool usePooling = false)
        {
            _parent = parent;
            _usePooling = usePooling;
        }

        // Backward compatibility constructor
        public ScrollViewBuilder(Transform? parent = null)
        {
            _parent = parent;
            _usePooling = false;
        }

        /// <summary>
        /// Sets the name of the scroll view.
        /// </summary>
        /// <param name="name">The name</param>
        /// <returns>This builder for chaining</returns>
        public ScrollViewBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Enables or disables horizontal scrolling.
        /// </summary>
        /// <param name="enabled">Whether horizontal scrolling is enabled</param>
        /// <returns>This builder for chaining</returns>
        public ScrollViewBuilder WithHorizontalScrolling(bool enabled = true)
        {
            _horizontalScrolling = enabled;
            return this;
        }

        /// <summary>
        /// Enables or disables vertical scrolling.
        /// </summary>
        /// <param name="enabled">Whether vertical scrolling is enabled</param>
        /// <returns>This builder for chaining</returns>
        public ScrollViewBuilder WithVerticalScrolling(bool enabled = true)
        {
            _verticalScrolling = enabled;
            return this;
        }

        /// <summary>
        /// Configures the scroll view to use object pooling.
        /// </summary>
        /// <returns>This builder for chaining</returns>
        public ScrollViewBuilder WithPooling()
        {
            return this; // Pooling is already configured in constructor
        }

        /// <summary>
        /// Builds and returns the scroll view.
        /// </summary>
        /// <returns>The created scroll view</returns>
        public IScrollView Build()
        {
            var scrollView = UIFactory.Instance.CreateScrollView(_parent, _name, _usePooling);
            scrollView.HorizontalScrolling = _horizontalScrolling;
            scrollView.VerticalScrolling = _verticalScrolling;
            return scrollView;
        }
    }
} 
