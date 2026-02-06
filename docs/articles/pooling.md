# Object Pooling Guide

bGUI includes an object pooling system to improve performance by reusing UI elements instead of creating and destroying them repeatedly.

## When to Use Pooling

Object pooling is beneficial when:

- Creating many UI elements dynamically (lists, grids, popups)
- Elements are shown/hidden frequently
- Memory allocation spikes cause stuttering
- Garbage collection is a concern

Pooling is **not necessary** for:

- Static UI that stays open
- Small menus with few elements
- One-time created elements

## Enabling Pooling

### Global Pooling

Enable pooling for all elements by default:

```csharp
public override void OnInitializeMelon()
{
    // Enable pooling globally
    UI.EnablePooling();
}
```

Now all `UI.Button()`, `UI.Panel()`, etc. calls will use pooled objects when available.

### Per-Element Pooling

Enable pooling for individual elements:

```csharp
// This element will use pooling even if global pooling is off
UI.Button(parent).Build();  // Depends on global flag

// To explicitly control, manage UsePoolingByDefault before building
UI.UsePoolingByDefault = true;
var btn1 = UI.Button(parent).Build();  // Pooled
var btn2 = UI.Button(parent).Build();  // Pooled
UI.UsePoolingByDefault = false;
var btn3 = UI.Button(parent).Build();  // Not pooled
```

## Using Pooled Elements

### Returning Elements to Pool

Instead of destroying elements, return them to the pool:

```csharp
// Instead of this (creates garbage):
button.Destroy();

// Do this (reusable):
UI.ReturnToPool(button);
```

### Complete Example

```csharp
public class DynamicList : MelonMod
{
    private CanvasWrapper _canvas;
    private PanelWrapper _listContainer;
    private List<PanelWrapper> _items = new List<PanelWrapper>();

    public override void OnInitializeMelon()
    {
        UI.EnablePooling();
        CreateUI();
    }

    private void CreateUI()
    {
        _canvas = UI.Canvas("ListCanvas")
            .SetRenderMode(RenderMode.ScreenSpaceOverlay)
            .SetSortingOrder(100)
            .Build();

        _listContainer = UI.Panel(_canvas.RectTransform)
            .SetSize(400, 600)
            .SetAnchor(0.5f, 0.5f)
            .WithVerticalLayout(v => v
                .SetPadding(10)
                .SetSpacing(5))
            .Build();
    }

    // Add item using pooled elements
    private void AddItem(string text)
    {
        var item = UI.Panel(_listContainer.RectTransform)
            .SetHeight(50)
            .SetBackgroundColor(Theme.Dark)
            .WithHorizontalLayout(h => h
                .SetPadding(10, 0)
                .SetSpacing(10))
            .Build();

        UI.Text(item.RectTransform)
            .SetContent(text)
            .SetAlignment(TextAnchor.MiddleLeft)
            .Build();

        UI.Button(item.RectTransform)
            .SetText("X")
            .Small()
            .Error()
            .OnClick(() => RemoveItem(item))
            .Build();

        _items.Add(item);
    }

    // Return to pool instead of destroying
    private void RemoveItem(PanelWrapper item)
    {
        _items.Remove(item);
        UI.ReturnToPool(item);  // Recycled for reuse
    }

    // Clear all items efficiently
    private void ClearAllItems()
    {
        foreach (var item in _items)
        {
            UI.ReturnToPool(item);
        }
        _items.Clear();
    }
}
```

## How Pooling Works

### Internal Mechanics

1. **PoolingService** maintains dictionaries of pooled objects by type
2. When creating an element:
   - If pooling enabled and object available → reuse pooled object
   - Otherwise → create new instance
3. When returning to pool:
   - Element is deactivated and stored
   - Next request returns stored element

### Pool Structure

```
PoolingService
├── Type: ButtonWrapper
│   └── List<ButtonWrapper> [pooled instances]
├── Type: PanelWrapper
│   └── List<PanelWrapper> [pooled instances]
├── Type: TextWrapper
│   └── List<TextWrapper> [pooled instances]
└── ... (one list per wrapper type)
```

## Best Practices

### 1. Return to Pool, Don't Destroy

```csharp
// BAD - Creates garbage
foreach (var btn in buttons)
{
    btn.Destroy();
}

// GOOD - Reusable
foreach (var btn in buttons)
{
    UI.ReturnToPool(btn);
}
buttons.Clear();
```

### 2. Reset State After Reuse

Pooled elements retain their state from previous use. Reset important properties:

```csharp
private void AddItem()
{
    var item = UI.Panel(_container.RectTransform)
        // Reset state from previous use
        .SetBackgroundColor(Theme.Dark)  // Reset color
        .SetAlpha(1)                      // Reset visibility
        .Build();

    // Explicitly reset other state if needed
    item.GameObject.SetActive(true);
}
```

### 3. Use for Dynamic Lists

Pooling shines with dynamic lists:

```csharp
public void RefreshInventory(List<Item> items)
{
    // Return old items to pool
    foreach (var slot in _slots)
    {
        UI.ReturnToPool(slot);
    }
    _slots.Clear();

    // Create new items (uses pooled if available)
    foreach (var item in items)
    {
        var slot = CreateInventorySlot(item);
        _slots.Add(slot);
    }
}
```

### 4. Disable When Not Needed

Turn off pooling if not needed to avoid overhead:

```csharp
public override void OnInitializeMelon()
{
    // Static UI - don't pool
    UI.DisablePooling();
}
```

### 5. Clear Pools on Scene Change

If you need to clear pools (usually not necessary):

```csharp
// Clear all pools
UIFactory.Instance.ClearAllPools();

// Or clear specific type
UIFactory.Instance.ClearPool<ButtonWrapper>();
```

## Pooling Limitations

Not all elements support pooling equally well:

| Element | Pooling Support | Notes |
|---------|-----------------|-------|
| Button | Full | Full state reset |
| Panel | Full | Full state reset |
| Text | Full | Full state reset |
| Slider | Full | Full state reset |
| Toggle | Full | Full state reset |
| Dropdown | Full | Full state reset |
| ToggleGroup | Partial | Simple wrapper, basic support |

## Performance Comparison

### Without Pooling (Dynamic List)

```
Creating 50 items:     15-20ms (GC allocation spike)
Destroying 50 items:   5-10ms + GC pressure
Repeated cycles:       Progressive GC pauses
```

### With Pooling (Dynamic List)

```
First creation:        15-20ms (same as without)
Reuse 50 items:        0.1-0.5ms (minimal allocation)
Repeated cycles:       Consistent performance
```

## Common Patterns

### Scrollable List with Pooling

```csharp
public class PooledScrollList
{
    private const int VISIBLE_ITEMS = 10;
    private List<PanelWrapper> _visibleItems = new List<PanelWrapper>();
    private List<string> _data = new List<string>();

    private void UpdateVisibleItems(int scrollIndex)
    {
        // Return old items
        foreach (var item in _visibleItems)
        {
            UI.ReturnToPool(item);
        }
        _visibleItems.Clear();

        // Show new window of items
        for (int i = scrollIndex; i < scrollIndex + VISIBLE_ITEMS && i < _data.Count; i++)
        {
            var item = CreateListItem(_data[i]);
            _visibleItems.Add(item);
        }
    }

    private PanelWrapper CreateListItem(string text)
    {
        return UI.Panel(_container.RectTransform)
            .SetHeight(40)
            .WithHorizontalLayout(h => h.SetPadding(10))
            .Build();
    }
}
```

### Popup Manager with Pooling

```csharp
public class PopupManager
{
    private Stack<PanelWrapper> _popupPool = new Stack<PanelWrapper>();

    public void ShowPopup(string message)
    {
        PanelWrapper popup;

        // Try to reuse from our local pool
        if (_popupPool.Count > 0)
        {
            popup = _popupPool.Pop();
            popup.GameObject.SetActive(true);
        }
        else
        {
            popup = CreatePopup();
        }

        // Configure popup
        var text = popup.GameObject.GetComponentInChildren<Text>();
        if (text != null) text.text = message;

        // Show with animation
        popup.FadeIn(0.3f);
    }

    public void HidePopup(PanelWrapper popup)
    {
        popup.FadeOut(0.2f, onComplete: () => {
            popup.GameObject.SetActive(false);
            _popupPool.Push(popup);
        });
    }
}
```

## Debugging Pool Usage

```csharp
// Check pool status (if you add this to PoolingService)
public class PoolingDiagnostics
{
    public void LogPoolStatus()
    {
        MelonLogger.Msg("=== Pool Status ===");
        MelonLogger.Msg($"Button pool: {GetPoolCount<ButtonWrapper>()}");
        MelonLogger.Msg($"Panel pool: {GetPoolCount<PanelWrapper>()}");
        MelonLogger.Msg($"Text pool: {GetPoolCount<TextWrapper>()}");
    }
}
```
