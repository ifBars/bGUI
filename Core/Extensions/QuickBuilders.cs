using UnityEngine;
using bGUI.Components;
using bGUI.Core.Constants;

namespace bGUI.Core.Extensions
{
    /// <summary>
    /// Quick builder methods for common UI patterns.
    /// </summary>
    public static class QuickBuilders
    {
        /// <summary>
        /// Creates a primary action button with default styling.
        /// </summary>
        public static ButtonWrapper PrimaryButton(Transform parent, string text)
        {
            return UI.Button(parent)
                .SetText(text)
                .Primary()
                .DefaultSize()
                .Build();
        }

        /// <summary>
        /// Creates a secondary button with default styling.
        /// </summary>
        public static ButtonWrapper SecondaryButton(Transform parent, string text)
        {
            return UI.Button(parent)
                .SetText(text)
                .Secondary()
                .DefaultSize()
                .Build();
        }

        /// <summary>
        /// Creates a success button (e.g., for confirmations).
        /// </summary>
        public static ButtonWrapper SuccessButton(Transform parent, string text)
        {
            return UI.Button(parent)
                .SetText(text)
                .Success()
                .DefaultSize()
                .Build();
        }

        /// <summary>
        /// Creates a danger/error button (e.g., for deletions).
        /// </summary>
        public static ButtonWrapper DangerButton(Transform parent, string text)
        {
            return UI.Button(parent)
                .SetText(text)
                .Error()
                .DefaultSize()
                .Build();
        }

        /// <summary>
        /// Creates a title text element.
        /// </summary>
        public static TextWrapper TitleText(Transform parent, string text)
        {
            return UI.Text(parent)
                .SetContent(text)
                .Title()
                .Primary()
                .Build();
        }

        /// <summary>
        /// Creates a heading text element.
        /// </summary>
        public static TextWrapper HeadingText(Transform parent, string text)
        {
            return UI.Text(parent)
                .SetContent(text)
                .Heading()
                .Primary()
                .Build();
        }

        /// <summary>
        /// Creates a label text element.
        /// </summary>
        public static TextWrapper Label(Transform parent, string text)
        {
            return UI.Text(parent)
                .SetContent(text)
                .Secondary()
                .Build();
        }

        /// <summary>
        /// Creates an error message text.
        /// </summary>
        public static TextWrapper ErrorText(Transform parent, string text)
        {
            return UI.Text(parent)
                .SetContent(text)
                .Error()
                .Small()
                .Build();
        }

        /// <summary>
        /// Creates a success message text.
        /// </summary>
        public static TextWrapper SuccessText(Transform parent, string text)
        {
            return UI.Text(parent)
                .SetContent(text)
                .Success()
                .Small()
                .Build();
        }

        /// <summary>
        /// Creates a card-style panel with default styling.
        /// </summary>
        public static PanelWrapper Card(Transform parent)
        {
            return UI.Panel(parent)
                .Light()
                .RoundedSmall()
                .Build();
        }

        /// <summary>
        /// Creates a dark card-style panel.
        /// </summary>
        public static PanelWrapper DarkCard(Transform parent)
        {
            return UI.Panel(parent)
                .Dark()
                .RoundedSmall()
                .Build();
        }

        /// <summary>
        /// Creates a container panel that fills its parent.
        /// </summary>
        public static PanelWrapper Container(Transform parent)
        {
            return UI.Panel(parent)
                .FillParent(Theme.Space.Medium)
                .Build();
        }

        /// <summary>
        /// Creates a horizontal layout container.
        /// </summary>
        public static PanelWrapper HorizontalContainer(Transform parent)
        {
            return UI.Panel(parent)
                .FillParent()
                .WithHorizontalLayout(layout => layout
                    .SetSpacing(Theme.Space.Small)
                    .SetPadding((int)Theme.Space.Medium))
                .Build();
        }

        /// <summary>
        /// Creates a vertical layout container.
        /// </summary>
        public static PanelWrapper VerticalContainer(Transform parent)
        {
            return UI.Panel(parent)
                .FillParent()
                .WithVerticalLayout(layout => layout
                    .SetSpacing(Theme.Space.Small)
                    .SetPadding((int)Theme.Space.Medium))
                .Build();
        }
    }
} 