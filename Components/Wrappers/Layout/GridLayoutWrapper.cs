using UnityEngine;
using UnityEngine.UI;
using bGUI.Core.Layout;

namespace bGUI.Components.Layout
{
    /// <summary>
    /// Wrapper for Unity's GridLayoutGroup component.
    /// </summary>
    public class GridLayoutWrapper : IGridLayout
    {
        private readonly GridLayoutGroup _layoutGroup;

        /// <summary>
        /// Gets the underlying GridLayoutGroup component.
        /// </summary>
        public GridLayoutGroup Component => _layoutGroup;

        public GridLayoutWrapper(GameObject gameObject)
        {
            _layoutGroup = gameObject.GetComponent<GridLayoutGroup>() ?? gameObject.AddComponent<GridLayoutGroup>();
            SetDefaultSettings();
        }

        private void SetDefaultSettings()
        {
            _layoutGroup.padding = new RectOffset(5, 5, 5, 5);
            _layoutGroup.cellSize = new Vector2(100, 100);
            _layoutGroup.spacing = new Vector2(5, 5);
            _layoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
            _layoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
            _layoutGroup.childAlignment = TextAnchor.MiddleCenter;
            _layoutGroup.constraint = GridLayoutGroup.Constraint.Flexible;
            _layoutGroup.constraintCount = 2;
        }

        public RectOffset Padding
        {
            get => _layoutGroup.padding;
            set => _layoutGroup.padding = value;
        }

        public Vector2 CellSize
        {
            get => _layoutGroup.cellSize;
            set => _layoutGroup.cellSize = value;
        }

        public Vector2 Spacing
        {
            get => _layoutGroup.spacing;
            set => _layoutGroup.spacing = value;
        }

        public GridLayoutGroup.Corner StartCorner
        {
            get => _layoutGroup.startCorner;
            set => _layoutGroup.startCorner = value;
        }

        public GridLayoutGroup.Axis StartAxis
        {
            get => _layoutGroup.startAxis;
            set => _layoutGroup.startAxis = value;
        }

        public TextAnchor ChildAlignment
        {
            get => _layoutGroup.childAlignment;
            set => _layoutGroup.childAlignment = value;
        }

        public GridLayoutGroup.Constraint Constraint
        {
            get => _layoutGroup.constraint;
            set => _layoutGroup.constraint = value;
        }

        public int ConstraintCount
        {
            get => _layoutGroup.constraintCount;
            set => _layoutGroup.constraintCount = value;
        }

        public IGridLayout SetPadding(int left, int right, int top, int bottom)
        {
            Padding = new RectOffset(left, right, top, bottom);
            return this;
        }

        public IGridLayout SetCellSize(float width, float height)
        {
            CellSize = new Vector2(width, height);
            return this;
        }

        public IGridLayout SetSpacing(float x, float y)
        {
            Spacing = new Vector2(x, y);
            return this;
        }

        public IGridLayout SetStartCorner(GridLayoutGroup.Corner corner)
        {
            StartCorner = corner;
            return this;
        }

        public IGridLayout SetStartAxis(GridLayoutGroup.Axis axis)
        {
            StartAxis = axis;
            return this;
        }

        public IGridLayout SetChildAlignment(TextAnchor alignment)
        {
            ChildAlignment = alignment;
            return this;
        }

        public IGridLayout SetConstraint(GridLayoutGroup.Constraint constraint, int count)
        {
            Constraint = constraint;
            ConstraintCount = count;
            return this;
        }
    }
} 