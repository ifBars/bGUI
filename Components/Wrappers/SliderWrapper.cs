using bGUI.Core.Abstractions;
using bGUI.Core.Components;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Components
{
    /// <summary>
    /// Wrapper for Unity's Slider component.
    /// </summary>
    public class SliderWrapper : UIElementBase, ISlider
    {
        private Slider _slider;
        private Image _backgroundImage = null!;
        private Image _fillImage = null!;
        private Image _handleImage = null!;
        private event Action<float>? _onValueChanged;

        /// <summary>
        /// Gets the underlying Slider component.
        /// </summary>
        public Slider SliderComponent => _slider;

        /// <summary>
        /// Event triggered when the slider value changes.
        /// </summary>
        public event Action<float> OnValueChanged
        {
            add
            {
                _onValueChanged += value;
                if (_onValueChanged != null && _onValueChanged.GetInvocationList().Length == 1)
                {
                    // Only register the event handler once
                    _slider.onValueChanged.AddListener(OnSliderValueChanged);
                }
            }
            remove
            {
                _onValueChanged -= value;
                if (_onValueChanged == null || _onValueChanged.GetInvocationList().Length == 0)
                {
                    // Unregister the event handler when there are no subscribers
                    _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
                }
            }
        }

        /// <summary>
        /// Gets or sets the current value of the slider.
        /// </summary>
        public float Value
        {
            get => _slider.value;
            set => _slider.value = value;
        }

        /// <summary>
        /// Gets or sets the minimum value of the slider.
        /// </summary>
        public float MinValue
        {
            get => _slider.minValue;
            set => _slider.minValue = value;
        }

        /// <summary>
        /// Gets or sets the maximum value of the slider.
        /// </summary>
        public float MaxValue
        {
            get => _slider.maxValue;
            set => _slider.maxValue = value;
        }

        /// <summary>
        /// Gets or sets whether the slider uses whole numbers only.
        /// </summary>
        public bool WholeNumbers
        {
            get => _slider.wholeNumbers;
            set => _slider.wholeNumbers = value;
        }

        /// <summary>
        /// Gets or sets the direction of the slider.
        /// </summary>
        public Slider.Direction Direction
        {
            get => _slider.direction;
            set => _slider.direction = value;
        }

        /// <summary>
        /// Gets or sets whether the slider is interactable.
        /// </summary>
        public bool Interactable
        {
            get => _slider.interactable;
            set => _slider.interactable = value;
        }

        /// <summary>
        /// Initializes a new instance of the SliderWrapper class.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the slider</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <param name="value">The initial value</param>
        public SliderWrapper(Transform? parent, string name = "Slider", float minValue = 0f, float maxValue = 1f, float value = 0f)
            : base(parent, name)
        {
            // Set default size for slider
            _rectTransform.sizeDelta = new Vector2(160f, 20f);

            // Add slider component
            _slider = _gameObject.AddComponent<Slider>();
            _slider.minValue = minValue;
            _slider.maxValue = maxValue;
            _slider.value = value;

            CreateSliderStructure();
            SetupDefaultStyling();
        }

        /// <summary>
        /// Creates the standard Unity slider structure (Background, Fill Area, Handle Slide Area).
        /// </summary>
        private void CreateSliderStructure()
        {
            // Create Background
            var backgroundGO = new GameObject("Background");
            var backgroundRect = backgroundGO.AddComponent<RectTransform>();
            backgroundRect.SetParent(_rectTransform, false);
            _backgroundImage = backgroundGO.AddComponent<Image>();
            // Make the background receive raycasts so clicking the track sets value
            _backgroundImage.raycastTarget = true;
            
            // Set up background RectTransform
            backgroundRect.anchorMin = Vector2.zero;
            backgroundRect.anchorMax = Vector2.one;
            backgroundRect.sizeDelta = Vector2.zero;
            backgroundRect.offsetMin = Vector2.zero;
            backgroundRect.offsetMax = Vector2.zero;

            // Create Fill Area
            var fillAreaGO = new GameObject("Fill Area");
            var fillAreaRect = fillAreaGO.AddComponent<RectTransform>();
            fillAreaRect.SetParent(_rectTransform, false);
            
            // Set up fill area RectTransform
            fillAreaRect.anchorMin = Vector2.zero;
            fillAreaRect.anchorMax = Vector2.one;
            fillAreaRect.sizeDelta = new Vector2(-20f, 0f);
            fillAreaRect.offsetMin = new Vector2(10f, 0f);
            fillAreaRect.offsetMax = new Vector2(-10f, 0f);

            // Create Fill
            var fillGO = new GameObject("Fill");
            var fillRect = fillGO.AddComponent<RectTransform>();
            fillRect.SetParent(fillAreaRect, false);
            _fillImage = fillGO.AddComponent<Image>();
            
            // Set up fill RectTransform
            fillRect.anchorMin = Vector2.zero;
            fillRect.anchorMax = new Vector2(0f, 1f);
            fillRect.sizeDelta = new Vector2(10f, 0f);
            fillRect.offsetMin = Vector2.zero;
            fillRect.offsetMax = Vector2.zero;

            // Create Handle Slide Area
            var handleAreaGO = new GameObject("Handle Slide Area");
            var handleAreaRect = handleAreaGO.AddComponent<RectTransform>();
            handleAreaRect.SetParent(_rectTransform, false);
            
            // Set up handle area RectTransform
            handleAreaRect.anchorMin = Vector2.zero;
            handleAreaRect.anchorMax = Vector2.one;
            handleAreaRect.sizeDelta = new Vector2(-20f, 0f);
            handleAreaRect.offsetMin = new Vector2(10f, 0f);
            handleAreaRect.offsetMax = new Vector2(-10f, 0f);

            // Create Handle
            var handleGO = new GameObject("Handle");
            var handleRect = handleGO.AddComponent<RectTransform>();
            handleRect.SetParent(handleAreaRect, false);
            _handleImage = handleGO.AddComponent<Image>();
            
            // Set up handle RectTransform
            handleRect.anchorMin = new Vector2(0f, 0f);
            handleRect.anchorMax = new Vector2(0f, 1f);
            handleRect.sizeDelta = new Vector2(20f, 0f);
            handleRect.offsetMin = Vector2.zero;
            handleRect.offsetMax = Vector2.zero;

            // Assign components to slider - INCLUDING the background!
            _slider.fillRect = fillRect;
            _slider.handleRect = handleRect;
            _slider.targetGraphic = _handleImage;
            _slider.transition = Selectable.Transition.ColorTint;
            
            // Create default sprites for the images
            CreateDefaultSprites();
        }

        /// <summary>
        /// Creates default sprites for slider images to ensure they render properly.
        /// </summary>
        private void CreateDefaultSprites()
        {
            // Create a proper default sprite from Unity's built-in white texture
            var defaultSprite = CreateUISprite();
            
            // Assign default sprites to all images with proper settings
            _backgroundImage.sprite = defaultSprite;
            _backgroundImage.type = Image.Type.Sliced;
            _backgroundImage.material = null;
            _backgroundImage.raycastTarget = true; // Background needs raycasts for clicking track to set value
            
            _fillImage.sprite = defaultSprite;
            _fillImage.type = Image.Type.Sliced;
            _fillImage.material = null;
            _fillImage.raycastTarget = false;
            
            _handleImage.sprite = defaultSprite;
            _handleImage.type = Image.Type.Sliced;
            _handleImage.material = null;
            _handleImage.raycastTarget = true; // Handle needs raycasts for interaction
        }

        /// <summary>
        /// Creates a proper UI sprite that works reliably for rendering.
        /// </summary>
        private Sprite CreateUISprite()
        {
            // Use Unity's built-in UI sprite which is guaranteed to work
            var uiSprite = Resources.GetBuiltinResource<Sprite>("UI/Skin/UISprite.psd");
            if (uiSprite != null)
            {
                return uiSprite;
            }
            
            // Fallback: create from white texture with proper border settings
            var texture = Texture2D.whiteTexture;
            return Sprite.Create(texture, 
                new Rect(0, 0, texture.width, texture.height), 
                new Vector2(0.5f, 0.5f), 
                100f, // pixels per unit
                0, // extrude
                SpriteMeshType.FullRect,
                new Vector4(1, 1, 1, 1)); // border for slicing
        }

        /// <summary>
        /// Sets up default styling for the slider.
        /// </summary>
        private void SetupDefaultStyling()
        {
            // Set default background color - make it much more visible
            _backgroundImage.color = new Color(0.5f, 0.5f, 0.5f, 1f); // Medium gray, fully opaque

            // Set default fill color - bright and visible
            _fillImage.color = new Color(0.2f, 0.8f, 0.2f, 1f); // Bright green

            // Set default handle color and styling
            _handleImage.color = Color.white;

            // Set default slider colors
            var colors = _slider.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(0.9f, 0.9f, 0.9f, 1f);
            colors.pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
            colors.selectedColor = new Color(0.9f, 0.9f, 0.9f, 1f);
            colors.disabledColor = new Color(0.8f, 0.8f, 0.8f, 0.5f);
            _slider.colors = colors;
        }

        /// <summary>
        /// Sets the background color of the slider.
        /// </summary>
        /// <param name="color">The color to set</param>
        public void SetBackgroundColor(Color color)
        {
            if (_backgroundImage != null)
            {
                _backgroundImage.color = color;
                UnityEngine.Debug.Log($"[SliderWrapper] Background color set to: {color}, GameObject active: {_backgroundImage.gameObject.activeSelf}, Image enabled: {_backgroundImage.enabled}");
            }
            else
            {
                UnityEngine.Debug.LogError("[SliderWrapper] Background image is null!");
            }
        }

        /// <summary>
        /// Sets the fill color of the slider.
        /// </summary>
        /// <param name="color">The color to set</param>
        public void SetFillColor(Color color)
        {
            if (_fillImage != null)
            {
                _fillImage.color = color;
                UnityEngine.Debug.Log($"[SliderWrapper] Fill color set to: {color}, GameObject active: {_fillImage.gameObject.activeSelf}, Image enabled: {_fillImage.enabled}");
            }
            else
            {
                UnityEngine.Debug.LogError("[SliderWrapper] Fill image is null!");
            }
        }

        /// <summary>
        /// Sets the handle color of the slider.
        /// </summary>
        /// <param name="color">The color to set</param>
        public void SetHandleColor(Color color)
        {
            if (_handleImage != null)
            {
                _handleImage.color = color;
            }
        }

        /// <summary>
        /// Sets the colors for different slider states.
        /// </summary>
        /// <param name="normalColor">Color when slider is normal</param>
        /// <param name="highlightedColor">Color when slider is highlighted</param>
        /// <param name="pressedColor">Color when slider is pressed</param>
        /// <param name="disabledColor">Color when slider is disabled</param>
        public void SetColors(Color normalColor, Color highlightedColor, Color pressedColor, Color disabledColor)
        {
            var colors = _slider.colors;
            colors.normalColor = normalColor;
            colors.highlightedColor = highlightedColor;
            colors.pressedColor = pressedColor;
            colors.disabledColor = disabledColor;
            _slider.colors = colors;
        }

        /// <summary>
        /// Sets the sprite for the slider background.
        /// </summary>
        /// <param name="sprite">The sprite to use as background</param>
        public void SetBackgroundImage(Sprite sprite)
        {
            if (_backgroundImage != null)
            {
                _backgroundImage.sprite = sprite;
                _backgroundImage.type = sprite != null ? Image.Type.Sliced : Image.Type.Simple;
            }
        }

        /// <summary>
        /// Sets the sprite for the slider fill area.
        /// </summary>
        /// <param name="sprite">The sprite to use for fill</param>
        public void SetFillImage(Sprite sprite)
        {
            if (_fillImage != null)
            {
                _fillImage.sprite = sprite;
                _fillImage.type = sprite != null ? Image.Type.Sliced : Image.Type.Simple;
            }
        }

        /// <summary>
        /// Sets the sprite for the slider handle.
        /// </summary>
        /// <param name="sprite">The sprite to use for handle</param>
        public void SetHandleImage(Sprite sprite)
        {
            if (_handleImage != null)
            {
                _handleImage.sprite = sprite;
                _handleImage.type = sprite != null ? Image.Type.Sliced : Image.Type.Simple;
            }
        }

        /// <summary>
        /// Handler for slider value change events.
        /// </summary>
        /// <param name="value">The new value</param>
        private void OnSliderValueChanged(float value)
        {
            _onValueChanged?.Invoke(value);
        }

        /// <summary>
        /// Destroys this slider.
        /// </summary>
        public override void Destroy()
        {
            if (_slider != null)
            {
                _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            }
            base.Destroy();
        }
    }
} 
