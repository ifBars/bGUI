 using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Core.Layout
{
    /// <summary>
    /// Interface for configuring VerticalLayoutGroup components.
    /// </summary>
    public interface IVerticalLayout
    {
        /// <summary>
        /// Gets the underlying VerticalLayoutGroup component.
        /// </summary>
        VerticalLayoutGroup Component { get; }

        /// <summary>
        /// Gets or sets the spacing between child elements.
        /// </summary>
        float Spacing { get; set; }

        /// <summary>
        /// Gets or sets the padding within the layout group.
        /// </summary>
        RectOffset Padding { get; set; }

        /// <summary>
        /// Gets or sets the alignment of child elements.
        /// </summary>
        TextAnchor ChildAlignment { get; set; }

        /// <summary>
        /// Gets or sets whether the layout group should control the width of its child elements.
        /// </summary>
        bool ChildControlWidth { get; set; }

        /// <summary>
        /// Gets or sets whether the layout group should control the height of its child elements.
        /// </summary>
        bool ChildControlHeight { get; set; }

        /// <summary>
        /// Gets or sets whether child elements are forced to expand to fill available width.
        /// </summary>
        bool ChildForceExpandWidth { get; set; }

        /// <summary>
        /// Gets or sets whether child elements are forced to expand to fill available height.
        /// </summary>
        bool ChildForceExpandHeight { get; set; }

        /// <summary>
        /// Gets or sets whether the layout group should scale its child elements along with its own size changes (width).
        /// </summary>
        bool ChildScaleWidth { get; set; }

        /// <summary>
        /// Gets or sets whether the layout group should scale its child elements along with its own size changes (height).
        /// </summary>
        bool ChildScaleHeight { get; set; }

        /// <summary>
        /// Gets or sets whether the child elements are arranged in reverse order.
        /// </summary>
        bool ReverseArrangement { get; set; }

        /// <summary>
        /// Sets the spacing between child elements.
        /// </summary>
        IVerticalLayout SetSpacing(float spacing);

        /// <summary>
        /// Sets the padding within the layout group.
        /// </summary>
        IVerticalLayout SetPadding(int left, int right, int top, int bottom);

        /// <summary>
        /// Sets the alignment of child elements.
        /// </summary>
        IVerticalLayout SetChildAlignment(TextAnchor alignment);

        /// <summary>
        /// Sets whether the layout group should control the size of its child elements.
        /// </summary>
        IVerticalLayout SetChildControlSize(bool controlWidth, bool controlHeight);

        /// <summary>
        /// Sets whether child elements are forced to expand to fill available space.
        /// </summary>
        IVerticalLayout SetChildForceExpand(bool expandWidth, bool expandHeight);

        /// <summary>
        /// Sets whether the layout group should scale its child elements.
        /// </summary>
        IVerticalLayout SetChildScale(bool scaleWidth, bool scaleHeight);

        /// <summary>
        /// Sets whether the child elements are arranged in reverse order.
        /// </summary>
        IVerticalLayout SetReverseArrangement(bool reverse);
    }
}