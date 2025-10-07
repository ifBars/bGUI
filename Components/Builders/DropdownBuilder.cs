using UnityEngine;
using System;
using System.Collections.Generic;
using bGUI.Core.Factory;
using bGUI.Core.Components;

namespace bGUI.Components
{
    /// <summary>
    /// Fluent builder for bGUI dropdowns.
    /// </summary>
    public class DropdownBuilder
    {
        private readonly IDropdown _dropdown;
        private readonly bool _usePooling;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropdownBuilder"/>.
        /// </summary>
        /// <param name="parent">Parent transform for the dropdown.</param>
        /// <param name="usePooling">Whether to use the internal object pool.</param>
        public DropdownBuilder(Transform? parent, bool usePooling = false)
        {
            _usePooling = usePooling;
            _dropdown = UIFactory.Instance.CreateDropdown(parent, "Dropdown", _usePooling);
        }

        // Backward compatibility constructor
        public DropdownBuilder(Transform? parent)
        {
            _usePooling = false;
            _dropdown = UIFactory.Instance.CreateDropdown(parent, "Dropdown", _usePooling);
        }

        public DropdownBuilder SetValue(int index)
        {
            _dropdown.Value = index;
            return this;
        }

        public DropdownBuilder OnValueChanged(Action<int> handler)
        {
            _dropdown.OnValueChanged += handler;
            return this;
        }

        public DropdownBuilder ClearOptions()
        {
            _dropdown.ClearOptions();
            return this;
        }

        public DropdownBuilder AddOption(string option)
        {
            _dropdown.AddOption(option);
            return this;
        }

        public DropdownBuilder AddOptions(IEnumerable<string> options)
        {
            _dropdown.AddOptions(options);
            return this;
        }

        public DropdownBuilder SetOptions(IEnumerable<string> options)
        {
            _dropdown.SetOptions(options);
            return this;
        }

        public DropdownBuilder SetCaption(string caption)
        {
            _dropdown.Caption = caption;
            return this;
        }

        public DropdownBuilder SetInteractable(bool interactable)
        {
            _dropdown.Interactable = interactable;
            return this;
        }

        public DropdownBuilder SetColors(Color normal, Color highlighted, Color pressed, Color disabled)
        {
            _dropdown.SetColors(normal, highlighted, pressed, disabled);
            return this;
        }

        public DropdownBuilder SetBackgroundImage(Sprite sprite)
        {
            _dropdown.SetBackgroundImage(sprite);
            return this;
        }

        public DropdownBuilder SetLabelColor(Color color)
        {
            _dropdown.SetLabelColor(color);
            return this;
        }

        public DropdownBuilder SetArrowImage(Sprite sprite)
        {
            _dropdown.SetArrowImage(sprite);
            return this;
        }

        public DropdownBuilder SetBackgroundColor(Color color)
        {
            _dropdown.SetBackgroundColor(color);
            return this;
        }

        public DropdownBuilder SetArrowColor(Color color)
        {
            _dropdown.SetArrowColor(color);
            return this;
        }

        public DropdownBuilder SetTemplateBackgroundColor(Color color)
        {
            _dropdown.SetTemplateBackgroundColor(color);
            return this;
        }

        public DropdownBuilder SetTemplateHeight(float height)
        {
            _dropdown.SetTemplateHeight(height);
            return this;
        }

        public DropdownBuilder SetVisibleItemCount(int count)
        {
            _dropdown.SetVisibleItemCount(count);
            return this;
        }

        public DropdownBuilder SetItemHeight(float height)
        {
            _dropdown.SetItemHeight(height);
            return this;
        }

        public DropdownBuilder SetItemSpacing(float spacing)
        {
            _dropdown.SetItemSpacing(spacing);
            return this;
        }

        public DropdownBuilder SetAdaptiveHeight()
        {
            _dropdown.SetAdaptiveHeight();
            return this;
        }

        public DropdownBuilder SetLargeItemMode()
        {
            _dropdown.SetLargeItemMode();
            return this;
        }

        public DropdownBuilder SetCompactItemMode()
        {
            _dropdown.SetCompactItemMode();
            return this;
        }

        public DropdownBuilder RefreshLayout()
        {
            _dropdown.RefreshLayout();
            return this;
        }

        public DropdownBuilder SetSize(float width, float height)
        {
            _dropdown.RectTransform.sizeDelta = new Vector2(width, height);
            return this;
        }

        public DropdownBuilder SetPosition(float x, float y)
        {
            _dropdown.RectTransform.anchoredPosition = new Vector2(x, y);
            return this;
        }

        public DropdownBuilder SetAnchor(float anchorX, float anchorY)
        {
            _dropdown.RectTransform.anchorMin = _dropdown.RectTransform.anchorMax = new Vector2(anchorX, anchorY);
            return this;
        }

        public DropdownBuilder SetPivot(float pivotX, float pivotY)
        {
            _dropdown.RectTransform.pivot = new Vector2(pivotX, pivotY);
            return this;
        }

        public DropdownWrapper Build() => (DropdownWrapper)_dropdown;
    }
}

