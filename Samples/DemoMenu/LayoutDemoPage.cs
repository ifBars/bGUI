using UnityEngine;
using UnityEngine.UI;
using bGUI.Components;
using bGUI.Core.Components;

namespace bGUI.Samples
{
    /// <summary>
    /// Demo page showcasing layout features.
    /// </summary>
    public class LayoutDemoPage : DemoPageBase
    {
        // Padding and spacing constants
        private const float SIDE_PADDING = 30f;
        private const float SECTION_SPACING = 60f;
        private const float HEADER_DESCRIPTION_SPACING = 20f;
        private const float PANEL_DESCRIPTION_SPACING = 30f;
        private const float HEADER_PANEL_SPACING = 40f; // Space between header and its panel
        
        // Main scroll view
        private IScrollView _scrollView = null!;
        private RectTransform _contentContainer = null!;
        
        // Layout areas
        private PanelWrapper _gridLayoutPanel = null!;
        private PanelWrapper _horizontalLayoutPanel = null!;
        private PanelWrapper _verticalLayoutPanel = null!;
        private PanelWrapper _nestedLayoutPanel = null!;
        private PanelWrapper _absoluteLayoutPanel = null!;
        
        // Track vertical position as we build the UI
        private float _currentY = 40f;
        
        /// <summary>
        /// Gets the title of the page.
        /// </summary>
        public override string PageTitle => "Layout Options";
        
        /// <summary>
        /// Initializes a new instance of the LayoutDemoPage.
        /// </summary>
        /// <param name="parent">The parent transform.</param>
        public LayoutDemoPage(RectTransform parent) : base(parent) { }
        
        /// <summary>
        /// Sets up the UI elements for this page.
        /// </summary>
        protected override void SetupUI()
        {
            // Create main scroll view
            _scrollView = UI.ScrollView(PagePanel.RectTransform)
                .WithVerticalScrolling(true)
                .WithHorizontalScrolling(false)
                .Build();
            
            // Configure the scroll view to fill the page panel
            _scrollView.RectTransform.anchorMin = new Vector2(0, 0);
            _scrollView.RectTransform.anchorMax = new Vector2(1, 1);
            _scrollView.RectTransform.sizeDelta = Vector2.zero;
            
            // Get reference to content container for easier access
            _contentContainer = _scrollView.Content;
            
            // Configure content container for vertical layout
            _contentContainer.anchorMin = new Vector2(0, 1);
            _contentContainer.anchorMax = new Vector2(1, 1);
            _contentContainer.pivot = new Vector2(0.5f, 1);
            _contentContainer.anchoredPosition = Vector2.zero;
            _contentContainer.sizeDelta = new Vector2(0, 1000); // Initial height, will adjust later
            
            CreateHeader();
            CreateGridLayout();
            CreateHorizontalLayout();
            CreateVerticalLayout();
            CreateNestedLayout();
            CreateAbsoluteLayout();
            
            // Adjust content height based on the last element's position plus padding
            AdjustContentHeight();
        }

        private void AdjustContentHeight()
        {
            // Add final padding at the bottom
            _currentY += 50f;
            
            // Set content height to our tracked position
            _contentContainer.sizeDelta = new Vector2(0, _currentY);
        }
        
        private void CreateHeader()
        {
            // Title
            CreateSectionHeader("Layout Showcase");
            _currentY += 30f; // Add space after the main header
            
            // Description
            _currentY += HEADER_DESCRIPTION_SPACING;
            CreateDescription("Demonstrates various layout techniques using the bGUI framework.");
            
            // Space before next section
            _currentY += SECTION_SPACING;
        }
        
        private void CreateGridLayout()
        {
            // Section header
            CreateSectionHeader("Grid Layout");
            _currentY += HEADER_PANEL_SPACING; // Add space between header and panel
            
            // Panel for grid layout
            float panelWidth = PagePanel.RectTransform.rect.width - (SIDE_PADDING * 2);
            float panelHeight = 120f;
            
            _gridLayoutPanel = UI.Panel(_contentContainer)
                .SetSize(panelWidth, panelHeight)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .SetBackgroundColor(new Color(0.25f, 0.25f, 0.3f, 1f))
                .WithGridLayout(layout => layout
                    .SetCellSize(80, 40)
                    .SetSpacing(8, 8)
                    .SetPadding(8, 8, 8, 8)
                    .SetStartCorner(GridLayoutGroup.Corner.UpperLeft)
                    .SetStartAxis(GridLayoutGroup.Axis.Horizontal)
                    .SetChildAlignment(TextAnchor.UpperLeft))
                    .Build();

            int cellCount = 26;
            for (int i = 0; i < cellCount; i++)
            {
                Color cellColor = Color.HSVToRGB(i * (1 / (float)cellCount), 0.8f, 0.9f);
                var cell = UI.Panel(_gridLayoutPanel.RectTransform)
                    .SetBackgroundColor(cellColor)
                    .Build();
                    
                UI.Text(cell.RectTransform)
                    .SetContent((i + 1).ToString())
                    .SetFontSize(14)
                    .SetFontStyle(FontStyle.Bold)
                    .SetColor(Color.white)
                    .SetAnchor(0.5f, 0.5f)
                    .Build();
            }
            
            // Update position after adding the panel
            _currentY += panelHeight;
            
            // Description
            _currentY += PANEL_DESCRIPTION_SPACING;
            CreateDescription("A Grid Layout arranges elements in a grid pattern with consistent cell sizes.");
            
            // Space before next section
            _currentY += SECTION_SPACING;
        }
        
        private void CreateHorizontalLayout()
        {
            // Section header
            CreateSectionHeader("Horizontal Layout");
            _currentY += HEADER_PANEL_SPACING;
            
            float panelWidth = PagePanel.RectTransform.rect.width - (SIDE_PADDING * 2);
            float panelHeight = 160f;
            
            // Creating a container to hold both horizontal layout and description side by side
            PanelWrapper container = UI.Panel(_contentContainer)
                .SetSize(panelWidth, panelHeight)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .Build();
                
            // Make container transparent
            container.GameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            
            // Create horizontal layout panel on the left
            _horizontalLayoutPanel = UI.Panel(container.RectTransform)
                .SetSize(panelWidth * 0.6f, 120f)
                .SetAnchor(0f, 0.5f)
                .SetPivot(0f, 0.5f)
                .SetPosition(0, 0)
                .SetBackgroundColor(new Color(0.25f, 0.25f, 0.3f, 1f))
                .WithHorizontalLayout(layout => layout
                    .SetSpacing(10)
                    .SetPadding(10, 10, 10, 10)
                    .SetChildAlignment(TextAnchor.MiddleLeft)
                    .SetChildControlSize(true, true)
                    .SetChildForceExpand(false, true))
                .Build();
            
            // 1. Custom width panel (green)
            var customPanel = UI.Panel(_horizontalLayoutPanel.RectTransform)
                .SetSize(150, 100)
                .SetBackgroundColor(new Color(0.3f, 0.7f, 0.4f, 1f))
                .Build();
                
            UI.Text(customPanel.RectTransform)
                .SetContent("Custom Width Panel")
                .SetFontSize(14)
                .SetColor(Color.white)
                .SetAnchor(0.5f, 0.5f)
                .Build();
                
            // Set fixed width for the custom panel within the horizontal layout
            Utilities.LayoutUtils.SetFixedWidth(customPanel.GameObject, 250);
            
            // 2. Flexible panel (purple)
            var flexPanel = UI.Panel(_horizontalLayoutPanel.RectTransform)
                .SetSize(100, 100)
                .SetBackgroundColor(new Color(0.5f, 0.5f, 0.9f, 1f))
                .Build();
                
            UI.Text(flexPanel.RectTransform)
                .SetContent("Flexible Panel")
                .SetFontSize(14)
                .SetColor(Color.white)
                .SetAnchor(0.5f, 0.5f)
                .Build();
                
            // Make this panel flexible to fill remaining space
            Utilities.LayoutUtils.SetFlexibleWidth(flexPanel.GameObject, 1);
            
            // Add description to the right side
            var description = UI.Text(container.RectTransform)
                .SetContent("A Horizontal Layout arranges elements in a row, from left to right. This example shows a panel with custom width and a flexible panel that expands to fill the remaining space.")
                .SetFontSize(16)
                .SetColor(new Color(0.8f, 0.8f, 0.8f, 1f))
                .SetAnchor(1f, 0.5f)
                .SetPivot(1f, 0.5f)
                .SetPosition(0, 0)
                .Build();
                
            description.TextComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
            description.TextComponent.verticalOverflow = VerticalWrapMode.Overflow;
            description.RectTransform.sizeDelta = new Vector2(panelWidth * 0.35f, 0);
            
            // Update position after adding the panel
            _currentY += panelHeight;
            
            // Space before next section
            _currentY += SECTION_SPACING;
        }
        
        private void CreateVerticalLayout()
        {
            // Section header
            CreateSectionHeader("Vertical Layout");
            _currentY += HEADER_PANEL_SPACING; // Add space between header and panel
            
            float panelWidth = PagePanel.RectTransform.rect.width - (SIDE_PADDING * 2);
            float panelHeight = 240f;
            
            // Creating a container to hold both vertical layout and description side by side
            PanelWrapper container = UI.Panel(_contentContainer)
                .SetSize(panelWidth, panelHeight)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .Build();
                
            // Make container transparent
            container.GameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            
            // Add vertical layout to the left side
            _verticalLayoutPanel = UI.Panel(container.RectTransform)
                .SetSize(panelWidth * 0.6f, 220f)
                .SetAnchor(0f, 0.5f)
                .SetPivot(0f, 0.5f)
                .SetPosition(0, 0)
                .SetBackgroundColor(new Color(0.25f, 0.25f, 0.3f, 1f))
                .WithVerticalLayout(layout => layout
                    .SetSpacing(10)
                    .SetPadding(15, 15, 15, 15)
                    .SetChildAlignment(TextAnchor.UpperCenter)
                    .SetChildControlSize(true, false)
                    .SetChildForceExpand(true, false))
                .Build();
            
            // Add various elements to show different aspects of vertical layout
            // 1. Standard buttons
            for (int i = 0; i < 2; i++)
            {
                UI.Button(_verticalLayoutPanel.RectTransform)
                    .SetText($"Standard Button {i+1}")
                    .SetSize(160, 30)
                    .SetColors(
                        new Color(0.8f, 0.3f, 0.3f, 1f),
                        new Color(0.9f, 0.4f, 0.4f, 1f),
                        new Color(0.7f, 0.2f, 0.2f, 1f),
                        Color.gray)
                    .Build();
            }
            
            // 2. Panel with custom height
            var tallPanel = UI.Panel(_verticalLayoutPanel.RectTransform)
                .SetSize(160, 60)
                .SetBackgroundColor(new Color(0.3f, 0.7f, 0.4f, 1f))
                .Build();
                
            UI.Text(tallPanel.RectTransform)
                .SetContent("Custom Height Panel")
                .SetFontSize(14)
                .SetColor(Color.white)
                .SetAnchor(0.5f, 0.5f)
                .Build();
                
            // Set fixed height for this panel
            Utilities.LayoutUtils.SetFixedHeight(tallPanel.GameObject, 60);
            
            // 3. Flexible panel that will take remaining space
            var flexPanel = UI.Panel(_verticalLayoutPanel.RectTransform)
                .SetSize(160, 40)
                .SetBackgroundColor(new Color(0.4f, 0.4f, 0.8f, 1f))
                .Build();
                
            UI.Text(flexPanel.RectTransform)
                .SetContent("Flexible Panel")
                .SetFontSize(14)
                .SetColor(Color.white)
                .SetAnchor(0.5f, 0.5f)
                .Build();
                
            // Make this panel flexible to fill remaining space
            Utilities.LayoutUtils.SetFlexibleHeight(flexPanel.GameObject, 1);
            
            // Add description to the right side
            var description = UI.Text(container.RectTransform)
                .SetContent("A Vertical Layout arranges elements in a column, from top to bottom (or bottom to top with reverse arrangement). This example shows standard fixed-height buttons, a panel with custom height, and a flexible panel that expands to fill available space. Elements can be aligned center, left, or right using the child alignment property.")
                .SetFontSize(16)
                .SetColor(new Color(0.8f, 0.8f, 0.8f, 1f))
                .SetAnchor(1f, 0.5f)
                .SetPivot(1f, 0.5f)
                .SetPosition(0, 0)
                .Build();
                
            description.TextComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
            description.TextComponent.verticalOverflow = VerticalWrapMode.Overflow;
            description.RectTransform.sizeDelta = new Vector2(panelWidth * 0.35f, 0);
            
            // Update position after adding the panel
            _currentY += panelHeight;
            
            // Space before next section
            _currentY += SECTION_SPACING;
        }
        
        private void CreateNestedLayout()
        {
            // Section header
            CreateSectionHeader("Nested Layouts");
            _currentY += HEADER_PANEL_SPACING; // Add space between header and panel
            
            // Panel for nested layouts
            float panelWidth = PagePanel.RectTransform.rect.width - (SIDE_PADDING * 2);
            float panelHeight = 160f;
            
            _nestedLayoutPanel = UI.Panel(_contentContainer)
                .SetSize(panelWidth, panelHeight)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .SetBackgroundColor(new Color(0.25f, 0.25f, 0.3f, 1f))
                .WithVerticalLayout(layout => layout
                    .SetSpacing(5)
                    .SetPadding(10, 10, 10, 10)
                    .SetChildAlignment(TextAnchor.UpperCenter)
                    .SetChildForceExpand(true, false))
                .Build();
            
            for (int row = 0; row < 3; row++)
            {
                var rowPanel = UI.Panel(_nestedLayoutPanel.RectTransform)
                    .SetSize(panelWidth - 20, 28)
                    .SetBackgroundColor(new Color(0.3f, 0.3f, 0.35f, 1f))
                    .WithHorizontalLayout(layout => layout
                        .SetSpacing(25)
                        .SetPadding(20, 20, 20, 20)
                        .SetChildAlignment(TextAnchor.MiddleLeft)
                        .SetChildForceExpand(false, true))
                    .Build();
                
                for (int col = 0; col < 4; col++)
                {
                    Color itemColor = Color.HSVToRGB((row * 0.2f) + (col * 0.05f), 0.7f, 0.9f);
                    var item = UI.Panel(rowPanel.RectTransform)
                        .SetSize(60, 20)
                        .SetBackgroundColor(itemColor)
                        .Build();
                        
                    UI.Text(item.RectTransform)
                        .SetContent($"{row+1}-{col+1}")
                        .SetFontSize(12)
                        .SetColor(Color.white)
                        .SetAnchor(0.5f, 0.5f)
                        .Build();
                }
            }
            
            // Update position after adding the panel
            _currentY += panelHeight;
            
            // Description
            _currentY += PANEL_DESCRIPTION_SPACING;
            CreateDescription("Nested Layouts combine multiple layout groups to create complex arrangements. Here, horizontal layouts are nested within a vertical layout.");
            
            // Space before next section
            _currentY += SECTION_SPACING;
        }
        
        private void CreateAbsoluteLayout()
        {
            // Section header
            CreateSectionHeader("Absolute Positioning");
            _currentY += HEADER_PANEL_SPACING; // Add space between header and panel
            
            float panelWidth = PagePanel.RectTransform.rect.width - (SIDE_PADDING * 2);
            float panelHeight = 160f;
            
            // Creating a container to hold both absolute layout and description side by side
            PanelWrapper container = UI.Panel(_contentContainer)
                .SetSize(panelWidth, panelHeight)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .Build();
                
            // Make container transparent
            container.GameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            
            // Add absolute layout panel
            _absoluteLayoutPanel = UI.Panel(container.RectTransform)
                .SetSize(160, 120)
                .SetAnchor(0f, 0.5f)
                .SetPivot(0f, 0.5f)
                .SetPosition(0, 0)
                .SetBackgroundColor(new Color(0.25f, 0.25f, 0.3f, 1f))
                .Build();
            
            // Center element
            var centerElement = UI.Panel(_absoluteLayoutPanel.RectTransform)
                .SetSize(40, 40)
                .SetAnchor(0.5f, 0.5f)
                .SetBackgroundColor(new Color(0.8f, 0.2f, 0.8f, 1f))
                .Build();
                
            UI.Text(centerElement.RectTransform)
                .SetContent("Center")
                .SetFontSize(10)
                .SetColor(Color.white)
                .SetAnchor(0.5f, 0.5f)
                .Build();
                
            // Top-left element
            var topLeftElement = UI.Panel(_absoluteLayoutPanel.RectTransform)
                .SetSize(28, 28)
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(4, -4)
                .SetBackgroundColor(new Color(0.2f, 0.8f, 0.2f, 1f))
                .Build();
                
            UI.Text(topLeftElement.RectTransform)
                .SetContent("TL")
                .SetFontSize(10)
                .SetColor(Color.white)
                .SetAnchor(0.5f, 0.5f)
                .Build();
                
            // Bottom-right element
            var bottomRightElement = UI.Panel(_absoluteLayoutPanel.RectTransform)
                .SetSize(28, 28)
                .SetAnchor(1f, 0f)
                .SetPivot(1f, 0f)
                .SetPosition(-4, 4)
                .SetBackgroundColor(new Color(0.2f, 0.2f, 0.8f, 1f))
                .Build();
                
            UI.Text(bottomRightElement.RectTransform)
                .SetContent("BR")
                .SetFontSize(10)
                .SetColor(Color.white)
                .SetAnchor(0.5f, 0.5f)
                .Build();
                
            // Top-right element
            var topRightElement = UI.Panel(_absoluteLayoutPanel.RectTransform)
                .SetSize(28, 28)
                .SetAnchor(1f, 1f)
                .SetPivot(1f, 1f)
                .SetPosition(-4, -4)
                .SetBackgroundColor(new Color(0.8f, 0.8f, 0.2f, 1f))
                .Build();
                
            UI.Text(topRightElement.RectTransform)
                .SetContent("TR")
                .SetFontSize(10)
                .SetColor(Color.white)
                .SetAnchor(0.5f, 0.5f)
                .Build();
                
            // Bottom-left element
            var bottomLeftElement = UI.Panel(_absoluteLayoutPanel.RectTransform)
                .SetSize(28, 28)
                .SetAnchor(0f, 0f)
                .SetPivot(0f, 0f)
                .SetPosition(4, 4)
                .SetBackgroundColor(new Color(0.8f, 0.2f, 0.2f, 1f))
                .Build();
                
            UI.Text(bottomLeftElement.RectTransform)
                .SetContent("BL")
                .SetFontSize(10)
                .SetColor(Color.white)
                .SetAnchor(0.5f, 0.5f)
                .Build();
            
            // Add description to the right side
            var description = UI.Text(container.RectTransform)
                .SetContent("Absolute positioning uses anchors and pivot points to position elements precisely. Elements can be placed at exact coordinates relative to their parent container.")
                .SetFontSize(16)
                .SetColor(new Color(0.8f, 0.8f, 0.8f, 1f))
                .SetAnchor(1f, 0.5f)
                .SetPivot(1f, 0.5f)
                .SetPosition(0, 0)
                .Build();
                
            description.TextComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
            description.TextComponent.verticalOverflow = VerticalWrapMode.Overflow;
            description.RectTransform.sizeDelta = new Vector2(panelWidth - 180, 0);
            
            // Update position after adding the panel
            _currentY += panelHeight;
        }
        
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
                
            // Increment current Y so other elements know where this header ends
            _currentY += 24f; // Approximately the height of the header text
        }
        
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
                
            // Increment current Y for the height of the description text
            _currentY += 20f; // Approximately the height of the description text
        }
    }
} 
