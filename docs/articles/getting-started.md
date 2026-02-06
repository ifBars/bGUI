# Getting Started with bGUI

This guide walks you through installing bGUI and creating your first mod menu.

## Prerequisites

- A Unity game with [MelonLoader](https://melonwiki.xyz/) installed
- Basic knowledge of C# and Unity
- A C# IDE (Visual Studio, Rider, or VS Code)

## Installation

### Step 1: Build bGUI

1. Clone or download the bGUI repository
2. Update the DLL references in `bGUI.csproj` to match your game path:

```xml
<!-- Update these paths to your game installation -->
<Reference Include="MelonLoader">
    <HintPath>D:\SteamLibrary\steamapps\common\YourGame\MelonLoader\net35\MelonLoader.dll</HintPath>
</Reference>
```

3. Build the project:

```bash
dotnet build bGUI.sln -c Release
```

### Step 2: Reference bGUI in Your Mod

Copy the built `bGUI.dll` from `bin/Release/netstandard2.1/` into your mod project and add a reference to it.

### Step 3: Create Your First Menu

Here's a minimal example that toggles a menu when you press F1:

```csharp
using MelonLoader;
using UnityEngine;
using bGUI;
using bGUI.Components;

[assembly: MelonInfo(typeof(MyMod), "MyMod", "1.0.0", "YourName")]
[assembly: MelonGame("GameDev", "YourGame")]

namespace MyMod
{
    public class MyMod : MelonMod
    {
        private CanvasWrapper? _canvas;
        private PanelWrapper? _menuPanel;
        private bool _isMenuVisible = false;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("MyMod initialized! Press F1 to toggle menu.");
        }

        public override void OnUpdate()
        {
            // Toggle menu with F1
            if (Input.GetKeyDown(KeyCode.F1))
            {
                _isMenuVisible = !_isMenuVisible;

                if (_isMenuVisible)
                {
                    if (_canvas == null) CreateUI();
                    _menuPanel?.GameObject.SetActive(true);
                }
                else
                {
                    _menuPanel?.GameObject.SetActive(false);
                }
            }
        }

        private void CreateUI()
        {
            // Create a canvas for our UI
            _canvas = UI.Canvas("MyModCanvas")
                .SetRenderMode(RenderMode.ScreenSpaceOverlay)
                .SetSortingOrder(100)  // Render on top
                .SetReferenceResolution(1920, 1080)
                .Build();

            // Create a centered panel
            _menuPanel = UI.Panel(_canvas.RectTransform)
                .SetSize(400, 300)
                .SetAnchor(0.5f, 0.5f)  // Center anchor
                .SetBackgroundColor(new Color(0.1f, 0.1f, 0.15f, 0.95f))
                .SetRounded(12)  // Rounded corners
                .Build();

            // Add a title
            UI.Text(_menuPanel.RectTransform)
                .SetContent("My Mod Menu")
                .SetFontSize(18)
                .SetColor(Color.white)
                .SetSize(370, 30)
                .Build();

            // Add a button
            UI.Button(_menuPanel.RectTransform)
                .SetText("Click Me!")
                .SetSize(150, 35)
                .Primary()  // Use primary color theme
                .OnClick(() => MelonLogger.Msg("Button clicked!"))
                .Build();
        }
    }
}
```

## Understanding the Fluent API

bGUI uses a **fluent builder pattern** where you chain method calls to configure UI elements:

```csharp
UI.Button(parent)
    .SetText("Save")           // Configure text
    .SetSize(120, 30)          // Set size
    .Primary()                 // Apply theme
    .OnClick(() => { })        // Add event handler
    .Build();                  // Finalize and return wrapper
```

### Key Concepts

1. **Static Facade (`UI` class)** - Entry point for all creation
   - `UI.Button()`, `UI.Panel()`, `UI.Text()`, etc.

2. **Builders** - Configure elements before creation
   - Each method returns `this` for chaining
   - Fluent, readable configuration

3. **Wrappers** - The resulting UI element
   - Implements interfaces like `IButton`, `IPanel`
   - Wraps Unity's UGUI components
   - Access via `.RectTransform`, `.GameObject`

4. **Themes** - Semantic color and size presets
   - `.Primary()`, `.Success()`, `.Error()` for colors
   - `.Small()`, `.DefaultSize()`, `.Large()` for sizing

## Next Steps

- Learn about [Theming](theming.md) to customize your UI appearance
- Explore [Layouts](layouts.md) to organize elements automatically
- Add [Animations](animations.md) for polished interactions
- Check the [API Guide](api-guide.md) for complete element reference
