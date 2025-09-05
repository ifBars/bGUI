using UnityEngine;

namespace bGUI.Core.Enums
{
    /// <summary>
    /// Defines common anchor presets for UI elements.
    /// </summary>
    public enum AnchorPreset
    {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
        StretchLeft,
        StretchCenter,
        StretchRight,
        StretchTop,
        StretchMiddle,
        StretchBottom,
        StretchFull
    }

    /// <summary>
    /// Defines interaction types for UI components.
    /// </summary>
    public enum InteractionType
    {
        Click,
        Hover,
        Focus,
        Unfocus,
        DragStart,
        Drag,
        DragEnd,
        ValueChanged,
        StateChanged,
        Enabled,
        Disabled,
        Selected,
        Deselected
    }

    /// <summary>
    /// Defines layout change types.
    /// </summary>
    public enum LayoutChangeType
    {
        ChildAdded,
        ChildRemoved,
        ChildOrderChanged,
        SpacingChanged,
        PaddingChanged,
        AlignmentChanged,
        SizeChanged,
        ConstraintChanged
    }

    /// <summary>
    /// Defines navigation directions.
    /// </summary>
    public enum NavigationDirection
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Backward,
        Previous,
        Next
    }

    /// <summary>
    /// Defines UI element states.
    /// </summary>
    public enum UIState
    {
        Normal,
        Highlighted,
        Pressed,
        Selected,
        Disabled,
        Hidden,
        Loading,
        Error,
        Success,
        Warning
    }

    /// <summary>
    /// Defines layout directions for flex layouts.
    /// </summary>
    public enum LayoutDirection
    {
        Horizontal,
        Vertical,
        Grid
    }

    /// <summary>
    /// Defines sizing modes for UI elements.
    /// </summary>
    public enum SizingMode
    {
        Fixed,
        Flexible,
        Preferred,
        Minimum,
        FitContent,
        FillParent
    }

    /// <summary>
    /// Defines animation easing types.
    /// </summary>
    public enum EasingType
    {
        Linear,
        EaseIn,
        EaseOut,
        EaseInOut,
        EaseInBack,
        EaseOutBack,
        EaseInOutBack,
        EaseInElastic,
        EaseOutElastic,
        EaseInOutElastic,
        EaseInBounce,
        EaseOutBounce,
        EaseInOutBounce
    }

    /// <summary>
    /// Helper class for converting anchor presets to Unity anchor values.
    /// </summary>
    public static class AnchorPresetHelper
    {
        /// <summary>
        /// Converts an AnchorPreset to the corresponding anchor min and max values.
        /// </summary>
        /// <param name="preset">The anchor preset to convert</param>
        /// <returns>A tuple containing anchor min and anchor max values</returns>
        public static (Vector2 anchorMin, Vector2 anchorMax) GetAnchors(AnchorPreset preset)
        {
            return preset switch
            {
                AnchorPreset.TopLeft => (new Vector2(0f, 1f), new Vector2(0f, 1f)),
                AnchorPreset.TopCenter => (new Vector2(0.5f, 1f), new Vector2(0.5f, 1f)),
                AnchorPreset.TopRight => (new Vector2(1f, 1f), new Vector2(1f, 1f)),
                AnchorPreset.MiddleLeft => (new Vector2(0f, 0.5f), new Vector2(0f, 0.5f)),
                AnchorPreset.MiddleCenter => (new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f)),
                AnchorPreset.MiddleRight => (new Vector2(1f, 0.5f), new Vector2(1f, 0.5f)),
                AnchorPreset.BottomLeft => (new Vector2(0f, 0f), new Vector2(0f, 0f)),
                AnchorPreset.BottomCenter => (new Vector2(0.5f, 0f), new Vector2(0.5f, 0f)),
                AnchorPreset.BottomRight => (new Vector2(1f, 0f), new Vector2(1f, 0f)),
                AnchorPreset.StretchLeft => (new Vector2(0f, 0f), new Vector2(0f, 1f)),
                AnchorPreset.StretchCenter => (new Vector2(0.5f, 0f), new Vector2(0.5f, 1f)),
                AnchorPreset.StretchRight => (new Vector2(1f, 0f), new Vector2(1f, 1f)),
                AnchorPreset.StretchTop => (new Vector2(0f, 1f), new Vector2(1f, 1f)),
                AnchorPreset.StretchMiddle => (new Vector2(0f, 0.5f), new Vector2(1f, 0.5f)),
                AnchorPreset.StretchBottom => (new Vector2(0f, 0f), new Vector2(1f, 0f)),
                AnchorPreset.StretchFull => (new Vector2(0f, 0f), new Vector2(1f, 1f)),
                _ => (new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f))
            };
        }

        /// <summary>
        /// Gets the default pivot point for an anchor preset.
        /// </summary>
        /// <param name="preset">The anchor preset</param>
        /// <returns>The default pivot point</returns>
        public static Vector2 GetDefaultPivot(AnchorPreset preset)
        {
            return preset switch
            {
                AnchorPreset.TopLeft => new Vector2(0f, 1f),
                AnchorPreset.TopCenter => new Vector2(0.5f, 1f),
                AnchorPreset.TopRight => new Vector2(1f, 1f),
                AnchorPreset.MiddleLeft => new Vector2(0f, 0.5f),
                AnchorPreset.MiddleCenter => new Vector2(0.5f, 0.5f),
                AnchorPreset.MiddleRight => new Vector2(1f, 0.5f),
                AnchorPreset.BottomLeft => new Vector2(0f, 0f),
                AnchorPreset.BottomCenter => new Vector2(0.5f, 0f),
                AnchorPreset.BottomRight => new Vector2(1f, 0f),
                _ => new Vector2(0.5f, 0.5f)
            };
        }
    }
} 