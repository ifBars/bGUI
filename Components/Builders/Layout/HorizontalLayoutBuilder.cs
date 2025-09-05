using UnityEngine;
using UnityEngine.UI;
using bGUI.Components.Layout;
using bGUI.Core.Layout;

namespace bGUI.Components.Builders.Layout
{
    /// <summary>
    /// Fluent builder for HorizontalLayoutGroup components.
    /// </summary>
    public class HorizontalLayoutBuilder
    {
        private readonly HorizontalLayoutWrapper _layoutWrapper;
        /// <summary>
        /// Initializes a new instance bound to the provided owner GameObject.
        /// </summary>
        /// <param name="owner">GameObject that will host the layout component.</param>
        public HorizontalLayoutBuilder(GameObject owner)
        {
            _layoutWrapper = new HorizontalLayoutWrapper(owner);
        }

        /// <summary>
        /// Sets spacing between child elements.
        /// </summary>
        /// <param name="spacing">Spacing in pixels.</param>
        public HorizontalLayoutBuilder SetSpacing(float spacing)
        {
            _layoutWrapper.SetSpacing(spacing);
            return this;
        }

        /// <summary>
        /// Sets layout padding on each side.
        /// </summary>
        public HorizontalLayoutBuilder SetPadding(int left, int right, int top, int bottom)
        {
            _layoutWrapper.SetPadding(left, right, top, bottom);
            return this;
        }

        /// <summary>
        /// Sets uniform padding on all sides.
        /// </summary>
        public HorizontalLayoutBuilder SetPadding(int all)
        {
            _layoutWrapper.SetPadding(all, all, all, all);
            return this;
        }

        /// <summary>
        /// Sets alignment for child elements.
        /// </summary>
        public HorizontalLayoutBuilder SetChildAlignment(TextAnchor alignment)
        {
            _layoutWrapper.SetChildAlignment(alignment);
            return this;
        }

        /// <summary>
        /// Controls whether children control their width/height.
        /// </summary>
        public HorizontalLayoutBuilder SetChildControlSize(bool controlWidth, bool controlHeight)
        {
            _layoutWrapper.SetChildControlSize(controlWidth, controlHeight);
            return this;
        }

        /// <summary>
        /// Forces children to expand to fill available space.
        /// </summary>
        public HorizontalLayoutBuilder SetChildForceExpand(bool expandWidth, bool expandHeight)
        {
            _layoutWrapper.SetChildForceExpand(expandWidth, expandHeight);
            return this;
        }

        /// <summary>
        /// Scales child widths/heights based on available space.
        /// </summary>
        public HorizontalLayoutBuilder SetChildScale(bool scaleWidth, bool scaleHeight)
        {
            _layoutWrapper.SetChildScale(scaleWidth, scaleHeight);
            return this;
        }

        /// <summary>
        /// Reverses child order.
        /// </summary>
        public HorizontalLayoutBuilder SetReverseArrangement(bool reverse)
        {
            _layoutWrapper.SetReverseArrangement(reverse);
            return this;
        }

        /// <summary>
        /// Builds and returns the layout wrapper.
        /// </summary>
        public IHorizontalLayout Build()
        {
            return _layoutWrapper;
        }

        /// <summary>
        /// Builds and returns the underlying Unity component.
        /// </summary>
        public HorizontalLayoutGroup BuildComponent()
        {
            return _layoutWrapper.Component;
        }
    }
}
