# Animations Guide

bGUI provides animation support for creating polished UI interactions. Animations can be applied to any UI element to add fade effects, movement, pulsing, rotation, and more.

## Basic Animation Usage

All animations work by attaching an animation component to a wrapper and updating it every frame.

### Fade Animation

Fades an element in and out using alpha transparency:

```csharp
using bGUI.Components.Animations;

// Create fade animation
var fadeAnim = new FadeAnimation(
    element: panel,
    minAlpha: 0.2f,      // Minimum alpha
    maxAlpha: 1.0f,      // Maximum alpha
    speed: 2f            // Speed of oscillation
);

// Enable the animation
fadeAnim.SetEnabled(true);
```

### Move Animation

Moves an element up and down in a wave pattern:

```csharp
// Create move animation
var moveAnim = new MoveAnimation(
    element: panel,
    amplitude: 10f,      // Pixels to move up/down
    speed: 3f            // Speed of movement
);

moveAnim.SetEnabled(true);
```

### Pulse Animation

Scales an element larger and smaller:

```csharp
// Create pulse animation
var pulseAnim = new PulseAnimation(
    element: button,
    minScale: 0.9f,      // Minimum scale
    maxScale: 1.1f,      // Maximum scale
    speed: 4f            // Speed of pulsing
);

pulseAnim.SetEnabled(true);
```

### Rotate Animation

Rotates an element continuously:

```csharp
// Create rotate animation
var rotateAnim = new RotateAnimation(
    element: icon,
    speed: 180f          // Degrees per second
);

rotateAnim.SetEnabled(true);
```

## Interactive Animations

Interactive animations are one-shot effects that run once and then stop, perfect for button feedback:

```csharp
using bGUI.Components.Animations;

// Bounce effect when clicked
UI.Button(parent)
    .SetText("Bounce")
    .OnClick(() => {
        InteractiveAnimations.Bounce(button);
    })
    .Build();

// Shake effect for error feedback
UI.Button(parent)
    .SetText("Submit")
    .OnClick(() => {
        if (!isValid)
        {
            InteractiveAnimations.Shake(inputField);
        }
    })
    .Build();

// Flash effect for attention
InteractiveAnimations.Flash(importantButton);
```

### Interactive Animation Methods

| Method | Description | Duration |
|--------|-------------|----------|
| `Bounce(element)` | Scales down then up | 0.3s |
| `Shake(element)` | Shakes left-right | 0.4s |
| `Flash(element)` | Flashes white then back | 0.3s |

## Sequence Animation

Highlights multiple elements in sequence:

```csharp
// Create a sequence
var sequence = new SequenceAnimation(
    elements: new List<PanelWrapper> { panel1, panel2, panel3, panel4 },
    activeColor: new Color(0.3f, 0.6f, 1f),
    inactiveColor: Theme.Dark,
    highlightDuration: 0.5f,
    loop: true
);

sequence.SetEnabled(true);
```

## Menu Open Animation

A common pattern is animating a menu when it opens:

```csharp
public class MyMod : MelonMod
{
    private PanelWrapper _menuPanel;
    private CanvasGroup _menuCanvasGroup;

    private void CreateUI()
    {
        // Create menu
        _menuPanel = UI.Panel(_canvas.RectTransform)
            .SetSize(400, 500)
            .SetAlpha(0)  // Start invisible
            .SetScale(0.9f, 0.9f)  // Start slightly smaller
            .Build();

        _menuCanvasGroup = _menuPanel.GameObject.GetComponent<CanvasGroup>();
        if (_menuCanvasGroup == null)
            _menuCanvasGroup = _menuPanel.GameObject.AddComponent<CanvasGroup>();

        // Start open animation
        MelonCoroutines.Start(AnimateMenuOpen());
    }

    private IEnumerator AnimateMenuOpen()
    {
        float duration = 0.3f;
        float elapsed = 0f;

        _menuPanel.GameObject.SetActive(true);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Fade in
            _menuCanvasGroup.alpha = Mathf.Lerp(0, 1, t);

            // Scale up
            float scale = Mathf.Lerp(0.9f, 1f, t);
            _menuPanel.RectTransform.localScale = new Vector3(scale, scale, 1);

            yield return null;
        }

        _menuCanvasGroup.alpha = 1;
        _menuPanel.RectTransform.localScale = Vector3.one;
    }
}
```

## Builder Animation Extensions

You can apply fade-in effects directly through the builder:

```csharp
// Fade in when built
UI.Panel(parent)
    .SetSize(400, 300)
    .SetBackgroundColor(Theme.Dark)
    .WithFadeIn(0.5f, onComplete: () => {
        MelonLogger.Msg("Animation complete!");
    })
    .Build();
```

## Managing Animations

### Stopping Animations

```csharp
// Disable an animation
fadeAnim.SetEnabled(false);

// Reset to initial state
fadeAnim.Reset();
```

### Animation Lifecycle

```csharp
public class MenuWithAnimations
{
    private List<BaseAnimation> _animations = new List<BaseAnimation>();

    private void CreateUI()
    {
        // Create animations
        var pulse = new PulseAnimation(title, 0.95f, 1.05f, 2f);
        var fade = new FadeAnimation(subtitle, 0.5f, 1f, 1.5f);

        _animations.Add(pulse);
        _animations.Add(fade);

        // Start all animations
        foreach (var anim in _animations)
        {
            anim.SetEnabled(true);
        }
    }

    private void OnDestroy()
    {
        // Clean up animations
        foreach (var anim in _animations)
        {
            anim.SetEnabled(false);
        }
        _animations.Clear();
    }
}
```

## Animation Best Practices

1. **Keep animations subtle** - UI should enhance, not distract
2. **Use consistent timing** - Standard duration is 0.2-0.3s for feedback
3. **Respect user preferences** - Allow disabling animations
4. **Clean up** - Disable animations when UI is destroyed
5. **Combine effects** - Layer multiple subtle animations for depth

## Animation Timing Constants

Use `Theme.Anim` for consistent animation durations:

```csharp
// Fast feedback
.WithFadeIn(Theme.Anim.Fast)      // 0.15s

// Standard transition
.WithFadeIn(Theme.Anim.Normal)    // 0.3s

// Dramatic effect
.WithFadeIn(Theme.Anim.Slow)      // 0.5s
```

## Custom Animations

You can create custom animations by inheriting from `BaseAnimation`:

```csharp
using bGUI.Components.Animations;

public class ColorShiftAnimation : BaseAnimation
{
    private readonly Color[] _colors;
    private readonly float _speed;
    private Image _image;
    private int _currentIndex;
    private float _timer;

    public ColorShiftAnimation(IUIElement element, Color[] colors, float speed) : base(element)
    {
        _colors = colors;
        _speed = speed;
        _image = element.GameObject.GetComponent<Image>();
    }

    public override void Update()
    {
        if (!IsEnabled || _image == null) return;

        _timer += Time.deltaTime;

        if (_timer >= 1f / _speed)
        {
            _timer = 0f;
            _currentIndex = (_currentIndex + 1) % _colors.Length;
            _image.color = _colors[_currentIndex];
        }
    }

    public override void Reset()
    {
        _timer = 0f;
        _currentIndex = 0;
        if (_image != null)
            _image.color = _colors[0];
    }
}

// Usage
var colorAnim = new ColorShiftAnimation(
    element: panel,
    colors: new[] { Color.red, Color.green, Color.blue },
    speed: 1f
);
colorAnim.SetEnabled(true);
```
