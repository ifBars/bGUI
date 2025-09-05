using bGUI.Core.Abstractions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Core.Components
{
    /// <summary>
    /// Interface for dropdown UI elements.
    /// </summary>
    public interface IDropdown : IUIElement
    {
        /// <summary>
        /// Gets the underlying Dropdown component.
        /// </summary>
        Dropdown DropdownComponent { get; }

        /// <summary>
        /// Event triggered when the dropdown value changes.
        /// </summary>
        event Action<int> OnValueChanged;

        /// <summary>
        /// Gets or sets the selected index.
        /// </summary>
        int Value { get; set; }

        /// <summary>
        /// Gets or sets whether the dropdown is interactable.
        /// </summary>
        bool Interactable { get; set; }

        /// <summary>
        /// Gets or sets the caption text (selected value label).
        /// </summary>
        string Caption { get; set; }

        /// <summary>
        /// Clears all options.
        /// </summary>
        void ClearOptions();

        /// <summary>
        /// Adds a single option.
        /// </summary>
        void AddOption(string option);

        /// <summary>
        /// Adds multiple options.
        /// </summary>
        void AddOptions(IEnumerable<string> options);

        /// <summary>
        /// Replaces all options with the provided list.
        /// </summary>
        void SetOptions(IEnumerable<string> options);

        /// <summary>
        /// Sets the colors for different dropdown states.
        /// </summary>
        void SetColors(Color normalColor, Color highlightedColor, Color pressedColor, Color disabledColor);

        /// <summary>
        /// Sets the sprite for the dropdown background.
        /// </summary>
        void SetBackgroundImage(Sprite sprite);
    }
}

