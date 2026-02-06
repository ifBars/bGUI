# bGUI

A fluent UI library for building in-game menus and interfaces in Unity via [MelonLoader](https://melonwiki.xyz/). bGUI wraps Unity's UGUI system with a clean, chainable builder API so you can create buttons, panels, sliders, toggles, dropdowns, layouts, menus, and more with minimal boilerplate.

## Features

- **Fluent Builder API** -- Chain calls to configure UI elements in a readable, declarative style
- **Full Component Suite** -- Buttons, panels, text, images, sliders, toggles, dropdowns, scroll views, toggle groups
- **Layout System** -- Horizontal, vertical, and grid layouts with full padding/spacing control
- **Menu & Page Navigation** -- Multi-page menus with sidebar navigation out of the box
- **Theming** -- Built-in semantic colors (Primary, Success, Warning, Error, etc.) and size presets
- **Rounded Corners** -- Runtime-generated rounded rectangle sprites with 9-slice support
- **Animations** -- Fade, move, pulse, rotate, sequence, and interactive animations
- **Object Pooling** -- Opt-in element recycling for performance-sensitive scenarios
- **Quick Builders** -- One-liner factory methods for common UI patterns (cards, titled text, containers)

## Requirements

- **Unity Game** with MelonLoader installed (bGUI was developed for Schedule I)
- **.NET Standard 2.1** runtime
- **MelonLoader (0.7.0+ preferred)**

## Installation

1. Build the project (see [Building](#building)) or download the pre-built `bGUI.dll` from releases
2. Copy `bGUI.dll` into your game's `UserLibs/` folder
3. Reference `bGUI.dll` from your MelonLoader mod project

## Building

```bash
# Restore dependencies
dotnet restore

# Debug build
dotnet build bGUI.sln -c Debug

# Release build
dotnet build bGUI.sln -c Release
```

> **Note:** The `.csproj` references Unity and MelonLoader DLLs from local paths. Update the `<HintPath>` entries in `bGUI.csproj` to match your game installation directory.

## Quick Start

### Minimal Example -- Toggle a Menu with F1

```csharp
using MelonLoader;
using UnityEngine;
using bGUI;
using bGUI.Components;

public class MyMod : MelonMod
{
    private CanvasWrapper? _canvas;
    private PanelWrapper? _panel;
    private bool _visible;

    public override void OnUpdate()
    {
        if (!Input.GetKeyDown(KeyCode.F1)) return;
        _visible = !_visible;

        if (_visible)
        {
            if (_canvas == null) CreateUI();
            _panel?.GameObject.SetActive(true);
        }
        else
        {
            _panel?.GameObject.SetActive(false);
        }
    }

    private void CreateUI()
    {
        _canvas = UI.Canvas("MyModCanvas")
            .SetRenderMode(RenderMode.ScreenSpaceOverlay)
            .SetSortingOrder(100)
            .SetReferenceResolution(1920, 1080)
            .Build();

        _panel = UI.Panel(_canvas.RectTransform)
            .SetSize(400, 300)
            .SetAnchor(0.5f, 0.5f)
            .SetBackgroundColor(new Color(0.1f, 0.1f, 0.15f, 0.95f))
            .SetRounded(12)
            .WithVerticalLayout(layout => layout
                .SetPadding(15)
                .SetSpacing(10)
                .SetChildAlignment(TextAnchor.UpperCenter))
            .Build();

        UI.Text(_panel.RectTransform)
            .SetContent("My Mod Menu")
            .SetFontSize(18)
            .SetColor(Color.white)
            .SetSize(370, 30)
            .Build();

        UI.Button(_panel.RectTransform)
            .SetText("Click Me")
            .SetSize(200, 35)
            .Primary()
            .OnClick(() => MelonLogger.Msg("Button clicked!"))
            .Build();
    }
}
```

### Buttons with Themes

```csharp
// Semantic color themes
UI.Button(parent).SetText("Save").Primary().Build();
UI.Button(parent).SetText("OK").Success().Build();
UI.Button(parent).SetText("Caution").Warning().Build();
UI.Button(parent).SetText("Delete").Error().Build();

// Size presets
UI.Button(parent).SetText("Small").Small().Build();
UI.Button(parent).SetText("Default").DefaultSize().Build();
UI.Button(parent).SetText("Large").Large().Build();

// Custom colors and rounded corners
UI.Button(parent)
    .SetText("Custom")
    .SetBackgroundColor(new Color(0.5f, 0.0f, 0.8f))
    .SetRounded(8)
    .SetSize(180, 40)
    .OnClick(() => { /* handler */ })
    .Build();
```

### Sliders

```csharp
// Basic slider
UI.Slider(parent)
    .SetRange(0, 100)
    .SetValue(50)
    .SetWholeNumbers(true)
    .SetSize(200, 20)
    .OnValueChanged(val => MelonLogger.Msg($"Value: {val}"))
    .Build();

// Percentage slider with color scheme
UI.Slider(parent)
    .SetAsPercentage(75)
    .SetHorizontal(250, 24)
    .SetColorScheme(
        fillColor: new Color(0.2f, 0.8f, 0.2f),
        backgroundColor: new Color(0.2f, 0.2f, 0.2f),
        handleColor: Color.white)
    .Build();
```

### Toggles and Dropdowns

```csharp
// Toggle
UI.Toggle(parent)
    .SetLabel("Enable Feature")
    .SetIsOn(true)
    .SetSize(200, 25)
    .OnValueChanged(isOn => MelonLogger.Msg($"Toggled: {isOn}"))
    .Build();

// Dropdown
UI.Dropdown(parent)
    .SetOptions(new[] { "Low", "Medium", "High", "Ultra" })
    .SetValue(1)
    .SetSize(180, 30)
    .OnValueChanged(idx => MelonLogger.Msg($"Selected: {idx}"))
    .Build();
```

### Panels with Layouts

```csharp
// Vertical layout panel
var container = UI.Panel(parent)
    .SetSize(400, 300)
    .SetBackgroundColor(new Color(0.15f, 0.15f, 0.2f, 0.95f))
    .SetRounded(10)
    .WithVerticalLayout(layout => layout
        .SetPadding(10)
        .SetSpacing(8)
        .SetChildAlignment(TextAnchor.UpperCenter))
    .Build();

// Grid layout
var grid = UI.Panel(parent)
    .SetSize(400, 400)
    .WithGridLayout(layout => layout
        .SetCellSize(90, 90)
        .SetSpacing(5, 5)
        .SetPadding(10))
    .Build();
```

### Quick Builders

Pre-styled factory methods for common patterns:

```csharp
using bGUI.Core.Extensions;

// Pre-themed buttons
var save = QuickBuilders.PrimaryButton(parent, "Save");
var cancel = QuickBuilders.SecondaryButton(parent, "Cancel");
var delete = QuickBuilders.DangerButton(parent, "Delete");

// Text presets
var title = QuickBuilders.TitleText(parent, "Settings");
var label = QuickBuilders.Label(parent, "Username:");
var error = QuickBuilders.ErrorText(parent, "Invalid input");

// Layout containers
var card = QuickBuilders.Card(parent);
var hBox = QuickBuilders.HorizontalContainer(parent);
var vBox = QuickBuilders.VerticalContainer(parent);
```

### Object Pooling

```csharp
// Enable globally
UI.EnablePooling();

// Or per-element (handled automatically by the static flag)
var btn = UI.Button(parent).SetText("Pooled").Build();

// Return to pool when done instead of destroying
UI.ReturnToPool(btn);

// Disable globally
UI.DisablePooling();
```

## Architecture

```
UI.cs (static facade)
  |
  +-- Builders (fluent API)
  |     ButtonBuilder, PanelBuilder, TextBuilder, SliderBuilder,
  |     ToggleBuilder, DropdownBuilder, CanvasBuilder, MenuBuilder, ...
  |
  +-- UIFactory (singleton, creates wrapped elements)
  |     |
  |     +-- PoolingService (opt-in object recycling)
  |
  +-- Wrappers (compose Unity components)
  |     ButtonWrapper, PanelWrapper, TextWrapper, SliderWrapper, ...
  |     Each implements an interface (IButton, IPanel, IText, ...)
  |
  +-- Extensions
  |     BuilderExtensions (theming), QuickBuilders (factories),
  |     UIElementExtensions (fade, dock), RectTransformExtensions
  |
  +-- Core
        Abstractions (IUIElement, UIElementBase, ContainerBase, LayoutBase<T>)
        Constants (Theme, UIConstants)
        Enums (AnchorPreset, EasingType, ...)
        Events (UIEventArgs hierarchy)
```

### Key Patterns

| Pattern | Description |
|---------|-------------|
| **Fluent Builders** | Each element type has a builder that returns `this` for chaining, ending with `.Build()` |
| **Wrapper Composition** | Wrappers hold Unity components (`has-a`) rather than inheriting from MonoBehaviour |
| **Interface Contracts** | Every element type has an interface (`IButton`, `IPanel`, etc.) defined before implementation |
| **Singleton Factory** | `UIFactory.Instance` centralizes element creation and pooling |
| **Static Facade** | `UI.Button()`, `UI.Panel()`, etc. provide a clean top-level API |
| **Extension Methods** | Theming, sizing, and animation applied via extensions to keep builders lean |

## Theme Reference

### Colors

| Token | RGB | Usage |
|-------|-----|-------|
| `Theme.Primary` | `(0.2, 0.6, 1.0)` | Primary actions, headings |
| `Theme.Secondary` | `(0.4, 0.4, 0.4)` | Secondary actions, labels |
| `Theme.Success` | `(0.2, 0.8, 0.2)` | Confirmations, positive |
| `Theme.Warning` | `(1.0, 0.8, 0.2)` | Caution, warnings |
| `Theme.Error` | `(0.8, 0.2, 0.2)` | Destructive, errors |
| `Theme.Info` | `(0.2, 0.8, 0.8)` | Informational |
| `Theme.Light` | `(0.9, 0.9, 0.9)` | Light backgrounds |
| `Theme.Dark` | `(0.2, 0.2, 0.25)` | Dark backgrounds |

### Sizes

| Token | Value | Usage |
|-------|-------|-------|
| `Theme.Size.SmallButton` | `80 x 25` | Compact buttons |
| `Theme.Size.Button` | `120 x 30` | Standard buttons |
| `Theme.Size.LargeButton` | `160 x 40` | Prominent buttons |
| `Theme.Font.Small` | `10` | Captions, footnotes |
| `Theme.Font.Normal` | `14` | Body text |
| `Theme.Font.Large` | `18` | Subheadings |
| `Theme.Font.Title` | `32` | Page titles |
| `Theme.Radius.Small` | `4` | Subtle rounding |
| `Theme.Radius.Medium` | `8` | Standard rounding |
| `Theme.Radius.Large` | `12` | Prominent rounding |

## API Documentation

Full API reference documentation is generated with [DocFX](https://dotnet.github.io/docfx/). To build locally:

```bash
# Install docfx (one-time)
dotnet tool install -g docfx

# Generate docs
docfx docs/docfx.json --serve
```

Then open `http://localhost:8080` in your browser. The generated site includes:

- **API Reference** -- Auto-generated from XML doc comments on every public type
- **Getting Started** -- Step-by-step setup guide
- **Guides** -- Theming, layouts, animations, object pooling

## Project Structure

```
bGUI/
  UI.cs                         # Static entry point
  Core/
    Abstractions/               # IUIElement, base classes, FluentBuilderBase<T,E>
    Components/                 # Component interfaces (IButton, ISlider, etc.)
    Containers/                 # Container interfaces (ICanvas, IMenu, IPage, IPanel)
    Layout/                     # Layout interfaces (IVerticalLayout, etc.)
    Extensions/                 # Builder, element, and RectTransform extensions
    Constants/                  # Theme.cs, UIConstants.cs
    Enums/                      # AnchorPreset, EasingType, etc.
    Events/                     # UIEventArgs hierarchy
    Factory/                    # UIFactory singleton
  Components/
    Builders/                   # Fluent builders (one per element type)
      Layout/                   # Layout builders
    Wrappers/                   # Unity component wrappers
      Layout/                   # Layout wrappers
    Animations/                 # Fade, Move, Pulse, Rotate, Sequence, Interactive
  Services/                     # PoolingService
  Utilities/                    # LayoutUtils, RoundedRectGenerator
  Samples/                      # Example mods (excluded from build)
  docs/                         # DocFX documentation source
```

## Samples

The `Samples/` directory contains complete example mods (excluded from library compilation):

- **BasicExample** -- Minimal F1-toggle menu demonstrating canvas, panel, and text creation
- **ModSettingsMenu** -- Full settings UI with MelonPreferences integration, sliders, toggles, and animations
- **DemoMenu** -- Multi-page showcase with buttons, layouts, styles, and animation demos

## License

[MIT](LICENSE) -- Copyright (c) 2025 ifBars
