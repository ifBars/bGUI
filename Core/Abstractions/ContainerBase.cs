using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace bGUI.Core.Abstractions
{
    /// <summary>
    /// Abstract base class for container UI elements that can hold other UI elements.
    /// </summary>
    public abstract class ContainerBase : UIElementBase
    {
        protected readonly List<IUIElement> _children = new List<IUIElement>();

        /// <summary>
        /// Gets a read-only collection of child UI elements.
        /// </summary>
        public IReadOnlyList<IUIElement> Children => _children.AsReadOnly();

        /// <summary>
        /// Gets the number of child elements.
        /// </summary>
        public int ChildCount => _children.Count;

        /// <summary>
        /// Initializes a new instance of the ContainerBase class.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="name">The name of the container</param>
        protected ContainerBase(Transform? parent, string name) : base(parent, name)
        {
            // Set default anchor and size for containers to fill parent
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.sizeDelta = Vector2.zero;
            _rectTransform.anchoredPosition = Vector2.zero;
        }

        /// <summary>
        /// Adds a child UI element to this container.
        /// </summary>
        /// <param name="child">The child element to add</param>
        /// <returns>This container for method chaining</returns>
        public virtual ContainerBase AddChild(IUIElement child)
        {
            if (child != null && !_children.Contains(child))
            {
                _children.Add(child);
                child.SetParent(_rectTransform);
                OnChildAdded(child);
            }
            return this;
        }

        /// <summary>
        /// Removes a child UI element from this container.
        /// </summary>
        /// <param name="child">The child element to remove</param>
        /// <returns>This container for method chaining</returns>
        public virtual ContainerBase RemoveChild(IUIElement child)
        {
            if (child != null && _children.Remove(child))
            {
                OnChildRemoved(child);
            }
            return this;
        }

        /// <summary>
        /// Removes all child UI elements from this container.
        /// </summary>
        /// <returns>This container for method chaining</returns>
        public virtual ContainerBase ClearChildren()
        {
            var childrenCopy = new List<IUIElement>(_children);
            foreach (var child in childrenCopy)
            {
                RemoveChild(child);
                child.Destroy();
            }
            _children.Clear();
            return this;
        }

        /// <summary>
        /// Gets a child element by name.
        /// </summary>
        /// <param name="name">The name of the child to find</param>
        /// <returns>The child element if found, null otherwise</returns>
        public virtual IUIElement? GetChild(string name)
        {
            return _children.Find(child => child.Name == name);
        }

        /// <summary>
        /// Gets a child element by index.
        /// </summary>
        /// <param name="index">The index of the child</param>
        /// <returns>The child element at the specified index</returns>
        public virtual IUIElement? GetChild(int index)
        {
            if (index >= 0 && index < _children.Count)
            {
                return _children[index];
            }
            return null;
        }

        /// <summary>
        /// Gets all child elements of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of child elements to get</typeparam>
        /// <returns>A list of child elements of the specified type</returns>
        public virtual List<T> GetChildrenOfType<T>() where T : class, IUIElement
        {
            var result = new List<T>();
            foreach (var child in _children)
            {
                if (child is T typedChild)
                {
                    result.Add(typedChild);
                }
            }
            return result;
        }

        /// <summary>
        /// Sets the layout group for this container.
        /// </summary>
        /// <typeparam name="T">The type of layout group to set</typeparam>
        /// <returns>The layout group component</returns>
        public virtual T SetLayoutGroup<T>() where T : LayoutGroup
        {
            // Remove existing layout groups
            var existingLayouts = _gameObject.GetComponents<LayoutGroup>();
            foreach (var layout in existingLayouts)
            {
                Object.DestroyImmediate(layout);
            }

            // Add new layout group
            return _gameObject.AddComponent<T>();
        }

        /// <summary>
        /// Called when a child element is added. Override in derived classes for custom behavior.
        /// </summary>
        /// <param name="child">The child that was added</param>
        protected virtual void OnChildAdded(IUIElement child)
        {
            // Override in derived classes
        }

        /// <summary>
        /// Called when a child element is removed. Override in derived classes for custom behavior.
        /// </summary>
        /// <param name="child">The child that was removed</param>
        protected virtual void OnChildRemoved(IUIElement child)
        {
            // Override in derived classes
        }

        /// <summary>
        /// Destroys this container and all its children.
        /// </summary>
        public override void Destroy()
        {
            ClearChildren();
            base.Destroy();
        }
    }
} 
