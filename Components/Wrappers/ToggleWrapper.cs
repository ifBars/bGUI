using bGUI.Core.Abstractions;
using bGUI.Core.Components;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Components
{
    /// <summary>
    /// Wrapper for Unity's Toggle component.
    /// </summary>
    public class ToggleWrapper : UIElementBase, IToggle
    {
        private Toggle _toggle;
        private Image _backgroundImage = null!;
        private Image _checkmarkImage = null!;
        private Text _label = null!;
        private event Action<bool>? _onValueChanged;

        /// <summary>
        /// Gets the underlying Toggle component.
        /// </summary>
        public Toggle ToggleComponent => _toggle;

        /// <summary>
        /// Event triggered when the toggle value changes.
        /// </summary>
        public event Action<bool> OnValueChanged
        {
            add
            {
                _onValueChanged += value;
                if (_onValueChanged != null && _onValueChanged.GetInvocationList().Length == 1)
                {
                    _toggle.onValueChanged.AddListener(OnToggleValueChanged);
                }
            }
            remove
            {
                _onValueChanged -= value;
                if (_onValueChanged == null || _onValueChanged.GetInvocationList().Length == 0)
                {
                    _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the toggle is on.
        /// </summary>
        public bool IsOn
        {
            get => _toggle.isOn;
            set => _toggle.isOn = value;
        }

        /// <summary>
        /// Gets or sets whether the toggle is interactable.
        /// </summary>
        public bool Interactable
        {
            get => _toggle.interactable;
            set => _toggle.interactable = value;
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        public string Label
        {
            get => _label != null ? _label.text : string.Empty;
            set { if (_label != null) _label.text = value; }
        }

        /// <summary>
        /// Initializes a new instance of the ToggleWrapper class.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the toggle</param>
        /// <param name="label">The label text</param>
        /// <param name="isOn">Initial toggle state</param>
        public ToggleWrapper(Transform? parent, string name = "Toggle", string label = "Toggle", bool isOn = false)
            : base(parent, name)
        {
            // Set a readable default size
            _rectTransform.sizeDelta = new Vector2(220f, 28f);

            // Add Toggle component
            _toggle = _gameObject.AddComponent<Toggle>();

            CreateToggleStructure();
            SetupDefaults(label, isOn);
        }

        private void CreateToggleStructure()
        {
            // Background (child of root)
            var backgroundGO = new GameObject("Background");
            var backgroundRect = backgroundGO.AddComponent<RectTransform>();
            backgroundRect.SetParent(_rectTransform, false);
            backgroundRect.anchorMin = new Vector2(0f, 0.5f);
            backgroundRect.anchorMax = new Vector2(0f, 0.5f);
            backgroundRect.pivot = new Vector2(0f, 0.5f);
            backgroundRect.sizeDelta = new Vector2(20f, 20f);
            _backgroundImage = backgroundGO.AddComponent<Image>();

            // Checkmark (child of Background)
            var checkmarkGO = new GameObject("Checkmark");
            var checkmarkRect = checkmarkGO.AddComponent<RectTransform>();
            checkmarkRect.SetParent(backgroundRect, false);
            checkmarkRect.anchorMin = new Vector2(0.2f, 0.2f);
            checkmarkRect.anchorMax = new Vector2(0.8f, 0.8f);
            checkmarkRect.offsetMin = Vector2.zero;
            checkmarkRect.offsetMax = Vector2.zero;
            _checkmarkImage = checkmarkGO.AddComponent<Image>();

            // Label (sibling)
            var labelGO = new GameObject("Label");
            var labelRect = labelGO.AddComponent<RectTransform>();
            labelRect.SetParent(_rectTransform, false);
            labelRect.anchorMin = new Vector2(0f, 0f);
            labelRect.anchorMax = new Vector2(1f, 1f);
            labelRect.offsetMin = new Vector2(25f, 0f);
            labelRect.offsetMax = Vector2.zero;
            _label = labelGO.AddComponent<Text>();
            _label.alignment = TextAnchor.MiddleLeft;
            _label.color = Color.white;
            _label.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            _label.fontSize = 16;

            // Wire up toggle references
            _toggle.targetGraphic = _backgroundImage; // transition graphic
            _toggle.graphic = _checkmarkImage;        // checkmark graphic
        }

        private void SetupDefaults(string label, bool isOn)
        {
            // Default sprites
            var uiSprite = Resources.GetBuiltinResource<Sprite>("UI/Skin/UISprite.psd");
            var checkmarkSprite = Resources.GetBuiltinResource<Sprite>("UI/Skin/Checkmark.psd");

            if (uiSprite != null)
            {
                _backgroundImage.sprite = uiSprite;
                _backgroundImage.type = Image.Type.Sliced;
                _backgroundImage.color = new Color(1f, 1f, 1f, 1f);
            }
            if (checkmarkSprite != null)
            {
                _checkmarkImage.sprite = checkmarkSprite;
                _checkmarkImage.type = Image.Type.Sliced;
                _checkmarkImage.color = Color.black;
            }

            // Defaults
            _label.text = label;
            _toggle.isOn = isOn;

            // High-contrast colors for dark UI
            var colors = _toggle.colors;
            colors.normalColor = new Color(0.25f, 0.25f, 0.3f, 1f);
            colors.highlightedColor = new Color(0.35f, 0.35f, 0.4f, 1f);
            colors.pressedColor = new Color(0.2f, 0.2f, 0.25f, 1f);
            colors.selectedColor = new Color(0.3f, 0.5f, 0.9f, 1f);
            colors.disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            _toggle.colors = colors;
        }

        public void SetColors(Color normalColor, Color highlightedColor, Color pressedColor, Color disabledColor)
        {
            var colors = _toggle.colors;
            colors.normalColor = normalColor;
            colors.highlightedColor = highlightedColor;
            colors.pressedColor = pressedColor;
            colors.disabledColor = disabledColor;
            _toggle.colors = colors;
        }

        public void SetBackgroundImage(Sprite sprite)
        {
            if (_backgroundImage != null)
            {
                _backgroundImage.sprite = sprite;
                _backgroundImage.type = sprite != null ? Image.Type.Sliced : Image.Type.Simple;
            }
        }

        public void SetCheckmarkImage(Sprite sprite)
        {
            if (_checkmarkImage != null)
            {
                _checkmarkImage.sprite = sprite;
                _checkmarkImage.type = sprite != null ? Image.Type.Sliced : Image.Type.Simple;
            }
        }

        public void SetLabelColor(Color color)
        {
            if (_label != null)
            {
                _label.color = color;
            }
        }

        private void OnToggleValueChanged(bool value)
        {
            _onValueChanged?.Invoke(value);
        }

        public override void Destroy()
        {
            if (_toggle != null)
            {
                _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
            }
            base.Destroy();
        }
    }
}

