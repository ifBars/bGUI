using bGUI.Core.Abstractions;
using bGUI.Core.Components;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Components
{
    /// <summary>
    /// Wrapper for Unity's Text component.
    /// </summary>
    public class TextWrapper : UIElementBase, IText
    {
        private Text _text = null!;

        /// <summary>
        /// Gets the underlying Text component.
        /// </summary>
        public Text TextComponent => _text;

        /// <summary>
        /// Gets the Transform (same as RectTransform for UI elements).
        /// </summary>
        public Transform Transform => _rectTransform;

        /// <summary>
        /// Gets or sets the text content.
        /// </summary>
        public string Content
        {
            get => _text?.text ?? "";
            set { if (_text != null) _text.text = value; }
        }

        /// <summary>
        /// Gets or sets the text color.
        /// </summary>
        public Color Color
        {
            get => _text?.color ?? Color.white;
            set { if (_text != null) _text.color = value; }
        }

        /// <summary>
        /// Gets or sets the font size.
        /// </summary>
        public int FontSize
        {
            get => _text?.fontSize ?? 14;
            set { if (_text != null) _text.fontSize = value; }
        }

        /// <summary>
        /// Gets or sets the font style.
        /// </summary>
        public FontStyle FontStyle
        {
            get => _text?.fontStyle ?? FontStyle.Normal;
            set { if (_text != null) _text.fontStyle = value; }
        }

        /// <summary>
        /// Gets or sets the horizontal alignment.
        /// </summary>
        public TextAnchor Alignment
        {
            get => _text?.alignment ?? TextAnchor.MiddleCenter;
            set { if (_text != null) _text.alignment = value; }
        }

        /// <summary>
        /// Initializes a new instance of the TextWrapper class.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the text element</param>
        /// <param name="content">The text content</param>
        public TextWrapper(Transform? parent, string name = "Text", string content = "")
            : base(parent, name)
        {
            // Add Text component
            _text = _gameObject.AddComponent<Text>();
            _text.text = content;
            _text.alignment = TextAnchor.MiddleCenter;
            _text.color = Color.black;
            _text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            _text.fontSize = 14;
            _text.raycastTarget = false;

            // Set RectTransform properties
            _rectTransform.sizeDelta = new Vector2(200f, 50f);
        }

        /// <summary>
        /// Sets the font for the text element.
        /// </summary>
        /// <param name="font">The font to use</param>
        public void SetFont(Font font)
        {
            if (font != null && _text != null)
            {
                _text.font = font;
            }
        }

        /// <summary>
        /// Sets text to wrap or not.
        /// </summary>
        /// <param name="wrap">Whether text should wrap</param>
        public void SetWrap(bool wrap)
        {
            if (_text != null)
            {
                _text.horizontalOverflow = wrap ? HorizontalWrapMode.Wrap : HorizontalWrapMode.Overflow;
            }
        }

        /// <summary>
        /// Sets the best fit mode for text.
        /// </summary>
        /// <param name="bestFit">Whether to use best fit</param>
        /// <param name="minSize">Minimum font size when using best fit</param>
        /// <param name="maxSize">Maximum font size when using best fit</param>
        public void SetBestFit(bool bestFit, int minSize = 10, int maxSize = 40)
        {
            if (_text != null)
            {
                _text.resizeTextForBestFit = bestFit;
                _text.resizeTextMinSize = minSize;
                _text.resizeTextMaxSize = maxSize;
            }
        }

        /// <summary>
        /// Sets the text content (alias for Content property).
        /// </summary>
        /// <param name="content">The content to set</param>
        /// <returns>This TextWrapper for method chaining</returns>
        public TextWrapper SetContent(string content)
        {
            Content = content;
            return this;
        }

        /// <summary>
        /// Sets the text color (alias for Color property).
        /// </summary>
        /// <param name="color">The color to set</param>
        /// <returns>This TextWrapper for method chaining</returns>
        public TextWrapper SetColor(Color color)
        {
            Color = color;
            return this;
        }

        /// <summary>
        /// Sets the font size (alias for FontSize property).
        /// </summary>
        /// <param name="fontSize">The font size to set</param>
        /// <returns>This TextWrapper for method chaining</returns>
        public TextWrapper SetFontSize(int fontSize)
        {
            FontSize = fontSize;
            return this;
        }

        /// <summary>
        /// Sets the anchor for this text element.
        /// </summary>
        /// <param name="anchorX">The X anchor (0-1)</param>
        /// <param name="anchorY">The Y anchor (0-1)</param>
        /// <returns>This TextWrapper for method chaining</returns>
        public TextWrapper SetAnchor(float anchorX, float anchorY)
        {
            if (_rectTransform != null)
            {
                _rectTransform.anchorMin = _rectTransform.anchorMax = new Vector2(anchorX, anchorY);
            }
            return this;
        }

        /// <summary>
        /// Gets whether this TextWrapper is still valid (components exist).
        /// </summary>
        public bool IsValid => _text != null && _gameObject != null && _rectTransform != null;
    }
}
