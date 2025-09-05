using bGUI.Core.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Core.Components
{
    /// <summary>
    /// Interface for image UI elements.
    /// </summary>
    public interface IImage : IUIElement
    {
        /// <summary>
        /// Gets the underlying Image component.
        /// </summary>
        Image ImageComponent { get; }

        /// <summary>
        /// Gets or sets the sprite displayed by this image.
        /// </summary>
        Sprite? Sprite { get; set; }

        /// <summary>
        /// Gets or sets the color of this image.
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// Gets or sets the material used by this image.
        /// </summary>
        Material Material { get; set; }

        /// <summary>
        /// Gets or sets whether this image is a raycast target.
        /// </summary>
        bool RaycastTarget { get; set; }

        /// <summary>
        /// Gets or sets the image type.
        /// </summary>
        Image.Type ImageType { get; set; }

        /// <summary>
        /// Sets the fill method and amount for filled images.
        /// </summary>
        /// <param name="fillMethod">The fill method to use</param>
        /// <param name="fillAmount">The fill amount (0-1)</param>
        /// <param name="clockwise">Whether to fill clockwise</param>
        void SetFillSettings(Image.FillMethod fillMethod, float fillAmount, bool clockwise);
    }
}
