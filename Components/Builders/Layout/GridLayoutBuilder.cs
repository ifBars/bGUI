using UnityEngine;
using UnityEngine.UI;
using bGUI.Components.Layout;
using bGUI.Core.Layout;

namespace bGUI.Components.Builders.Layout
{
    /// <summary>
    /// Fluent builder for GridLayoutGroup components.
    /// </summary>
    public class GridLayoutBuilder
    {
        private readonly GridLayoutWrapper _layoutWrapper;
        /// <summary>
        /// Initializes a new instance bound to the provided owner GameObject.
        /// </summary>
        /// <param name="owner">GameObject that will host the layout component.</param>
        public GridLayoutBuilder(GameObject owner)
        {
            _layoutWrapper = new GridLayoutWrapper(owner);
        }

        /// <summary>
        /// Sets layout padding on each side.
        /// </summary>
        public GridLayoutBuilder SetPadding(int left, int right, int top, int bottom)
        {
            _layoutWrapper.SetPadding(left, right, top, bottom);
            return this;
        }

        /// <summary>
        /// Sets uniform padding on all sides.
        /// </summary>
        public GridLayoutBuilder SetPadding(int all)
        {
            _layoutWrapper.SetPadding(all, all, all, all);
            return this;
        }

        /// <summary>
        /// Sets cell size for grid elements.
        /// </summary>
        public GridLayoutBuilder SetCellSize(float width, float height)
        {
            _layoutWrapper.SetCellSize(width, height);
            return this;
        }

        /// <summary>
        /// Sets spacing between grid cells.
        /// </summary>
        public GridLayoutBuilder SetSpacing(float x, float y)
        {
            _layoutWrapper.SetSpacing(x, y);
            return this;
        }

        /// <summary>
        /// Sets the starting corner for layouting.
        /// </summary>
        public GridLayoutBuilder SetStartCorner(GridLayoutGroup.Corner corner)
        {
            _layoutWrapper.SetStartCorner(corner);
            return this;
        }

        /// <summary>
        /// Sets the initial axis used to place cells.
        /// </summary>
        public GridLayoutBuilder SetStartAxis(GridLayoutGroup.Axis axis)
        {
            _layoutWrapper.SetStartAxis(axis);
            return this;
        }

        /// <summary>
        /// Sets child alignment within each cell.
        /// </summary>
        public GridLayoutBuilder SetChildAlignment(TextAnchor alignment)
        {
            _layoutWrapper.SetChildAlignment(alignment);
            return this;
        }

        /// <summary>
        /// Sets grid constraint and count (columns/rows).
        /// </summary>
        public GridLayoutBuilder SetConstraint(GridLayoutGroup.Constraint constraint, int count)
        {
            _layoutWrapper.SetConstraint(constraint, count);
            return this;
        }

        /// <summary>
        /// Builds and returns the layout wrapper.
        /// </summary>
        public IGridLayout Build()
        {
            return _layoutWrapper;
        }

        /// <summary>
        /// Builds and returns the underlying Unity component.
        /// </summary>
        public GridLayoutGroup BuildComponent()
        {
            return _layoutWrapper.Component;
        }
    }
} 
