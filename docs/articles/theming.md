# Theming Guide

bGUI provides a comprehensive theming system with semantic colors, predefined sizes, and style presets.

## Color Themes

bGUI defines semantic color tokens that you can apply to buttons, panels, and text.

### Built-in Colors

Access colors directly via the `Theme` class:

```csharp
using bGUI.Core.Constants;

Color primary = Theme.Primary;    // Blue
Color success = Theme.Success;    // Green
Color warning = Theme.Warning;    // Yellow
Color error = Theme.Error;        // Red
Color info = Theme.Info;          // Cyan
Color light = Theme.Light;        // Light gray
Color dark = Theme.Dark;          // Dark gray
```

### Button Themes

Apply semantic themes to buttons using extension methods:

```csharp
// Semantic themes
UI.Button(parent).SetText("Save").Primary().Build();
UI.Button(parent).SetText("OK").Success().Build();
UI.Button(parent).SetText("Warning").Warning().Build();
UI.Button(parent).SetText("Delete").Error().Build();
UI.Button(parent).SetText("Info").Info().Build();
UI.Button(parent).SetText("Cancel").Secondary().Build();
UI.Button(parent).SetText("Menu").Light().Build();
UI.Button(parent).SetText("Dark").Dark().Build();
```

### Panel Themes

```csharp
UI.Panel(parent)
    .SetSize(200, 100)
    .Primary()   // Background = primary color
    .Build();

UI.Panel(parent)
    .Dark()      // Dark background
    .Build();

UI.Panel(parent)
    .Light()     // Light background
    .Build();
```

### Text Themes

```csharp
UI.Text(parent)
    .SetContent("Primary text")
    .Primary()   // Primary color
    .Build();

UI.Text(parent)
    .SetContent("Error message")
    .Error()     // Error color
    .Build();
```

## Size Presets

### Button Sizes

```csharp
// Using theme size constants
UI.Button(parent).SetText("Small").Small().Build();
UI.Button(parent).SetText("Default").DefaultSize().Build();
UI.Button(parent).SetText("Large").Large().Build();

// Or set manually
UI.Button(parent)
    .SetSize(Theme.Size.Button)        // 120x30
    .SetSize(Theme.Size.LargeButton)   // 160x40
    .SetSize(Theme.Size.SmallButton)   // 80x25
    .Build();
```

### Text Sizes

```csharp
UI.Text(parent).SetContent("Title").Title().Build();      // 32pt
UI.Text(parent).SetContent("Heading").Heading().Build();  // 24pt
UI.Text(parent).SetContent("Body").Large().Build();       // 18pt
UI.Text(parent).SetContent("Caption").Small().Build();    // 10pt
```

## Rounded Corners

Create buttons and panels with rounded corners:

```csharp
// Use preset radii
UI.Button(parent).RoundedSmall().Build();   // 4px radius
UI.Button(parent).RoundedLarge().Build();   // 12px radius

// Or specify custom radius
UI.Panel(parent)
    .SetRounded(8)      // 8px radius
    .Build();

// Make it circular (for square elements)
UI.Button(parent)
    .SetSize(50, 50)
    .Circle()           // Large radius for circular shape
    .Build();
```

## Creating Custom Themes

### Custom Button Colors

```csharp
var customColor = new Color(0.6f, 0.2f, 0.8f);  // Purple

UI.Button(parent)
    .SetBackgroundColor(customColor)
    .Build();
```

### Custom Color Blocks

For more control over button states:

```csharp
var colors = new ColorBlock
{
    normalColor = new Color(0.2f, 0.6f, 1f),
    highlightedColor = new Color(0.4f, 0.7f, 1f),
    pressedColor = new Color(0.1f, 0.4f, 0.8f),
    selectedColor = new Color(0.4f, 0.7f, 1f),
    disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.5f),
    colorMultiplier = 1f,
    fadeDuration = 0.1f
};

UI.Button(parent)
    .SetColors(colors)
    .Build();
```

## Quick Builder Themes

Use `QuickBuilders` for common pre-styled elements:

```csharp
using bGUI.Core.Extensions;

// Pre-themed buttons
var save = QuickBuilders.PrimaryButton(parent, "Save");
var cancel = QuickBuilders.SecondaryButton(parent, "Cancel");
var confirm = QuickBuilders.SuccessButton(parent, "Confirm");
var delete = QuickBuilders.DangerButton(parent, "Delete");

// Text presets
var title = QuickBuilders.TitleText(parent, "Settings");
var heading = QuickBuilders.HeadingText(parent, "Section");
var label = QuickBuilders.Label(parent, "Username:");
var error = QuickBuilders.ErrorText(parent, "Invalid input");
var success = QuickBuilders.SuccessText(parent, "Saved!");

// Panel presets
var card = QuickBuilders.Card(parent);       // Light rounded card
var darkCard = QuickBuilders.DarkCard(parent); // Dark rounded card
```

## Theme Constants Reference

### Colors

| Token | Value | RGB |
|-------|-------|-----|
| `Theme.Primary` | Blue | 51, 153, 255 |
| `Theme.Secondary` | Gray | 102, 102, 102 |
| `Theme.Success` | Green | 51, 204, 51 |
| `Theme.Warning` | Yellow | 255, 204, 51 |
| `Theme.Error` | Red | 204, 51, 51 |
| `Theme.Info` | Cyan | 51, 204, 204 |
| `Theme.Light` | Light Gray | 230, 230, 230 |
| `Theme.Dark` | Dark Gray | 51, 51, 64 |

### Sizes

| Token | Value |
|-------|-------|
| `Theme.Size.SmallButton` | 80 x 25 |
| `Theme.Size.Button` | 120 x 30 |
| `Theme.Size.LargeButton` | 160 x 40 |
| `Theme.Size.TextField` | 200 x 25 |
| `Theme.Size.Icon` | 24 x 24 |

### Font Sizes

| Token | Size |
|-------|------|
| `Theme.Font.Tiny` | 8pt |
| `Theme.Font.Small` | 10pt |
| `Theme.Font.Normal` | 14pt |
| `Theme.Font.Medium` | 16pt |
| `Theme.Font.Large` | 18pt |
| `Theme.Font.ExtraLarge` | 24pt |
| `Theme.Font.Title` | 32pt |

### Spacing

| Token | Value |
|-------|-------|
| `Theme.Space.Tiny` | 2px |
| `Theme.Space.Small` | 5px |
| `Theme.Space.Medium` | 10px |
| `Theme.Space.Large` | 20px |
| `Theme.Space.ExtraLarge` | 30px |

### Corner Radius

| Token | Value |
|-------|-------|
| `Theme.Radius.None` | 0 |
| `Theme.Radius.Small` | 4 |
| `Theme.Radius.Medium` | 8 |
| `Theme.Radius.Large` | 12 |
| `Theme.Radius.Round` | 50 |

### Animation Durations

| Token | Value |
|-------|-------|
| `Theme.Anim.Fast` | 0.15s |
| `Theme.Anim.Normal` | 0.3s |
| `Theme.Anim.Slow` | 0.5s |
