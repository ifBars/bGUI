using UnityEngine;

namespace bGUI.Core.Constants
{
    /// <summary>
    /// Constants for default UI values and settings.
    /// </summary>
    public static class UIConstants
    {
        /// <summary>
        /// Default colors for UI elements.
        /// </summary>
        public static class Colors
        {
            public static readonly Color Primary = new Color(0.2f, 0.6f, 1f, 1f);
            public static readonly Color Secondary = new Color(0.4f, 0.4f, 0.4f, 1f);
            public static readonly Color Success = new Color(0.2f, 0.8f, 0.2f, 1f);
            public static readonly Color Warning = new Color(1f, 0.8f, 0.2f, 1f);
            public static readonly Color Error = new Color(0.8f, 0.2f, 0.2f, 1f);
            public static readonly Color Info = new Color(0.2f, 0.8f, 0.8f, 1f);
            
            public static readonly Color Background = new Color(0.2f, 0.2f, 0.25f, 1f);
            public static readonly Color Surface = new Color(0.3f, 0.3f, 0.35f, 1f);
            public static readonly Color OnBackground = new Color(0.9f, 0.9f, 0.9f, 1f);
            public static readonly Color OnSurface = new Color(0.8f, 0.8f, 0.8f, 1f);
            
            public static readonly Color Transparent = new Color(0f, 0f, 0f, 0f);
            public static readonly Color SemiTransparent = new Color(0f, 0f, 0f, 0.5f);
        }

        /// <summary>
        /// Default button colors.
        /// </summary>
        public static class ButtonColors
        {
            public static readonly Color Normal = new Color(0.9f, 0.9f, 0.9f, 1f);
            public static readonly Color Highlighted = new Color(0.8f, 0.8f, 0.8f, 1f);
            public static readonly Color Pressed = new Color(0.7f, 0.7f, 0.7f, 1f);
            public static readonly Color Selected = new Color(0.8f, 0.8f, 0.8f, 1f);
            public static readonly Color Disabled = new Color(0.7f, 0.7f, 0.7f, 0.5f);
        }

        /// <summary>
        /// Default sizes for UI elements.
        /// </summary>
        public static class Sizes
        {
            public const float ButtonWidth = 120f;
            public const float ButtonHeight = 30f;
            public const float TextFieldWidth = 200f;
            public const float TextFieldHeight = 25f;
            public const float ScrollbarWidth = 20f;
            public const float SliderHeight = 20f;
            public const float ToggleSize = 20f;
            
            public const float HeaderHeight = 50f;
            public const float FooterHeight = 40f;
            public const float SidebarWidth = 250f;
            
            public const float IconSize = 24f;
            public const float SmallIconSize = 16f;
            public const float LargeIconSize = 32f;
        }

        /// <summary>
        /// Default spacing and padding values.
        /// </summary>
        public static class Spacing
        {
            public const float Tiny = 2f;
            public const float Small = 5f;
            public const float Medium = 10f;
            public const float Large = 20f;
            public const float ExtraLarge = 30f;
            
            public const float LayoutSpacing = Small;
            public const float ComponentPadding = Medium;
            public const float ContainerPadding = Large;
        }

        /// <summary>
        /// Default font sizes.
        /// </summary>
        public static class FontSizes
        {
            public const int Tiny = 8;
            public const int Small = 10;
            public const int Normal = 14;
            public const int Medium = 16;
            public const int Large = 18;
            public const int ExtraLarge = 24;
            public const int Title = 32;
            public const int Heading = 28;
        }

        /// <summary>
        /// Default animation durations.
        /// </summary>
        public static class Animations
        {
            public const float FastDuration = 0.15f;
            public const float NormalDuration = 0.3f;
            public const float SlowDuration = 0.5f;
            public const float ExtraSlowDuration = 1f;
            
            public const float FadeInDuration = NormalDuration;
            public const float FadeOutDuration = NormalDuration;
            public const float SlideInDuration = NormalDuration;
            public const float SlideOutDuration = NormalDuration;
        }

        /// <summary>
        /// Default corner radius values for rounded elements.
        /// </summary>
        public static class CornerRadius
        {
            public const int None = 0;
            public const int Small = 4;
            public const int Medium = 8;
            public const int Large = 12;
            public const int ExtraLarge = 16;
            public const int Round = 50; // For circular elements
        }

        /// <summary>
        /// Common screen resolutions for reference.
        /// </summary>
        public static class Resolutions
        {
            public static readonly Vector2 FullHD = new Vector2(1920, 1080);
            public static readonly Vector2 HD = new Vector2(1280, 720);
            public static readonly Vector2 FourK = new Vector2(3840, 2160);
            public static readonly Vector2 iPad = new Vector2(1024, 768);
            public static readonly Vector2 iPhoneX = new Vector2(1125, 2436);
        }

        /// <summary>
        /// Default layer orders for UI elements.
        /// </summary>
        public static class LayerOrder
        {
            public const int Background = 0;
            public const int Content = 10;
            public const int Overlay = 20;
            public const int Modal = 30;
            public const int Tooltip = 40;
            public const int Notification = 50;
        }

        /// <summary>
        /// Safe area percentages for different screen types.
        /// </summary>
        public static class SafeArea
        {
            public const float TopPercent = 0.05f;      // 5% from top
            public const float BottomPercent = 0.05f;   // 5% from bottom
            public const float LeftPercent = 0.02f;     // 2% from left
            public const float RightPercent = 0.02f;    // 2% from right
        }

        /// <summary>
        /// Default grid layout settings.
        /// </summary>
        public static class Grid
        {
            public static readonly Vector2 DefaultCellSize = new Vector2(100f, 100f);
            public static readonly Vector2 DefaultSpacing = new Vector2(Spacing.Small, Spacing.Small);
            public const int DefaultConstraintCount = 2;
        }
    }

    /// <summary>
    /// Helper methods for working with UI constants.
    /// </summary>
    public static class UIConstantsHelper
    {
        /// <summary>
        /// Gets a color by semantic meaning.
        /// </summary>
        /// <param name="colorType">The semantic color type</param>
        /// <returns>The corresponding color</returns>
        public static Color GetSemanticColor(SemanticColorType colorType)
        {
            return colorType switch
            {
                SemanticColorType.Primary => UIConstants.Colors.Primary,
                SemanticColorType.Secondary => UIConstants.Colors.Secondary,
                SemanticColorType.Success => UIConstants.Colors.Success,
                SemanticColorType.Warning => UIConstants.Colors.Warning,
                SemanticColorType.Error => UIConstants.Colors.Error,
                SemanticColorType.Info => UIConstants.Colors.Info,
                SemanticColorType.Background => UIConstants.Colors.Background,
                SemanticColorType.Surface => UIConstants.Colors.Surface,
                _ => UIConstants.Colors.Primary
            };
        }

        /// <summary>
        /// Gets button colors for a specific state.
        /// </summary>
        /// <param name="colorType">The semantic color type for the button</param>
        /// <returns>ColorBlock with appropriate colors</returns>
        public static UnityEngine.UI.ColorBlock GetButtonColors(SemanticColorType colorType = SemanticColorType.Primary)
        {
            var baseColor = GetSemanticColor(colorType);
            
            var colors = new UnityEngine.UI.ColorBlock
            {
                normalColor = baseColor,
                highlightedColor = Color.Lerp(baseColor, Color.white, 0.1f),
                pressedColor = Color.Lerp(baseColor, Color.black, 0.1f),
                selectedColor = Color.Lerp(baseColor, Color.white, 0.1f),
                disabledColor = Color.Lerp(baseColor, Color.gray, 0.5f),
                colorMultiplier = 1f,
                fadeDuration = 0.1f
            };
            
            return colors;
        }
    }

    /// <summary>
    /// Semantic color types for UI elements.
    /// </summary>
    public enum SemanticColorType
    {
        Primary,
        Secondary,
        Success,
        Warning,
        Error,
        Info,
        Background,
        Surface
    }
} 