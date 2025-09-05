using System;
using bGUI.Core.Abstractions;
using bGUI.Core.Enums;

namespace bGUI.Core.Events
{
    /// <summary>
    /// Base class for UI event arguments.
    /// </summary>
    public abstract class UIEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the UI element that triggered the event.
        /// </summary>
        public IUIElement Source { get; }

        /// <summary>
        /// Gets the timestamp when the event occurred.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Gets or sets whether the event has been handled.
        /// </summary>
        public bool Handled { get; set; }

        /// <summary>
        /// Initializes a new instance of the UIEventArgs class.
        /// </summary>
        /// <param name="source">The UI element that triggered the event</param>
        protected UIEventArgs(IUIElement source)
        {
            Source = source;
            Timestamp = DateTime.UtcNow;
            Handled = false;
        }
    }

    /// <summary>
    /// Event arguments for UI component interaction events.
    /// </summary>
    public class ComponentEventArgs : UIEventArgs
    {
        /// <summary>
        /// Gets the type of interaction that occurred.
        /// </summary>
        public InteractionType InteractionType { get; }

        /// <summary>
        /// Gets additional data associated with the interaction.
        /// </summary>
        public object? Data { get; }

        /// <summary>
        /// Initializes a new instance of the ComponentEventArgs class.
        /// </summary>
        /// <param name="source">The UI element that triggered the event</param>
        /// <param name="interactionType">The type of interaction</param>
        /// <param name="data">Additional data associated with the interaction</param>
        public ComponentEventArgs(IUIElement source, InteractionType interactionType, object? data = null)
            : base(source)
        {
            InteractionType = interactionType;
            Data = data;
        }
    }

    /// <summary>
    /// Event arguments for layout-related events.
    /// </summary>
    public class LayoutEventArgs : UIEventArgs
    {
        /// <summary>
        /// Gets the type of layout change that occurred.
        /// </summary>
        public LayoutChangeType ChangeType { get; }

        /// <summary>
        /// Gets the previous state before the layout change.
        /// </summary>
        public object? PreviousState { get; }

        /// <summary>
        /// Gets the new state after the layout change.
        /// </summary>
        public object? NewState { get; }

        /// <summary>
        /// Initializes a new instance of the LayoutEventArgs class.
        /// </summary>
        /// <param name="source">The UI element that triggered the event</param>
        /// <param name="changeType">The type of layout change</param>
        /// <param name="previousState">The previous state</param>
        /// <param name="newState">The new state</param>
        public LayoutEventArgs(IUIElement source, LayoutChangeType changeType, object? previousState = null, object? newState = null)
            : base(source)
        {
            ChangeType = changeType;
            PreviousState = previousState;
            NewState = newState;
        }
    }

    /// <summary>
    /// Event arguments for navigation events.
    /// </summary>
    public class NavigationEventArgs : UIEventArgs
    {
        /// <summary>
        /// Gets the navigation direction.
        /// </summary>
        public NavigationDirection Direction { get; }

        /// <summary>
        /// Gets the target UI element for navigation.
        /// </summary>
        public IUIElement? Target { get; }

        /// <summary>
        /// Initializes a new instance of the NavigationEventArgs class.
        /// </summary>
        /// <param name="source">The UI element that triggered the event</param>
        /// <param name="direction">The navigation direction</param>
        /// <param name="target">The target UI element</param>
        public NavigationEventArgs(IUIElement source, NavigationDirection direction, IUIElement? target = null)
            : base(source)
        {
            Direction = direction;
            Target = target;
        }
    }
} 
