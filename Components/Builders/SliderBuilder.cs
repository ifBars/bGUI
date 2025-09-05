using UnityEngine;
using UnityEngine.UI;
using System;
using bGUI.Core.Factory;
using bGUI.Core.Components;

namespace bGUI.Components
{
    /// <summary>
    /// Fluent builder for bGUI sliders.
    /// </summary>
    public class SliderBuilder
    {
        private readonly ISlider _slider;
        private readonly bool _usePooling;
        /// <summary>
        /// Initializes a new instance of the <see cref="SliderBuilder"/>.
        /// </summary>
        /// <param name="parent">Parent transform for the slider.</param>
        /// <param name="usePooling">Whether to use the internal object pool.</param>
        public SliderBuilder(Transform? parent, bool usePooling = false)
        {
            _usePooling = usePooling;
            _slider = UIFactory.Instance.CreateSlider(parent, "Slider", 0f, 1f, 0f, _usePooling);
        }

        // Backward compatibility constructor
        public SliderBuilder(Transform? parent)
        {
            _usePooling = false;
            _slider = UIFactory.Instance.CreateSlider(parent, "Slider", 0f, 1f, 0f, _usePooling);
        }

        /// <summary>
        /// Sets the current slider value.
        /// </summary>
        /// <param name="value">Value to set.</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetValue(float value)
        {
            _slider.Value = value;
            return this;
        }

        /// <summary>
        /// Sets the minimum slider value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetMinValue(float minValue)
        {
            _slider.MinValue = minValue;
            return this;
        }

        /// <summary>
        /// Sets the maximum slider value.
        /// </summary>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetMaxValue(float maxValue)
        {
            _slider.MaxValue = maxValue;
            return this;
        }

        /// <summary>
        /// Sets both the minimum and maximum slider values.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetRange(float minValue, float maxValue)
        {
            _slider.MinValue = minValue;
            _slider.MaxValue = maxValue;
            return this;
        }

        /// <summary>
        /// Enables whole number stepping.
        /// </summary>
        /// <param name="wholeNumbers">True to clamp to integers.</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetWholeNumbers(bool wholeNumbers)
        {
            _slider.WholeNumbers = wholeNumbers;
            return this;
        }

        /// <summary>
        /// Sets the slider direction.
        /// </summary>
        /// <param name="direction">Direction enum.</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetDirection(Slider.Direction direction)
        {
            _slider.Direction = direction;
            return this;
        }

        /// <summary>
        /// Sets the slider size in pixels.
        /// </summary>
        /// <param name="width">Width in pixels.</param>
        /// <param name="height">Height in pixels.</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetSize(float width, float height)
        {
            _slider.RectTransform.sizeDelta = new Vector2(width, height);
            return this;
        }

        /// <summary>
        /// Sets the anchored position of the slider.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetPosition(float x, float y)
        {
            _slider.RectTransform.anchoredPosition = new Vector2(x, y);
            return this;
        }

        /// <summary>
        /// Sets the anchor point of the slider.
        /// </summary>
        /// <param name="anchorX">Anchor X (0-1).</param>
        /// <param name="anchorY">Anchor Y (0-1).</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetAnchor(float anchorX, float anchorY)
        {
            _slider.RectTransform.anchorMin = _slider.RectTransform.anchorMax = new Vector2(anchorX, anchorY);
            return this;
        }

        /// <summary>
        /// Sets the rect pivot of the slider.
        /// </summary>
        /// <param name="pivotX">Pivot X (0-1).</param>
        /// <param name="pivotY">Pivot Y (0-1).</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetPivot(float pivotX, float pivotY)
        {
            _slider.RectTransform.pivot = new Vector2(pivotX, pivotY);
            return this;
        }

        /// <summary>
        /// Registers a callback for value changes.
        /// </summary>
        /// <param name="action">Action receiving the new value.</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder OnValueChanged(Action<float> action)
        {
            _slider.OnValueChanged += action;
            return this;
        }

        /// <summary>
        /// Sets the interaction colors.
        /// </summary>
        /// <param name="normal">Normal color.</param>
        /// <param name="highlighted">Highlighted color.</param>
        /// <param name="pressed">Pressed color.</param>
        /// <param name="disabled">Disabled color.</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetColors(Color normal, Color highlighted, Color pressed, Color disabled)
        {
            _slider.SetColors(normal, highlighted, pressed, disabled);
            return this;
        }

        /// <summary>
        /// Enables or disables slider interaction.
        /// </summary>
        /// <param name="interactable">True to enable.</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetInteractable(bool interactable)
        {
            _slider.Interactable = interactable;
            return this;
        }

        /// <summary>
        /// Sets the background sprite.
        /// </summary>
        /// <param name="sprite">Sprite to use.</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetBackgroundImage(Sprite sprite)
        {
            _slider.SetBackgroundImage(sprite);
            return this;
        }

        /// <summary>
        /// Sets the fill sprite.
        /// </summary>
        /// <param name="sprite">Sprite to use.</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetFillImage(Sprite sprite)
        {
            _slider.SetFillImage(sprite);
            return this;
        }

        /// <summary>
        /// Sets the handle sprite.
        /// </summary>
        /// <param name="sprite">Sprite to use.</param>
        /// <returns>This builder for chaining.</returns>
        public SliderBuilder SetHandleImage(Sprite sprite)
        {
            _slider.SetHandleImage(sprite);
            return this;
        }

        /// <summary>
        /// Sets up the slider as a horizontal slider (most common configuration).
        /// </summary>
        /// <param name="width">Width of the slider</param>
        /// <param name="height">Height of the slider (default 20)</param>
        /// <returns>This builder for chaining</returns>
        public SliderBuilder SetHorizontal(float width = 160f, float height = 20f)
        {
            _slider.Direction = Slider.Direction.LeftToRight;
            _slider.RectTransform.sizeDelta = new Vector2(width, height);
            return this;
        }

        /// <summary>
        /// Sets up the slider as a vertical slider.
        /// </summary>
        /// <param name="width">Width of the slider (default 20)</param>
        /// <param name="height">Height of the slider</param>
        /// <returns>This builder for chaining</returns>
        public SliderBuilder SetVertical(float width = 20f, float height = 160f)
        {
            _slider.Direction = Slider.Direction.BottomToTop;
            _slider.RectTransform.sizeDelta = new Vector2(width, height);
            return this;
        }

        /// <summary>
        /// Sets up the slider with a specific color scheme.
        /// </summary>
        /// <param name="fillColor">Color of the fill area</param>
        /// <param name="backgroundColor">Color of the background</param>
        /// <param name="handleColor">Color of the handle</param>
        /// <returns>This builder for chaining</returns>
        public SliderBuilder SetColorScheme(Color fillColor, Color backgroundColor, Color handleColor)
        {
            var sliderWrapper = _slider as SliderWrapper;
            if (sliderWrapper != null)
            {
                // Set the background color
                sliderWrapper.SetBackgroundColor(backgroundColor);
                
                // Set the fill color
                sliderWrapper.SetFillColor(fillColor);
                
                // Set the handle color
                sliderWrapper.SetHandleColor(handleColor);

                // Also set the slider's interaction colors to match the handle
                var colors = sliderWrapper.SliderComponent.colors;
                colors.normalColor = handleColor;
                colors.highlightedColor = handleColor * 0.9f;
                colors.pressedColor = handleColor * 0.8f;
                colors.disabledColor = handleColor * 0.5f;
                sliderWrapper.SliderComponent.colors = colors;
            }

            return this;
        }

        /// <summary>
        /// Creates a percentage slider (0-100 range with whole numbers).
        /// </summary>
        /// <param name="initialValue">Initial percentage value (0-100)</param>
        /// <returns>This builder for chaining</returns>
        public SliderBuilder SetAsPercentage(float initialValue = 50f)
        {
            _slider.MinValue = 0f;
            _slider.MaxValue = 100f;
            _slider.WholeNumbers = true;
            _slider.Value = Mathf.Clamp(initialValue, 0f, 100f);
            return this;
        }
        /// <summary>
        /// Builds and returns the configured slider wrapper.
        /// </summary>
        /// <returns>The created <see cref="SliderWrapper"/>.</returns>
        public SliderWrapper Build() => (SliderWrapper)_slider;
    }
} 
