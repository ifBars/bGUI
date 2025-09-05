using bGUI.Core.Abstractions;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Core.Components
{
    /// <summary>
    /// Interface for button UI elements.
    /// </summary>
    public interface IButton : IUIElement
    {
        /// <summary>
        /// Gets the underlying Button component.
        /// </summary>
        Button ButtonComponent { get; }

        /// <summary>
        /// Event triggered when the button is clicked.
        /// </summary>
        event Action OnClick;

        /// <summary>
        /// Gets or sets the text displayed on the button.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets whether the button is interactable.
        /// </summary>
        bool Interactable { get; set; }

        /// <summary>
        /// Sets the sprite for the button background.
        /// </summary>
        /// <param name="sprite">The sprite to use as background</param>
        void SetBackgroundImage(Sprite sprite);

        /// <summary>
        /// Sets the colors for different button states.
        /// </summary>
        /// <param name="normalColor">Color when button is normal</param>
        /// <param name="highlightedColor">Color when button is highlighted</param>
        /// <param name="pressedColor">Color when button is pressed</param>
        /// <param name="disabledColor">Color when button is disabled</param>
        void SetColors(Color normalColor, Color highlightedColor, Color pressedColor, Color disabledColor);
        
        /// <summary>
        /// Sets the button to have rounded corners.
        /// </summary>
        /// <param name="cornerRadius">Radius of the corners in pixels</param>
        /// <param name="borderSize">Size of the border for 9-slice</param>
        void SetRounded(int cornerRadius, int borderSize = 10);
    }
}