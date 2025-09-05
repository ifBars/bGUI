using UnityEngine;
using UnityEngine.UI;
using bGUI.Components;
using bGUI.Core.Abstractions;
using bGUI.Core.Components;

namespace bGUI.Samples
{
    /// <summary>
    /// Demo page showcasing visual styling features.
    /// </summary>
    public class StylesDemoPage : DemoPageBase
    {
        // Padding and spacing constants
        private const float SIDE_PADDING = 30f;
        private const float SECTION_SPACING = 60f;
        private const float THEME_PANEL_HEIGHT = 300f;
        private const float CUSTOM_PANEL_HEIGHT = 250f;

        // Main scroll view for better organization
        private IScrollView _scrollView = null!;
        private RectTransform _contentContainer = null!;
        
        // Style showcase panels
        private PanelWrapper _themeShowcaseContainer = null!;
        private PanelWrapper _darkThemePanel = null!;
        private PanelWrapper _lightThemePanel = null!;
        private PanelWrapper _colorfulThemePanel = null!;
        private PanelWrapper _customStylesContainer = null!;
        
        // Interactive elements
        private ButtonWrapper _themeToggleButton = null!;
        private int _currentTheme = 0;
        
        // Track vertical position as we build the UI
        private float _currentY = 40f;
        
        /// <summary>
        /// Gets the title of the page.
        /// </summary>
        public override string PageTitle => "Visual Styles";
        
        /// <summary>
        /// Initializes a new instance of the StylesDemoPage.
        /// </summary>
        /// <param name="parent">The parent transform.</param>
        public StylesDemoPage(RectTransform parent) : base(parent) { }
        
        /// <summary>
        /// Sets up the UI elements for this page.
        /// </summary>
        protected override void SetupUI()
        {
            CreateScrollableContent();
            CreateHeader();
            CreateThemeShowcase();
            CreateCustomStyles();
            CreateThemeControls();
            
            // Adjust content height based on the final position
            AdjustContentHeight();
        }
        
        /// <summary>
        /// Creates the main scrollable content container.
        /// </summary>
        private void CreateScrollableContent()
        {
            // Create main scroll view
            _scrollView = UI.ScrollView(PagePanel.RectTransform)
                .WithVerticalScrolling(true)
                .WithHorizontalScrolling(false)
                .Build();
            
            // Configure the scroll view to fill the page panel
            _scrollView.RectTransform.anchorMin = Vector2.zero;
            _scrollView.RectTransform.anchorMax = Vector2.one;
            _scrollView.RectTransform.sizeDelta = Vector2.zero;
            
            // Get reference to content container
            _contentContainer = _scrollView.Content;
            
            // Configure content container for top-down layout
            _contentContainer.anchorMin = new Vector2(0, 1);
            _contentContainer.anchorMax = new Vector2(1, 1);
            _contentContainer.pivot = new Vector2(0.5f, 1);
            _contentContainer.anchoredPosition = Vector2.zero;
            _contentContainer.sizeDelta = new Vector2(0, 1000); // Initial height
        }
        
        /// <summary>
        /// Creates the page header and description.
        /// </summary>
        private void CreateHeader()
        {
            CreateSectionHeader("Visual Styling Showcase");
            CreateDescription("Demonstrates various visual styles and theming options using the bGUI framework.");
        }
        
        /// <summary>
        /// Creates the theme showcase section with all three themes.
        /// </summary>
        private void CreateThemeShowcase()
        {
            CreateSectionHeader("Theme Showcase");
            CreateDescription("Switch between different visual themes to see various styling approaches.");
            
            // Create container for theme panels
            _themeShowcaseContainer = UI.Panel(_contentContainer)
                .SetSize(PagePanel.RectTransform.rect.width - (SIDE_PADDING * 2), THEME_PANEL_HEIGHT)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .SetBackgroundColor(new Color(0.1f, 0.1f, 0.12f, 0.5f))
                .Build();
            
            // Create all theme panels (they'll occupy the same space and toggle visibility)
            CreateDarkTheme();
            CreateLightTheme();
            CreateColorfulTheme();
            
            // Initially show dark theme
            ShowTheme(0);
            
            _currentY += THEME_PANEL_HEIGHT + SECTION_SPACING;
        }
        
        /// <summary>
        /// Creates the dark theme panel using grid layout.
        /// </summary>
        private void CreateDarkTheme()
        {
            _darkThemePanel = CreateThemePanel("Dark Theme", new Color(0.15f, 0.15f, 0.18f, 1f));
            
            // Create elements container with grid layout
            var elementsPanel = UI.Panel(_darkThemePanel.RectTransform)
                .SetSize(_darkThemePanel.RectTransform.sizeDelta.x - 40, _darkThemePanel.RectTransform.sizeDelta.y - 80)
                .SetAnchor(0.5f, 0.5f)
                .SetPosition(0, -20)
                .SetBackgroundColor(new Color(0.18f, 0.18f, 0.22f, 1f))
                .WithGridLayout(layout => layout
                    .SetCellSize(250, 80)
                    .SetSpacing(20, 15)
                    .SetPadding(20)
                    .SetChildAlignment(TextAnchor.MiddleCenter))
                .Build();
            
            // Create theme elements
            CreateThemeButton(elementsPanel, "Dark Button", 
                new Color(0.25f, 0.25f, 0.3f, 1f),
                new Color(0.35f, 0.35f, 0.4f, 1f),
                new Color(0.2f, 0.2f, 0.25f, 1f),
                new Color(0.9f, 0.9f, 0.9f, 1f));
            
            CreateThemeTextPanel(elementsPanel, "Dark text panel with light text for readability.", 
                new Color(0.2f, 0.2f, 0.24f, 1f), new Color(0.85f, 0.85f, 0.85f, 1f));
            
            CreateThemeButton(elementsPanel, "Accent Button",
                new Color(0.3f, 0.5f, 0.9f, 1f),
                new Color(0.4f, 0.6f, 1f, 1f),
                new Color(0.2f, 0.4f, 0.8f, 1f),
                Color.white);
            
            CreateThemeInputField(elementsPanel, "Dark Input", 
                new Color(0.12f, 0.12f, 0.15f, 1f),
                new Color(0.2f, 0.2f, 0.24f, 1f),
                new Color(0.7f, 0.7f, 0.7f, 1f));
        }
        
        /// <summary>
        /// Creates the light theme panel using grid layout.
        /// </summary>
        private void CreateLightTheme()
        {
            _lightThemePanel = CreateThemePanel("Light Theme", new Color(0.9f, 0.9f, 0.92f, 1f));
            
            // Create elements container with grid layout
            var elementsPanel = UI.Panel(_lightThemePanel.RectTransform)
                .SetSize(_lightThemePanel.RectTransform.sizeDelta.x - 40, _lightThemePanel.RectTransform.sizeDelta.y - 80)
                .SetAnchor(0.5f, 0.5f)
                .SetPosition(0, -20)
                .SetBackgroundColor(new Color(0.95f, 0.95f, 0.97f, 1f))
                .WithGridLayout(layout => layout
                    .SetCellSize(250, 80)
                    .SetSpacing(20, 15)
                    .SetPadding(20)
                    .SetChildAlignment(TextAnchor.MiddleCenter))
                .Build();
            
            // Create theme elements
            CreateThemeButton(elementsPanel, "Light Button",
                new Color(0.8f, 0.8f, 0.85f, 1f),
                new Color(0.9f, 0.9f, 0.95f, 1f),
                new Color(0.7f, 0.7f, 0.75f, 1f),
                new Color(0.2f, 0.2f, 0.25f, 1f));
            
            CreateThemeTextPanel(elementsPanel, "Light text panel with dark text for readability.",
                new Color(0.85f, 0.85f, 0.9f, 1f), new Color(0.2f, 0.2f, 0.25f, 1f));
            
            CreateThemeButton(elementsPanel, "Accent Button",
                new Color(0.4f, 0.6f, 1f, 1f),
                new Color(0.5f, 0.7f, 1f, 1f),
                new Color(0.3f, 0.5f, 0.9f, 1f),
                Color.white);
            
            CreateThemeInputField(elementsPanel, "Light Input",
                new Color(0.9f, 0.9f, 0.92f, 1f),
                Color.white,
                new Color(0.4f, 0.4f, 0.45f, 1f));
        }
        
        /// <summary>
        /// Creates the colorful theme panel using grid layout.
        /// </summary>
        private void CreateColorfulTheme()
        {
            _colorfulThemePanel = CreateThemePanel("Colorful Theme", new Color(0.1f, 0.12f, 0.25f, 1f));
            
            // Create elements container with grid layout
            var elementsPanel = UI.Panel(_colorfulThemePanel.RectTransform)
                .SetSize(_colorfulThemePanel.RectTransform.sizeDelta.x - 40, _colorfulThemePanel.RectTransform.sizeDelta.y - 80)
                .SetAnchor(0.5f, 0.5f)
                .SetPosition(0, -20)
                .SetBackgroundColor(new Color(0.15f, 0.18f, 0.3f, 1f))
                .WithGridLayout(layout => layout
                    .SetCellSize(250, 80)
                    .SetSpacing(20, 15)
                    .SetPadding(20)
                    .SetChildAlignment(TextAnchor.MiddleCenter))
                .Build();
            
            // Create theme elements with vibrant colors
            CreateThemeButton(elementsPanel, "Blue Button",
                new Color(0.2f, 0.5f, 0.9f, 1f),
                new Color(0.3f, 0.6f, 1f, 1f),
                new Color(0.1f, 0.4f, 0.8f, 1f),
                Color.white);
            
            CreateThemeTextPanel(elementsPanel, "Colorful theme with vibrant elements.",
                new Color(0.9f, 0.3f, 0.5f, 1f), Color.white);
            
            CreateThemeButton(elementsPanel, "Green Button",
                new Color(0.2f, 0.8f, 0.4f, 1f),
                new Color(0.3f, 0.9f, 0.5f, 1f),
                new Color(0.1f, 0.7f, 0.3f, 1f),
                Color.white);
            
            CreateThemePanel(elementsPanel, "Vibrant Panel", "Bold colors!",
                new Color(1f, 0.6f, 0.1f, 1f), Color.white);
        }
        
        /// <summary>
        /// Creates the custom styles section.
        /// </summary>
        private void CreateCustomStyles()
        {
            CreateSectionHeader("Custom Styling Elements");
            CreateDescription("Various advanced styling techniques and visual effects.");
            
            // Create container for custom style cards
            _customStylesContainer = UI.Panel(_contentContainer)
                .SetSize(PagePanel.RectTransform.rect.width - (SIDE_PADDING * 2), CUSTOM_PANEL_HEIGHT)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .SetBackgroundColor(new Color(0.22f, 0.22f, 0.25f, 1f))
                .WithHorizontalLayout(layout => layout
                    .SetSpacing(20)
                    .SetPadding(20)
                    .SetChildAlignment(TextAnchor.MiddleCenter)
                    .SetChildForceExpand(true, false))
                .Build();
            
            // Create style cards
            CreateGlassEffectCard();
            CreateGradientStyleCard();
            CreateRoundedStyleCard();
            
            _currentY += CUSTOM_PANEL_HEIGHT + SECTION_SPACING;
        }
        
        /// <summary>
        /// Creates the theme control buttons.
        /// </summary>
        private void CreateThemeControls()
        {
            // Create theme toggle button
            _themeToggleButton = UI.Button(_contentContainer)
                .SetText("Switch Theme")
                .SetSize(200, 50)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .SetColors(
                    new Color(0.3f, 0.6f, 0.9f, 1f),
                    new Color(0.4f, 0.7f, 1f, 1f),
                    new Color(0.2f, 0.5f, 0.8f, 1f),
                    Color.gray)
                .OnClick(ToggleTheme)
                .Build();
            
            _currentY += 70f; // Button height + padding
        }
        
        /// <summary>
        /// Adjusts the content height based on the current Y position.
        /// </summary>
        private void AdjustContentHeight()
        {
            _currentY += 50f; // Final padding
            _contentContainer.sizeDelta = new Vector2(0, _currentY);
        }
        
        /// <summary>
        /// Creates a section header.
        /// </summary>
        private void CreateSectionHeader(string text)
        {
            UI.Text(_contentContainer)
                .SetContent(text)
                .SetFontSize(24)
                .SetFontStyle(FontStyle.Bold)
                .SetColor(Color.white)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .Build();
                
            _currentY += 40f;
        }
        
        /// <summary>
        /// Creates a description text.
        /// </summary>
        private void CreateDescription(string text)
        {
            UI.Text(_contentContainer)
                .SetContent(text)
                .SetFontSize(16)
                .SetAlignment(TextAnchor.MiddleCenter)
                .SetColor(new Color(0.8f, 0.8f, 0.8f, 1f))
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .SetWidth(PagePanel.RectTransform.rect.width - (SIDE_PADDING * 2))
                .Build();
                
            _currentY += 40f;
        }
        
        /// <summary>
        /// Creates a base theme panel with title.
        /// </summary>
        private PanelWrapper CreateThemePanel(string title, Color backgroundColor)
        {
            var panel = UI.Panel(_themeShowcaseContainer.RectTransform)
                .SetSize(_themeShowcaseContainer.RectTransform.sizeDelta.x, _themeShowcaseContainer.RectTransform.sizeDelta.y)
                .SetAnchor(0.5f, 0.5f)
                .SetPosition(0, 0)
                .SetBackgroundColor(backgroundColor)
                .Build();
            
            // Add title
            UI.Text(panel.RectTransform)
                .SetContent(title)
                .SetFontSize(24)
                .SetFontStyle(FontStyle.Bold)
                .SetColor(title == "Light Theme" ? new Color(0.2f, 0.2f, 0.25f, 1f) : 
                         title == "Colorful Theme" ? new Color(1f, 0.8f, 0.2f, 1f) : 
                         new Color(0.9f, 0.9f, 0.9f, 1f))
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -20)
                .Build();
            
            return panel;
        }
        
        /// <summary>
        /// Creates a theme button with specified colors.
        /// </summary>
        private void CreateThemeButton(PanelWrapper parent, string text, Color normal, Color highlighted, Color pressed, Color textColor)
        {
            var button = UI.Button(parent.RectTransform)
                .SetText(text)
                .SetColors(normal, highlighted, pressed, Color.gray)
                .Build();
            
            StyleButtonText(button, textColor);
        }
        
        /// <summary>
        /// Creates a theme text panel.
        /// </summary>
        private void CreateThemeTextPanel(PanelWrapper parent, string text, Color backgroundColor, Color textColor)
        {
            var panel = UI.Panel(parent.RectTransform)
                .SetBackgroundColor(backgroundColor)
                .Build();
            
            var textComponent = UI.Text(panel.RectTransform)
                .SetContent(text)
                .SetFontSize(12)
                .SetColor(textColor)
                .SetAnchor(0.5f, 0.5f)
                .Build();
            
            textComponent.TextComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
        }
        
        /// <summary>
        /// Creates a theme input field representation.
        /// </summary>
        private void CreateThemeInputField(PanelWrapper parent, string label, Color backgroundColor, Color fieldColor, Color textColor)
        {
            var container = UI.Panel(parent.RectTransform)
                .SetBackgroundColor(backgroundColor)
                .WithVerticalLayout(layout => layout
                    .SetSpacing(5)
                    .SetPadding(10)
                    .SetChildAlignment(TextAnchor.MiddleCenter))
                .Build();
            
            // Label
            UI.Text(container.RectTransform)
                .SetContent(label)
                .SetFontSize(12)
                .SetFontStyle(FontStyle.Bold)
                .SetColor(textColor)
                .SetHeight(20)
                .Build();
            
            // Input field mock
            var inputField = UI.Panel(container.RectTransform)
                .SetBackgroundColor(fieldColor)
                .SetSize(200, 30)
                .Build();
            
            UI.Text(inputField.RectTransform)
                .SetContent("Sample input...")
                .SetFontSize(11)
                .SetColor(textColor)
                .SetAnchor(0f, 0.5f)
                .SetPivot(0f, 0.5f)
                .SetPosition(5, 0)
                .Build();
        }
        
        /// <summary>
        /// Creates a themed panel with title and content.
        /// </summary>
        private void CreateThemePanel(PanelWrapper parent, string title, string content, Color backgroundColor, Color textColor)
        {
            var panel = UI.Panel(parent.RectTransform)
                .SetBackgroundColor(backgroundColor)
                .WithVerticalLayout(layout => layout
                    .SetSpacing(5)
                    .SetPadding(10)
                    .SetChildAlignment(TextAnchor.MiddleCenter))
                .Build();
            
            UI.Text(panel.RectTransform)
                .SetContent(title)
                .SetFontSize(14)
                .SetFontStyle(FontStyle.Bold)
                .SetColor(textColor)
                .Build();
            
            UI.Text(panel.RectTransform)
                .SetContent(content)
                .SetFontSize(12)
                .SetColor(textColor)
                .Build();
        }
        
        /// <summary>
        /// Creates a glass effect style card.
        /// </summary>
        private void CreateGlassEffectCard()
        {
            var card = UI.Panel(_customStylesContainer.RectTransform)
                .SetSize(200, 210)
                .SetBackgroundColor(new Color(1f, 1f, 1f, 0.1f))
                .WithVerticalLayout(layout => layout
                    .SetSpacing(10)
                    .SetPadding(15)
                    .SetChildAlignment(TextAnchor.UpperCenter))
                .Build();
            
            CreateStyleCardContent(card, "Glass Effect", 
                "Semi-transparent panel with subtle background for a modern glass effect.",
                "Glass Button", new Color(1f, 1f, 1f, 0.2f), new Color(1f, 1f, 1f, 0.9f));
        }
        
        /// <summary>
        /// Creates a gradient style card.
        /// </summary>
        private void CreateGradientStyleCard()
        {
            var card = UI.Panel(_customStylesContainer.RectTransform)
                .SetSize(200, 210)
                .SetBackgroundColor(new Color(0.3f, 0.3f, 0.35f, 1f))
                .WithVerticalLayout(layout => layout
                    .SetSpacing(10)
                    .SetPadding(15)
                    .SetChildAlignment(TextAnchor.UpperCenter))
                .Build();
            
            CreateStyleCardContent(card, "Gradient Style",
                "Panels and buttons with gradient colors for depth and visual interest.",
                "Gradient Button", new Color(0.4f, 0.3f, 0.8f, 1f), Color.white);
        }
        
        /// <summary>
        /// Creates a rounded style card.
        /// </summary>
        private void CreateRoundedStyleCard()
        {
            var card = UI.Panel(_customStylesContainer.RectTransform)
                .SetSize(200, 210)
                .SetBackgroundColor(new Color(0.3f, 0.3f, 0.35f, 1f))
                .WithVerticalLayout(layout => layout
                    .SetSpacing(10)
                    .SetPadding(15)
                    .SetChildAlignment(TextAnchor.UpperCenter))
                .Build();
            
            CreateStyleCardContent(card, "Rounded Style",
                "Elements with rounded corners for a softer, more modern appearance.",
                "Rounded Button", new Color(0.2f, 0.7f, 0.5f, 1f), Color.white);
        }
        
        /// <summary>
        /// Creates content for a style card.
        /// </summary>
        private void CreateStyleCardContent(PanelWrapper card, string title, string description, string buttonText, Color buttonColor, Color textColor)
        {
            // Title
            UI.Text(card.RectTransform)
                .SetContent(title)
                .SetFontSize(18)
                .SetFontStyle(FontStyle.Bold)
                .SetColor(Color.white)
                .SetHeight(25)
                .Build();
            
            // Description
            var desc = UI.Text(card.RectTransform)
                .SetContent(description)
                .SetFontSize(14)
                .SetColor(new Color(1f, 1f, 1f, 0.8f))
                .SetHeight(100)
                .Build();
            
            desc.TextComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
            desc.TextComponent.verticalOverflow = VerticalWrapMode.Overflow;
            
            // Button
            var button = UI.Button(card.RectTransform)
                .SetText(buttonText)
                .SetSize(160, 40)
                .SetColors(buttonColor, Color.Lerp(buttonColor, Color.white, 0.2f), 
                          Color.Lerp(buttonColor, Color.black, 0.2f), Color.gray)
                .Build();
            
            StyleButtonText(button, textColor);
        }
        
        /// <summary>
        /// Shows the specified theme and hides others.
        /// </summary>
        private void ShowTheme(int themeIndex)
        {
            _darkThemePanel.GameObject.SetActive(themeIndex == 0);
            _lightThemePanel.GameObject.SetActive(themeIndex == 1);
            _colorfulThemePanel.GameObject.SetActive(themeIndex == 2);
        }
        
        /// <summary>
        /// Styles a button's text component.
        /// </summary>
        private void StyleButtonText(ButtonWrapper button, Color textColor)
        {
            var text = button.ButtonComponent.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.color = textColor;
                text.fontStyle = FontStyle.Bold;
            }
        }
        
        /// <summary>
        /// Toggles between themes.
        /// </summary>
        private void ToggleTheme()
        {
            _currentTheme = (_currentTheme + 1) % 3;
            ShowTheme(_currentTheme);
        }
    }
} 
