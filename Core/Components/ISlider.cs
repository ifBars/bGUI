using bGUI.Core.Abstractions;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Core.Components
{
    /// <summary>
    /// Interface for slider UI elements.
    /// </summary>
    public interface ISlider : IUIElement
    {
        /// <summary>
        /// Gets the underlying Slider component.
        /// </summary>
        Slider SliderComponent { get; }

        /// <summary>
        /// Event triggered when the slider value changes.
        /// </summary>
        event Action<float> OnValueChanged;

        /// <summary>
        /// Gets or sets the current value of the slider.
        /// </summary>
        float Value { get; set; }

        /// <summary>
        /// Gets or sets the minimum value of the slider.
        /// </summary>
        float MinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value of the slider.
        /// </summary>
        float MaxValue { get; set; }

        /// <summary>
        /// Gets or sets whether the slider uses whole numbers only.
        /// </summary>
        bool WholeNumbers { get; set; }

        /// <summary>
        /// Gets or sets the direction of the slider.
        /// </summary>
        Slider.Direction Direction { get; set; }

        /// <summary>
        /// Gets or sets whether the slider is interactable.
        /// </summary>
        bool Interactable { get; set; }

        /// <summary>
        /// Sets the colors for different slider states.
        /// </summary>
        /// <param name="normalColor">Color when slider is normal</param>
        /// <param name="highlightedColor">Color when slider is highlighted</param>
        /// <param name="pressedColor">Color when slider is pressed</param>
        /// <param name="disabledColor">Color when slider is disabled</param>
        void SetColors(Color normalColor, Color highlightedColor, Color pressedColor, Color disabledColor);

        /// <summary>
        /// Sets the sprite for the slider background.
        /// </summary>
        /// <param name="sprite">The sprite to use as background</param>
        void SetBackgroundImage(Sprite sprite);

        /// <summary>
        /// Sets the sprite for the slider fill area.
        /// </summary>
        /// <param name="sprite">The sprite to use for fill</param>
        void SetFillImage(Sprite sprite);

        /// <summary>
        /// Sets the sprite for the slider handle.
        /// </summary>
        /// <param name="sprite">The sprite to use for handle</param>
        void SetHandleImage(Sprite sprite);

        /// <summary>
        /// Sets the background color of the slider.
        /// </summary>
        /// <param name="color">The color to set</param>
        void SetBackgroundColor(Color color);

        /// <summary>
        /// Sets the fill color of the slider.
        /// </summary>
        /// <param name="color">The color to set</param>
        void SetFillColor(Color color);

        /// <summary>
        /// Sets the handle color of the slider.
        /// </summary>
        /// <param name="color">The color to set</param>
        void SetHandleColor(Color color);
    }
} 