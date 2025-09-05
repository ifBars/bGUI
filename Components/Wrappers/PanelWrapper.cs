using bGUI.Core.Abstractions;
using bGUI.Core.Containers;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Components
{
    /// <summary>
    /// Wrapper for Unity's Panel component (GameObject with Image component).
    /// </summary>
    public class PanelWrapper : UIElementBase, IPanel
    {
        private Image _image = null!;

        /// <summary>
        /// Gets the underlying Image component.
        /// </summary>
        public Image ImageComponent => _image;

        /// <summary>
        /// Gets the Transform (same as RectTransform for UI elements).
        /// </summary>
        public Transform Transform => _rectTransform;

        /// <summary>
        /// Initializes a new instance of the PanelWrapper class.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the panel</param>
        public PanelWrapper(Transform? parent, string name = "Panel")
            : base(parent, name)
        {
            // Add Image component
            _image = _gameObject.AddComponent<Image>();
            _image.color = new Color(1f, 1f, 1f, 0.8f);

            // Set RectTransform to stretch to fill parent by default
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.sizeDelta = Vector2.zero;
            _rectTransform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Sets the background color of the panel.
        /// </summary>
        /// <param name="color">The color to set</param>
        public void SetBackgroundColor(Color color)
        {
            _image.color = color;
        }

        /// <summary>
        /// Sets the background sprite of the panel.
        /// </summary>
        /// <param name="sprite">The sprite to use as background</param>
        public void SetBackgroundImage(Sprite sprite)
        {
            _image.sprite = sprite;
            _image.type = sprite != null ? Image.Type.Sliced : Image.Type.Simple;
        }

        /// <summary>
        /// Sets whether the panel uses a raycast target.
        /// </summary>
        /// <param name="isRaycastTarget">Whether the panel blocks raycasts</param>
        public void SetRaycastTarget(bool isRaycastTarget)
        {
            _image.raycastTarget = isRaycastTarget;
        }

        /// <summary>
        /// Adds a layout group component to the panel.
        /// </summary>
        /// <typeparam name="T">The type of layout group to add</typeparam>
        /// <returns>The added layout group component</returns>
        public T AddLayoutGroup<T>() where T : LayoutGroup
        {
            // Remove any existing layout groups
            var existingLayoutGroups = _gameObject.GetComponents<LayoutGroup>();
            foreach (var layoutGroupToDestroy in existingLayoutGroups)
            {
                Object.Destroy(layoutGroupToDestroy);
            }

            // Add the new layout group
            var layoutGroup = _gameObject.AddComponent<T>();

            // Configure common settings based on the layout group type
            if (layoutGroup is HorizontalLayoutGroup horizontalLayout)
            {
                horizontalLayout.childAlignment = TextAnchor.MiddleCenter;
                horizontalLayout.childForceExpandWidth = false;
                horizontalLayout.childForceExpandHeight = false;
                horizontalLayout.spacing = 5f;
                horizontalLayout.padding = new RectOffset(10, 10, 10, 10);
            }
            else if (layoutGroup is VerticalLayoutGroup verticalLayout)
            {
                verticalLayout.childAlignment = TextAnchor.MiddleCenter;
                verticalLayout.childForceExpandWidth = false;
                verticalLayout.childForceExpandHeight = false;
                verticalLayout.spacing = 5f;
                verticalLayout.padding = new RectOffset(10, 10, 10, 10);
            }
            else if (layoutGroup is GridLayoutGroup gridLayout)
            {
                gridLayout.childAlignment = TextAnchor.MiddleCenter;
                gridLayout.cellSize = new Vector2(100f, 100f);
                gridLayout.spacing = new Vector2(5f, 5f);
                gridLayout.padding = new RectOffset(10, 10, 10, 10);
            }

            return layoutGroup;
        }

        /// <summary>
        /// Sets the anchor for this panel.
        /// </summary>
        /// <param name="anchorX">The X anchor (0-1)</param>
        /// <param name="anchorY">The Y anchor (0-1)</param>
        /// <returns>This PanelWrapper for method chaining</returns>
        public PanelWrapper SetAnchor(float anchorX, float anchorY)
        {
            _rectTransform.anchorMin = _rectTransform.anchorMax = new Vector2(anchorX, anchorY);
            return this;
        }

        /// <summary>
        /// Sets the size of this panel.
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <returns>This PanelWrapper for method chaining</returns>
        public PanelWrapper SetSize(float width, float height)
        {
            _rectTransform.sizeDelta = new Vector2(width, height);
            return this;
        }

        /// <summary>
        /// Sets the position of this panel.
        /// </summary>
        /// <param name="x">The X position</param>
        /// <param name="y">The Y position</param>
        /// <returns>This PanelWrapper for method chaining</returns>
        public PanelWrapper SetPosition(float x, float y)
        {
            if (_rectTransform != null)
            {
                _rectTransform.anchoredPosition = new Vector2(x, y);
            }
            return this;
        }

        /// <summary>
        /// Gets whether this PanelWrapper is still valid (components exist).
        /// </summary>
        public bool IsValid => _image != null && _gameObject != null && _rectTransform != null;
    }
}
