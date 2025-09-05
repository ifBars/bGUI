using bGUI.Components;
using bGUI.Core.Abstractions;
using bGUI.Core.Components;
using bGUI.Core.Containers;
using bGUI.Services;
using UnityEngine;

namespace bGUI.Core.Factory
{
    /// <summary>
    /// Factory class for creating UI elements.
    /// </summary>
    public class UIFactory
    {
        private static UIFactory? _instance;
        private PoolingService _poolingService;

        /// <summary>
        /// Gets the singleton instance of the UIFactory.
        /// </summary>
        public static UIFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UIFactory();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the UIFactory class.
        /// </summary>
        private UIFactory()
        {
            _poolingService = new PoolingService();
        }

        /// <summary>
        /// Creates a button element.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the button</param>
        /// <param name="text">The text to display on the button</param>
        /// <param name="usePooling">Whether to use object pooling</param>
        /// <returns>A new button element</returns>
        public IButton CreateButton(Transform? parent, string name = "Button", string text = "Button", bool usePooling = false)
        {
            if (usePooling && _poolingService.TryGetPooledObject<ButtonWrapper>(out var pooledButton))
            {
                pooledButton.SetParent(parent);
                pooledButton.Name = name;
                pooledButton.Text = text;
                pooledButton.IsActive = true;
                return pooledButton;
            }

            return new ButtonWrapper(parent, name, text);
        }

        /// <summary>
        /// Creates a panel element.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the panel</param>
        /// <param name="usePooling">Whether to use object pooling</param>
        /// <returns>A new panel element</returns>
        public IPanel CreatePanel(Transform? parent, string name = "Panel", bool usePooling = false)
        {
            if (usePooling && _poolingService.TryGetPooledObject<PanelWrapper>(out var pooledPanel))
            {
                pooledPanel.SetParent(parent);
                pooledPanel.Name = name;
                pooledPanel.IsActive = true;
                return pooledPanel;
            }

            return new PanelWrapper(parent, name);
        }

        /// <summary>
        /// Creates a text element.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the text element</param>
        /// <param name="content">The text content</param>
        /// <param name="usePooling">Whether to use object pooling</param>
        /// <returns>A new text element</returns>
        public IText CreateText(Transform? parent, string name = "Text", string content = "", bool usePooling = false)
        {
            if (usePooling && _poolingService.TryGetPooledObject<TextWrapper>(out var pooledText))
            {
                pooledText.SetParent(parent);
                pooledText.Name = name;
                pooledText.Content = content;
                pooledText.IsActive = true;
                return pooledText;
            }

            return new TextWrapper(parent, name, content);
        }

        /// <summary>
        /// Creates an image element.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the image</param>
        /// <param name="sprite">The sprite to display</param>
        /// <param name="usePooling">Whether to use object pooling</param>
        /// <returns>A new image element</returns>
        public IImage CreateImage(Transform? parent, string name = "Image", Sprite? sprite = null, bool usePooling = false)
        {
            if (usePooling && _poolingService.TryGetPooledObject<ImageWrapper>(out var pooledImage))
            {
                pooledImage.SetParent(parent);
                pooledImage.Name = name;
                pooledImage.Sprite = sprite;
                pooledImage.IsActive = true;
                return pooledImage;
            }

            return new ImageWrapper(parent, name, sprite);
        }

        /// <summary>
        /// Creates a scroll view element.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the scroll view</param>
        /// <param name="usePooling">Whether to use object pooling</param>
        /// <returns>A new scroll view element</returns>
        public IScrollView CreateScrollView(Transform? parent, string name = "ScrollView", bool usePooling = false)
        {
            if (usePooling && _poolingService.TryGetPooledObject<ScrollViewWrapper>(out var pooledScrollView))
            {
                pooledScrollView.SetParent(parent);
                pooledScrollView.Name = name;
                pooledScrollView.IsActive = true;
                return pooledScrollView;
            }

            return new ScrollViewWrapper(parent, name);
        }

        /// <summary>
        /// Creates a slider element.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the slider</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <param name="value">The initial value</param>
        /// <param name="usePooling">Whether to use object pooling</param>
        /// <returns>A new slider element</returns>
        public ISlider CreateSlider(Transform? parent, string name = "Slider", float minValue = 0f, float maxValue = 1f, float value = 0f, bool usePooling = false)
        {
            if (usePooling && _poolingService.TryGetPooledObject<SliderWrapper>(out var pooledSlider))
            {
                pooledSlider.SetParent(parent);
                pooledSlider.Name = name;
                pooledSlider.MinValue = minValue;
                pooledSlider.MaxValue = maxValue;
                pooledSlider.Value = value;
                pooledSlider.IsActive = true;
                return pooledSlider;
            }

            return new SliderWrapper(parent, name, minValue, maxValue, value);
        }

        /// <summary>
        /// Creates a toggle element.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the toggle</param>
        /// <param name="label">The label text</param>
        /// <param name="isOn">Initial toggle state</param>
        /// <param name="usePooling">Whether to use object pooling</param>
        /// <returns>A new toggle element</returns>
        public IToggle CreateToggle(Transform? parent, string name = "Toggle", string label = "Toggle", bool isOn = false, bool usePooling = false)
        {
            if (usePooling && _poolingService.TryGetPooledObject<ToggleWrapper>(out var pooledToggle))
            {
                pooledToggle.SetParent(parent);
                pooledToggle.Name = name;
                pooledToggle.Label = label;
                pooledToggle.IsOn = isOn;
                pooledToggle.IsActive = true;
                return pooledToggle;
            }

            return new ToggleWrapper(parent, name, label, isOn);
        }

        /// <summary>
        /// Creates a dropdown element.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the dropdown</param>
        /// <param name="usePooling">Whether to use object pooling</param>
        /// <returns>A new dropdown element</returns>
        public IDropdown CreateDropdown(Transform? parent, string name = "Dropdown", bool usePooling = false)
        {
            if (usePooling && _poolingService.TryGetPooledObject<DropdownWrapper>(out var pooledDropdown))
            {
                pooledDropdown.SetParent(parent);
                pooledDropdown.Name = name;
                pooledDropdown.IsActive = true;
                return pooledDropdown;
            }

            return new DropdownWrapper(parent, name);
        }

        /// <summary>
        /// Creates a toggle group container element.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the toggle group</param>
        /// <param name="usePooling">Whether to use object pooling</param>
        /// <returns>A new toggle group wrapper</returns>
        public ToggleGroupWrapper CreateToggleGroup(Transform? parent, string name = "ToggleGroup", bool usePooling = false)
        {
            // Pooling not implemented for custom wrapper; safe to instantiate directly
            return new ToggleGroupWrapper(parent, name);
        }

        /// <summary>
        /// Returns a UI element to the object pool.
        /// </summary>
        /// <param name="element">The element to return to the pool</param>
        public void ReturnToPool(IUIElement element)
        {
            element.IsActive = false;
            _poolingService.ReturnToPool(element);
        }
    }
}
