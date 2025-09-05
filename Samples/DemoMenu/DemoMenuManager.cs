using System;
using bGUI.Components;
using bGUI.Core.Containers;

namespace bGUI.Samples
{
    /// <summary>
    /// Manages the demo menu UI, including page navigation and visibility.
    /// </summary>
    public class DemoMenuManager : IDisposable
    {
        // Menu reference
        private IMenu? _menu;
        
        // Demo pages
        private IPage? _currentPage;
        private IPage? _buttonsPage;
        private IPage? _layoutPage;
        private IPage? _stylesPage;
        private IPage? _animationPage;
        
        // State tracking
        private bool _isMenuVisible = false;
        
        /// <summary>
        /// Initializes a new instance of the DemoMenuManager.
        /// </summary>
        public DemoMenuManager()
        {
            try
            {
                CreateMenu();
                InitializePages();
                
                // Hide the menu initially
                _menu?.Hide();
            }
            catch (Exception ex)
            {
                DemoMenuMod.Logger.Error($"Error initializing DemoMenuManager: {ex}");
            }
        }
        
        /// <summary>
        /// Updates animation and transition states.
        /// </summary>
        public void Update()
        {
            if (!_isMenuVisible) return;
            
            // Update the menu (handles transitions and page updates)
            _menu?.Update();
        }
        
        /// <summary>
        /// Toggles menu visibility.
        /// </summary>
        public void ToggleMenu()
        {
            _menu?.Toggle();
            _isMenuVisible = !_isMenuVisible;
            
            if (_isMenuVisible)
            {
                DemoMenuMod.Logger.Msg("bGUI Showcase menu opened");
            }
            else
            {
                DemoMenuMod.Logger.Msg("bGUI Showcase menu closed");
            }
        }
        
        /// <summary>
        /// Creates the menu with our Canvas and Menu abstractions.
        /// </summary>
        private void CreateMenu()
        {
            // Create the menu
            MenuWrapper menuWrapper = new MenuWrapper("bGUI Framework Showcase");
            _menu = menuWrapper;
            
            DemoMenuMod.Logger.Msg("Created main UI layout");
        }
        
        /// <summary>
        /// Initializes all demo pages and creates navigation buttons.
        /// </summary>
        private void InitializePages()
        {
            // Create pages using the menu's content area
            _buttonsPage = CreateButtonsPage();
            _layoutPage = CreateLayoutPage();
            _stylesPage = CreateStylesPage();
            _animationPage = CreateAnimationPage();
            
            // Add pages to menu
            _menu?.AddPage(_buttonsPage!);
            _menu?.AddPage(_layoutPage!);
            _menu?.AddPage(_stylesPage!);
            _menu?.AddPage(_animationPage!);
            
            // Create navigation buttons
            _menu?.CreateNavigationButton("Buttons", 0, () => _menu!.ShowPage(_buttonsPage!), false);
            _menu?.CreateNavigationButton("Layouts", 1, () => _menu!.ShowPage(_layoutPage!), false);
            _menu?.CreateNavigationButton("Styles", 2, () => _menu!.ShowPage(_stylesPage!), false);
            _menu?.CreateNavigationButton("Animations", 3, () => _menu!.ShowPage(_animationPage!), false);
            _menu?.CreateNavigationButton("Close Menu", 5, () => ToggleMenu(), true);
            
            DemoMenuMod.Logger.Msg("Initialized demo pages");
        }
        
        /// <summary>
        /// Creates the buttons demo page.
        /// </summary>
        private IPage CreateButtonsPage()
        {
            return new ButtonsDemoPage(_menu!.ContentArea.RectTransform);
        }
        
        /// <summary>
        /// Creates the layout demo page.
        /// </summary>
        private IPage CreateLayoutPage()
        {
            return new LayoutDemoPage(_menu!.ContentArea.RectTransform);
        }
        
        /// <summary>
        /// Creates the styles demo page.
        /// </summary>
        private IPage CreateStylesPage()
        {
            return new StylesDemoPage(_menu!.ContentArea.RectTransform);
        }
        
        /// <summary>
        /// Creates the animation demo page.
        /// </summary>
        private IPage CreateAnimationPage()
        {
            return new AnimationDemoPage(_menu!.ContentArea.RectTransform);
        }
        
        /// <summary>
        /// Disposes of resources.
        /// </summary>
        public void Dispose()
        {
            _menu = null;
            _currentPage = null;
            _buttonsPage = null;
            _layoutPage = null;
            _stylesPage = null;
            _animationPage = null;
        }
    }
} 
