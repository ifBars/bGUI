using bGUI.Core.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Core.Components
{
    /// <summary>
    /// Interface for text UI elements.
    /// </summary>
    public interface IText : IUIElement
    {
        /// <summary>
        /// Gets the underlying Text component.
        /// </summary>
        Text TextComponent { get; }

        /// <summary>
        /// Gets or sets the text content.
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// Gets or sets the text color.
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// Gets or sets the font size.
        /// </summary>
        int FontSize { get; set; }

        /// <summary>
        /// Gets or sets the font style.
        /// </summary>
        FontStyle FontStyle { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment.
        /// </summary>
        TextAnchor Alignment { get; set; }

        /// <summary>
        /// Sets the font for the text element.
        /// </summary>
        /// <param name="font">The font to use</param>
        void SetFont(Font font);
    }
}