using UnityEngine;
using bGUI.Core.Enums;
using bGUI.Core.Extensions;
using bGUI.Core.Constants;

namespace bGUI.Core.Abstractions
{
    /// <summary>
    /// Base class for fluent builders that provides common UI building functionality.
    /// </summary>
    /// <typeparam name="TBuilder">The type of the builder (for fluent chaining)</typeparam>
    /// <typeparam name="TElement">The type of UI element being built</typeparam>
    public abstract class FluentBuilderBase<TBuilder, TElement> 
        where TBuilder : FluentBuilderBase<TBuilder, TElement>
        where TElement : class, IUIElement
    {
        protected readonly TElement _element;

        /// <summary>
        /// Gets the UI element being built.
        /// </summary>
        protected TElement Element => _element;

        /// <summary>
        /// Initializes a new instance of the FluentBuilderBase class.
        /// </summary>
        /// <param name="element">The UI element to build</param>
        protected FluentBuilderBase(TElement element)
        {
            _element = element;
        }

        /// <summary>
        /// Sets the name of the UI element.
        /// </summary>
        /// <param name="name">The name to set</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder SetName(string name)
        {
            _element.Name = name;
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets whether the UI element is active.
        /// </summary>
        /// <param name="active">Whether the element should be active</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder SetActive(bool active)
        {
            _element.IsActive = active;
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the size of the UI element.
        /// </summary>
        /// <param name="width">The width to set</param>
        /// <param name="height">The height to set</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder SetSize(float width, float height)
        {
            _element.RectTransform.SetSize(width, height);
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the size of the UI element.
        /// </summary>
        /// <param name="size">The size to set</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder SetSize(Vector2 size)
        {
            _element.RectTransform.SetSize(size);
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the position of the UI element.
        /// </summary>
        /// <param name="x">The x position</param>
        /// <param name="y">The y position</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder SetPosition(float x, float y)
        {
            _element.RectTransform.SetPosition(x, y);
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the position of the UI element.
        /// </summary>
        /// <param name="position">The position to set</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder SetPosition(Vector2 position)
        {
            _element.RectTransform.SetPosition(position);
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the anchor preset for the UI element.
        /// </summary>
        /// <param name="preset">The anchor preset to apply</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder SetAnchorPreset(AnchorPreset preset)
        {
            _element.RectTransform.SetAnchorPreset(preset);
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the anchors of the UI element.
        /// </summary>
        /// <param name="anchorMin">The minimum anchor</param>
        /// <param name="anchorMax">The maximum anchor</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder SetAnchors(Vector2 anchorMin, Vector2 anchorMax)
        {
            _element.RectTransform.SetAnchors(anchorMin, anchorMax);
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the pivot of the UI element.
        /// </summary>
        /// <param name="pivot">The pivot to set</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder SetPivot(Vector2 pivot)
        {
            _element.RectTransform.SetPivot(pivot);
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the pivot of the UI element.
        /// </summary>
        /// <param name="x">The x pivot</param>
        /// <param name="y">The y pivot</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder SetPivot(float x, float y)
        {
            _element.RectTransform.SetPivot(x, y);
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the UI element to fill its parent.
        /// </summary>
        /// <param name="padding">Optional uniform padding</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder FillParent(float padding = 0f)
        {
            _element.RectTransform.FillParent(padding);
            return (TBuilder)this;
        }

        /// <summary>
        /// Centers the UI element in its parent.
        /// </summary>
        /// <returns>This builder for method chaining</returns>
        public TBuilder CenterInParent()
        {
            _element.RectTransform.CenterInParent();
            return (TBuilder)this;
        }

        /// <summary>
        /// Docks the UI element to a specific side of its parent.
        /// </summary>
        /// <param name="side">The side to dock to</param>
        /// <param name="size">The size along the docking axis</param>
        /// <param name="margin">Optional margin from edges</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder Dock(DockSide side, float size, float margin = 0f)
        {
            _element.Dock(side, size, margin);
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the alpha of the UI element.
        /// </summary>
        /// <param name="alpha">The alpha value (0-1)</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder SetAlpha(float alpha)
        {
            _element.SetAlpha(alpha);
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets whether the UI element is interactable.
        /// </summary>
        /// <param name="interactable">Whether the element is interactable</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder SetInteractable(bool interactable)
        {
            _element.SetInteractable(interactable);
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets whether the UI element blocks raycasts.
        /// </summary>
        /// <param name="blocksRaycasts">Whether the element blocks raycasts</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder SetBlocksRaycasts(bool blocksRaycasts)
        {
            _element.SetBlocksRaycasts(blocksRaycasts);
            return (TBuilder)this;
        }

        /// <summary>
        /// Applies a fade in animation to the element when it's built.
        /// </summary>
        /// <param name="duration">The duration of the fade</param>
        /// <param name="onComplete">Optional callback when fade is complete</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder WithFadeIn(float duration = UIConstants.Animations.FadeInDuration, System.Action? onComplete = null)
        {
            // Store the fade settings to apply after build
            _fadeInSettings = new FadeSettings { Duration = duration, OnComplete = onComplete };
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the parent of the UI element.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder SetParent(Transform? parent)
        {
            _element.SetParent(parent);
            return (TBuilder)this;
        }

        /// <summary>
        /// Applies a style preset to the UI element.
        /// </summary>
        /// <param name="styleAction">Action to apply style customizations</param>
        /// <returns>This builder for method chaining</returns>
        public TBuilder WithStyle(System.Action<TElement> styleAction)
        {
            styleAction?.Invoke(_element);
            return (TBuilder)this;
        }

        /// <summary>
        /// Builds and returns the UI element.
        /// </summary>
        /// <returns>The built UI element</returns>
        public virtual TElement Build()
        {
            // Apply post-build settings
            if (_fadeInSettings.HasValue)
            {
                var settings = _fadeInSettings.Value;
                _element.FadeIn(settings.Duration, settings.OnComplete);
            }

            return _element;
        }

        /// <summary>
        /// Builds the UI element and configures it with the provided action.
        /// </summary>
        /// <param name="configAction">Action to configure the built element</param>
        /// <returns>The built UI element</returns>
        public virtual TElement Build(System.Action<TElement> configAction)
        {
            var builtElement = Build();
            configAction?.Invoke(builtElement);
            return builtElement;
        }

        // Private fields for storing build-time settings
        private FadeSettings? _fadeInSettings;

        private struct FadeSettings
        {
            public float Duration;
            public System.Action? OnComplete;
        }
    }
} 
