using UnityEngine;
using UnityEngine.UI;
using bGUI.Core.Constants;
using bGUI.Core.Factory;
using bGUI.Core.Components;

namespace bGUI.Components
{
    /// <summary>
    /// Fluent builder for bGUI buttons.
    /// </summary>
    public class ButtonBuilder
    {
        private readonly IButton _button;
        private readonly bool _usePooling;
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonBuilder"/>.
        /// </summary>
        /// <param name="parent">Parent transform to attach the button to.</param>
        /// <param name="usePooling">Whether to use the internal object pool.</param>
        public ButtonBuilder(Transform? parent, bool usePooling = false)
        {
            _usePooling = usePooling;
            _button = UIFactory.Instance.CreateButton(parent, "Button", "Button", _usePooling);
        }

        // Backward compatibility constructor
        public ButtonBuilder(Transform? parent)
        {
            _usePooling = false;
            _button = UIFactory.Instance.CreateButton(parent, "Button", "Button", _usePooling);
        }
        /// <summary>
        /// Sets the visible text on the button.
        /// </summary>
        /// <param name="text">Text to display.</param>
        /// <returns>This builder for chaining.</returns>
        public ButtonBuilder SetText(string text)
        {
            _button.Text = text;
            return this;
        }
        /// <summary>
        /// Sets the size of the button in pixels.
        /// </summary>
        /// <param name="width">Width in pixels.</param>
        /// <param name="height">Height in pixels.</param>
        /// <returns>This builder for chaining.</returns>
        public ButtonBuilder SetSize(float width, float height)
        {
            _button.RectTransform.sizeDelta = new Vector2(width, height);
            return this;
        }
        /// <summary>
        /// Sets the size of the button.
        /// </summary>
        /// <param name="size">Size vector (width, height).</param>
        /// <returns>This builder for chaining.</returns>
        public ButtonBuilder SetSize(Vector2 size)
        {
            _button.RectTransform.sizeDelta = size;
            return this;
        }
        /// <summary>
        /// Sets the anchored position of the button.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        /// <returns>This builder for chaining.</returns>
        public ButtonBuilder SetPosition(float x, float y)
        {
            _button.RectTransform.anchoredPosition = new Vector2(x, y);
            return this;
        }
        
        /// <summary>
        /// Sets the anchor point of the button.
        /// </summary>
        /// <param name="anchorX">Anchor X (0-1).</param>
        /// <param name="anchorY">Anchor Y (0-1).</param>
        /// <returns>This builder for chaining.</returns>
        public ButtonBuilder SetAnchor(float anchorX, float anchorY)
        {
            _button.RectTransform.anchorMin = _button.RectTransform.anchorMax = new Vector2(anchorX, anchorY);
            return this;
        }
        
        /// <summary>
        /// Sets the pivot of the button.
        /// </summary>
        /// <param name="pivotX">Pivot X (0-1).</param>
        /// <param name="pivotY">Pivot Y (0-1).</param>
        /// <returns>This builder for chaining.</returns>
        public ButtonBuilder SetPivot(float pivotX, float pivotY)
        {
            _button.RectTransform.pivot = new Vector2(pivotX, pivotY);
            return this;
        }
        /// <summary>
        /// Registers a click handler for the button.
        /// </summary>
        /// <param name="action">Action invoked when button is clicked.</param>
        /// <returns>This builder for chaining.</returns>
        public ButtonBuilder OnClick(System.Action action)
        {
            _button.OnClick += action;
            return this;
        }
        /// <summary>
        /// Sets the color block for button states.
        /// </summary>
        /// <param name="normal">Normal color.</param>
        /// <param name="highlighted">Highlighted color.</param>
        /// <param name="pressed">Pressed color.</param>
        /// <param name="disabled">Disabled color.</param>
        /// <returns>This builder for chaining.</returns>
        public ButtonBuilder SetColors(Color normal, Color highlighted, Color pressed, Color disabled)
        {
            _button.SetColors(normal, highlighted, pressed, disabled);
            return this;
        }
        /// <summary>
        /// Applies a Unity ColorBlock to the button.
        /// </summary>
        /// <param name="colorBlock">Color block to assign.</param>
        /// <returns>This builder for chaining.</returns>
        public ButtonBuilder SetColors(ColorBlock colorBlock)
        {
            _button.ButtonComponent.colors = colorBlock;
            return this;
        }
        /// <summary>
        /// Sets a uniform base background color.
        /// </summary>
        /// <param name="color">Background color.</param>
        /// <returns>This builder for chaining.</returns>
        public ButtonBuilder SetBackgroundColor(Color color)
        {
            var colorBlock = new ColorBlock
            {
                normalColor = color,
                highlightedColor = Color.Lerp(color, Color.white, 0.1f),
                pressedColor = Color.Lerp(color, Color.black, 0.1f),
                selectedColor = Color.Lerp(color, Color.white, 0.1f),
                disabledColor = Color.Lerp(color, Color.gray, 0.5f),
                colorMultiplier = 1f,
                fadeDuration = 0.1f
            };
            _button.ButtonComponent.colors = colorBlock;
            return this;
        }
        /// <summary>
        /// Sets whether the button is interactable.
        /// </summary>
        /// <param name="interactable">True to enable interaction.</param>
        /// <returns>This builder for chaining.</returns>
        public ButtonBuilder SetInteractable(bool interactable)
        {
            _button.Interactable = interactable;
            return this;
        }
        /// <summary>
        /// Sets the background sprite for the button.
        /// </summary>
        /// <param name="sprite">Sprite to use.</param>
        /// <returns>This builder for chaining.</returns>
        public ButtonBuilder SetBackground(Sprite sprite)
        {
            _button.SetBackgroundImage(sprite);
            return this;
        }

        /// <summary>
        /// Sets the button to have rounded corners.
        /// </summary>
        /// <param name="cornerRadius">Radius of the corners in pixels</param>
        /// <param name="borderSize">Size of the border for 9-slice</param>
        /// <returns>This builder for chaining</returns>
        public ButtonBuilder SetRounded(int cornerRadius, int borderSize = 10)
        {
            _button.SetRounded(cornerRadius, borderSize);
            return this;
        }

        /// <summary>
        /// Sets the button to have small rounded corners.
        /// </summary>
        /// <returns>This builder for chaining</returns>
        public ButtonBuilder RoundedSmall()
        {
            return SetRounded(Theme.Radius.Small);
        }

        /// <summary>
        /// Sets the button to have large rounded corners.
        /// </summary>
        /// <returns>This builder for chaining</returns>
        public ButtonBuilder RoundedLarge()
        {
            return SetRounded(Theme.Radius.Large);
        }
        /// <summary>
        /// Builds and returns the configured button wrapper.
        /// </summary>
        /// <returns>The created <see cref="ButtonWrapper"/>.</returns>
        public ButtonWrapper Build() => (ButtonWrapper)_button;
    }
}
