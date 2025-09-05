using UnityEngine;
using bGUI.Core.Factory;
using bGUI.Core.Components;

namespace bGUI.Components
{
    /// <summary>
    /// Fluent builder for bGUI text elements.
    /// </summary>
    public class TextBuilder
    {
        private readonly IText _text;
        private readonly bool _usePooling;
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBuilder"/>.
        /// </summary>
        /// <param name="parent">Parent transform for the text element.</param>
        /// <param name="usePooling">Whether to use the internal object pool.</param>
        public TextBuilder(Transform? parent, bool usePooling = false)
        {
            _usePooling = usePooling;
            _text = UIFactory.Instance.CreateText(parent, "Text", "", _usePooling);
        }

        // Backward compatibility constructor
        public TextBuilder(Transform? parent)
        {
            _usePooling = false;
            _text = UIFactory.Instance.CreateText(parent, "Text", "", _usePooling);
        }

        /// <summary>
        /// Sets the textual content.
        /// </summary>
        /// <param name="content">String to display.</param>
        /// <returns>This builder for chaining.</returns>
        public TextBuilder SetContent(string content)
        {
            _text.Content = content;
            return this;
        }

        /// <summary>
        /// Sets the text color.
        /// </summary>
        /// <param name="color">Text color.</param>
        /// <returns>This builder for chaining.</returns>
        public TextBuilder SetColor(Color color)
        {
            _text.Color = color;
            return this;
        }

        /// <summary>
        /// Sets the font size.
        /// </summary>
        /// <param name="size">Font size in points.</param>
        /// <returns>This builder for chaining.</returns>
        public TextBuilder SetFontSize(int size)
        {
            _text.FontSize = size;
            return this;
        }

        /// <summary>
        /// Sets the font style.
        /// </summary>
        /// <param name="style">Font style enum.</param>
        /// <returns>This builder for chaining.</returns>
        public TextBuilder SetFontStyle(FontStyle style)
        {
            _text.FontStyle = style;
            return this;
        }

        /// <summary>
        /// Sets the text alignment.
        /// </summary>
        /// <param name="anchor">Alignment anchor.</param>
        /// <returns>This builder for chaining.</returns>
        public TextBuilder SetAlignment(TextAnchor anchor)
        {
            _text.Alignment = anchor;
            return this;
        }

        /// <summary>
        /// Sets the size of the text rect in pixels.
        /// </summary>
        /// <param name="width">Width in pixels.</param>
        /// <param name="height">Height in pixels.</param>
        /// <returns>This builder for chaining.</returns>
        public TextBuilder SetSize(float width, float height)
        {
            _text.RectTransform.sizeDelta = new Vector2(width, height);
            return this;
        }

        /// <summary>
        /// Sets the width of the text rect.
        /// </summary>
        /// <param name="width">Width in pixels.</param>
        /// <returns>This builder for chaining.</returns>
        public TextBuilder SetWidth(float width)
        {
            _text.RectTransform.sizeDelta = new Vector2(width, _text.RectTransform.sizeDelta.y);
            return this;
        }

        /// <summary>
        /// Sets the height of the text rect.
        /// </summary>
        /// <param name="height">Height in pixels.</param>
        /// <returns>This builder for chaining.</returns>
        public TextBuilder SetHeight(float height)
        {
            _text.RectTransform.sizeDelta = new Vector2(_text.RectTransform.sizeDelta.x, height);
            return this;
        }

        /// <summary>
        /// Sets the rect anchor.
        /// </summary>
        /// <param name="anchorX">Anchor X (0-1).</param>
        /// <param name="anchorY">Anchor Y (0-1).</param>
        /// <returns>This builder for chaining.</returns>
        public TextBuilder SetAnchor(float anchorX, float anchorY)
        {
            _text.RectTransform.anchorMin = _text.RectTransform.anchorMax = new Vector2(anchorX, anchorY);
            _text.RectTransform.anchoredPosition = Vector2.zero;
            return this;
        }

        /// <summary>
        /// Sets the rect pivot.
        /// </summary>
        /// <param name="pivotX">Pivot X (0-1).</param>
        /// <param name="pivotY">Pivot Y (0-1).</param>
        /// <returns>This builder for chaining.</returns>
        public TextBuilder SetPivot(float pivotX, float pivotY)
        {
            _text.RectTransform.pivot = new Vector2(pivotX, pivotY);
            return this;
        }

        /// <summary>
        /// Sets the anchored position.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        /// <returns>This builder for chaining.</returns>
        public TextBuilder SetPosition(float x, float y)
        {
            _text.RectTransform.anchoredPosition = new Vector2(x, y);
            return this;
        }
        /// <summary>
        /// Builds and returns the configured text wrapper.
        /// </summary>
        /// <returns>The created <see cref="TextWrapper"/>.</returns>
        public TextWrapper Build() => (TextWrapper)_text;
    }
} 
