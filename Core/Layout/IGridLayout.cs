using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Core.Layout
{
    /// <summary>
    /// Interface for configuring GridLayoutGroup components.
    /// </summary>
    public interface IGridLayout
    {
        /// <summary>
        /// Gets the underlying GridLayoutGroup component.
        /// </summary>
        GridLayoutGroup Component { get; }

        /// <summary>
        /// Gets or sets the padding within the layout group.
        /// </summary>
        RectOffset Padding { get; set; }

        /// <summary>
        /// Gets or sets the size of each cell in the grid.
        /// </summary>
        Vector2 CellSize { get; set; }

        /// <summary>
        /// Gets or sets the spacing between cells in the grid.
        /// </summary>
        Vector2 Spacing { get; set; }

        /// <summary>
        /// Gets or sets the corner from which the layout starts.
        /// </summary>
        GridLayoutGroup.Corner StartCorner { get; set; }

        /// <summary>
        /// Gets or sets the axis along which cells are laid out first.
        /// </summary>
        GridLayoutGroup.Axis StartAxis { get; set; }

        /// <summary>
        /// Gets or sets the alignment of child elements within their cells.
        /// </summary>
        TextAnchor ChildAlignment { get; set; }

        /// <summary>
        /// Gets or sets the constraint on the number of columns or rows.
        /// </summary>
        GridLayoutGroup.Constraint Constraint { get; set; }

        /// <summary>
        /// Gets or sets the number of cells in the constrained dimension.
        /// </summary>
        int ConstraintCount { get; set; }

        /// <summary>
        /// Sets the padding within the layout group.
        /// </summary>
        IGridLayout SetPadding(int left, int right, int top, int bottom);

        /// <summary>
        /// Sets the size of each cell in the grid.
        /// </summary>
        IGridLayout SetCellSize(float width, float height);

        /// <summary>
        /// Sets the spacing between cells in the grid.
        /// </summary>
        IGridLayout SetSpacing(float x, float y);

        /// <summary>
        /// Sets the corner from which the layout starts.
        /// </summary>
        IGridLayout SetStartCorner(GridLayoutGroup.Corner corner);

        /// <summary>
        /// Sets the axis along which cells are laid out first.
        /// </summary>
        IGridLayout SetStartAxis(GridLayoutGroup.Axis axis);

        /// <summary>
        /// Sets the alignment of child elements within their cells.
        /// </summary>
        IGridLayout SetChildAlignment(TextAnchor alignment);

        /// <summary>
        /// Sets the constraint on the number of columns or rows.
        /// </summary>
        IGridLayout SetConstraint(GridLayoutGroup.Constraint constraint, int count);
    }
} 