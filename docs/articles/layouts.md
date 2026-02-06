# Layouts Guide

bGUI provides three layout systems for organizing UI elements: Horizontal, Vertical, and Grid layouts.

## Basic Layout Concept

Layouts in bGUI are applied to panels using the `WithLayout()` method. When you apply a layout to a panel, all children added to that panel will be automatically arranged according to the layout rules.

## Vertical Layout

Arranges children vertically from top to bottom.

### Basic Vertical Layout

```csharp
var panel = UI.Panel(parent)
    .SetSize(300, 400)
    .SetBackgroundColor(Theme.Dark)
    .WithVerticalLayout(layout => layout
        .SetPadding(10)          // Padding around content
        .SetSpacing(8)           // Space between elements
        .SetChildAlignment(TextAnchor.UpperCenter))
    .Build();

// Children are automatically stacked
UI.Button(panel.RectTransform).SetText("Button 1").Build();
UI.Button(panel.RectTransform).SetText("Button 2").Build();
UI.Button(panel.RectTransform).SetText("Button 3").Build();
```

### Vertical Layout with Control Child Sizes

```csharp
var panel = UI.Panel(parent)
    .SetSize(300, 400)
    .WithVerticalLayout(layout => layout
        .SetChildControlWidth(true)   // Children expand to fill width
        .SetChildControlHeight(false) // Don't force height
        .SetChildForceExpandWidth(true)
        .SetPadding(10)
        .SetSpacing(10))
    .Build();
```

## Horizontal Layout

Arranges children horizontally from left to right.

### Basic Horizontal Layout

```csharp
var panel = UI.Panel(parent)
    .SetSize(400, 60)
    .SetBackgroundColor(Theme.Dark)
    .WithHorizontalLayout(layout => layout
        .SetPadding(10)
        .SetSpacing(10)
        .SetChildAlignment(TextAnchor.MiddleCenter))
    .Build();

// Children are arranged horizontally
UI.Button(panel.RectTransform).SetText("Save").Build();
UI.Button(panel.RectTransform).SetText("Load").Build();
UI.Button(panel.RectTransform).SetText("Exit").Build();
```

### Horizontal Button Bar

```csharp
var buttonBar = UI.Panel(parent)
    .FillParentWidth()
    .SetHeight(50)
    .SetBackgroundColor(Theme.Primary)
    .WithHorizontalLayout(layout => layout
        .SetPadding(10, 0)
        .SetSpacing(15)
        .SetChildAlignment(TextAnchor.MiddleLeft))
    .Build();

UI.Button(buttonBar.RectTransform)
    .SetText("New")
    .Light()
    .Small()
    .Build();

UI.Button(buttonBar.RectTransform)
    .SetText("Open")
    .Light()
    .Small()
    .Build();
```

## Grid Layout

Arranges children in a grid with fixed cell sizes.

### Basic Grid Layout

```csharp
var grid = UI.Panel(parent)
    .SetSize(400, 400)
    .SetBackgroundColor(Theme.Dark)
    .WithGridLayout(layout => layout
        .SetCellSize(90, 90)        // Size of each cell
        .SetSpacing(10, 10)         // Horizontal and vertical spacing
        .SetPadding(15)             // Padding around grid
        .SetStartCorner(GridLayoutGroup.Corner.UpperLeft)
        .SetStartAxis(GridLayoutGroup.Axis.Horizontal)
        .SetChildAlignment(TextAnchor.MiddleCenter))
    .Build();

// Add items to the grid
for (int i = 0; i < 9; i++)
{
    UI.Panel(grid.RectTransform)
        .SetSize(80, 80)
        .SetBackgroundColor(Theme.Secondary)
        .SetRounded(8)
        .Build();
}
```

### Inventory Grid Example

```csharp
var inventory = UI.Panel(parent)
    .SetSize(350, 450)
    .SetBackgroundColor(new Color(0.15f, 0.15f, 0.2f, 0.95f))
    .SetRounded(12)
    .WithGridLayout(layout => layout
        .SetCellSize(70, 70)
        .SetSpacing(5, 5)
        .SetPadding(10)
        .SetConstraint(GridLayoutGroup.Constraint.FixedColumnCount)
        .SetConstraintCount(4))  // 4 columns
    .Build();

// Add inventory slots
for (int i = 0; i < 20; i++)
{
    var slot = UI.Panel(inventory.RectTransform)
        .SetSize(70, 70)
        .SetBackgroundColor(new Color(0.3f, 0.3f, 0.35f))
        .SetRounded(4)
        .Build();
}
```

## Nested Layouts

You can nest panels with different layouts to create complex UIs:

```csharp
// Main container with vertical layout
var mainPanel = UI.Panel(parent)
    .SetSize(500, 600)
    .WithVerticalLayout(v => v
        .SetPadding(15)
        .SetSpacing(10))
    .Build();

// Title bar with horizontal layout
var titleBar = UI.Panel(mainPanel.RectTransform)
    .SetHeight(40)
    .WithHorizontalLayout(h => h
        .SetPadding(10, 0)
        .SetSpacing(10))
    .Build();

UI.Text(titleBar.RectTransform)
    .SetContent("Settings")
    .Heading()
    .Build();

UI.Button(titleBar.RectTransform)
    .SetText("X")
    .Small()
    .OnClick(CloseMenu)
    .Build();

// Content area with vertical layout
var content = UI.Panel(mainPanel.RectTransform)
    .SetFlexibleHeight(1)  // Fill remaining space
    .WithVerticalLayout(v => v
        .SetSpacing(15)
        .SetPadding(10))
    .Build();

// Add settings rows
AddSettingRow(content.RectTransform, "Volume", CreateSlider);
AddSettingRow(content.RectTransform, "Brightness", CreateSlider);
AddSettingRow(content.RectTransform, "Fullscreen", CreateToggle);

// Bottom bar with horizontal layout
var bottomBar = UI.Panel(mainPanel.RectTransform)
    .SetHeight(50)
    .WithHorizontalLayout(h => h
        .SetPadding(10)
        .SetSpacing(10)
        .SetChildAlignment(TextAnchor.MiddleRight))
    .Build();

UI.Button(bottomBar.RectTransform)
    .SetText("Cancel")
    .Secondary()
    .Build();

UI.Button(bottomBar.RectTransform)
    .SetText("Apply")
    .Primary()
    .Build();
```

## Quick Layout Containers

Use QuickBuilders for common layout patterns:

```csharp
using bGUI.Core.Extensions;

// Horizontal container
var hBox = QuickBuilders.HorizontalContainer(parent);
// - Fills parent
// - Small spacing
// - Medium padding

// Vertical container
var vBox = QuickBuilders.VerticalContainer(parent);
// - Fills parent
// - Small spacing
// - Medium padding
```

## Layout Properties Reference

### Common Properties (all layouts)

| Property | Description |
|----------|-------------|
| `SetPadding(padding)` | Uniform padding around content |
| `SetPadding(left, right, top, bottom)` | Individual padding values |
| `SetSpacing(spacing)` | Space between children (vertical/horizontal) |
| `SetSpacing(x, y)` | Horizontal and vertical spacing (grid) |
| `SetChildAlignment(alignment)` | Alignment of children within cells |

### Horizontal/Vertical Specific

| Property | Description |
|----------|-------------|
| `SetChildControlWidth(bool)` | Control child widths |
| `SetChildControlHeight(bool)` | Control child heights |
| `SetChildForceExpandWidth(bool)` | Force children to expand width |
| `SetChildForceExpandHeight(bool)` | Force children to expand height |

### Grid Specific

| Property | Description |
|----------|-------------|
| `SetCellSize(width, height)` | Fixed size of each grid cell |
| `SetStartCorner(corner)` | Where grid starts (UpperLeft, UpperRight, etc.) |
| `SetStartAxis(axis)` | Fill direction (Horizontal/Vertical) |
| `SetConstraint(constraint)` | Constraint type (Flexible, FixedColumnCount, FixedRowCount) |
| `SetConstraintCount(count)` | Number of columns/rows for fixed constraints |

## Layout Utils

The `LayoutUtils` class provides helper methods:

```csharp
using bGUI.Utilities;

// Setup canvas scaler
LayoutUtils.SetupCanvasScaler(canvas, 1920, 1080);

// Setup anchors
LayoutUtils.SetFillParent(rectTransform);
LayoutUtils.SetDockTop(rectTransform, height);
LayoutUtils.SetDockBottom(rectTransform, height);
LayoutUtils.SetDockLeft(rectTransform, width);
LayoutUtils.SetDockRight(rectTransform, width);

// Layout element helpers
LayoutUtils.SetFixedWidth(rectTransform, width);
LayoutUtils.SetFixedHeight(rectTransform, height);
LayoutUtils.SetFlexibleWidth(rectTransform, flex);
LayoutUtils.SetFlexibleHeight(rectTransform, flex);
```

## Best Practices

1. **Use Layout Groups** - Let layouts handle positioning instead of manual coordinates
2. **Set Appropriate Padding** - Give content breathing room
3. **Consistent Spacing** - Use `Theme.Space` constants for consistency
4. **Flexible Heights** - Use `SetFlexibleHeight(1)` for content areas
5. **Nesting** - Break complex UIs into nested panels with different layouts
6. **Quick Builders** - Use `HorizontalContainer`/`VerticalContainer` for simple cases
