using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Core.Layout
{
    /// <summary>
    /// Interface for configuring HorizontalLayoutGroup components.
    /// </summary>
    public interface IHorizontalLayout
    {
        /// <summary>
        /// Gets the underlying HorizontalLayoutGroup component.
        /// </summary>
        HorizontalLayoutGroup Component { get; }

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
        IHorizontalLayout SetSpacing(float spacing);

        /// <summary>
        /// Sets the padding within the layout group.
        /// </summary>
        IHorizontalLayout SetPadding(int left, int right, int top, int bottom);

        /// <summary>
        /// Sets the alignment of child elements.
        /// </summary>
        IHorizontalLayout SetChildAlignment(TextAnchor alignment);

        /// <summary>
        /// Sets whether the layout group should control the size of its child elements.
        /// </summary>
        IHorizontalLayout SetChildControlSize(bool controlWidth, bool controlHeight);
        
        /// <summary>
        /// Sets whether child elements are forced to expand to fill available space.
        /// </summary>
        IHorizontalLayout SetChildForceExpand(bool expandWidth, bool expandHeight);

        /// <summary>
        /// Sets whether the layout group should scale its child elements.
        /// </summary>
        IHorizontalLayout SetChildScale(bool scaleWidth, bool scaleHeight);
        
        /// <summary>
        /// Sets whether the child elements are arranged in reverse order.
        /// </summary>
        IHorizontalLayout SetReverseArrangement(bool reverse);
    }
} 