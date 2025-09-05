using bGUI.Core.Constants;
using bGUI.Components;

namespace bGUI.Core.Extensions
{
    /// <summary>
    /// Extension methods for builders to provide simple theming and styling.
    /// </summary>
    public static class BuilderExtensions
    {
        #region Button Theme Extensions
        
        /// <summary>
        /// Applies primary theme to the button.
        /// </summary>
        public static ButtonBuilder Primary(this ButtonBuilder builder)
        {
            return builder.SetColors(Theme.PrimaryButton);
        }

        /// <summary>
        /// Applies secondary theme to the button.
        /// </summary>
        public static ButtonBuilder Secondary(this ButtonBuilder builder)
        {
            return builder.SetColors(Theme.SecondaryButton);
        }

        /// <summary>
        /// Applies success theme to the button.
        /// </summary>
        public static ButtonBuilder Success(this ButtonBuilder builder)
        {
            return builder.SetColors(Theme.SuccessButton);
        }

        /// <summary>
        /// Applies warning theme to the button.
        /// </summary>
        public static ButtonBuilder Warning(this ButtonBuilder builder)
        {
            return builder.SetColors(Theme.WarningButton);
        }

        /// <summary>
        /// Applies error theme to the button.
        /// </summary>
        public static ButtonBuilder Error(this ButtonBuilder builder)
        {
            return builder.SetColors(Theme.ErrorButton);
        }

        /// <summary>
        /// Applies info theme to the button.
        /// </summary>
        public static ButtonBuilder Info(this ButtonBuilder builder)
        {
            return builder.SetColors(Theme.InfoButton);
        }

        /// <summary>
        /// Applies light theme to the button.
        /// </summary>
        public static ButtonBuilder Light(this ButtonBuilder builder)
        {
            return builder.SetColors(Theme.LightButton);
        }

        /// <summary>
        /// Applies dark theme to the button.
        /// </summary>
        public static ButtonBuilder Dark(this ButtonBuilder builder)
        {
            return builder.SetColors(Theme.DarkButton);
        }

        #endregion

        #region Size Extensions

        /// <summary>
        /// Sets the button to small size.
        /// </summary>
        public static ButtonBuilder Small(this ButtonBuilder builder)
        {
            return builder.SetSize(Theme.Size.SmallButton);
        }

        /// <summary>
        /// Sets the button to default size.
        /// </summary>
        public static ButtonBuilder DefaultSize(this ButtonBuilder builder)
        {
            return builder.SetSize(Theme.Size.Button);
        }

        /// <summary>
        /// Sets the button to large size.
        /// </summary>
        public static ButtonBuilder Large(this ButtonBuilder builder)
        {
            return builder.SetSize(Theme.Size.LargeButton);
        }

        #endregion

        #region Text Builder Extensions

        /// <summary>
        /// Sets the text color to primary.
        /// </summary>
        public static TextBuilder Primary(this TextBuilder builder)
        {
            return builder.SetColor(Theme.Primary);
        }

        /// <summary>
        /// Sets the text color to secondary.
        /// </summary>
        public static TextBuilder Secondary(this TextBuilder builder)
        {
            return builder.SetColor(Theme.Secondary);
        }

        /// <summary>
        /// Sets the text color to success.
        /// </summary>
        public static TextBuilder Success(this TextBuilder builder)
        {
            return builder.SetColor(Theme.Success);
        }

        /// <summary>
        /// Sets the text color to warning.
        /// </summary>
        public static TextBuilder Warning(this TextBuilder builder)
        {
            return builder.SetColor(Theme.Warning);
        }

        /// <summary>
        /// Sets the text color to error.
        /// </summary>
        public static TextBuilder Error(this TextBuilder builder)
        {
            return builder.SetColor(Theme.Error);
        }

        /// <summary>
        /// Sets the text to title size.
        /// </summary>
        public static TextBuilder Title(this TextBuilder builder)
        {
            return builder.SetFontSize(Theme.Font.Title);
        }

        /// <summary>
        /// Sets the text to heading size.
        /// </summary>
        public static TextBuilder Heading(this TextBuilder builder)
        {
            return builder.SetFontSize(Theme.Font.ExtraLarge);
        }

        /// <summary>
        /// Sets the text to small size.
        /// </summary>
        public static TextBuilder Small(this TextBuilder builder)
        {
            return builder.SetFontSize(Theme.Font.Small);
        }

        /// <summary>
        /// Sets the text to large size.
        /// </summary>
        public static TextBuilder Large(this TextBuilder builder)
        {
            return builder.SetFontSize(Theme.Font.Large);
        }

        #endregion

        #region Panel Builder Extensions

        /// <summary>
        /// Sets the panel background to primary color.
        /// </summary>
        public static PanelBuilder Primary(this PanelBuilder builder)
        {
            return builder.SetBackgroundColor(Theme.Primary);
        }

        /// <summary>
        /// Sets the panel background to secondary color.
        /// </summary>
        public static PanelBuilder Secondary(this PanelBuilder builder)
        {
            return builder.SetBackgroundColor(Theme.Secondary);
        }

        /// <summary>
        /// Sets the panel background to dark color.
        /// </summary>
        public static PanelBuilder Dark(this PanelBuilder builder)
        {
            return builder.SetBackgroundColor(Theme.Dark);
        }

        /// <summary>
        /// Sets the panel background to light color.
        /// </summary>
        public static PanelBuilder Light(this PanelBuilder builder)
        {
            return builder.SetBackgroundColor(Theme.Light);
        }

        #endregion

        #region Common Styling Extensions

        /// <summary>
        /// Applies rounded corners with small radius.
        /// </summary>
        public static T Rounded<T>(this T builder, int radius = Theme.Radius.Medium) where T : class
        {
            if (builder is ButtonBuilder buttonBuilder)
                return (T)(object)buttonBuilder.SetRounded(radius);
            if (builder is PanelBuilder panelBuilder)
                return (T)(object)panelBuilder.SetRounded(radius);
            
            return builder;
        }

        /// <summary>
        /// Applies small rounded corners.
        /// </summary>
        public static T RoundedSmall<T>(this T builder) where T : class
        {
            return builder.Rounded(Theme.Radius.Small);
        }

        /// <summary>
        /// Applies large rounded corners.
        /// </summary>
        public static T RoundedLarge<T>(this T builder) where T : class
        {
            return builder.Rounded(Theme.Radius.Large);
        }

        /// <summary>
        /// Makes the element circular (for square elements).
        /// </summary>
        public static T Circle<T>(this T builder) where T : class
        {
            return builder.Rounded(Theme.Radius.Round);
        }

        #endregion

        #region Wrapper Extensions

        /// <summary>
        /// Sets the text color to primary for TextWrapper.
        /// </summary>
        public static TextWrapper Primary(this TextWrapper wrapper)
        {
            return wrapper.SetColor(Theme.Primary);
        }

        /// <summary>
        /// Sets the text color to secondary for TextWrapper.
        /// </summary>
        public static TextWrapper Secondary(this TextWrapper wrapper)
        {
            return wrapper.SetColor(Theme.Secondary);
        }

        /// <summary>
        /// Sets the text color to success for TextWrapper.
        /// </summary>
        public static TextWrapper Success(this TextWrapper wrapper)
        {
            return wrapper.SetColor(Theme.Success);
        }

        /// <summary>
        /// Sets the text color to warning for TextWrapper.
        /// </summary>
        public static TextWrapper Warning(this TextWrapper wrapper)
        {
            return wrapper.SetColor(Theme.Warning);
        }

        /// <summary>
        /// Sets the text color to error for TextWrapper.
        /// </summary>
        public static TextWrapper Error(this TextWrapper wrapper)
        {
            return wrapper.SetColor(Theme.Error);
        }

        /// <summary>
        /// Sets the text color to info for TextWrapper.
        /// </summary>
        public static TextWrapper Info(this TextWrapper wrapper)
        {
            return wrapper.SetColor(Theme.Info);
        }

        /// <summary>
        /// Sets the text to small size for TextWrapper.
        /// </summary>
        public static TextWrapper Small(this TextWrapper wrapper)
        {
            return wrapper.SetFontSize(Theme.Font.Small);
        }

        /// <summary>
        /// Sets the text to large size for TextWrapper.
        /// </summary>
        public static TextWrapper Large(this TextWrapper wrapper)
        {
            return wrapper.SetFontSize(Theme.Font.Large);
        }

        /// <summary>
        /// Sets the button to small size for ButtonWrapper.
        /// </summary>
        public static ButtonWrapper Small(this ButtonWrapper wrapper)
        {
            wrapper.RectTransform.sizeDelta = Theme.Size.SmallButton;
            return wrapper;
        }

        #endregion

        #region Animation Extensions

        /// <summary>
        /// Adds a fade in animation.
        /// </summary>
        public static T FadeIn<T>(this T builder, float duration = Theme.Anim.Normal) where T : class
        {
            // This would need to be implemented in the FluentBuilderBase
            // For now, return the builder as-is
            return builder;
        }

        #endregion
    }
} 