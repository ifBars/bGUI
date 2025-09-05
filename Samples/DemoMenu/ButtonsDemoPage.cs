using UnityEngine;
using UnityEngine.UI;
using bGUI.Components;
using System.Collections.Generic;

namespace bGUI.Samples
{
    /// <summary>
    /// Demo page showcasing button features.
    /// </summary>
    public class ButtonsDemoPage : DemoPageBase
    {
        private TextWrapper _stateText = null!;
        private int _clickCounter = 0;
        
        // Interactive button samples
        private ButtonWrapper _counterButton = null!;
        private ButtonWrapper _toggleButton = null!;
        private ButtonWrapper _disableButton = null!;
        private ButtonWrapper _randomizeButton = null!;
        
        // Button array for randomizing colors
        private List<ButtonWrapper> _styleButtons = new List<ButtonWrapper>();
        private bool _toggleState = false;
        
        // Timer for the disable button
        private float _disableButtonTimer = 0f;
        private bool _isDisableButtonTimerActive = false;
        
        /// <summary>
        /// Gets the title of the page.
        /// </summary>
        public override string PageTitle => "Button Components";
        
        /// <summary>
        /// Initializes a new instance of the ButtonsDemoPage.
        /// </summary>
        /// <param name="parent">The parent transform.</param>
        public ButtonsDemoPage(RectTransform parent) : base(parent) { }
        
        /// <summary>
        /// Sets up the UI elements for this page.
        /// </summary>
        protected override void SetupUI()
        {
            // Headers and descriptions
            CreateSectionHeader("Button Showcase", -40);
            CreateDescription("Demonstrates various button styles and interactions using the bGUI framework.", -80);
            
            // Create the state display text
            _stateText = UI.Text(PagePanel.RectTransform)
                .SetContent("Click Counter: 0")
                .SetFontSize(20)
                .SetColor(new Color(0.9f, 0.9f, 0.9f, 1f))
                .SetAnchor(1f, 0f)
                .SetPivot(1f, 0f)
                .SetPosition(-30, 30)
                .Build();
            
            // Create simple button styles
            CreateStandardButtons();
            
            // Create interactive buttons
            CreateInteractiveButtons();
            
            // Create styled buttons
            CreateStyledButtons();
            
            // Programmatically created button examples
            CreateProgrammaticButtons();
        }
        
        /// <summary>
        /// Creates standard button examples.
        /// </summary>
        private void CreateStandardButtons()
        {
            CreateSectionHeader("Standard Buttons", -140);
            
            // Default Button
            var defaultButton = UI.Button(PagePanel.RectTransform)
                .SetText("Default Button")
                .SetSize(200, 50)
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(50, -190)
                .OnClick(() => UpdateStateText("Default button clicked"))
                .Build();
            
            _styleButtons.Add(defaultButton);
            
            // Blue Button
            var blueButton = UI.Button(PagePanel.RectTransform)
                .SetText("Blue Button")
                .SetSize(200, 50)
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(280, -190)
                .SetColors(
                    new Color(0.2f, 0.4f, 0.8f, 1f),
                    new Color(0.3f, 0.5f, 0.9f, 1f),
                    new Color(0.1f, 0.3f, 0.7f, 1f),
                    Color.gray)
                .OnClick(() => UpdateStateText("Blue button clicked"))
                .Build();
            
            _styleButtons.Add(blueButton);
            
            // Red Button
            var redButton = UI.Button(PagePanel.RectTransform)
                .SetText("Red Button")
                .SetSize(200, 50)
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(510, -190)
                .SetColors(
                    new Color(0.8f, 0.2f, 0.2f, 1f),
                    new Color(0.9f, 0.3f, 0.3f, 1f),
                    new Color(0.7f, 0.1f, 0.1f, 1f),
                    Color.gray)
                .OnClick(() => UpdateStateText("Red button clicked"))
                .Build();
            
            _styleButtons.Add(redButton);
            
            // Green Button
            var greenButton = UI.Button(PagePanel.RectTransform)
                .SetText("Green Button")
                .SetSize(200, 50)
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(740, -190)
                .SetColors(
                    new Color(0.2f, 0.8f, 0.2f, 1f),
                    new Color(0.3f, 0.9f, 0.3f, 1f),
                    new Color(0.1f, 0.7f, 0.1f, 1f),
                    Color.gray)
                .OnClick(() => UpdateStateText("Green button clicked"))
                .Build();
            
            _styleButtons.Add(greenButton);
        }
        
        /// <summary>
        /// Creates interactive button examples.
        /// </summary>
        private void CreateInteractiveButtons()
        {
            CreateSectionHeader("Interactive Buttons", -260);
            
            // Counter button
            _counterButton = UI.Button(PagePanel.RectTransform)
                .SetText("Click Counter: 0")
                .SetSize(200, 50)
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(50, -310)
                .SetColors(
                    new Color(0.3f, 0.3f, 0.8f, 1f),
                    new Color(0.4f, 0.4f, 0.9f, 1f),
                    new Color(0.2f, 0.2f, 0.7f, 1f),
                    Color.gray)
                .OnClick(IncrementCounter)
                .Build();
            
            // Toggle button
            _toggleButton = UI.Button(PagePanel.RectTransform)
                .SetText("Toggle: OFF")
                .SetSize(200, 50)
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(280, -310)
                .SetColors(
                    new Color(0.5f, 0.5f, 0.5f, 1f),
                    new Color(0.6f, 0.6f, 0.6f, 1f),
                    new Color(0.4f, 0.4f, 0.4f, 1f),
                    Color.gray)
                .OnClick(ToggleState)
                .Build();
            
            // Button that can disable itself
            _disableButton = UI.Button(PagePanel.RectTransform)
                .SetText("Disable Self")
                .SetSize(200, 50)
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(510, -310)
                .SetColors(
                    new Color(0.8f, 0.4f, 0.1f, 1f),
                    new Color(0.9f, 0.5f, 0.2f, 1f),
                    new Color(0.7f, 0.3f, 0.0f, 1f),
                    Color.gray)
                .OnClick(DisableButtonClick)
                .Build();
            
            // Random color button
            _randomizeButton = UI.Button(PagePanel.RectTransform)
                .SetText("Randomize Colors")
                .SetSize(200, 50)
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(740, -310)
                .SetColors(
                    new Color(0.4f, 0.4f, 0.4f, 1f),
                    new Color(0.5f, 0.5f, 0.5f, 1f),
                    new Color(0.3f, 0.3f, 0.3f, 1f),
                    Color.gray)
                .OnClick(RandomizeColors)
                .Build();
        }
        
        /// <summary>
        /// Creates button examples with custom styles.
        /// </summary>
        private void CreateStyledButtons()
        {
            CreateSectionHeader("Styled Buttons", -380);
            
            // Rounded Button
            var roundedButton = UI.Button(PagePanel.RectTransform)
                .SetText("Rounded Button")
                .SetSize(200, 50)
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(50, -430)
                .SetColors(
                    new Color(0.3f, 0.6f, 0.9f, 1f),  // Normal - blue
                    new Color(0.4f, 0.7f, 1f, 1f),    // Highlighted - lighter blue
                    new Color(0.2f, 0.5f, 0.8f, 1f),  // Pressed - darker blue
                    Color.gray)                       // Disabled
                .SetRounded(15)  // Apply rounded corners with 15px radius
                .OnClick(() => UpdateStateText("Rounded button clicked"))
                .Build();
            
            // Ensure the button's text is properly centered and white
            var buttonText = roundedButton.ButtonComponent.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.alignment = TextAnchor.MiddleCenter;
                buttonText.fontSize = 16;
                buttonText.color = Color.white;
            }
            
            // Icon Button
            var iconButton = UI.Button(PagePanel.RectTransform)
                .SetText("")
                .SetSize(50, 50)
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(280, -430)
                .SetColors(
                    new Color(0.2f, 0.2f, 0.2f, 1f),
                    new Color(0.3f, 0.3f, 0.3f, 1f),
                    new Color(0.1f, 0.1f, 0.1f, 1f),
                    Color.gray)
                .SetRounded(25)  // Make it rounded (50px width/height with 25px radius = circle)
                .OnClick(() => UpdateStateText("Icon button clicked"))
                .Build();
            
            // Add a + icon
            var iconText = UI.Text(iconButton.RectTransform)
                .SetContent("+")
                .SetFontSize(30)
                .SetFontStyle(FontStyle.Bold)
                .SetColor(Color.white)
                .SetAnchor(0.5f, 0.5f)
                .SetPivot(0.5f, 0.5f)
                .SetPosition(0, 0)
                .Build();
            
            // Text-only button (no background)
            var textButton = UI.Button(PagePanel.RectTransform)
                .SetText("Text Only Button")
                .SetSize(200, 50)
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(370, -430)
                .SetColors(
                    new Color(0, 0, 0, 0),
                    new Color(0, 0, 0, 0.1f),
                    new Color(0, 0, 0, 0.2f),
                    Color.gray)
                .OnClick(() => UpdateStateText("Text-only button clicked"))
                .Build();
            
            // Style the text to make it look like a link
            var linkText = textButton.ButtonComponent.GetComponentInChildren<Text>();
            if (linkText != null)
            {
                linkText.color = new Color(0.4f, 0.7f, 1f, 1f);
                linkText.fontStyle = FontStyle.Italic;
                linkText.alignment = TextAnchor.MiddleCenter;
            }
            
            // Large Button
            var largeButton = UI.Button(PagePanel.RectTransform)
                .SetText("Large Button")
                .SetSize(300, 80)
                .SetAnchor(0f, 1f)
                .SetPivot(0f, 1f)
                .SetPosition(600, -445)
                .SetColors(
                    new Color(0.4f, 0.1f, 0.6f, 1f),
                    new Color(0.5f, 0.2f, 0.7f, 1f),
                    new Color(0.3f, 0.0f, 0.5f, 1f),
                    Color.gray)
                .SetRounded(20)  // Apply rounded corners with larger radius
                .OnClick(() => UpdateStateText("Large button clicked"))
                .Build();
            
            // Style the text
            var largeText = largeButton.ButtonComponent.GetComponentInChildren<Text>();
            if (largeText != null)
            {
                largeText.fontSize = 24;
                largeText.fontStyle = FontStyle.Bold;
                largeText.alignment = TextAnchor.MiddleCenter;
            }
            
            _styleButtons.Add(roundedButton);
            _styleButtons.Add(iconButton);
            _styleButtons.Add(textButton);
            _styleButtons.Add(largeButton);
        }
        
        /// <summary>
        /// Creates programmatically generated button examples.
        /// </summary>
        private void CreateProgrammaticButtons()
        {
            CreateSectionHeader("Programmatic Buttons", -520);
            CreateDescription("These buttons are generated programmatically with gradient colors.", -560);
            
            // Create a row of gradient buttons
            for (int i = 0; i < 5; i++)
            {
                // Calculate a gradient color
                Color baseColor = Color.HSVToRGB(i * 0.2f, 0.8f, 0.9f);
                
                var gradientButton = UI.Button(PagePanel.RectTransform)
                    .SetText($"Button {i+1}")
                    .SetSize(160, 50)
                    .SetAnchor(0f, 1f)
                    .SetPivot(0f, 1f)
                    .SetPosition(50 + (i * 180), -600)
                    .SetColors(
                        baseColor,
                        Color.Lerp(baseColor, Color.white, 0.2f),
                        Color.Lerp(baseColor, Color.black, 0.2f),
                        Color.gray)
                    .OnClick(() => UpdateStateText($"Gradient button clicked"))
                    .Build();
                
                _styleButtons.Add(gradientButton);
            }
        }
        
        /// <summary>
        /// Updates the state text.
        /// </summary>
        private void UpdateStateText(string message)
        {
            _stateText.Content = message;
        }
        
        /// <summary>
        /// Increments the counter and updates the button text.
        /// </summary>
        private void IncrementCounter()
        {
            _clickCounter++;
            _counterButton.ButtonComponent.GetComponentInChildren<Text>().text = $"Click Counter: {_clickCounter}";
            UpdateStateText($"Counter: {_clickCounter}");
        }
        
        /// <summary>
        /// Toggles the state of the toggle button.
        /// </summary>
        private void ToggleState()
        {
            _toggleState = !_toggleState;
            
            _toggleButton.ButtonComponent.GetComponentInChildren<Text>().text = $"Toggle: {(_toggleState ? "ON" : "OFF")}";
            
            // Change button color based on state
            Color normalColor = _toggleState 
                ? new Color(0.2f, 0.8f, 0.2f, 1f) 
                : new Color(0.5f, 0.5f, 0.5f, 1f);
            
            Color hoverColor = _toggleState 
                ? new Color(0.3f, 0.9f, 0.3f, 1f) 
                : new Color(0.6f, 0.6f, 0.6f, 1f);
            
            Color pressedColor = _toggleState 
                ? new Color(0.1f, 0.7f, 0.1f, 1f) 
                : new Color(0.4f, 0.4f, 0.4f, 1f);
            
            _toggleButton.SetColors(normalColor, hoverColor, pressedColor, Color.gray);
            
            UpdateStateText($"Toggle state: {(_toggleState ? "ON" : "OFF")}");
        }
        
        /// <summary>
        /// Disables the button when clicked.
        /// </summary>
        private void DisableButtonClick()
        {
            UpdateStateText("Button disabled");
            _disableButton.ButtonComponent.interactable = false;
            
            // Start a timer to re-enable the button
            _disableButtonTimer = 3.0f;
            _isDisableButtonTimerActive = true;
            _disableButton.ButtonComponent.GetComponentInChildren<Text>().text = "Re-enabling (3s)";
        }
        
        /// <summary>
        /// Randomizes the colors of all styled buttons.
        /// </summary>
        private void RandomizeColors()
        {
            foreach (var button in _styleButtons)
            {
                Color randomColor = Random.ColorHSV(0f, 1f, 0.7f, 0.9f, 0.7f, 0.9f);
                
                button.SetColors(
                    randomColor,
                    Color.Lerp(randomColor, Color.white, 0.2f),
                    Color.Lerp(randomColor, Color.black, 0.2f),
                    Color.gray);
            }
            
            UpdateStateText("Button colors randomized");
        }
        
        /// <summary>
        /// Updates the demo page.
        /// </summary>
        public override void Update()
        {
            // Re-enable the disable button after 3 seconds
            if (_isDisableButtonTimerActive && !_disableButton.ButtonComponent.interactable)
            {
                _disableButtonTimer -= Time.deltaTime;
                var text = _disableButton.ButtonComponent.GetComponentInChildren<Text>();
                
                if (_disableButtonTimer <= 0)
                {
                    _disableButton.ButtonComponent.interactable = true;
                    text.text = "Disable Self";
                    UpdateStateText("Button re-enabled");
                    _isDisableButtonTimerActive = false;
                }
                else
                {
                    text.text = $"Re-enabling ({_disableButtonTimer:F1}s)";
                }
            }
        }
    }
} 
