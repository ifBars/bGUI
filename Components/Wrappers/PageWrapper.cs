using bGUI.Core.Abstractions;
using bGUI.Core.Containers;
using UnityEngine;

namespace bGUI.Components
{
    /// <summary>
    /// Wrapper for UI pages.
    /// </summary>
    public class PageWrapper : UIElementBase, IPage
    {
        private readonly string _pageTitle;
        private readonly PanelWrapper _pagePanel;
        private bool _isVisible;

        /// <summary>
        /// Gets the title of the page.
        /// </summary>
        public string PageTitle => _pageTitle;

        /// <summary>
        /// Gets whether the page is currently visible.
        /// </summary>
        public bool IsVisible => _isVisible;
        
        /// <summary>
        /// Gets the main panel of this page.
        /// </summary>
        public PanelWrapper PagePanel => _pagePanel;

        /// <summary>
        /// Initializes a new instance of the PageWrapper class.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="title">The title of the page</param>
        /// <param name="name">The name of the page</param>
        public PageWrapper(Transform parent, string title, string name = "Page") 
            : base(parent, name)
        {
            _pageTitle = title;
            
            // Create the main panel for this page
            _pagePanel = UI.Panel(_rectTransform)
                .SetAnchor(0.5f, 0.5f)
                .SetSize(parent.GetComponent<RectTransform>().rect.width - 40, 
                         parent.GetComponent<RectTransform>().rect.height - 40)
                .SetBackgroundColor(new Color(0.2f, 0.2f, 0.25f, 0.5f))
                .Build();
            
            // Center panel in parent
            _pagePanel.RectTransform.anchoredPosition = Vector2.zero;
            
            // Initially hidden
            _isVisible = false;
            _pagePanel.GameObject.SetActive(false);
        }

        /// <summary>
        /// Shows the page.
        /// </summary>
        public virtual void Show()
        {
            _pagePanel.GameObject.SetActive(true);
            _isVisible = true;
            
            // Reset panel scale and alpha
            CanvasGroup group = GetOrAddCanvasGroup();
            group.alpha = 1f;
            
            _pagePanel.RectTransform.localScale = Vector3.one;
        }

        /// <summary>
        /// Hides the page.
        /// </summary>
        public virtual void Hide()
        {
            _pagePanel.GameObject.SetActive(false);
            _isVisible = false;
        }

        /// <summary>
        /// Starts the transition out animation.
        /// </summary>
        public virtual void StartTransitionOut()
        {
            // Fade out
            CanvasGroup group = GetOrAddCanvasGroup();
            group.alpha = 0f;
        }

        /// <summary>
        /// Starts the transition in animation.
        /// </summary>
        public virtual void StartTransitionIn()
        {
            // Fade in
            CanvasGroup group = GetOrAddCanvasGroup();
            group.alpha = 1f;
        }

        /// <summary>
        /// Updates the page.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Gets or adds a canvas group to the page panel.
        /// </summary>
        protected CanvasGroup GetOrAddCanvasGroup()
        {
            CanvasGroup group = _pagePanel.GameObject.GetComponent<CanvasGroup>();
            if (group == null)
            {
                group = _pagePanel.GameObject.AddComponent<CanvasGroup>();
            }
            return group;
        }
        
        /// <summary>
        /// Creates a section header with the given text.
        /// </summary>
        public TextWrapper CreateSectionHeader(string text, float yPosition)
        {
            return UI.Text(_pagePanel.RectTransform)
                .SetContent(text)
                .SetFontSize(24)
                .SetColor(new Color(0.9f, 0.9f, 0.9f, 1f))
                .SetFontStyle(FontStyle.Bold)
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(30, yPosition)
                .Build();
        }
        
        /// <summary>
        /// Creates a description text.
        /// </summary>
        public TextWrapper CreateDescription(string text, float yPosition)
        {
            var description = UI.Text(_pagePanel.RectTransform)
                .SetContent(text)
                .SetFontSize(18)
                .SetColor(new Color(0.8f, 0.8f, 0.8f, 1f))
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(30, yPosition)
                .Build();
            
            // Ensure text wraps properly
            description.TextComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
            description.TextComponent.verticalOverflow = VerticalWrapMode.Overflow;
            description.RectTransform.sizeDelta = new Vector2(_pagePanel.RectTransform.rect.width - 60, 0);
            
            return description;
        }
    }
} 