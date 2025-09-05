using UnityEngine;
using UnityEngine.UI;
using bGUI.Components.Layout;
using bGUI.Core.Layout;

namespace bGUI.Components.Builders.Layout
{
    /// <summary>
    /// Fluent builder for VerticalLayoutGroup components.
    /// </summary>
    public class VerticalLayoutBuilder
    {
        private readonly VerticalLayoutWrapper _layoutWrapper;
        /// <summary>
        /// Initializes a new instance bound to the provided owner GameObject.
        /// </summary>
        /// <param name="owner">GameObject that will host the layout component.</param>
        public VerticalLayoutBuilder(GameObject owner)
        {
            _layoutWrapper = new VerticalLayoutWrapper(owner);
        }

        /// <summary>
        /// Sets spacing between child elements.
        /// </summary>
        /// <param name="spacing">Spacing in pixels.</param>
        public VerticalLayoutBuilder SetSpacing(float spacing)
        {
            _layoutWrapper.SetSpacing(spacing);
            return this;
        }

        /// <summary>
        /// Sets layout padding on each side.
        /// </summary>
        public VerticalLayoutBuilder SetPadding(int left, int right, int top, int bottom)
        {
            _layoutWrapper.SetPadding(left, right, top, bottom);
            return this;
        }

        /// <summary>
        /// Sets uniform padding on all sides.
        /// </summary>
        public VerticalLayoutBuilder SetPadding(int all)
        {
            _layoutWrapper.SetPadding(all, all, all, all);
            return this;
        }

        /// <summary>
        /// Sets alignment for child elements.
        /// </summary>
        public VerticalLayoutBuilder SetChildAlignment(TextAnchor alignment)
        {
            _layoutWrapper.SetChildAlignment(alignment);
            return this;
        }

        /// <summary>
        /// Controls whether children control their width/height.
        /// </summary>
        public VerticalLayoutBuilder SetChildControlSize(bool controlWidth, bool controlHeight)
        {
            _layoutWrapper.SetChildControlSize(controlWidth, controlHeight);
            return this;
        }

        /// <summary>
        /// Forces children to expand to fill available space.
        /// </summary>
        public VerticalLayoutBuilder SetChildForceExpand(bool expandWidth, bool expandHeight)
        {
            _layoutWrapper.SetChildForceExpand(expandWidth, expandHeight);
            return this;
        }

        /// <summary>
        /// Scales child widths/heights based on available space.
        /// </summary>
        public VerticalLayoutBuilder SetChildScale(bool scaleWidth, bool scaleHeight)
        {
            _layoutWrapper.SetChildScale(scaleWidth, scaleHeight);
            return this;
        }

        /// <summary>
        /// Reverses child order.
        /// </summary>
        public VerticalLayoutBuilder SetReverseArrangement(bool reverse)
        {
            _layoutWrapper.SetReverseArrangement(reverse);
            return this;
        }

        /// <summary>
        /// Builds and returns the layout wrapper.
        /// </summary>
        public IVerticalLayout Build()
        {
            return _layoutWrapper;
        }

        /// <summary>
        /// Builds and returns the underlying Unity component.
        /// </summary>
        public VerticalLayoutGroup BuildComponent()
        {
            return _layoutWrapper.Component;
        }
    }
}
