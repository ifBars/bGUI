using bGUI.Core.Abstractions;
using bGUI.Core.Components;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Components
{
    /// <summary>
    /// Wrapper for Unity's ScrollRect UI component.
    /// </summary>
    public class ScrollViewWrapper : UIElementBase, IScrollView
    {
        private ScrollRect _scrollRect = null!;
        private RectTransform _viewport = null!;
        private RectTransform _content = null!;

        /// <summary>
        /// Gets the underlying ScrollRect component.
        /// </summary>
        public ScrollRect ScrollRect => _scrollRect;

        /// <summary>
        /// Gets the viewport RectTransform.
        /// </summary>
        public RectTransform Viewport => _viewport;

        /// <summary>
        /// Gets the content RectTransform.
        /// </summary>
        public RectTransform Content => _content;

        /// <summary>
        /// Gets or sets whether horizontal scrolling is enabled.
        /// </summary>
        public bool HorizontalScrolling
        {
            get => _scrollRect.horizontal;
            set => _scrollRect.horizontal = value;
        }

        /// <summary>
        /// Gets or sets whether vertical scrolling is enabled.
        /// </summary>
        public bool VerticalScrolling
        {
            get => _scrollRect.vertical;
            set => _scrollRect.vertical = value;
        }

        /// <summary>
        /// Gets or sets the normalized position of the horizontal scrollbar (0-1).
        /// </summary>
        public float HorizontalNormalizedPosition
        {
            get => _scrollRect.horizontalNormalizedPosition;
            set => _scrollRect.horizontalNormalizedPosition = value;
        }

        /// <summary>
        /// Gets or sets the normalized position of the vertical scrollbar (0-1).
        /// </summary>
        public float VerticalNormalizedPosition
        {
            get => _scrollRect.verticalNormalizedPosition;
            set => _scrollRect.verticalNormalizedPosition = value;
        }

        /// <summary>
        /// Creates a new ScrollViewWrapper.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the scroll view</param>
        public ScrollViewWrapper(Transform? parent, string name = "ScrollView")
            : base(parent, name)
        {
            InitializeScrollView();
        }

        /// <summary>
        /// Initializes the scroll view components.
        /// </summary>
        private void InitializeScrollView()
        {
            // Create ScrollRect component
            _scrollRect = GameObject.AddComponent<ScrollRect>();

            // Create Viewport
            var viewportObj = new GameObject("Viewport", typeof(RectTransform), typeof(Image), typeof(Mask));
            viewportObj.transform.SetParent(RectTransform);
            _viewport = viewportObj.GetComponent<RectTransform>();
            _viewport.anchorMin = Vector2.zero;
            _viewport.anchorMax = Vector2.one;
            _viewport.sizeDelta = Vector2.zero;
            _viewport.anchoredPosition = Vector2.zero;

            // Configure Mask
            var mask = viewportObj.GetComponent<Mask>();
            mask.showMaskGraphic = false;
            
            // Configure Viewport Image
            var viewportImage = viewportObj.GetComponent<Image>();
            viewportImage.color = Color.white;

            // Create Content
            var contentObj = new GameObject("Content", typeof(RectTransform));
            contentObj.transform.SetParent(_viewport);
            _content = contentObj.GetComponent<RectTransform>();
            _content.anchorMin = new Vector2(0, 1);
            _content.anchorMax = new Vector2(1, 1);
            _content.pivot = new Vector2(0.5f, 1);
            _content.sizeDelta = new Vector2(0, 0);
            _content.anchoredPosition = Vector2.zero;

            // Setup ScrollRect
            _scrollRect.viewport = _viewport;
            _scrollRect.content = _content;
            _scrollRect.horizontal = false;
            _scrollRect.vertical = true;
            _scrollRect.scrollSensitivity = 20f;
            _scrollRect.elasticity = 0.1f;
            _scrollRect.inertia = true;
            _scrollRect.decelerationRate = 0.135f;
        }

        /// <summary>
        /// Adds an element to the scroll view content.
        /// </summary>
        /// <param name="element">The UI element to add</param>
        public void AddContent(IUIElement element)
        {
            element.SetParent(_content);
        }

        /// <summary>
        /// Clears all content from the scroll view.
        /// </summary>
        public void ClearContent()
        {
            foreach (Transform child in _content)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
} 
