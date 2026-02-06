# API Guide

This guide provides an overview of bGUI's architecture and how to use the fluent API effectively.

## Architecture Overview

```
┌─────────────────────────────────────────────────────────────┐
│                        UI.cs (Facade)                        │
│  ┌──────────┐ ┌──────────┐ ┌──────────┐ ┌────────────────┐  │
│  │ UI.Button│ │ UI.Panel │ │ UI.Text  │ │ UI.Slider...   │  │
│  └────┬─────┘ └────┬─────┘ └────┬─────┘ └───────┬────────┘  │
└───────┼────────────┼────────────┼────────────────┼───────────┘
        │            │            │                │
        ▼            ▼            ▼                ▼
┌──────────────────────────────────────────────────────────────┐
│                    Fluent Builders                            │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────────┐    │
│  │ButtonBuilder │  │ PanelBuilder │  │ SliderBuilder... │    │
│  │  .SetText()  │  │.SetBackground│  │ .SetRange()      │    │
│  │  .OnClick()  │  │.WithLayout() │  │ .SetValue()      │    │
│  └──────┬───────┘  └──────┬───────┘  └────────┬─────────┘    │
└─────────┼─────────────────┼───────────────────┼──────────────┘
          │                 │                   │
          ▼                 ▼                   ▼
┌──────────────────────────────────────────────────────────────┐
│                    UIFactory (Singleton)                      │
│  ┌────────────────────────────────────────────────────────┐  │
│  │   Create() methods - instantiate wrappers               │  │
│  │   PoolingService - optional object recycling            │  │
│  └────────────────────────────────────────────────────────┘  │
└──────────────────────────────────────────────────────────────┘
          │
          ▼
┌──────────────────────────────────────────────────────────────┐
│                    Wrappers                                   │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────────┐    │
│  │ButtonWrapper │  │ PanelWrapper │  │ SliderWrapper... │    │
│  │ implements   │  │ implements   │  │ implements       │    │
│  │ IButton      │  │ IPanel       │  │ ISlider          │    │
│  └──────────────┘  └──────────────┘  └──────────────────┘    │
└──────────────────────────────────────────────────────────────┘
          │
          ▼
┌──────────────────────────────────────────────────────────────┐
│                    Unity UGUI                                 │
│  ┌──────────┐  ┌──────────┐  ┌──────────┐  ┌──────────┐      │
│  │  Button  │  │  Image   │  │  Text    │  │ Slider   │      │
│  └──────────┘  └──────────┘  └──────────┘  └──────────┘      │
└──────────────────────────────────────────────────────────────┘
```

## The Static Facade

The `UI` class provides the entry point for all element creation:

```csharp
// Element builders
UI.Button(parent)        // Returns ButtonBuilder
UI.Panel(parent)         // Returns PanelBuilder
UI.Text(parent)          // Returns TextBuilder
UI.Image(parent)         // Returns ImageBuilder
UI.Slider(parent)        // Returns SliderBuilder
UI.Toggle(parent)        // Returns ToggleBuilder
UI.Dropdown(parent)      // Returns DropdownBuilder
UI.ScrollView(parent)    // Returns ScrollViewBuilder
UI.ToggleGroup(parent)   // Returns ToggleGroupBuilder

// Layout builders
UI.HorizontalLayout(gameObject)
UI.VerticalLayout(gameObject)
UI.GridLayout(gameObject)

// Canvas builder
UI.Canvas(name)

// Pooling control
UI.EnablePooling()
UI.DisablePooling()
UI.ReturnToPool(element)
```

## Builders

Builders use the **fluent pattern** - each method returns `this` for chaining.

### Common Builder Methods

All builders inherit from `FluentBuilderBase<TBuilder, TElement>`:

```csharp
.SetName(string)
.SetActive(bool)
.SetSize(float width, float height)
.SetSize(Vector2)
.SetPosition(float x, float y)
.SetPosition(Vector2)
.SetAnchor(anchorX, anchorY)     // 0-1 values
.SetPivot(pivotX, pivotY)        // 0-1 values
.SetAnchorPreset(AnchorPreset)
.SetAlpha(float)
.SetInteractable(bool)
.SetBlocksRaycasts(bool)
.FillParent(float padding)
.CenterInParent()
.Dock(DockSide, size, margin)
.WithFadeIn(duration, onComplete)
.WithStyle(Action<TElement>)
.SetParent(Transform)
.Build()
.Build(Action<TElement>)
```

### Button Builder

```csharp
UI.Button(parent)
    .SetText(string)
    .SetSize(width, height)
    .SetPosition(x, y)
    .SetAnchor(anchorX, anchorY)
    .SetPivot(pivotX, pivotY)
    .OnClick(Action)
    .SetColors(ColorBlock)
    .SetColors(normal, highlighted, pressed, disabled)
    .SetBackgroundColor(Color)
    .SetBackground(Sprite)
    .SetInteractable(bool)
    .SetRounded(radius, borderSize)
    .RoundedSmall()
    .RoundedLarge()
    // Theme extensions
    .Primary()
    .Secondary()
    .Success()
    .Warning()
    .Error()
    .Info()
    .Light()
    .Dark()
    // Size extensions
    .Small()
    .DefaultSize()
    .Large()
    .Build()
```

### Panel Builder

```csharp
UI.Panel(parent)
    .SetBackgroundColor(Color)
    .SetBackground(Sprite)
    .SetRaycastTarget(bool)
    .SetSize(width, height)
    .SetAnchor(anchorX, anchorY)
    .SetPivot(pivotX, pivotY)
    .SetPosition(x, y)
    .FillParent(padding)
    .CenterInParent()
    .Dock(side, size, margin)
    // Layouts
    .WithLayout<T>()  // Add raw LayoutGroup
    .WithHorizontalLayout(Action<HorizontalLayoutBuilder>)
    .WithVerticalLayout(Action<VerticalLayoutBuilder>)
    .WithGridLayout(Action<GridLayoutBuilder>)
    // Styling
    .SetRounded(radius, borderSize)
    .SetAlpha(alpha)
    // Theme extensions
    .Primary()
    .Secondary()
    .Dark()
    .Light()
    .RoundedSmall()
    .RoundedLarge()
    .Circle()
    .Build()
```

### Text Builder

```csharp
UI.Text(parent)
    .SetContent(string)
    .SetFontSize(int)
    .SetColor(Color)
    .SetAlignment(TextAnchor)
    .SetSize(width, height)
    // Theme extensions
    .Primary()
    .Secondary()
    .Success()
    .Warning()
    .Error()
    .Title()
    .Heading()
    .Large()
    .Small()
    .Build()
```

### Slider Builder

```csharp
UI.Slider(parent)
    .SetValue(float)
    .SetMinValue(float)
    .SetMaxValue(float)
    .SetRange(min, max)
    .SetWholeNumbers(bool)
    .SetDirection(Slider.Direction)
    .SetSize(width, height)
    .SetPosition(x, y)
    .SetAnchor(anchorX, anchorY)
    .SetPivot(pivotX, pivotY)
    .OnValueChanged(Action<float>)
    .SetColors(normal, highlighted, pressed, disabled)
    .SetInteractable(bool)
    .SetBackgroundImage(Sprite)
    .SetFillImage(Sprite)
    .SetHandleImage(Sprite)
    // Convenience methods
    .SetHorizontal(width, height)
    .SetVertical(width, height)
    .SetColorScheme(fillColor, backgroundColor, handleColor)
    .SetAsPercentage(initialValue)
    .Build()
```

### Toggle Builder

```csharp
UI.Toggle(parent)
    .SetIsOn(bool)
    .SetLabel(string)
    .OnValueChanged(Action<bool>)
    .SetInteractable(bool)
    .SetColors(normal, highlighted, pressed, disabled)
    .SetBackgroundImage(Sprite)
    .SetCheckmarkImage(Sprite)
    .SetLabelColor(Color)
    .SetSize(width, height)
    .SetPosition(x, y)
    .SetAnchor(anchorX, anchorY)
    .SetPivot(pivotX, pivotY)
    .Build()
```

### Dropdown Builder

```csharp
UI.Dropdown(parent)
    .SetValue(int)
    .OnValueChanged(Action<int>)
    .ClearOptions()
    .AddOption(string)
    .AddOptions(IEnumerable<string>)
    .SetOptions(IEnumerable<string>)
    .SetCaption(string)
    .SetInteractable(bool)
    .SetColors(normal, highlighted, pressed, disabled)
    .SetBackgroundImage(Sprite)
    .SetLabelColor(Color)
    .SetArrowImage(Sprite)
    .SetBackgroundColor(Color)
    .SetArrowColor(Color)
    .SetTemplateBackgroundColor(Color)
    .SetTemplateHeight(float)
    .SetVisibleItemCount(int)
    .SetItemHeight(float)
    .SetItemSpacing(float)
    .SetAdaptiveHeight()
    .SetLargeItemMode()
    .SetCompactItemMode()
    .RefreshLayout()
    .SetSize(width, height)
    .SetPosition(x, y)
    .SetAnchor(anchorX, anchorY)
    .SetPivot(pivotX, pivotY)
    .Build()
```

### Canvas Builder

```csharp
UI.Canvas(name)
    .SetRenderMode(RenderMode)
    .SetSortingOrder(int)
    .SetReferenceResolution(width, height)
    .SetScaleMode(CanvasScaler.ScaleMode)
    .SetMatchWidthOrHeight(float)
    .SetPreserveAspect(bool)
    .SetDontDestroyOnLoad(bool)
    .Build()
```

## Wrappers

Wrappers provide access to the underlying Unity components while maintaining the fluent interface.

### Common Wrapper Properties

```csharp
wrapper.GameObject        // Access the GameObject
wrapper.RectTransform     // Access the RectTransform
wrapper.Name              // Get/set name
wrapper.IsActive          // Get/set active state
wrapper.SetParent(Transform)
wrapper.Destroy()
```

### Button Wrapper

```csharp
ButtonWrapper button = UI.Button(parent).Build();

button.Text                           // Get/set button text
button.ButtonComponent                // Access Unity Button
button.Interactable                   // Get/set interactable
button.OnClick                        // Event for clicks

button.SetColors(normal, highlighted, pressed, disabled)
button.SetBackgroundImage(Sprite)
button.SetRounded(radius, borderSize)
button.Primary()                      // Theme extension
// ... other extensions
```

### Panel Wrapper

```csharp
PanelWrapper panel = UI.Panel(parent).Build();

panel.SetBackgroundColor(Color)
panel.SetBackgroundImage(Sprite)
panel.SetRaycastTarget(bool)
panel.SetRounded(radius, borderSize)
panel.AddLayoutGroup<T>()

// Container methods (from ContainerBase)
panel.AddChild(IUIElement)
panel.RemoveChild(IUIElement)
panel.ClearChildren()
panel.GetChild(int)
panel.GetChildrenOfType<T>()
```

## Extension Methods

### Builder Extensions (bGUI.Core.Extensions.BuilderExtensions)

Theme and styling shortcuts:

```csharp
// Button themes
buttonBuilder.Primary()
buttonBuilder.Secondary()
buttonBuilder.Success()
buttonBuilder.Warning()
buttonBuilder.Error()
buttonBuilder.Info()
buttonBuilder.Light()
buttonBuilder.Dark()

// Button sizes
buttonBuilder.Small()
buttonBuilder.DefaultSize()
buttonBuilder.Large()

// Text themes
textBuilder.Primary()
textBuilder.Success()
textBuilder.Title()
textBuilder.Heading()
textBuilder.Large()
textBuilder.Small()

// Panel themes
panelBuilder.Primary()
panelBuilder.Secondary()
panelBuilder.Dark()
panelBuilder.Light()

// Common styling
builder.Rounded(radius)
builder.RoundedSmall()
builder.RoundedLarge()
builder.Circle()
```

### UI Element Extensions (bGUI.Core.Extensions.UIElementExtensions)

```csharp
element.SetAnchorPreset(AnchorPreset)
element.SetSize(width, height)
element.SetPosition(x, y)
element.FillParent(padding)
element.CenterInParent()
element.Dock(side, size, margin)
element.FadeIn(duration, onComplete)
element.FadeOut(duration, onComplete)
element.SetAlpha(alpha)
element.SetInteractable(interactable)
element.SetBlocksRaycasts(blocks)
```

### RectTransform Extensions (bGUI.Core.Extensions.RectTransformExtensions)

```csharp
rectTransform.SetAnchorPreset(AnchorPreset)
rectTransform.SetSize(width, height)
rectTransform.SetWidth(width)
rectTransform.SetHeight(height)
rectTransform.SetPosition(x, y)
rectTransform.SetPivot(x, y)
rectTransform.SetAnchors(min, max)
rectTransform.FillParent(padding)
rectTransform.FillParent(left, right, top, bottom)
rectTransform.CenterInParent()
rectTransform.DockTop(height, margin)
rectTransform.DockBottom(height, margin)
rectTransform.DockLeft(width, margin)
rectTransform.DockRight(width, margin)
```

### Quick Builders (bGUI.Core.Extensions.QuickBuilders)

Pre-styled factory methods:

```csharp
// Buttons
QuickBuilders.PrimaryButton(parent, text)
QuickBuilders.SecondaryButton(parent, text)
QuickBuilders.SuccessButton(parent, text)
QuickBuilders.DangerButton(parent, text)

// Text
QuickBuilders.TitleText(parent, text)
QuickBuilders.HeadingText(parent, text)
QuickBuilders.Label(parent, text)
QuickBuilders.ErrorText(parent, text)
QuickBuilders.SuccessText(parent, text)

// Panels
QuickBuilders.Card(parent)
QuickBuilders.DarkCard(parent)
QuickBuilders.Container(parent)
QuickBuilders.HorizontalContainer(parent)
QuickBuilders.VerticalContainer(parent)
```

## Interfaces

### Core Interfaces

| Interface | Description |
|-----------|-------------|
| `IUIElement` | Base for all UI elements (Name, IsActive, RectTransform, GameObject) |
| `IContainer` | Elements that can contain children (AddChild, RemoveChild) |
| `IButton` | Button functionality (Text, Interactable, OnClick) |
| `ISlider` | Slider functionality (Value, MinValue, MaxValue, OnValueChanged) |
| `IToggle` | Toggle functionality (IsOn, Label, OnValueChanged) |
| `IDropdown` | Dropdown functionality (Value, Options, OnValueChanged) |
| `IText` | Text display (Content, FontSize, Color, Alignment) |
| `IImage` | Image display (Sprite, Color) |
| `IPanel` | Panel/background container |
| `ICanvas` | Canvas wrapper |
| `IMenu` | Multi-page menu with navigation |
| `IPage` | Single page within a menu |
| `IScrollView` | Scrollable content area |

## Anchor Presets

```csharp
using bGUI.Core.Enums;

AnchorPreset.TopLeft
AnchorPreset.TopCenter
AnchorPreset.TopRight
AnchorPreset.MiddleLeft
AnchorPreset.MiddleCenter
AnchorPreset.MiddleRight
AnchorPreset.BottomLeft
AnchorPreset.BottomCenter
AnchorPreset.BottomRight
AnchorPreset.StretchLeft
AnchorPreset.StretchRight
AnchorPreset.StretchTop
AnchorPreset.StretchBottom
AnchorPreset.StretchAll
```

## Dock Sides

```csharp
using bGUI.Core.Extensions;

DockSide.Top
DockSide.Bottom
DockSide.Left
DockSide.Right
```

## Working Example

```csharp
public class CompleteExample : MelonMod
{
    private void CreateSettingsMenu()
    {
        // Create canvas
        var canvas = UI.Canvas("SettingsMenu")
            .SetRenderMode(RenderMode.ScreenSpaceOverlay)
            .SetSortingOrder(100)
            .SetReferenceResolution(1920, 1080)
            .Build();

        // Create main panel with vertical layout
        var panel = UI.Panel(canvas.RectTransform)
            .SetSize(450, 550)
            .SetAnchor(0.5f, 0.5f)
            .SetBackgroundColor(new Color(0.1f, 0.1f, 0.15f, 0.95f))
            .SetRounded(12)
            .WithVerticalLayout(layout => layout
                .SetPadding(20)
                .SetSpacing(15)
                .SetChildAlignment(TextAnchor.UpperCenter))
            .Build();

        // Title
        UI.Text(panel.RectTransform)
            .SetContent("Settings")
            .Title()
            .SetSize(410, 40)
            .Build();

        // Volume slider
        var volumeRow = CreateSettingRow(panel.RectTransform, "Volume");
        UI.Slider(volumeRow.RectTransform)
            .SetRange(0, 100)
            .SetValue(75)
            .SetWholeNumbers(true)
            .SetHorizontal(200, 24)
            .OnValueChanged(v => MelonLogger.Msg($"Volume: {v}"))
            .Build();

        // Fullscreen toggle
        var fullscreenRow = CreateSettingRow(panel.RectTransform, "Fullscreen");
        UI.Toggle(fullscreenRow.RectTransform)
            .SetIsOn(true)
            .OnValueChanged(on => MelonLogger.Msg($"Fullscreen: {on}"))
            .Build();

        // Difficulty dropdown
        var difficultyRow = CreateSettingRow(panel.RectTransform, "Difficulty");
        UI.Dropdown(difficultyRow.RectTransform)
            .SetOptions(new[] { "Easy", "Normal", "Hard", "Extreme" })
            .SetValue(1)
            .SetSize(150, 30)
            .OnValueChanged(idx => MelonLogger.Msg($"Difficulty: {idx}"))
            .Build();

        // Buttons row
        var buttonRow = UI.Panel(panel.RectTransform)
            .SetHeight(40)
            .WithHorizontalLayout(layout => layout
                .SetSpacing(15)
                .SetChildAlignment(TextAnchor.MiddleCenter))
            .Build();

        UI.Button(buttonRow.RectTransform)
            .SetText("Cancel")
            .SetSize(100, 35)
            .Secondary()
            .OnClick(() => HideMenu())
            .Build();

        UI.Button(buttonRow.RectTransform)
            .SetText("Apply")
            .SetSize(100, 35)
            .Success()
            .OnClick(() => SaveSettings())
            .Build();
    }

    private PanelWrapper CreateSettingRow(Transform parent, string label)
    {
        return UI.Panel(parent)
            .SetHeight(40)
            .WithHorizontalLayout(layout => layout
                .SetSpacing(10)
                .SetChildAlignment(TextAnchor.MiddleLeft))
            .Build();
    }
}
```
