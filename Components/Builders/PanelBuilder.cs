using bGUI.Components.Builders.Layout;
using bGUI.Utilities;
using UnityEngine;
using UnityEngine.UI;
using bGUI.Core.Extensions;
using bGUI.Core.Factory;
using bGUI.Core.Containers;

namespace bGUI.Components
{
    /// <summary>
    /// Fluent builder for bGUI panels.
    /// </summary>
    public class PanelBuilder
    {
        private readonly IPanel _panel;
        private readonly bool _usePooling;
        private int? _cornerRadius;
        private int _borderSize = 10;
        /// <summary>
        /// Initializes a new instance of the <see cref="PanelBuilder"/>.
        /// </summary>
        /// <param name="parent">Parent transform for the panel.</param>
        /// <param name="usePooling">Whether to use the internal object pool.</param>
        public PanelBuilder(Transform? parent, bool usePooling = false)
        {
            _usePooling = usePooling;
            _panel = UIFactory.Instance.CreatePanel(parent, "Panel", _usePooling);
        }

        // Backward compatibility constructor
        public PanelBuilder(Transform? parent)
        {
            _usePooling = false;
            _panel = UIFactory.Instance.CreatePanel(parent, "Panel", _usePooling);
        }

        /// <summary>
        /// Sets the panel's background color.
        /// </summary>
        /// <param name="color">Color to apply.</param>
        /// <returns>This builder for chaining.</returns>
        public PanelBuilder SetBackgroundColor(Color color)
        {
            (_panel as PanelWrapper)?.SetBackgroundColor(color);
            return this;
        }

        /// <summary>
        /// Sets the panel's background sprite.
        /// </summary>
        /// <param name="sprite">Sprite to apply.</param>
        /// <returns>This builder for chaining.</returns>
        public PanelBuilder SetBackground(Sprite sprite)
        {
            (_panel as PanelWrapper)?.SetBackgroundImage(sprite);
            return this;
        }

        /// <summary>
        /// Returns the underlying wrapper for advanced scenarios.
        /// </summary>
        public PanelWrapper Build() => (PanelWrapper)_panel;

        /// <summary>
        /// Sets whether the panel blocks raycasts.
        /// </summary>
        /// <param name="value">True to block raycasts.</param>
        /// <returns>This builder for chaining.</returns>
        public PanelBuilder SetRaycastTarget(bool value)
        {
            (_panel as PanelWrapper)?.SetRaycastTarget(value);
            return this;
        }

        /// <summary>
        /// Sets the panel size in pixels.
        /// </summary>
        /// <param name="width">Width in pixels.</param>
        /// <param name="height">Height in pixels.</param>
        /// <returns>This builder for chaining.</returns>
        public PanelBuilder SetSize(float width, float height)
        {
            _panel.RectTransform.sizeDelta = new Vector2(width, height);
            return this;
        }

        /// <summary>
        /// Sets the panel anchors.
        /// </summary>
        /// <param name="anchorX">Anchor X (0-1).</param>
        /// <param name="anchorY">Anchor Y (0-1).</param>
        /// <returns>This builder for chaining.</returns>
        public PanelBuilder SetAnchor(float anchorX, float anchorY)
        {
            _panel.RectTransform.anchorMin = _panel.RectTransform.anchorMax = new Vector2(anchorX, anchorY);
            _panel.RectTransform.anchoredPosition = Vector2.zero;
            return this;
        }

        /// <summary>
        /// Sets the panel pivot.
        /// </summary>
        /// <param name="pivotX">Pivot X (0-1).</param>
        /// <param name="pivotY">Pivot Y (0-1).</param>
        /// <returns>This builder for chaining.</returns>
        public PanelBuilder SetPivot(float pivotX, float pivotY)
        {
            _panel.RectTransform.pivot = new Vector2(pivotX, pivotY);
            return this;
        }

        /// <summary>
        /// Sets the anchored position of the panel.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        /// <returns>This builder for chaining.</returns>
        public PanelBuilder SetPosition(float x, float y)
        {
            _panel.RectTransform.anchoredPosition = new Vector2(x, y);
            return this;
        }

        /// <summary>
        /// Stretches the panel to fill its parent.
        /// </summary>
        /// <param name="padding">Uniform padding in pixels.</param>
        /// <returns>This builder for chaining.</returns>
        public PanelBuilder FillParent(float padding = 0f)
        {
            _panel.RectTransform.anchorMin = Vector2.zero;
            _panel.RectTransform.anchorMax = Vector2.one;
            _panel.RectTransform.sizeDelta = new Vector2(-padding * 2f, -padding * 2f);
            _panel.RectTransform.anchoredPosition = Vector2.zero;
            return this;
        }

        /// <summary>
        /// Centers the panel within its parent.
        /// </summary>
        /// <returns>This builder for chaining.</returns>
        public PanelBuilder CenterInParent()
        {
            _panel.RectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            _panel.RectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            _panel.RectTransform.anchoredPosition = Vector2.zero;
            return this;
        }

        /// <summary>
        /// Docks the panel to a side of its parent.
        /// </summary>
        /// <param name="side">Side to dock to.</param>
        /// <param name="size">Size along the docking axis.</param>
        /// <param name="margin">Optional margin.</param>
        /// <returns>This builder for chaining.</returns>
        public PanelBuilder Dock(DockSide side, float size, float margin = 0f)
        {
            switch (side)
            {
                case DockSide.Top:
                    _panel.RectTransform.anchorMin = new Vector2(0f, 1f);
                    _panel.RectTransform.anchorMax = new Vector2(1f, 1f);
                    _panel.RectTransform.sizeDelta = new Vector2(-margin * 2f, size);
                    _panel.RectTransform.anchoredPosition = new Vector2(0f, -size / 2f - margin);
                    break;
                case DockSide.Bottom:
                    _panel.RectTransform.anchorMin = new Vector2(0f, 0f);
                    _panel.RectTransform.anchorMax = new Vector2(1f, 0f);
                    _panel.RectTransform.sizeDelta = new Vector2(-margin * 2f, size);
                    _panel.RectTransform.anchoredPosition = new Vector2(0f, size / 2f + margin);
                    break;
                case DockSide.Left:
                    _panel.RectTransform.anchorMin = new Vector2(0f, 0f);
                    _panel.RectTransform.anchorMax = new Vector2(0f, 1f);
                    _panel.RectTransform.sizeDelta = new Vector2(size, -margin * 2f);
                    _panel.RectTransform.anchoredPosition = new Vector2(size / 2f + margin, 0f);
                    break;
                case DockSide.Right:
                    _panel.RectTransform.anchorMin = new Vector2(1f, 0f);
                    _panel.RectTransform.anchorMax = new Vector2(1f, 1f);
                    _panel.RectTransform.sizeDelta = new Vector2(size, -margin * 2f);
                    _panel.RectTransform.anchoredPosition = new Vector2(-size / 2f - margin, 0f);
                    break;
            }
            return this;
        }

        /// <summary>
        /// Adds a layout group of type T to the panel.
        /// </summary>
        public PanelBuilder WithLayout<T>() where T : UnityEngine.UI.LayoutGroup
        {
            (_panel as PanelWrapper)?.AddLayoutGroup<T>();
            return this;
        }

        /// <summary>
        /// Adds and configures a HorizontalLayoutGroup via builder.
        /// </summary>
        /// <param name="config">Optional configuration action.</param>
        public PanelBuilder WithHorizontalLayout(System.Action<HorizontalLayoutBuilder>? config = null)
        {
            var panelWrapper = _panel as PanelWrapper;
            if (panelWrapper != null)
            {
                // Ensure no other layout group exists
                var existingLayouts = panelWrapper.GameObject.GetComponents<UnityEngine.UI.LayoutGroup>();
                foreach (var layout in existingLayouts)
                {
                    Object.DestroyImmediate(layout); // Use DestroyImmediate if called during editor time or initial setup
                }
                var builder = new HorizontalLayoutBuilder(panelWrapper.GameObject);
                config?.Invoke(builder);
                builder.Build(); // Builds and attaches the component
            }
            return this;
        }

        /// <summary>
        /// Adds and configures a VerticalLayoutGroup via builder.
        /// </summary>
        /// <param name="config">Optional configuration action.</param>
        public PanelBuilder WithVerticalLayout(System.Action<VerticalLayoutBuilder>? config = null)
        {
            var panelWrapper = _panel as PanelWrapper;
            if (panelWrapper != null)
            {
                // Ensure no other layout group exists
                var existingLayouts = panelWrapper.GameObject.GetComponents<UnityEngine.UI.LayoutGroup>();
                foreach (var layout in existingLayouts)
                {
                    Object.DestroyImmediate(layout);
                }
                var builder = new VerticalLayoutBuilder(panelWrapper.GameObject);
                config?.Invoke(builder);
                builder.Build(); // Builds and attaches the component
            }
            return this;
        }

        /// <summary>
        /// Adds and configures a GridLayoutGroup via builder.
        /// </summary>
        /// <param name="config">Optional configuration action.</param>
        public PanelBuilder WithGridLayout(System.Action<GridLayoutBuilder>? config = null)
        {
            var panelWrapper = _panel as PanelWrapper;
            if (panelWrapper != null)
            {
                // Ensure no other layout group exists
                var existingLayouts = panelWrapper.GameObject.GetComponents<UnityEngine.UI.LayoutGroup>();
                foreach (var layout in existingLayouts)
                {
                    Object.DestroyImmediate(layout);
                }
                var builder = new GridLayoutBuilder(panelWrapper.GameObject);
                config?.Invoke(builder);
                builder.Build(); // Builds and attaches the component
            }
            return this;
        }

        /// <summary>
        /// Sets the panel to have rounded corners.
        /// </summary>
        /// <param name="cornerRadius">Radius of the corners in pixels</param>
        /// <param name="borderSize">Size of the border for 9-slice</param>
        /// <returns>This builder for chaining</returns>
        public PanelBuilder SetRounded(int cornerRadius, int borderSize = 10)
        {
            _cornerRadius = cornerRadius;
            _borderSize = borderSize;
            return this;
        }

        /// <summary>
        /// Sets the alpha on the panel's CanvasGroup (adds one if missing).
        /// </summary>
        /// <param name="alpha">Alpha value (0-1).</param>
        /// <returns>This builder for chaining.</returns>
        public PanelBuilder SetAlpha(float alpha)
        {
            var panelWrapper = _panel as PanelWrapper;
            if (panelWrapper != null)
            {
                var canvasGroup = panelWrapper.GameObject.GetComponent<CanvasGroup>() ?? panelWrapper.GameObject.AddComponent<CanvasGroup>();
                canvasGroup.alpha = Mathf.Clamp01(alpha);
            }
            return this;
        }
    }
} 
