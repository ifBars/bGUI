using bGUI.Core.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Core.Components
{
    /// <summary>
    /// Interface for a scroll view UI element in the bGUI framework.
    /// </summary>
    public interface IScrollView : IUIElement
    {
        /// <summary>
        /// Gets the underlying ScrollRect component.
        /// </summary>
        ScrollRect ScrollRect { get; }
        
        /// <summary>
        /// Gets the viewport RectTransform.
        /// </summary>
        RectTransform Viewport { get; }
        
        /// <summary>
        /// Gets the content RectTransform.
        /// </summary>
        RectTransform Content { get; }
        
        /// <summary>
        /// Gets or sets whether horizontal scrolling is enabled.
        /// </summary>
        bool HorizontalScrolling { get; set; }
        
        /// <summary>
        /// Gets or sets whether vertical scrolling is enabled.
        /// </summary>
        bool VerticalScrolling { get; set; }
        
        /// <summary>
        /// Gets or sets the normalized position of the horizontal scrollbar (0-1).
        /// </summary>
        float HorizontalNormalizedPosition { get; set; }
        
        /// <summary>
        /// Gets or sets the normalized position of the vertical scrollbar (0-1).
        /// </summary>
        float VerticalNormalizedPosition { get; set; }
        
        /// <summary>
        /// Adds an element to the scroll view content.
        /// </summary>
        /// <param name="element">The UI element to add</param>
        void AddContent(IUIElement element);
        
        /// <summary>
        /// Clears all content from the scroll view.
        /// </summary>
        void ClearContent();
    }
} 