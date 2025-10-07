using bGUI.Core.Abstractions;
using bGUI.Core.Components;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Components
{
    /// <summary>
    /// Wrapper for Unity's Dropdown component.
    /// </summary>
    public class DropdownWrapper : UIElementBase, IDropdown
    {
        private Dropdown _dropdown;
        private Image _backgroundImage = null!;
        private Text _captionText = null!;
        private Image _arrowImage = null!;
        private RectTransform _templateRect = null!;
        private RectTransform _contentRect = null!;
        private Text _itemText = null!;
        private Image _itemImage = null!;
        private Image _templateBackgroundImage = null!;
        private ScrollRect _templateScrollRect = null!;
        private float _itemHeight = 32f; // Increased from 24f for better readability
        private float _itemSpacing = 4f; // Increased from 2f for better visual separation
        private event Action<int>? _onValueChanged;
        
        // Helper to keep scroll position consistent when template toggles
        private class DropdownTemplateActivator : MonoBehaviour
        {
            private ScrollRect _scrollRect;
            public void Initialize(ScrollRect scrollRect)
            {
                _scrollRect = scrollRect;
            }
            private void OnEnable()
            {
                if (_scrollRect != null)
                {
                    _scrollRect.verticalNormalizedPosition = 1f;
                }
            }
        }

        /// <summary>
        /// Gets the underlying Dropdown component.
        /// </summary>
        public Dropdown DropdownComponent => _dropdown;

        /// <summary>
        /// Event triggered when the dropdown value changes.
        /// </summary>
        public event Action<int> OnValueChanged
        {
            add
            {
                _onValueChanged += value;
                if (_onValueChanged != null && _onValueChanged.GetInvocationList().Length == 1)
                {
                    _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
                }
            }
            remove
            {
                _onValueChanged -= value;
                if (_onValueChanged == null || _onValueChanged.GetInvocationList().Length == 0)
                {
                    _dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected index.
        /// </summary>
        public int Value
        {
            get => _dropdown.value;
            set => _dropdown.value = value;
        }

        /// <summary>
        /// Gets or sets whether the dropdown is interactable.
        /// </summary>
        public bool Interactable
        {
            get => _dropdown.interactable;
            set => _dropdown.interactable = value;
        }

        /// <summary>
        /// Gets or sets the caption text (selected value label).
        /// </summary>
        public string Caption
        {
            get => _captionText != null ? _captionText.text : string.Empty;
            set { if (_captionText != null) _captionText.text = value; }
        }

        /// <summary>
        /// Initializes a new instance of the DropdownWrapper class.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the dropdown</param>
        public DropdownWrapper(Transform? parent, string name = "Dropdown")
            : base(parent, name)
        {
            _rectTransform.sizeDelta = new Vector2(160f, 30f);

            _backgroundImage = _gameObject.AddComponent<Image>();
            _dropdown = _gameObject.AddComponent<Dropdown>();

            CreateDropdownStructure();
            SetupDefaults();
        }

        private void CreateDropdownStructure()
        {
            // Label
            var labelGO = new GameObject("Label");
            var labelRect = labelGO.AddComponent<RectTransform>();
            labelRect.SetParent(_rectTransform, false);
            labelRect.anchorMin = new Vector2(0f, 0f);
            labelRect.anchorMax = new Vector2(1f, 1f);
            labelRect.offsetMin = new Vector2(10f, 0f);
            labelRect.offsetMax = new Vector2(-20f, 0f);
            _captionText = labelGO.AddComponent<Text>();
            _captionText.alignment = TextAnchor.MiddleLeft;
            _captionText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            _captionText.color = Color.black;

            // Arrow
            var arrowGO = new GameObject("Arrow");
            var arrowRect = arrowGO.AddComponent<RectTransform>();
            arrowRect.SetParent(_rectTransform, false);
            arrowRect.anchorMin = new Vector2(1f, 0.5f);
            arrowRect.anchorMax = new Vector2(1f, 0.5f);
            arrowRect.pivot = new Vector2(1f, 0.5f);
            arrowRect.sizeDelta = new Vector2(20f, 20f);
            arrowRect.anchoredPosition = new Vector2(-10f, 0f);
            _arrowImage = arrowGO.AddComponent<Image>();

            // Template (initially inactive)
            var templateGO = new GameObject("Template");
            _templateRect = templateGO.AddComponent<RectTransform>();
            _templateRect.SetParent(_rectTransform, false);
            _templateRect.anchorMin = new Vector2(0f, 0f);
            _templateRect.anchorMax = new Vector2(1f, 0f);
            _templateRect.pivot = new Vector2(0.5f, 1f);
            _templateRect.sizeDelta = new Vector2(0f, 150f);
            _templateRect.anchoredPosition = new Vector2(0f, 0f); // Flush with dropdown bottom edge
            templateGO.SetActive(false);

            _templateBackgroundImage = templateGO.AddComponent<Image>();
            _templateBackgroundImage.type = Image.Type.Sliced;
            var scrollRect = templateGO.AddComponent<ScrollRect>();
            // Clamp and stabilize scrolling to avoid snapping back
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.movementType = ScrollRect.MovementType.Clamped;
            scrollRect.inertia = false;
            scrollRect.elasticity = 0f;
            scrollRect.scrollSensitivity = Mathf.Max(18f, _itemHeight * 0.9f); // Responsive, scales with item size
            _templateScrollRect = scrollRect;

            // Viewport
            var viewportGO = new GameObject("Viewport");
            var viewportRect = viewportGO.AddComponent<RectTransform>();
            viewportRect.SetParent(_templateRect, false);
            viewportRect.anchorMin = new Vector2(0f, 0f);
            viewportRect.anchorMax = new Vector2(1f, 1f);
            viewportRect.sizeDelta = Vector2.zero;
            var viewportImage = viewportGO.AddComponent<Image>();
            viewportImage.type = Image.Type.Sliced;
            // Transparent but raycastable so scroll/drag work reliably
            viewportImage.color = new Color(1f, 1f, 1f, 0f);
            viewportImage.raycastTarget = true;
            // Use RectMask2D for reliable rectangular clipping
            viewportGO.AddComponent<RectMask2D>();

            // Content
            var contentGO = new GameObject("Content");
            _contentRect = contentGO.AddComponent<RectTransform>();
            _contentRect.SetParent(viewportRect, false);
            _contentRect.anchorMin = new Vector2(0f, 1f);
            _contentRect.anchorMax = new Vector2(1f, 1f);
            _contentRect.pivot = new Vector2(0.5f, 1f);
            _contentRect.anchoredPosition = Vector2.zero;
            _contentRect.sizeDelta = new Vector2(0f, 0f);
            var vlg = contentGO.AddComponent<VerticalLayoutGroup>();
            vlg.childControlHeight = true;
            vlg.childForceExpandHeight = false;
            vlg.childForceExpandWidth = true;
            vlg.childAlignment = TextAnchor.UpperLeft;
            vlg.spacing = _itemSpacing;
            vlg.padding = new RectOffset(2, 2, 2, 2);
            var fitter = contentGO.AddComponent<ContentSizeFitter>();
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            // Item
            var itemGO = new GameObject("Item");
            var itemRect = itemGO.AddComponent<RectTransform>();
            itemRect.SetParent(_contentRect, false);
            // Top-align items so the first item starts exactly at the top of the viewport
            itemRect.anchorMin = new Vector2(0f, 1f);
            itemRect.anchorMax = new Vector2(1f, 1f);
            itemRect.pivot = new Vector2(0.5f, 1f);
            itemRect.sizeDelta = new Vector2(0f, _itemHeight);

            var itemToggle = itemGO.AddComponent<Toggle>();
            var layout = itemGO.AddComponent<LayoutElement>();
            layout.minHeight = _itemHeight;
            layout.preferredHeight = _itemHeight;
            layout.flexibleHeight = 0f; // Prevent stretching
            var itemBGGO = new GameObject("Item Background");
            var itemBGRect = itemBGGO.AddComponent<RectTransform>();
            itemBGRect.SetParent(itemRect, false);
            itemBGRect.anchorMin = new Vector2(0f, 0f);
            itemBGRect.anchorMax = new Vector2(1f, 1f);
            _itemImage = itemBGGO.AddComponent<Image>();

            var itemCheckGO = new GameObject("Item Checkmark");
            var itemCheckRect = itemCheckGO.AddComponent<RectTransform>();
            itemCheckRect.SetParent(itemBGRect, false);
            itemCheckRect.anchorMin = new Vector2(0f, 0.5f);
            itemCheckRect.anchorMax = new Vector2(0f, 0.5f);
            itemCheckRect.pivot = new Vector2(0f, 0.5f);
            itemCheckRect.anchoredPosition = new Vector2(6f, -2f);
            itemCheckRect.sizeDelta = new Vector2(20f, 20f);
            var itemCheckImage = itemCheckGO.AddComponent<Image>();

            var itemLabelGO = new GameObject("Item Label");
            var itemLabelRect = itemLabelGO.AddComponent<RectTransform>();
            itemLabelRect.SetParent(itemRect, false);
            itemLabelRect.anchorMin = new Vector2(0f, 0f);
            itemLabelRect.anchorMax = new Vector2(1f, 1f);
            itemLabelRect.offsetMin = new Vector2(26f, 0f);
            _itemText = itemLabelGO.AddComponent<Text>();
            _itemText.alignment = TextAnchor.MiddleLeft;
            _itemText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            _itemText.color = Color.black;

            // Wire template components
            scrollRect.viewport = viewportRect;
            scrollRect.content = _contentRect;
            itemToggle.targetGraphic = _itemImage;
            itemToggle.graphic = itemCheckImage;

            // Hook dropdown to parts
            _dropdown.template = _templateRect;
            _dropdown.captionText = _captionText;
            _dropdown.itemText = _itemText;
            // Unity's default Dropdown uses an optional captionImage; we leave it null here.
            _dropdown.captionImage = null;
            _dropdown.itemImage = _itemImage;
            _dropdown.targetGraphic = _backgroundImage;
            // Ensure the list opens scrolled to the top by default
            scrollRect.verticalNormalizedPosition = 1f;
            // Keep content at top when template is enabled
            templateGO.AddComponent<DropdownTemplateActivator>().Initialize(scrollRect);
        }

        private void SetupDefaults()
        {
            var uiSprite = Resources.GetBuiltinResource<Sprite>("UI/Skin/UISprite.psd");
            var arrowSprite = Resources.GetBuiltinResource<Sprite>("UI/Skin/DropdownArrow.psd");

            if (uiSprite != null)
            {
                _backgroundImage.sprite = uiSprite;
                _backgroundImage.type = Image.Type.Sliced;
                // Dark theme default background
                _backgroundImage.color = new Color(0.25f, 0.27f, 0.32f, 1f);
                _templateBackgroundImage.sprite = uiSprite;
                _templateBackgroundImage.type = Image.Type.Sliced;
                _templateBackgroundImage.color = new Color(0.18f, 0.2f, 0.24f, 0.98f);
            }
            if (arrowSprite != null)
            {
                _arrowImage.sprite = arrowSprite;
                _arrowImage.type = Image.Type.Sliced;
                _arrowImage.color = Color.white;
            }

            var colors = _dropdown.colors;
            colors.normalColor = new Color(0.25f, 0.27f, 0.32f, 1f);
            colors.highlightedColor = new Color(0.30f, 0.33f, 0.38f, 1f);
            colors.pressedColor = new Color(0.20f, 0.22f, 0.26f, 1f);
            colors.selectedColor = new Color(0.30f, 0.50f, 0.90f, 1f);
            colors.disabledColor = new Color(0.25f, 0.27f, 0.32f, 0.5f);
            _dropdown.colors = colors;

            // Default single option placeholder
            SetOptions(new[] { "Option A", "Option B" });
            Value = 0;
        }

        public void ClearOptions()
        {
            _dropdown.ClearOptions();
        }

        public void AddOption(string option)
        {
            _dropdown.options.Add(new Dropdown.OptionData(option));
            _dropdown.RefreshShownValue();
        }

        public void AddOptions(IEnumerable<string> options)
        {
            foreach (var o in options)
            {
                _dropdown.options.Add(new Dropdown.OptionData(o));
            }
            _dropdown.RefreshShownValue();
        }

        public void SetOptions(IEnumerable<string> options)
        {
            _dropdown.ClearOptions();
            var list = new List<Dropdown.OptionData>();
            foreach (var o in options)
                list.Add(new Dropdown.OptionData(o));
            _dropdown.AddOptions(list);
            _dropdown.RefreshShownValue();
            
            // Debug logging
            Debug.Log($"[DropdownWrapper] SetOptions: Added {list.Count} options. Total options: {_dropdown.options.Count}");
        }

        public void SetColors(Color normalColor, Color highlightedColor, Color pressedColor, Color disabledColor)
        {
            var colors = _dropdown.colors;
            colors.normalColor = normalColor;
            colors.highlightedColor = highlightedColor;
            colors.pressedColor = pressedColor;
            colors.disabledColor = disabledColor;
            _dropdown.colors = colors;
        }

        public void SetBackgroundImage(Sprite sprite)
        {
            if (_backgroundImage != null)
            {
                _backgroundImage.sprite = sprite;
                _backgroundImage.type = sprite != null ? Image.Type.Sliced : Image.Type.Simple;
            }
        }

        public void SetLabelColor(Color color)
        {
            if (_captionText != null)
            {
                _captionText.color = color;
            }
        }

        public void SetArrowImage(Sprite sprite)
        {
            if (_arrowImage != null)
            {
                _arrowImage.sprite = sprite;
                _arrowImage.type = sprite != null ? Image.Type.Sliced : Image.Type.Simple;
            }
        }

        public void SetBackgroundColor(Color color)
        {
            if (_backgroundImage != null)
            {
                _backgroundImage.color = color;
            }
        }

        public void SetArrowColor(Color color)
        {
            if (_arrowImage != null)
            {
                _arrowImage.color = color;
            }
        }

        public void SetTemplateBackgroundColor(Color color)
        {
            if (_templateBackgroundImage != null)
            {
                _templateBackgroundImage.color = color;
            }
        }

        public void SetTemplateHeight(float height)
        {
            if (_templateRect != null)
            {
                var size = _templateRect.sizeDelta;
                size.y = height;
                _templateRect.sizeDelta = size;
            }
        }

        public void SetVisibleItemCount(int count)
        {
            if (count < 1) count = 1;
            float height = CalculateTemplateHeight(count);
            SetTemplateHeight(height);
            // Ensure layout rebuild so ScrollRect gets the updated viewport size
            if (_templateRect != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(_templateRect);
                if (_templateScrollRect != null)
                {
                    _templateScrollRect.verticalNormalizedPosition = 1f;
                }
            }
        }

        /// <summary>
        /// Sets the height of individual dropdown items.
        /// </summary>
        /// <param name="height">The height of each item in pixels</param>
        public void SetItemHeight(float height)
        {
            if (height < 16f) height = 16f; // Minimum readable height
            _itemHeight = height;
            
            // Update the item template if it exists
            if (_contentRect != null)
            {
                var itemGO = _contentRect.GetChild(0);
                if (itemGO != null)
                {
                    var itemRect = itemGO.GetComponent<RectTransform>();
                    var layout = itemGO.GetComponent<LayoutElement>();
                    if (itemRect != null)
                    {
                        itemRect.sizeDelta = new Vector2(itemRect.sizeDelta.x, _itemHeight);
                    }
                    if (layout != null)
                    {
                        layout.minHeight = _itemHeight;
                        layout.preferredHeight = _itemHeight;
                    }
                }
            }
            if (_templateScrollRect != null)
            {
                _templateScrollRect.scrollSensitivity = Mathf.Max(18f, _itemHeight * 0.9f);
            }
        }

        /// <summary>
        /// Sets the spacing between dropdown items.
        /// </summary>
        /// <param name="spacing">The spacing between items in pixels</param>
        public void SetItemSpacing(float spacing)
        {
            if (spacing < 0f) spacing = 0f;
            _itemSpacing = spacing;
            
            // Update the vertical layout group if it exists
            if (_contentRect != null)
            {
                var vlg = _contentRect.GetComponent<VerticalLayoutGroup>();
                if (vlg != null)
                {
                    vlg.spacing = _itemSpacing;
                }
            }
        }

        /// <summary>
        /// Calculates the optimal template height for the given number of visible items.
        /// </summary>
        /// <param name="visibleCount">Number of items to be visible</param>
        /// <returns>The calculated template height</returns>
        private float CalculateTemplateHeight(int visibleCount)
        {
            // Calculate based on item height, spacing, and padding
            float contentHeight = visibleCount * _itemHeight;
            float spacingHeight = Mathf.Max(0, visibleCount - 1) * _itemSpacing;
            float padding = 12f; // Slightly larger to avoid clipping first/last item
            return contentHeight + spacingHeight + padding;
        }

        /// <summary>
        /// Sets an adaptive template height based on the number of options available.
        /// Will show all items if 6 or fewer, otherwise shows a scrollable list.
        /// </summary>
        public void SetAdaptiveHeight()
        {
            int optionCount = _dropdown.options.Count;
            int visibleCount = Mathf.Clamp(optionCount, 3, 6); // Show at least 3 rows, max 6
            
            // Debug logging
            Debug.Log($"[DropdownWrapper] SetAdaptiveHeight: {optionCount} options, showing {visibleCount} visible");
            
            SetVisibleItemCount(visibleCount);
        }

        /// <summary>
        /// Configures the dropdown for optimal readability with larger items.
        /// </summary>
        public void SetLargeItemMode()
        {
            SetItemHeight(40f);
            SetItemSpacing(6f);
            SetVisibleItemCount(5); // Fewer visible items due to larger size
        }

        /// <summary>
        /// Configures the dropdown for compact display with smaller items.
        /// </summary>
        public void SetCompactItemMode()
        {
            SetItemHeight(28f);
            SetItemSpacing(2f);
            SetVisibleItemCount(8);
        }

        /// <summary>
        /// Forces a complete refresh of the dropdown layout and sizing.
        /// Call this after all options and settings have been configured.
        /// </summary>
        public void RefreshLayout()
        {
            _dropdown.RefreshShownValue();
            // Force rebuild the template
            if (_templateRect != null && _templateRect.gameObject.activeSelf)
            {
                _templateRect.gameObject.SetActive(false);
                _templateRect.gameObject.SetActive(true);
            }
        }

        private void OnDropdownValueChanged(int value)
        {
            _onValueChanged?.Invoke(value);
        }

        public override void Destroy()
        {
            if (_dropdown != null)
            {
                _dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
            }
            base.Destroy();
        }
    }
}

