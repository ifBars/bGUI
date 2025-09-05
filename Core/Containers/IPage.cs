using bGUI.Core.Abstractions;

namespace bGUI.Core.Containers
{
    /// <summary>
    /// Interface for page UI elements.
    /// </summary>
    public interface IPage : IUIElement
    {
        /// <summary>
        /// Gets the title of the page.
        /// </summary>
        string PageTitle { get; }
        
        /// <summary>
        /// Gets whether the page is currently visible.
        /// </summary>
        bool IsVisible { get; }
        
        /// <summary>
        /// Shows the page.
        /// </summary>
        void Show();
        
        /// <summary>
        /// Hides the page.
        /// </summary>
        void Hide();
        
        /// <summary>
        /// Starts the transition out animation.
        /// </summary>
        void StartTransitionOut();
        
        /// <summary>
        /// Starts the transition in animation.
        /// </summary>
        void StartTransitionIn();
        
        /// <summary>
        /// Updates the page.
        /// </summary>
        void Update();
    }
} 