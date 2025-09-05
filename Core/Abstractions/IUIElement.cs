using UnityEngine;

namespace bGUI.Core.Abstractions
{
    /// <summary>
    /// Base interface for all UI elements in the bGUI framework.
    /// </summary>
    public interface IUIElement
    {
        /// <summary>
        /// Gets the underlying GameObject of this UI element.
        /// </summary>
        GameObject GameObject { get; }

        /// <summary>
        /// Gets the RectTransform of this UI element.
        /// </summary>
        RectTransform RectTransform { get; }

        /// <summary>
        /// Gets or sets the name of this UI element.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets whether this UI element is active.
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// Sets the parent of this UI element.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        void SetParent(Transform? parent);

        /// <summary>
        /// Destroys this UI element.
        /// </summary>
        void Destroy();
    }
}
