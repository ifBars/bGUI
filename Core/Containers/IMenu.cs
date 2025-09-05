using bGUI.Core.Abstractions;
using System;
using bGUI.Components;

namespace bGUI.Core.Containers
{
    /// <summary>
    /// Interface for menu UI elements.
    /// </summary>
    public interface IMenu : IUIElement
    {
        /// <summary>
        /// Gets the current page of the menu.
        /// </summary>
        IPage? CurrentPage { get; }
        
        /// <summary>
        /// Gets the content area panel for pages.
        /// </summary>
        PanelWrapper ContentArea { get; }
        
        /// <summary>
        /// Shows the menu.
        /// </summary>
        void Show();
        
        /// <summary>
        /// Hides the menu.
        /// </summary>
        void Hide();
        
        /// <summary>
        /// Toggles the menu visibility.
        /// </summary>
        void Toggle();
        
        /// <summary>
        /// Updates the menu.
        /// </summary>
        void Update();
        
        /// <summary>
        /// Adds a page to the menu.
        /// </summary>
        /// <param name="page">The page to add</param>
        void AddPage(IPage page);
        
        /// <summary>
        /// Shows a specific page and hides all others.
        /// </summary>
        /// <param name="page">The page to show</param>
        void ShowPage(IPage page);
        
        /// <summary>
        /// Shows a page by its index.
        /// </summary>
        /// <param name="pageIndex">The index of the page</param>
        void ShowPageByIndex(int pageIndex);
        
        /// <summary>
        /// Creates a navigation button.
        /// </summary>
        /// <param name="text">The button text</param>
        /// <param name="position">The position index</param>
        /// <param name="onClick">The click action</param>
        /// <param name="isExit">Whether it's an exit button</param>
        /// <returns>The created button</returns>
        ButtonWrapper CreateNavigationButton(string text, int position, Action onClick, bool isExit = false);
    }
} 
