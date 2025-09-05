using System;
using System.Collections.Generic;
using bGUI.Core.Abstractions;
using bGUI.Core.Containers;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Components
{
    /// <summary>
    /// Wrapper for UI menus.
    /// </summary>
    public class MenuWrapper : UIElementBase, IMenu
    {
        private readonly CanvasWrapper _canvas;
        private readonly PanelWrapper _mainContainer;
        private readonly PanelWrapper _header;
        private readonly TextWrapper _titleText;
        private readonly PanelWrapper _sidebar;
        private readonly PanelWrapper _contentArea;
        
        private IPage? _currentPage;
        private List<IPage> _pages = new List<IPage>();
        private bool _isMenuVisible = false;
        private readonly string _menuName;

        /// <summary>
        /// Gets the current page of the menu.
        /// </summary>
        public IPage? CurrentPage => _currentPage;
        
        /// <summary>
        /// Gets the content area panel for pages.
        /// </summary>
        public PanelWrapper ContentArea => _contentArea;
        
        /// <summary>
        /// Gets the header panel.
        /// </summary>
        public PanelWrapper Header => _header;
        
        /// <summary>
        /// Gets the sidebar panel.
        /// </summary>
        public PanelWrapper Sidebar => _sidebar;
        
        /// <summary>
        /// Gets the main container panel.
        /// </summary>
        public PanelWrapper MainContainer => _mainContainer;
        
        /// <summary>
        /// Gets the canvas wrapper.
        /// </summary>
        public CanvasWrapper Canvas => _canvas;

        /// <summary>
        /// Initializes a new instance of the MenuWrapper class.
        /// </summary>
        /// <param name="name">The name of the menu</param>
        public MenuWrapper(string name = "Menu") 
            : base(null, name)
        {
            _menuName = name;
            
            // Create canvas
            _canvas = new CanvasWrapper($"{name}Canvas");
            _canvas.SetSortingOrder(10000);
            
            // Create main container
            _mainContainer = UI.Panel(_canvas.RectTransform)
                .SetAnchor(0.5f, 0.5f)
                .SetSize(1600, 900)
                .SetBackgroundColor(new Color(0.12f, 0.12f, 0.15f, 0.95f))
                .Build();
            
            // Replace the base class GameObject with the main container's GameObject
            ReplaceGameObject(_mainContainer.GameObject, _mainContainer.RectTransform);
            
            // Create header
            _header = UI.Panel(_mainContainer.RectTransform)
                .SetAnchor(0.5f, 1.0f)
                .SetPivot(0.5f, 1.0f)
                .SetSize(1600, 80)
                .SetBackgroundColor(new Color(0.1f, 0.1f, 0.1f, 1.0f))
                .Build();
            
            // Create title text
            _titleText = UI.Text(_header.RectTransform)
                .SetContent("Menu")
                .SetFontSize(36)
                .SetColor(new Color(0.9f, 0.9f, 0.9f, 1f))
                .SetFontStyle(FontStyle.Bold)
                .SetAnchor(0.5f, 0.5f)
                .Build();
            
            // Create sidebar
            _sidebar = UI.Panel(_mainContainer.RectTransform)
                .SetAnchor(0f, 0.5f)
                .SetPivot(0f, 0.5f)
                .SetSize(300, 820)
                .SetBackgroundColor(new Color(0.15f, 0.15f, 0.18f, 1f))
                .Build();
            
            // Position sidebar below header
            _sidebar.RectTransform.anchoredPosition = new Vector2(0, -40);
            
            // Create content area
            _contentArea = UI.Panel(_mainContainer.RectTransform)
                .SetAnchor(1f, 0.5f)
                .SetPivot(1f, 0.5f)
                .SetSize(1300, 820)
                .SetBackgroundColor(new Color(0.18f, 0.18f, 0.22f, 0.8f))
                .Build();
            
            // Position content area below header
            _contentArea.RectTransform.anchoredPosition = new Vector2(0, -40);
            
            // Hide the menu initially
            _canvas.IsActive = false;
        }

        /// <summary>
        /// Shows the menu.
        /// </summary>
        public void Show()
        {
            _canvas.IsActive = true;
            _isMenuVisible = true;
            
            // Default to first page if no page is active
            if (_currentPage == null && _pages.Count > 0)
            {
                ShowPage(_pages[0]);
            }
        }

        /// <summary>
        /// Hides the menu.
        /// </summary>
        public void Hide()
        {
            _canvas.IsActive = false;
            _isMenuVisible = false;
        }

        /// <summary>
        /// Toggles the menu visibility.
        /// </summary>
        public void Toggle()
        {
            if (_isMenuVisible)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        /// <summary>
        /// Updates the menu.
        /// </summary>
        public void Update()
        {
            if (!_isMenuVisible) return;
            
            // Update current page
            _currentPage?.Update();
        }

        /// <summary>
        /// Adds a page to the menu.
        /// </summary>
        /// <param name="page">The page to add</param>
        public void AddPage(IPage page)
        {
            if (page != null && !_pages.Contains(page))
            {
                _pages.Add(page);
                page.Hide();
            }
        }

        /// <summary>
        /// Shows a specific page and hides all others.
        /// </summary>
        /// <param name="page">The page to show</param>
        public void ShowPage(IPage page)
        {
            if (_currentPage != null)
            {
                _currentPage.Hide();
            }
            
            page.Show();
            _currentPage = page;
            
            // Update title using the stored menu name instead of GameObject.name
            _titleText.Content = $"{_menuName}: {page.PageTitle}";
        }

        /// <summary>
        /// Shows a page by its index.
        /// </summary>
        /// <param name="pageIndex">The index of the page</param>
        public void ShowPageByIndex(int pageIndex)
        {
            if (pageIndex >= 0 && pageIndex < _pages.Count)
            {
                ShowPage(_pages[pageIndex]);
            }
        }

        /// <summary>
        /// Creates a navigation button.
        /// </summary>
        /// <param name="text">The button text</param>
        /// <param name="position">The position index</param>
        /// <param name="onClick">The click action</param>
        /// <param name="isExit">Whether it's an exit button</param>
        /// <returns>The created button</returns>
        public ButtonWrapper CreateNavigationButton(string text, int position, Action onClick, bool isExit = false)
        {
            Color normalColor = isExit 
                ? new Color(0.8f, 0.2f, 0.2f, 1f) 
                : new Color(0.2f, 0.4f, 0.8f, 1f);
            
            Color hoverColor = isExit 
                ? new Color(0.9f, 0.3f, 0.3f, 1f) 
                : new Color(0.3f, 0.5f, 0.9f, 1f);
            
            Color pressedColor = isExit 
                ? new Color(0.7f, 0.15f, 0.15f, 1f) 
                : new Color(0.15f, 0.3f, 0.7f, 1f);
            
            var button = UI.Button(_sidebar.RectTransform)
                .SetText(text)
                .SetSize(260, 60)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetColors(normalColor, hoverColor, pressedColor, Color.gray)
                .OnClick(onClick)
                .Build();
            
            // Position based on index (starting from top with spacing)
            float yPos = -120 - (position * 70);
            button.RectTransform.anchoredPosition = new Vector2(0, yPos);
            
            // Style the text
            var buttonText = button.ButtonComponent.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.fontSize = 24;
                buttonText.fontStyle = FontStyle.Bold;
            }
            
            return button;
        }
    }
} 
