using UnityEngine;
using System;
using bGUI.Core.Factory;
using bGUI.Core.Components;

namespace bGUI.Components
{
    /// <summary>
    /// Fluent builder for bGUI toggles (checkboxes).
    /// </summary>
    public class ToggleBuilder
    {
        private readonly IToggle _toggle;
        private readonly bool _usePooling;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleBuilder"/>.
        /// </summary>
        /// <param name="parent">Parent transform for the toggle.</param>
        /// <param name="usePooling">Whether to use the internal object pool.</param>
        public ToggleBuilder(Transform? parent, bool usePooling = false)
        {
            _usePooling = usePooling;
            _toggle = UIFactory.Instance.CreateToggle(parent, "Toggle", "Toggle", false, _usePooling);
        }

        // Backward compatibility constructor
        public ToggleBuilder(Transform? parent)
        {
            _usePooling = false;
            _toggle = UIFactory.Instance.CreateToggle(parent, "Toggle", "Toggle", false, _usePooling);
        }

        public ToggleBuilder SetIsOn(bool isOn)
        {
            _toggle.IsOn = isOn;
            return this;
        }

        public ToggleBuilder SetLabel(string text)
        {
            _toggle.Label = text;
            return this;
        }

        public ToggleBuilder OnValueChanged(Action<bool> handler)
        {
            _toggle.OnValueChanged += handler;
            return this;
        }

        public ToggleBuilder SetInteractable(bool interactable)
        {
            _toggle.Interactable = interactable;
            return this;
        }

        public ToggleBuilder SetColors(Color normal, Color highlighted, Color pressed, Color disabled)
        {
            _toggle.SetColors(normal, highlighted, pressed, disabled);
            return this;
        }

        public ToggleBuilder SetBackgroundImage(UnityEngine.Sprite sprite)
        {
            _toggle.SetBackgroundImage(sprite);
            return this;
        }

        public ToggleBuilder SetCheckmarkImage(UnityEngine.Sprite sprite)
        {
            _toggle.SetCheckmarkImage(sprite);
            return this;
        }

        public ToggleBuilder SetLabelColor(Color color)
        {
            _toggle.SetLabelColor(color);
            return this;
        }

        public ToggleBuilder SetSize(float width, float height)
        {
            _toggle.RectTransform.sizeDelta = new Vector2(width, height);
            return this;
        }

        public ToggleBuilder SetPosition(float x, float y)
        {
            _toggle.RectTransform.anchoredPosition = new Vector2(x, y);
            return this;
        }

        public ToggleBuilder SetAnchor(float anchorX, float anchorY)
        {
            _toggle.RectTransform.anchorMin = _toggle.RectTransform.anchorMax = new Vector2(anchorX, anchorY);
            return this;
        }

        public ToggleBuilder SetPivot(float pivotX, float pivotY)
        {
            _toggle.RectTransform.pivot = new Vector2(pivotX, pivotY);
            return this;
        }

        public ToggleWrapper Build() => (ToggleWrapper)_toggle;
    }
}

