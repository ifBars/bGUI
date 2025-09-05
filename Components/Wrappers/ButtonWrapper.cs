using bGUI.Core.Abstractions;
using bGUI.Core.Components;
using bGUI.Utilities;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Components
{
    /// <summary>
    /// Wrapper for Unity's Button component.
    /// </summary>
    public class ButtonWrapper : UIElementBase, IButton
    {
        private Button _button;
        private Text _text;
        private event Action? _onClick;

        /// <summary>
        /// Gets the underlying Button component.
        /// </summary>
        public Button ButtonComponent => _button;

        /// <summary>
        /// Gets the Transform (same as RectTransform for UI elements).
        /// </summary>
        public Transform Transform => _rectTransform;

        /// <summary>
        /// Event triggered when the button is clicked.
        /// </summary>
        public event Action OnClick
        {
            add
            {
                _onClick += value;
                if (_onClick != null && _onClick.GetInvocationList().Length == 1)
                {
                    // Only register the event handler once
                    _button.onClick.AddListener(OnButtonClick);
                }
            }
            remove
            {
                _onClick -= value;
                if (_onClick == null || _onClick.GetInvocationList().Length == 0)
                {
                    // Unregister the event handler when there are no subscribers
                    _button.onClick.RemoveListener(OnButtonClick);
                }
            }
        }

        /// <summary>
        /// Gets or sets the text displayed on the button.
        /// </summary>
        public string Text
        {
            get => _text.text;
            set => _text.text = value;
        }

        /// <summary>
        /// Gets or sets whether the button is interactable.
        /// </summary>
        public bool Interactable
        {
            get => _button.interactable;
            set => _button.interactable = value;
        }

        /// <summary>
        /// Initializes a new instance of the ButtonWrapper class.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the button</param>
        /// <param name="text">The text to display on the button</param>
        public ButtonWrapper(Transform? parent, string name = "Button", string text = "Button")
            : base(parent, name)
        {
            // Add required components
            var image = _gameObject.AddComponent<Image>();
            _button = _gameObject.AddComponent<Button>();
            _button.targetGraphic = image;

            // Create and set up text child GameObject
            var textGO = new GameObject("Text");
            var textRectTransform = textGO.AddComponent<RectTransform>();
            textRectTransform.SetParent(_rectTransform, false);

            // Set up text properties
            _text = textGO.AddComponent<Text>();
            _text.text = text;
            _text.alignment = TextAnchor.MiddleCenter;
            _text.color = Color.black;
            _text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

            // Set up text RectTransform to fill the button
            textRectTransform.anchorMin = Vector2.zero;
            textRectTransform.anchorMax = Vector2.one;
            textRectTransform.sizeDelta = Vector2.zero;
            textRectTransform.offsetMin = Vector2.zero;
            textRectTransform.offsetMax = Vector2.zero;

            // Set default button colors
            var colors = _button.colors;
            colors.normalColor = new Color(0.9f, 0.9f, 0.9f, 1f);
            colors.highlightedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
            colors.pressedColor = new Color(0.7f, 0.7f, 0.7f, 1f);
            colors.selectedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
            colors.disabledColor = new Color(0.7f, 0.7f, 0.7f, 0.5f);
            _button.colors = colors;
        }

        /// <summary>
        /// Sets the sprite for the button background.
        /// </summary>
        /// <param name="sprite">The sprite to use as background</param>
        public void SetBackgroundImage(Sprite sprite)
        {
            var image = _button.targetGraphic as Image;
            if (image != null)
            {
                image.sprite = sprite;
                image.type = sprite != null ? Image.Type.Sliced : Image.Type.Simple;
            }
        }

        /// <summary>
        /// Sets the colors for different button states.
        /// </summary>
        /// <param name="normalColor">Color when button is normal</param>
        /// <param name="highlightedColor">Color when button is highlighted</param>
        /// <param name="pressedColor">Color when button is pressed</param>
        /// <param name="disabledColor">Color when button is disabled</param>
        public void SetColors(Color normalColor, Color highlightedColor, Color pressedColor, Color disabledColor)
        {
            var colors = _button.colors;
            colors.normalColor = normalColor;
            colors.highlightedColor = highlightedColor;
            colors.pressedColor = pressedColor;
            colors.disabledColor = disabledColor;
            _button.colors = colors;
        }

        /// <summary>
        /// Sets the button to have rounded corners.
        /// </summary>
        /// <param name="cornerRadius">Radius of the corners in pixels</param>
        /// <param name="borderSize">Size of the border for 9-slice</param>
        public void SetRounded(int cornerRadius, int borderSize = 10)
        {
            // Get the button's image component
            var image = _button.targetGraphic as Image;
            if (image != null)
            {
                // Create a rounded rect sprite
                var size = RectTransform.sizeDelta;
                var sprite = RoundedRectGenerator.CreateRoundedRectSprite9Slice(
                    (int)size.x, (int)size.y, cornerRadius, borderSize, _button.colors.normalColor);
                    
                // Apply to button
                image.sprite = sprite;
                image.type = Image.Type.Sliced;
            }
        }

        /// <summary>
        /// Handler for button click events.
        /// </summary>
        private void OnButtonClick()
        {
            _onClick?.Invoke();
        }

        /// <summary>
        /// Destroys this button.
        /// </summary>
        public override void Destroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(OnButtonClick);
            }
            base.Destroy();
        }
    }
}
