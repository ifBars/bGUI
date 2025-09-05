using bGUI.Core.Abstractions;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Core.Components
{
    /// <summary>
    /// Interface for toggle (checkbox) UI elements.
    /// </summary>
    public interface IToggle : IUIElement
    {
        /// <summary>
        /// Gets the underlying Toggle component.
        /// </summary>
        Toggle ToggleComponent { get; }

        /// <summary>
        /// Event triggered when the toggle value changes.
        /// </summary>
        event Action<bool> OnValueChanged;

        /// <summary>
        /// Gets or sets whether the toggle is on.
        /// </summary>
        bool IsOn { get; set; }

        /// <summary>
        /// Gets or sets whether the toggle is interactable.
        /// </summary>
        bool Interactable { get; set; }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// Sets the colors for different toggle states.
        /// </summary>
        /// <param name="normalColor">Color when normal</param>
        /// <param name="highlightedColor">Color when highlighted</param>
        /// <param name="pressedColor">Color when pressed</param>
        /// <param name="disabledColor">Color when disabled</param>
        void SetColors(Color normalColor, Color highlightedColor, Color pressedColor, Color disabledColor);

        /// <summary>
        /// Sets the sprite for the toggle background.
        /// </summary>
        /// <param name="sprite">The sprite to use as background</param>
        void SetBackgroundImage(Sprite sprite);

        /// <summary>
        /// Sets the sprite for the toggle checkmark.
        /// </summary>
        /// <param name="sprite">The sprite to use for the checkmark</param>
        void SetCheckmarkImage(Sprite sprite);

        /// <summary>
        /// Sets the label color.
        /// </summary>
        /// <param name="color">The label color</param>
        void SetLabelColor(Color color);
    }
}

