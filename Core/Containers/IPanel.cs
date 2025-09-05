using bGUI.Core.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Core.Containers
{
    /// <summary>
    /// Interface for panel UI elements.
    /// </summary>
    public interface IPanel : IUIElement
    {
        /// <summary>
        /// Gets the underlying Image component.
        /// </summary>
        Image ImageComponent { get; }

        /// <summary>
        /// Sets the background color of the panel.
        /// </summary>
        /// <param name="color">The color to set</param>
        void SetBackgroundColor(Color color);

        /// <summary>
        /// Sets the background sprite of the panel.
        /// </summary>
        /// <param name="sprite">The sprite to use as background</param>
        void SetBackgroundImage(Sprite sprite);

        /// <summary>
        /// Sets whether the panel uses a raycast target.
        /// </summary>
        /// <param name="isRaycastTarget">Whether the panel blocks raycasts</param>
        void SetRaycastTarget(bool isRaycastTarget);

        /// <summary>
        /// Adds a layout group component to the panel.
        /// </summary>
        /// <typeparam name="T">The type of layout group to add</typeparam>
        /// <returns>The added layout group component</returns>
        T AddLayoutGroup<T>() where T : LayoutGroup;
    }
}