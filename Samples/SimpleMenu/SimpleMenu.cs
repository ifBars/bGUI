using MelonLoader;
using UnityEngine;
using bGUI;
using bGUI.Core.Extensions;
using bGUI.Core.Constants;
using bGUI.Components;
using System.Collections.Generic;

[assembly: MelonInfo(typeof(bGUI.Samples.SimpleMenuMod), "SimpleMenu", "1.0.0", "Bars")]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace bGUI.Samples
{
    /// <summary>
    /// A demonstration MelonLoader mod showcasing bGUI's elegant abstraction system.
    /// Creates a beautiful, feature-rich settings menu with minimal code.
    /// </summary>
    public class SimpleMenuMod : MelonMod
    {
        // Menu state
        private bool _showMenu = false;
        private int _selectedTab = 0;
        private float _volumeLevel = 0.75f;
        private bool _enableFeatureX = true;
        private bool _enableFeatureY = false;
        private int _qualityLevel = 2;
        private string _status = "Ready";
        
        // UI References
        private CanvasWrapper _canvas;
        private PanelWrapper _mainPanel;
        private PanelWrapper _tabContent;
        private readonly List<ButtonWrapper> _tabButtons = new List<ButtonWrapper>();
        private readonly string[] _tabNames = { "General", "Graphics", "Audio", "Controls", "About" };
        private readonly string[] _qualityOptions = { "Low", "Medium", "High", "Ultra" };

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("SimpleMenu initialized! Press F1 to toggle menu.");
            CreateUI();
        }

        public override void OnUpdate()
        {
            // Toggle menu with F1
            if (Input.GetKeyDown(KeyCode.F1))
            {
                _showMenu = !_showMenu;
                _canvas.GameObject.SetActive(_showMenu);
            }

            // Update status based on settings
            if (_enableFeatureX && _enableFeatureY)
                _status = "All features enabled";
            else if (_enableFeatureX || _enableFeatureY)
                _status = "Some features enabled";
            else
                _status = "Basic mode";
        }

        /// <summary>
        /// Creates the entire UI using bGUI's elegant abstractions.
        /// Notice how clean and readable this code is!
        /// </summary>
        private void CreateUI()
        {
            // Create main canvas with a beautiful fade-in
            _canvas = UI.Canvas("SimpleMenu Canvas")
                .SetRenderMode(RenderMode.ScreenSpaceOverlay)
                .SetSortingOrder(100)
                .FadeIn()
                .Build();
            
            _canvas.GameObject.SetActive(false);

            // Create the main container - a beautiful card with dark theme
            _mainPanel = QuickBuilders.DarkCard(_canvas.RectTransform)
                .SetSize(800, 600)
                .CenterInParent()
                .RoundedLarge()
                .FadeIn(Theme.Anim.Slow);

            // Create stunning header with gradient-like effect
            CreateHeader();
            
            // Create tab navigation - sleek and modern
            CreateTabNavigation();
            
            // Create content area that adapts to selected tab
            CreateContentArea();
            
            // Create footer with status and close button
            CreateFooter();
            
            // Show the general tab by default
            ShowTab(0);
        }

        /// <summary>
        /// Creates a beautiful header with title and subtitle.
        /// </summary>
        private void CreateHeader()
        {
            var headerContainer = QuickBuilders.VerticalContainer(_mainPanel.RectTransform)
                .SetSize(780, 80)
                .SetAnchor(0.5f, 1f)
                .SetPosition(0, -10);

            // Main title with primary theme
            QuickBuilders.TitleText(headerContainer.Transform, "SimpleMenu")
                .SetAnchor(0.5f, 0.5f)
                .Center();

            // Subtitle with secondary color
            QuickBuilders.Label(headerContainer.Transform, "Powered by bGUI Framework")
                .SetAnchor(0.5f, 0.5f)
                .SetPosition(0, -30)
                .Small();
        }

        /// <summary>
        /// Creates the tab navigation bar - demonstrates theme consistency.
        /// </summary>
        private void CreateTabNavigation()
        {
            var tabContainer = QuickBuilders.HorizontalContainer(_mainPanel.RectTransform)
                .SetSize(780, 50)
                .SetAnchor(0.5f, 1f)
                .SetPosition(0, -100);

            for (int i = 0; i < _tabNames.Length; i++)
            {
                int tabIndex = i; // Capture for closure
                var tabButton = UI.Button(tabContainer.Transform)
                    .SetText(_tabNames[i])
                    .SetSize(150, 35)
                    .Secondary()
                    .RoundedSmall()
                    .OnClick(() => ShowTab(tabIndex))
                    .Build();
                
                _tabButtons.Add(tabButton);
            }
        }

        /// <summary>
        /// Creates the main content area - showcases dynamic UI generation.
        /// </summary>
        private void CreateContentArea()
        {
            _tabContent = QuickBuilders.Container(_mainPanel.RectTransform)
                .SetSize(760, 400)
                .SetAnchor(0.5f, 0.5f)
                .SetPosition(0, -50)
                .Light()
                .RoundedSmall();
        }

        /// <summary>
        /// Creates the footer with status and actions.
        /// </summary>
        private void CreateFooter()
        {
            var footerContainer = QuickBuilders.HorizontalContainer(_mainPanel.RectTransform)
                .SetSize(780, 50)
                .SetAnchor(0.5f, 0f)
                .SetPosition(0, 25);

            // Status text on the left
            QuickBuilders.Label(footerContainer.Transform, "Status: Ready")
                .SetName("StatusText")
                .Info();

            // Spacer (empty panel to push buttons right)
            UI.Panel(footerContainer.Transform)
                .SetSize(400, 1)
                .Build();

            // Action buttons on the right
            QuickBuilders.SuccessButton(footerContainer.Transform, "Apply")
                .Small()
                .OnClick(() => {
                    MelonLogger.Msg("Settings applied!");
                    UpdateStatusText("Settings applied successfully!");
                });

            QuickBuilders.SecondaryButton(footerContainer.Transform, "Close")
                .Small()
                .OnClick(() => {
                    _showMenu = false;
                    _canvas.GameObject.SetActive(false);
                });
        }

        /// <summary>
        /// Shows content for the specified tab - demonstrates dynamic content generation.
        /// </summary>
        private void ShowTab(int tabIndex)
        {
            _selectedTab = tabIndex;
            
            // Update tab button appearances
            for (int i = 0; i < _tabButtons.Count; i++)
            {
                // This would need the button to expose its colors, but demonstrates the concept
                var button = _tabButtons[i];
                // In a real implementation, we'd have methods to update button themes
            }

            // Clear existing content
            foreach (Transform child in _tabContent.RectTransform)
            {
                Object.DestroyImmediate(child.gameObject);
            }

            // Create content based on selected tab
            switch (tabIndex)
            {
                case 0: CreateGeneralTab(); break;
                case 1: CreateGraphicsTab(); break;
                case 2: CreateAudioTab(); break;
                case 3: CreateControlsTab(); break;
                case 4: CreateAboutTab(); break;
            }
        }

        /// <summary>
        /// General settings tab - showcases various UI elements working together.
        /// </summary>
        private void CreateGeneralTab()
        {
            var container = QuickBuilders.VerticalContainer(_tabContent.RectTransform);

            // Section header
            QuickBuilders.HeadingText(container.RectTransform, "General Settings")
                .Primary()
                .SetPosition(0, -20);

            // Feature toggles with beautiful styling
            CreateToggleRow(container.RectTransform, "Enable Feature X", _enableFeatureX, 
                value => _enableFeatureX = value);
            
            CreateToggleRow(container.RectTransform, "Enable Feature Y", _enableFeatureY, 
                value => _enableFeatureY = value);

            // Quality selector
            CreateQualitySelector(container.RectTransform);
        }

        /// <summary>
        /// Graphics settings tab - demonstrates sliders and advanced controls.
        /// </summary>
        private void CreateGraphicsTab()
        {
            var container = QuickBuilders.VerticalContainer(_tabContent.RectTransform);

            QuickBuilders.HeadingText(container.RectTransform, "Graphics Settings")
                .Primary()
                .SetPosition(0, -20);

            QuickBuilders.Label(container.RectTransform, "Quality settings and visual options")
                .Secondary()
                .SetPosition(0, -50);

            // In a real implementation, we'd add sliders and more complex controls here
            QuickBuilders.Label(container.RectTransform, "🎮 Graphics options coming soon...")
                .SetPosition(0, -100)
                .Large();
        }

        /// <summary>
        /// Audio settings tab - showcases volume controls.
        /// </summary>
        private void CreateAudioTab()
        {
            var container = QuickBuilders.VerticalContainer(_tabContent.RectTransform);

            QuickBuilders.HeadingText(container.RectTransform, "Audio Settings")
                .Primary()
                .SetPosition(0, -20);

            // Volume control (simplified - would use actual slider in real implementation)
            var volumeContainer = QuickBuilders.HorizontalContainer(container.RectTransform)
                .SetPosition(0, -80);

            QuickBuilders.Label(volumeContainer.RectTransform, "Master Volume:")
                .SetSize(120, 30);

            QuickBuilders.SecondaryButton(volumeContainer.RectTransform, "-")
                .Small()
                .OnClick(() => {
                    _volumeLevel = Mathf.Max(0f, _volumeLevel - 0.1f);
                    UpdateVolumeDisplay();
                });

            QuickBuilders.Label(volumeContainer.RectTransform, $"{(_volumeLevel * 100):F0}%")
                .SetName("VolumeDisplay")
                .SetSize(60, 30)
                .Primary();

            QuickBuilders.SecondaryButton(volumeContainer.RectTransform, "+")
                .Small()
                .OnClick(() => {
                    _volumeLevel = Mathf.Min(1f, _volumeLevel + 0.1f);
                    UpdateVolumeDisplay();
                });
        }

        /// <summary>
        /// Controls tab - demonstrates input configuration UI.
        /// </summary>
        private void CreateControlsTab()
        {
            var container = QuickBuilders.VerticalContainer(_tabContent.RectTransform);

            QuickBuilders.HeadingText(container.RectTransform, "Controls")
                .Primary()
                .SetPosition(0, -20);

            QuickBuilders.Label(container.RectTransform, "🎮 Press F1 to toggle this menu")
                .SetPosition(0, -80)
                .Large()
                .Info();

            QuickBuilders.Label(container.RectTransform, "More keybinding options coming soon...")
                .Secondary()
                .SetPosition(0, -120);
        }

        /// <summary>
        /// About tab - showcases the framework's elegance.
        /// </summary>
        private void CreateAboutTab()
        {
            var container = QuickBuilders.VerticalContainer(_tabContent.RectTransform);

            QuickBuilders.HeadingText(container.RectTransform, "About SimpleMenu")
                .Primary()
                .SetPosition(0, -20);

            QuickBuilders.Label(container.RectTransform, "This menu demonstrates the power of bGUI's")
                .SetPosition(0, -60);

            QuickBuilders.Label(container.RectTransform, "new abstraction system. Notice how clean")
                .SetPosition(0, -80);

            QuickBuilders.Label(container.RectTransform, "and readable the code is!")
                .SetPosition(0, -100);

            QuickBuilders.SuccessText(container.RectTransform, "✨ Built with bGUI Framework ✨")
                .SetPosition(0, -140)
                .Large();

            // Showcase different button themes
            var buttonContainer = QuickBuilders.HorizontalContainer(container.RectTransform)
                .SetPosition(0, -200);

            QuickBuilders.PrimaryButton(buttonContainer.RectTransform, "Primary");
            QuickBuilders.SuccessButton(buttonContainer.RectTransform, "Success");
            QuickBuilders.DangerButton(buttonContainer.RectTransform, "Danger");
            QuickBuilders.SecondaryButton(buttonContainer.RectTransform, "Secondary");
        }

        /// <summary>
        /// Helper method to create toggle rows - demonstrates reusable UI patterns.
        /// </summary>
        private void CreateToggleRow(Transform parent, string label, bool currentValue, System.Action<bool> onToggle)
        {
            var row = QuickBuilders.HorizontalContainer(parent)
                .SetSize(700, 40);

            QuickBuilders.Label(row.RectTransform, label)
                .SetSize(200, 30);

            var toggleButton = currentValue 
                ? QuickBuilders.SuccessButton(row.RectTransform, "ON")
                : QuickBuilders.SecondaryButton(row.RectTransform, "OFF");
                
            toggleButton
                .Small()
                .OnClick(() => {
                    bool newValue = !currentValue;
                    onToggle(newValue);
                    // In real implementation, we'd update the button appearance here
                });
        }

        /// <summary>
        /// Creates a quality level selector - demonstrates custom UI patterns.
        /// </summary>
        private void CreateQualitySelector(Transform parent)
        {
            var row = QuickBuilders.HorizontalContainer(parent)
                .SetSize(700, 40)
                .SetPosition(0, -120);

            QuickBuilders.Label(row.RectTransform, "Quality Level:")
                .SetSize(120, 30);

            for (int i = 0; i < _qualityOptions.Length; i++)
            {
                int qualityIndex = i;
                var button = UI.Button(row.RectTransform)
                    .SetText(_qualityOptions[i])
                    .SetSize(80, 25)
                    .Small()
                    .OnClick(() => _qualityLevel = qualityIndex)
                    .Build();

                // Highlight selected quality
                if (i == _qualityLevel)
                    button.SetColors(Theme.PrimaryButton);
                else
                    button.SetColors(Theme.SecondaryButton);
            }
        }

        /// <summary>
        /// Updates the volume display - demonstrates dynamic UI updates.
        /// </summary>
        private void UpdateVolumeDisplay()
        {
            var volumeDisplay = GameObject.Find("VolumeDisplay");
            if (volumeDisplay != null)
            {
                var textComponent = volumeDisplay.GetComponent<UnityEngine.UI.Text>();
                if (textComponent != null)
                    textComponent.text = $"{(_volumeLevel * 100):F0}%";
            }
        }

        /// <summary>
        /// Updates the status text - demonstrates dynamic content updates.
        /// </summary>
        private void UpdateStatusText(string status)
        {
            _status = status;
            var statusText = GameObject.Find("StatusText");
            if (statusText != null)
            {
                var textComponent = statusText.GetComponent<UnityEngine.UI.Text>();
                if (textComponent != null)
                    textComponent.text = $"Status: {status}";
            }
        }
    }
} 