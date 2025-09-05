using UnityEngine;
using UnityEngine.UI;
using bGUI.Core.Layout;

namespace bGUI.Components.Layout
{
    /// <summary>
    /// Wrapper for Unity's HorizontalLayoutGroup component.
    /// </summary>
    public class HorizontalLayoutWrapper : IHorizontalLayout
    {
        private readonly HorizontalLayoutGroup _layoutGroup;

        /// <summary>
        /// Gets the underlying HorizontalLayoutGroup component.
        /// </summary>
        public HorizontalLayoutGroup Component => _layoutGroup;

        public HorizontalLayoutWrapper(GameObject gameObject)
        {
            _layoutGroup = gameObject.GetComponent<HorizontalLayoutGroup>() ?? gameObject.AddComponent<HorizontalLayoutGroup>();
            // Apply bGUI default settings
            SetDefaultSettings();
        }

        private void SetDefaultSettings()
        {
            _layoutGroup.spacing = 5f;
            _layoutGroup.padding = new RectOffset(5, 5, 5, 5);
            _layoutGroup.childAlignment = TextAnchor.MiddleLeft;
            _layoutGroup.childControlWidth = true;
            _layoutGroup.childControlHeight = true;
            _layoutGroup.childForceExpandWidth = false;
            _layoutGroup.childForceExpandHeight = false;
            _layoutGroup.childScaleWidth = false;
            _layoutGroup.childScaleHeight = false;
            _layoutGroup.reverseArrangement = false;
        }

        public float Spacing
        {
            get => _layoutGroup.spacing;
            set => _layoutGroup.spacing = value;
        }

        public RectOffset Padding
        {
            get => _layoutGroup.padding;
            set => _layoutGroup.padding = value;
        }

        public TextAnchor ChildAlignment
        {
            get => _layoutGroup.childAlignment;
            set => _layoutGroup.childAlignment = value;
        }

        public bool ChildControlWidth
        {
            get => _layoutGroup.childControlWidth;
            set => _layoutGroup.childControlWidth = value;
        }

        public bool ChildControlHeight
        {
            get => _layoutGroup.childControlHeight;
            set => _layoutGroup.childControlHeight = value;
        }

        public bool ChildForceExpandWidth
        {
            get => _layoutGroup.childForceExpandWidth;
            set => _layoutGroup.childForceExpandWidth = value;
        }

        public bool ChildForceExpandHeight
        {
            get => _layoutGroup.childForceExpandHeight;
            set => _layoutGroup.childForceExpandHeight = value;
        }

        public bool ChildScaleWidth
        {
            get => _layoutGroup.childScaleWidth;
            set => _layoutGroup.childScaleWidth = value;
        }

        public bool ChildScaleHeight
        {
            get => _layoutGroup.childScaleHeight;
            set => _layoutGroup.childScaleHeight = value;
        }

        public bool ReverseArrangement
        {
            get => _layoutGroup.reverseArrangement;
            set => _layoutGroup.reverseArrangement = value;
        }

        public IHorizontalLayout SetSpacing(float spacing)
        {
            Spacing = spacing;
            return this;
        }

        public IHorizontalLayout SetPadding(int left, int right, int top, int bottom)
        {
            Padding = new RectOffset(left, right, top, bottom);
            return this;
        }

        public IHorizontalLayout SetChildAlignment(TextAnchor alignment)
        {
            ChildAlignment = alignment;
            return this;
        }

        public IHorizontalLayout SetChildControlSize(bool controlWidth, bool controlHeight)
        {
            ChildControlWidth = controlWidth;
            ChildControlHeight = controlHeight;
            return this;
        }

        public IHorizontalLayout SetChildForceExpand(bool expandWidth, bool expandHeight)
        {
            ChildForceExpandWidth = expandWidth;
            ChildForceExpandHeight = expandHeight;
            return this;
        }

        public IHorizontalLayout SetChildScale(bool scaleWidth, bool scaleHeight)
        {
            ChildScaleWidth = scaleWidth;
            ChildScaleHeight = scaleHeight;
            return this;
        }

        public IHorizontalLayout SetReverseArrangement(bool reverse)
        {
            ReverseArrangement = reverse;
            return this;
        }
    }
} 