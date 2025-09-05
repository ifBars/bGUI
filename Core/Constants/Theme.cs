using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Core.Constants
{
    /// <summary>
    /// Simple, direct access to UI themes and colors.
    /// </summary>
    public static class Theme
    {
        // Direct color access - much simpler than helper methods
        public static readonly Color Primary = new Color(0.2f, 0.6f, 1f, 1f);
        public static readonly Color Secondary = new Color(0.4f, 0.4f, 0.4f, 1f);
        public static readonly Color Success = new Color(0.2f, 0.8f, 0.2f, 1f);
        public static readonly Color Warning = new Color(1f, 0.8f, 0.2f, 1f);
        public static readonly Color Error = new Color(0.8f, 0.2f, 0.2f, 1f);
        public static readonly Color Info = new Color(0.2f, 0.8f, 0.8f, 1f);
        public static readonly Color Light = new Color(0.9f, 0.9f, 0.9f, 1f);
        public static readonly Color Dark = new Color(0.2f, 0.2f, 0.25f, 1f);

        // Button color blocks - direct access
        public static readonly ColorBlock PrimaryButton = CreateButtonColors(Primary);
        public static readonly ColorBlock SecondaryButton = CreateButtonColors(Secondary);
        public static readonly ColorBlock SuccessButton = CreateButtonColors(Success);
        public static readonly ColorBlock WarningButton = CreateButtonColors(Warning);
        public static readonly ColorBlock ErrorButton = CreateButtonColors(Error);
        public static readonly ColorBlock InfoButton = CreateButtonColors(Info);
        public static readonly ColorBlock LightButton = CreateButtonColors(Light);
        public static readonly ColorBlock DarkButton = CreateButtonColors(Dark);

        // Common sizes - simple access
        public static class Size
        {
            public static readonly Vector2 Button = new Vector2(120f, 30f);
            public static readonly Vector2 SmallButton = new Vector2(80f, 25f);
            public static readonly Vector2 LargeButton = new Vector2(160f, 40f);
            public static readonly Vector2 TextField = new Vector2(200f, 25f);
            public static readonly Vector2 Icon = new Vector2(24f, 24f);
            public static readonly Vector2 SmallIcon = new Vector2(16f, 16f);
            public static readonly Vector2 LargeIcon = new Vector2(32f, 32f);
        }

        // Common spacing - direct access
        public static class Space
        {
            public const float Tiny = 2f;
            public const float Small = 5f;
            public const float Medium = 10f;
            public const float Large = 20f;
            public const float ExtraLarge = 30f;
        }

        // Font sizes - direct access
        public static class Font
        {
            public const int Tiny = 8;
            public const int Small = 10;
            public const int Normal = 14;
            public const int Medium = 16;
            public const int Large = 18;
            public const int ExtraLarge = 24;
            public const int Title = 32;
        }

        // Animation durations - direct access
        public static class Anim
        {
            public const float Fast = 0.15f;
            public const float Normal = 0.3f;
            public const float Slow = 0.5f;
        }

        // Corner radius values
        public static class Radius
        {
            public const int None = 0;
            public const int Small = 4;
            public const int Medium = 8;
            public const int Large = 12;
            public const int Round = 50;
        }

        /// <summary>
        /// Creates a ColorBlock for buttons from a base color.
        /// </summary>
        private static ColorBlock CreateButtonColors(Color baseColor)
        {
            return new ColorBlock
            {
                normalColor = baseColor,
                highlightedColor = Color.Lerp(baseColor, Color.white, 0.1f),
                pressedColor = Color.Lerp(baseColor, Color.black, 0.1f),
                selectedColor = Color.Lerp(baseColor, Color.white, 0.1f),
                disabledColor = Color.Lerp(baseColor, Color.gray, 0.5f),
                colorMultiplier = 1f,
                fadeDuration = 0.1f
            };
        }

        /// <summary>
        /// Creates a custom ColorBlock from a base color.
        /// </summary>
        public static ColorBlock Button(Color baseColor)
        {
            return CreateButtonColors(baseColor);
        }
    }
} 