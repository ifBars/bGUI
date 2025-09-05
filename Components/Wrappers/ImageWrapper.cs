using bGUI.Core.Abstractions;
using bGUI.Core.Components;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Components
{
    /// <summary>
    /// Wrapper for Unity's Image component.
    /// </summary>
    public class ImageWrapper : UIElementBase, IImage
    {
        private Image _image;

        /// <summary>
        /// Gets the underlying Image component.
        /// </summary>
        public Image ImageComponent => _image;

        /// <summary>
        /// Gets or sets the sprite displayed by this image.
        /// </summary>
        public Sprite? Sprite
        {
            get => _image.sprite;
            set => _image.sprite = value;
        }

        /// <summary>
        /// Gets or sets the color of this image.
        /// </summary>
        public Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }

        /// <summary>
        /// Gets or sets the material used by this image.
        /// </summary>
        public Material Material
        {
            get => _image.material;
            set => _image.material = value;
        }

        /// <summary>
        /// Gets or sets whether this image is a raycast target.
        /// </summary>
        public bool RaycastTarget
        {
            get => _image.raycastTarget;
            set => _image.raycastTarget = value;
        }

        /// <summary>
        /// Gets or sets the image type.
        /// </summary>
        public Image.Type ImageType
        {
            get => _image.type;
            set => _image.type = value;
        }

        /// <summary>
        /// Initializes a new instance of the ImageWrapper class.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the image</param>
        /// <param name="sprite">The sprite to display</param>
        public ImageWrapper(Transform? parent, string name = "Image", Sprite? sprite = null)
            : base(parent, name)
        {
            // Add Image component
            _image = _gameObject.AddComponent<Image>();
            _image.sprite = sprite;
            _image.raycastTarget = false;

            // Set RectTransform properties
            _rectTransform.sizeDelta = new Vector2(100f, 100f);

            // If sprite is assigned, use its size
            if (sprite != null)
            {
                _rectTransform.sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);

                // Check if the sprite is sliced
                if (sprite.border.sqrMagnitude > 0)
                {
                    _image.type = Image.Type.Sliced;
                }
            }
        }

        /// <summary>
        /// Sets the fill method and amount for filled images.
        /// </summary>
        /// <param name="fillMethod">The fill method to use</param>
        /// <param name="fillAmount">The fill amount (0-1)</param>
        /// <param name="clockwise">Whether to fill clockwise</param>
        public void SetFillSettings(Image.FillMethod fillMethod, float fillAmount, bool clockwise)
        {
            _image.type = Image.Type.Filled;
            _image.fillMethod = fillMethod;
            _image.fillAmount = Mathf.Clamp01(fillAmount);
            _image.fillClockwise = clockwise;
        }

        /// <summary>
        /// Sets the image to preserve aspect ratio.
        /// </summary>
        /// <param name="preserve">Whether to preserve the aspect ratio</param>
        public void SetPreserveAspect(bool preserve)
        {
            _image.preserveAspect = preserve;
        }

        /// <summary>
        /// Sets the image as a mask.
        /// </summary>
        /// <param name="showMaskGraphic">Whether to show the mask graphic</param>
        public void SetAsMask(bool showMaskGraphic)
        {
            var mask = _gameObject.GetComponent<Mask>() ?? _gameObject.AddComponent<Mask>();
            mask.showMaskGraphic = showMaskGraphic;
        }
    }
}
