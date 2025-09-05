using UnityEngine;
using bGUI.Core.Factory;
using bGUI.Core.Components;

namespace bGUI.Components
{
    /// <summary>
    /// Fluent builder for bGUI image elements.
    /// </summary>
    public class ImageBuilder
    {
        private readonly IImage _image;
        private readonly bool _usePooling;
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageBuilder"/>.
        /// </summary>
        /// <param name="parent">Parent transform for the image.</param>
        /// <param name="usePooling">Whether to use the internal object pool.</param>
        public ImageBuilder(Transform? parent, bool usePooling = false)
        {
            _usePooling = usePooling;
            _image = UIFactory.Instance.CreateImage(parent, "Image", null, _usePooling);
        }

        // Backward compatibility constructor
        public ImageBuilder(Transform? parent)
        {
            _usePooling = false;
            _image = UIFactory.Instance.CreateImage(parent, "Image", null, _usePooling);
        }

        /// <summary>
        /// Sets the sprite for the image.
        /// </summary>
        /// <param name="sprite">Sprite to display.</param>
        /// <returns>This builder for chaining.</returns>
        public ImageBuilder SetSprite(Sprite sprite)
        {
            _image.Sprite = sprite;
            return this;
        }

        /// <summary>
        /// Sets the tint color for the image.
        /// </summary>
        /// <param name="color">Tint color.</param>
        /// <returns>This builder for chaining.</returns>
        public ImageBuilder SetColor(Color color)
        {
            _image.Color = color;
            return this;
        }

        /// <summary>
        /// Sets the size of the image in pixels.
        /// </summary>
        /// <param name="width">Width in pixels.</param>
        /// <param name="height">Height in pixels.</param>
        /// <returns>This builder for chaining.</returns>
        public ImageBuilder SetSize(float width, float height)
        {
            _image.RectTransform.sizeDelta = new Vector2(width, height);
            return this;
        }

        /// <summary>
        /// Sets the anchor point of the image.
        /// </summary>
        /// <param name="anchorX">Anchor X (0-1).</param>
        /// <param name="anchorY">Anchor Y (0-1).</param>
        /// <returns>This builder for chaining.</returns>
        public ImageBuilder SetAnchor(float anchorX, float anchorY)
        {
            _image.RectTransform.anchorMin = _image.RectTransform.anchorMax = new Vector2(anchorX, anchorY);
            _image.RectTransform.anchoredPosition = Vector2.zero;
            return this;
        }

        /// <summary>
        /// Sets whether the image is a raycast target.
        /// </summary>
        /// <param name="value">True to receive raycasts.</param>
        /// <returns>This builder for chaining.</returns>
        public ImageBuilder SetRaycastTarget(bool value)
        {
            _image.RaycastTarget = value;
            return this;
        }
        /// <summary>
        /// Builds and returns the configured image wrapper.
        /// </summary>
        /// <returns>The created <see cref="ImageWrapper"/>.</returns>
        public ImageWrapper Build() => (ImageWrapper)_image;
    }
} 
