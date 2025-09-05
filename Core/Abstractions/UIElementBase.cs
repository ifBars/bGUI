using UnityEngine;

namespace bGUI.Core.Abstractions
{
    /// <summary>
    /// Base class for all UI element wrappers.
    /// </summary>
    public abstract class UIElementBase : IUIElement
    {
        protected GameObject _gameObject;
        protected RectTransform _rectTransform;

        /// <summary>
        /// Gets the underlying GameObject of this UI element.
        /// </summary>
        public GameObject GameObject => _gameObject;

        /// <summary>
        /// Gets the RectTransform of this UI element.
        /// </summary>
        public RectTransform RectTransform => _rectTransform;

        /// <summary>
        /// Gets or sets the name of this UI element.
        /// </summary>
        public string Name
        {
            get => _gameObject.name;
            set => _gameObject.name = value;
        }

        /// <summary>
        /// Gets or sets whether this UI element is active.
        /// </summary>
        public bool IsActive
        {
            get => _gameObject.activeSelf;
            set => _gameObject.SetActive(value);
        }

        /// <summary>
        /// Initializes a new instance of the UIElementBase class.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the UI element</param>
        protected UIElementBase(Transform? parent, string name)
        {
            _gameObject = new GameObject(name);
            _rectTransform = _gameObject.AddComponent<RectTransform>();

            if (parent != null)
            {
                _rectTransform.SetParent(parent, false);
            }

            // Initialize RectTransform properties with default values
            _rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            _rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            _rectTransform.pivot = new Vector2(0.5f, 0.5f);
            _rectTransform.sizeDelta = new Vector2(100f, 100f);
            _rectTransform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Sets the parent of this UI element.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        public void SetParent(Transform? parent)
        {
            if (parent != null)
            {
                _rectTransform.SetParent(parent, false);
                _rectTransform.localPosition = Vector3.zero;
            }
        }

        /// <summary>
        /// Destroys this UI element.
        /// </summary>
        public virtual void Destroy()
        {
            if (_gameObject != null)
            {
                Object.Destroy(_gameObject);
            }
        }

        /// <summary>
        /// Replaces the current GameObject and RectTransform with new ones.
        /// This method properly cleans up the original GameObject to prevent memory leaks.
        /// Should only be used by wrapper classes that need to replace the base GameObject.
        /// </summary>
        /// <param name="newGameObject">The new GameObject to use</param>
        /// <param name="newRectTransform">The new RectTransform to use</param>
        protected void ReplaceGameObject(GameObject newGameObject, RectTransform newRectTransform)
        {
            // Clean up the original GameObject created in the constructor
            if (_gameObject != null && _gameObject != newGameObject)
            {
                Object.Destroy(_gameObject);
            }
            
            _gameObject = newGameObject;
            _rectTransform = newRectTransform;
        }
    }
} 
