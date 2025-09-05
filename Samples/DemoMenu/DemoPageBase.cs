using UnityEngine;
using bGUI.Components;
using bGUI.Core.Containers;

namespace bGUI.Samples
{
    /// <summary>
    /// Abstract base class for all demo pages.
    /// </summary>
    public abstract class DemoPageBase : IPage
    {
        /// <summary>
        /// Gets the title of the page to display in the header.
        /// </summary>
        public abstract string PageTitle { get; }
        
        /// <summary>
        /// The parent transform for this page.
        /// </summary>
        protected RectTransform ParentTransform { get; private set; }
        
        /// <summary>
        /// The main panel for this page.
        /// </summary>
        protected PanelWrapper PagePanel { get; private set; }
        
        /// <summary>
        /// Whether the page is currently visible.
        /// </summary>
        public bool IsVisible { get; protected set; }
        
        /// <summary>
        /// Gets the underlying GameObject of this UI element.
        /// </summary>
        public GameObject GameObject => PagePanel.GameObject;
        
        /// <summary>
        /// Gets the RectTransform of this UI element.
        /// </summary>
        public RectTransform RectTransform => PagePanel.RectTransform;
        
        /// <summary>
        /// Gets or sets the name of this UI element.
        /// </summary>
        public string Name
        {
            get => PagePanel.Name;
            set => PagePanel.Name = value;
        }
        
        /// <summary>
        /// Gets or sets whether this UI element is active.
        /// </summary>
        public bool IsActive
        {
            get => PagePanel.IsActive;
            set => PagePanel.IsActive = value;
        }
        
        /// <summary>
        /// Initializes a new demo page.
        /// </summary>
        /// <param name="parent">The parent transform.</param>
        protected DemoPageBase(RectTransform parent)
        {
            ParentTransform = parent;
            
            // Create the main panel for this page
            PagePanel = UI.Panel(parent)
                .SetAnchor(0.5f, 0.5f)
                .SetSize(parent.rect.width - 40, parent.rect.height - 40)
                .SetBackgroundColor(new Color(0.2f, 0.2f, 0.25f, 0.5f))
                .Build();
            
            // Center panel in parent
            PagePanel.RectTransform.anchoredPosition = Vector2.zero;
            
            // Create page content
            SetupUI();
            
            // Initially hidden
            IsVisible = false;
            PagePanel.GameObject.SetActive(false);
        }
        
        /// <summary>
        /// Sets up the UI elements for this page.
        /// </summary>
        protected abstract void SetupUI();
        
        /// <summary>
        /// Updates this page.
        /// </summary>
        public virtual void Update() { }
        
        /// <summary>
        /// Shows this page.
        /// </summary>
        public virtual void Show()
        {
            PagePanel.GameObject.SetActive(true);
            IsVisible = true;
            
            // Reset panel scale and alpha
            CanvasGroup group = GetOrAddCanvasGroup();
            group.alpha = 1f;
            
            PagePanel.RectTransform.localScale = Vector3.one;
        }
        
        /// <summary>
        /// Hides this page.
        /// </summary>
        public virtual void Hide()
        {
            PagePanel.GameObject.SetActive(false);
            IsVisible = false;
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
        /// Gets or adds a canvas group to the page panel.
        /// </summary>
        protected CanvasGroup GetOrAddCanvasGroup()
        {
            CanvasGroup group = PagePanel.GameObject.GetComponent<CanvasGroup>();
            if (group == null)
            {
                group = PagePanel.GameObject.AddComponent<CanvasGroup>();
            }
            return group;
        }
        
        /// <summary>
        /// Creates a section header with the given text.
        /// </summary>
        protected TextWrapper CreateSectionHeader(string text, float yPosition)
        {
            return UI.Text(PagePanel.RectTransform)
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
        protected TextWrapper CreateDescription(string text, float yPosition)
        {
            var description = UI.Text(PagePanel.RectTransform)
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
            description.RectTransform.sizeDelta = new Vector2(PagePanel.RectTransform.rect.width - 60, 0);
            
            return description;
        }
        
        /// <summary>
        /// Sets the parent of this UI element.
        /// </summary>
        public void SetParent(Transform? parent)
        {
            PagePanel.SetParent(parent);
            if (parent is RectTransform rt)
            {
                ParentTransform = rt;
            }
        }
        
        /// <summary>
        /// Destroys this UI element.
        /// </summary>
        public void Destroy()
        {
            PagePanel.Destroy();
        }
    }
} 
