using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using bGUI;
using bGUI.Components;
using bGUI.Components.Builders;
using bGUI.Components.Animations;
using bGUI.Core;
using System;
using System.Collections.Generic;

[assembly: MelonInfo(typeof(bGUI.Samples.ModSettingsMenu), "Mod Settings Menu", "1.0.0", "Bars")]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace bGUI.Samples
{
    public class ModSettingsMenu : MelonMod
    {
        // MelonPreferences
        private MelonPreferences_Category _prefCategory;
        private MelonPreferences_Entry<bool> _toggleSetting;
        private MelonPreferences_Entry<float> _sliderSetting;
        private MelonPreferences_Entry<string> _textSetting;
        private MelonPreferences_Entry<bool> _animationsEnabled;

        // UI State
        private bool _isMenuVisible;
        private CanvasWrapper? _canvas;
        private PanelWrapper _menuPanel;
        private PanelWrapper _titlePanel;
        private bool _isUILoaded;

        // Animation System
        private List<BaseAnimation> _animations = new List<BaseAnimation>();
        private InteractiveAnimations _interactiveAnimations;
        private SequenceAnimation _openSequence;
        private List<PanelWrapper> _animatedElements = new List<PanelWrapper>();

        // Visual Elements
        private ButtonWrapper _toggleButton;
        private TextWrapper _sliderText;
        private SliderWrapper _powerSlider;
        private float _menuOpenTime;

        public override void OnInitializeMelon()
        {
            // Initialize preferences
            _prefCategory = MelonPreferences.CreateCategory("ExampleMod");
            _toggleSetting = _prefCategory.CreateEntry("ToggleSetting", true, "Toggle Setting");
            _sliderSetting = _prefCategory.CreateEntry("SliderSetting", 0.5f, "Slider Setting");
            _textSetting = _prefCategory.CreateEntry("TextSetting", "bGUI Rocks!", "Text Setting");
            _animationsEnabled = _prefCategory.CreateEntry("AnimationsEnabled", true, "Enable Animations");

            // Initialize animation system
            _interactiveAnimations = new InteractiveAnimations();

            // Subscribe to scene changes to handle UI recreation
            MelonEvents.OnSceneWasLoaded.Subscribe(OnSceneLoaded);

            MelonLogger.Msg("🎮 Creative Mod Settings Menu loaded! Press F1 to open the magic!");
        }

        private void OnSceneLoaded(int buildIndex, string sceneName)
        {
            // If UI was loaded and menu was visible, recreate it
            if (_isUILoaded && _isMenuVisible)
            {
                DestroyUI();
                CreateUI();
                _menuPanel.GameObject.SetActive(true);
                StartOpenAnimation();
            }
        }

        private void EnsureUI()
        {
            if (!_isUILoaded || _canvas == null || _menuPanel == null)
            {
                DestroyUI();
                CreateUI();
                _isUILoaded = true;
            }
        }

        private void DestroyUI()
        {
            StopAllAnimations();
            
            if (_canvas != null)
            {
                _canvas.Destroy();
                _canvas = null;
            }
            _menuPanel = null;
            _titlePanel = null;
            _toggleButton = null;
            _sliderText = null;
            _powerSlider = null;
            _animatedElements.Clear();
            _isUILoaded = false;
        }

        private void CreateUI()
        {
            try
            {
                // Create canvas with fade-in effect
                _canvas = UI.Canvas("CreativeModSettingsCanvas")
                    .SetRenderMode(RenderMode.ScreenSpaceOverlay)
                    .SetSortingOrder(100)
                    .SetReferenceResolution(1920, 1080)
                    .Build();

                // Create main menu panel with gradient-like effect
                _menuPanel = UI.Panel(_canvas.RectTransform)
                    .SetSize(450, 600)
                    .SetAnchor(0.5f, 0.5f)
                    .SetBackgroundColor(new Color(0.08f, 0.12f, 0.18f, 0.98f))
                    .SetRounded(15)
                    .WithVerticalLayout(layout => layout
                        .SetPadding(0)
                        .SetSpacing(0)
                        .SetChildAlignment(TextAnchor.UpperCenter))
                    .Build();

                _animatedElements.Add(_menuPanel);

                // Create window-style title bar
                var titleBarPanel = UI.Panel(_menuPanel.RectTransform)
                    .SetSize(450, 40)
                    .SetBackgroundColor(new Color(0.25f, 0.25f, 0.25f, 0.95f))
                    .SetRounded(15)
                    .WithHorizontalLayout(layout => layout
                        .SetPadding(10, 10, 5, 5)
                        .SetSpacing(175)
                        .SetChildAlignment(TextAnchor.MiddleLeft))
                    .Build();

                // Title text on the left
                UI.Text(titleBarPanel.RectTransform)
                    .SetContent("CREATIVE MOD SETTINGS")
                    .SetFontSize(14)
                    .SetColor(Color.white)
                    .SetAlignment(TextAnchor.MiddleLeft)
                    .SetSize(300, 30)
                    .Build();

                // Window control buttons on the right
                var controlsPanel = UI.Panel(titleBarPanel.RectTransform)
                    .SetSize(120, 30)
                    .SetBackgroundColor(Color.clear)
                    .WithHorizontalLayout(layout => layout
                        .SetPadding(0)
                        .SetSpacing(5)
                        .SetChildAlignment(TextAnchor.MiddleRight))
                    .Build();

                // Random button (minimize-style)
                UI.Button(controlsPanel.RectTransform)
                    .SetSize(30, 25)
                    .SetText("R")
                    .SetColors(
                        new Color(0.6f, 0.3f, 0.8f),
                        new Color(0.7f, 0.4f, 0.9f),
                        new Color(0.5f, 0.2f, 0.7f),
                        new Color(0.3f, 0.3f, 0.3f, 0.5f))
                    .SetRounded(4)
                    .OnClick(() => {
                        _sliderSetting.Value = UnityEngine.Random.Range(0f, 1f);
                        _textSetting.Value = GetRandomMessage();
                        MelonPreferences.Save();
                        
                        // Update slider value to match the preference
                        if (_powerSlider != null)
                        {
                            _powerSlider.Value = _sliderSetting.Value;
                        }
                        
                        if (_animationsEnabled.Value)
                        {
                            StartSequenceAnimation();
                        }
                        
                        UpdateUI();
                    })
                    .Build();

                // Reset button (maximize-style)
                UI.Button(controlsPanel.RectTransform)
                    .SetSize(30, 25)
                    .SetText("O")
                    .SetColors(
                        new Color(0.8f, 0.6f, 0.2f),
                        new Color(0.9f, 0.7f, 0.3f),
                        new Color(0.7f, 0.5f, 0.1f),
                        new Color(0.3f, 0.3f, 0.3f, 0.5f))
                    .SetRounded(4)
                    .OnClick(() => {
                        _toggleSetting.Value = true;
                        _sliderSetting.Value = 0.5f;
                        _textSetting.Value = "bGUI Rocks!";
                        _animationsEnabled.Value = true;
                        MelonPreferences.Save();
                        
                        // Update slider value to match the preference
                        if (_powerSlider != null)
                        {
                            _powerSlider.Value = _sliderSetting.Value;
                        }
                        
                        SetupAnimations();
                        UpdateUI();
                    })
                    .Build();

                // Close button (close-style)
                UI.Button(controlsPanel.RectTransform)
                    .SetSize(30, 25)
                    .SetText("X")
                    .SetColors(
                        new Color(0.8f, 0.2f, 0.2f),
                        new Color(0.9f, 0.3f, 0.3f),
                        new Color(0.7f, 0.1f, 0.1f),
                        new Color(0.3f, 0.3f, 0.3f, 0.5f))
                    .SetRounded(4)
                    .OnClick(() => {
                        _isMenuVisible = false;
                        if (_menuPanel != null)
                        {
                            if (_animationsEnabled.Value)
                            {
                                StartCloseAnimation();
                            }
                            else
                            {
                                _menuPanel.GameObject.SetActive(false);
                            }
                        }
                    })
                    .Build();

                // Create content area panel
                var contentPanel = UI.Panel(_menuPanel.RectTransform)
                    .SetSize(450, 560)
                    .SetBackgroundColor(Color.clear)
                    .WithVerticalLayout(layout => layout
                        .SetPadding(25)
                        .SetSpacing(15)
                        .SetChildAlignment(TextAnchor.UpperCenter))
                    .Build();

                // Create animated title panel with glow effect (moved to content area)
                _titlePanel = UI.Panel(contentPanel.RectTransform)
                    .SetSize(400, 60)
                    .SetBackgroundColor(new Color(0.2f, 0.4f, 0.8f, 0.8f))
                    .SetRounded(10)
                    .Build();

                _animatedElements.Add(_titlePanel);

                UI.Text(_titlePanel.RectTransform)
                    .SetContent("Welcome to bGUI Settings!")
                    .SetFontSize(18)
                    .SetColor(Color.white)
                    .SetAlignment(TextAnchor.MiddleCenter)
                    .SetSize(380, 40)
                    .SetAnchor(0.5f, 0.5f)
                    .Build();

                // Create status indicator
                var statusPanel = UI.Panel(contentPanel.RectTransform)
                    .SetSize(400, 30)
                    .SetBackgroundColor(new Color(0.1f, 0.6f, 0.1f, 0.6f))
                    .SetRounded(15)
                    .Build();

                UI.Text(statusPanel.RectTransform)
                    .SetContent($"Status: {(_animationsEnabled.Value ? "Animations ON" : "Static Mode")}")
                    .SetFontSize(14)
                    .SetColor(Color.white)
                    .SetAlignment(TextAnchor.MiddleCenter)
                    .SetSize(380, 25)
                    .SetAnchor(0.5f, 0.5f)
                    .Build();

                // Create animated toggle setting
                CreateToggleSetting(contentPanel);
                CreateSliderSetting(contentPanel);
                CreateTextSetting(contentPanel);
                CreateAnimationToggle(contentPanel);

                // Setup animations
                SetupAnimations();

                // Set initial visibility
                _menuPanel.GameObject.SetActive(_isMenuVisible);
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Failed to create UI: {ex}");
                DestroyUI();
            }
        }

        private void CreateToggleSetting(PanelWrapper contentPanel)
        {
            var togglePanel = UI.Panel(contentPanel.RectTransform)
                .SetSize(400, 50)
                .SetBackgroundColor(new Color(0.15f, 0.2f, 0.25f, 0.9f))
                .SetRounded(8)
                .WithHorizontalLayout(layout => layout
                    .SetPadding(15)
                    .SetSpacing(15)
                    .SetChildAlignment(TextAnchor.MiddleLeft))
                .Build();

            _animatedElements.Add(togglePanel);

            UI.Text(togglePanel.RectTransform)
                .SetContent("Toggle Feature")
                .SetColor(new Color(0.9f, 0.9f, 1f))
                .SetFontSize(16)
                .Build();

            _toggleButton = UI.Button(togglePanel.RectTransform)
                .SetSize(80, 35)
                .SetText(_toggleSetting.Value ? "ON" : "OFF")
                .SetColors(
                    _toggleSetting.Value ? new Color(0.2f, 0.8f, 0.2f) : new Color(0.8f, 0.2f, 0.2f),
                    _toggleSetting.Value ? new Color(0.3f, 0.9f, 0.3f) : new Color(0.9f, 0.3f, 0.3f),
                    _toggleSetting.Value ? new Color(0.1f, 0.7f, 0.1f) : new Color(0.7f, 0.1f, 0.1f),
                    new Color(0.4f, 0.4f, 0.4f, 0.5f))
                .SetRounded(8)
                .OnClick(() => {
                    _toggleSetting.Value = !_toggleSetting.Value;
                    MelonPreferences.Save();
                    
                    if (_animationsEnabled.Value)
                    {
                        _interactiveAnimations.PlayBounceAnimation(_toggleButton);
                    }
                    
                    UpdateToggleButton();
                })
                .Build();
        }

        private void CreateSliderSetting(PanelWrapper contentPanel)
        {
            var sliderPanel = UI.Panel(contentPanel.RectTransform)
                .SetSize(400, 80)
                .SetBackgroundColor(new Color(0.15f, 0.2f, 0.25f, 0.9f))
                .SetRounded(8)
                .WithVerticalLayout(layout => layout
                    .SetPadding(15)
                    .SetSpacing(10)
                    .SetChildAlignment(TextAnchor.MiddleLeft))
                .Build();

            _animatedElements.Add(sliderPanel);

            _sliderText = UI.Text(sliderPanel.RectTransform)
                .SetContent($"Power Level: {(_sliderSetting.Value * 100):F0}%")
                .SetColor(new Color(0.9f, 0.9f, 1f))
                .SetFontSize(16)
                .Build();

            // Create the actual slider using our new SliderBuilder with more visible colors
            _powerSlider = UI.Slider(sliderPanel.RectTransform)
                .SetHorizontal(370, 30) // Increased height for better visibility
                .SetRange(0f, 1f)
                .SetValue(_sliderSetting.Value)
                .SetColorScheme(
                    Color.Lerp(Color.red, Color.green, _sliderSetting.Value), // Fill color
                    new Color(0.6f, 0.6f, 0.6f, 1f), // Much brighter background color
                    new Color(1f, 1f, 1f, 1f) // Bright white handle
                )
                .OnValueChanged(OnSliderValueChanged)
                .Build();

            // Ensure the slider's handle is visible by setting its size
            if (_powerSlider.SliderComponent.handleRect != null)
            {
                var handleRect = _powerSlider.SliderComponent.handleRect;
                handleRect.sizeDelta = new Vector2(20f, 20f); // Make handle larger and square
            }

            // Update fill color based on current value
            UpdateSliderColors();
        }

        private void OnSliderValueChanged(float value)
        {
            // Update the preference value
            _sliderSetting.Value = value;
            MelonPreferences.Save();
            
            // Update the text display
            if (_sliderText != null)
            {
                _sliderText.Content = $"Power Level: {(value * 100):F0}%";
            }
            
            // Update slider colors based on new value
            UpdateSliderColors();
        }

        private void UpdateSliderColors()
        {
            if (_powerSlider != null)
            {
                // Create a dynamic color that changes from red to green based on value
                var fillColor = Color.Lerp(Color.red, Color.green, _powerSlider.Value);
                
                // Use the new simplified color methods with bright, visible colors
                _powerSlider.SetFillColor(fillColor);
                _powerSlider.SetBackgroundColor(new Color(0.6f, 0.6f, 0.6f, 1f)); // Bright gray background
                _powerSlider.SetHandleColor(Color.white);
            }
        }

        private void CreateTextSetting(PanelWrapper contentPanel)
        {
            var textPanel = UI.Panel(contentPanel.RectTransform)
                .SetSize(400, 70)
                .SetBackgroundColor(new Color(0.15f, 0.2f, 0.25f, 0.9f))
                .SetRounded(8)
                .WithVerticalLayout(layout => layout
                    .SetPadding(15)
                    .SetSpacing(8)
                    .SetChildAlignment(TextAnchor.MiddleLeft))
                .Build();

            _animatedElements.Add(textPanel);

            UI.Text(textPanel.RectTransform)
                .SetContent("Message:")
                .SetColor(new Color(0.9f, 0.9f, 1f))
                .SetFontSize(16)
                .Build();

            var messageBg = UI.Panel(textPanel.RectTransform)
                .SetSize(370, 25)
                .SetBackgroundColor(new Color(0.05f, 0.05f, 0.1f, 0.8f))
                .SetRounded(5)
                .Build();

            UI.Text(messageBg.RectTransform)
                .SetContent($"  {_textSetting.Value}")
                .SetColor(new Color(0.8f, 1f, 0.8f))
                .SetFontSize(14)
                .SetAlignment(TextAnchor.MiddleLeft)
                .SetSize(360, 20)
                .SetAnchor(0.5f, 0.5f)
                .Build();
        }

        private void CreateAnimationToggle(PanelWrapper contentPanel)
        {
            var animPanel = UI.Panel(contentPanel.RectTransform)
                .SetSize(400, 50)
                .SetBackgroundColor(new Color(0.2f, 0.15f, 0.25f, 0.9f))
                .SetRounded(8)
                .WithHorizontalLayout(layout => layout
                    .SetPadding(15)
                    .SetSpacing(15)
                    .SetChildAlignment(TextAnchor.MiddleLeft))
                .Build();

            _animatedElements.Add(animPanel);

            UI.Text(animPanel.RectTransform)
                .SetContent("Animations")
                .SetColor(new Color(0.9f, 0.9f, 1f))
                .SetFontSize(16)
                .Build();

            // Create a local variable to store the button reference
            ButtonWrapper animButton = null;

            // Create the button and assign it to our local variable
            animButton = UI.Button(animPanel.RectTransform)
                .SetSize(80, 35)
                .SetText(_animationsEnabled.Value ? "ON" : "OFF")
                .SetColors(
                    _animationsEnabled.Value ? new Color(0.8f, 0.2f, 0.8f) : new Color(0.4f, 0.4f, 0.4f),
                    _animationsEnabled.Value ? new Color(0.9f, 0.3f, 0.9f) : new Color(0.5f, 0.5f, 0.5f),
                    _animationsEnabled.Value ? new Color(0.7f, 0.1f, 0.7f) : new Color(0.3f, 0.3f, 0.3f),
                    new Color(0.3f, 0.3f, 0.3f, 0.5f))
                .SetRounded(8)
                .OnClick(() => {
                    _animationsEnabled.Value = !_animationsEnabled.Value;
                    MelonPreferences.Save();
                    
                    if (_animationsEnabled.Value)
                    {
                        SetupAnimations();
                        animButton.Text = "ON";
                        animButton.SetColors(
                            new Color(0.8f, 0.2f, 0.8f),
                            new Color(0.9f, 0.3f, 0.9f),
                            new Color(0.7f, 0.1f, 0.7f),
                            new Color(0.3f, 0.3f, 0.3f, 0.5f));
                    }
                    else
                    {
                        StopAllAnimations();
                        animButton.Text = "OFF";
                        animButton.SetColors(
                            new Color(0.4f, 0.4f, 0.4f),
                            new Color(0.5f, 0.5f, 0.5f),
                            new Color(0.3f, 0.3f, 0.3f),
                            new Color(0.3f, 0.3f, 0.3f, 0.5f));
                    }
                })
                .Build();
        }

        private void SetupAnimations()
        {
            if (!_animationsEnabled.Value) return;

            StopAllAnimations();

            // Add pulse animation to title
            if (_titlePanel != null)
            {
                _animations.Add(new PulseAnimation(_titlePanel, 1.5f, 0.05f));
            }

            // Add subtle move animations to panels
            for (int i = 0; i < _animatedElements.Count; i++)
            {
                if (_animatedElements[i] != null)
                {
                    _animations.Add(new MoveAnimation(_animatedElements[i], 0.5f + i * 0.2f, 2f + i));
                }
            }

            // Setup sequence animation for fun effects
            if (_animatedElements.Count > 0)
            {
                _openSequence = new SequenceAnimation(_animatedElements, 0.1f);
            }
        }

        private void StartOpenAnimation()
        {
            if (_animationsEnabled.Value && _openSequence != null)
            {
                _openSequence.Play();
            }
        }

        private void StartSequenceAnimation()
        {
            if (_animationsEnabled.Value && _openSequence != null)
            {
                _openSequence.Play();
            }
        }

        private void StartCloseAnimation()
        {
            // Simple fade out effect by hiding panel after a short delay
            MelonCoroutines.Start(DelayedHide());
        }

        private System.Collections.IEnumerator DelayedHide()
        {
            yield return new WaitForSeconds(0.2f);
            if (_menuPanel != null)
            {
                _menuPanel.GameObject.SetActive(false);
            }
        }

        private void UpdateToggleButton()
        {
            if (_toggleButton != null)
            {
                _toggleButton.Text = _toggleSetting.Value ? "ON" : "OFF";
                _toggleButton.SetColors(
                    _toggleSetting.Value ? new Color(0.2f, 0.8f, 0.2f) : new Color(0.8f, 0.2f, 0.2f),
                    _toggleSetting.Value ? new Color(0.3f, 0.9f, 0.3f) : new Color(0.9f, 0.3f, 0.3f),
                    _toggleSetting.Value ? new Color(0.1f, 0.7f, 0.1f) : new Color(0.7f, 0.1f, 0.1f),
                    new Color(0.4f, 0.4f, 0.4f, 0.5f));
            }
        }

        private void UpdateUI()
        {
            if (_sliderText != null)
            {
                _sliderText.Content = $"📊 Power Level: {(_sliderSetting.Value * 100):F0}%";
            }
            
            // Update slider value and colors if it exists
            if (_powerSlider != null)
            {
                _powerSlider.Value = _sliderSetting.Value;
                UpdateSliderColors();
            }
            
            // Update toggle button state
            UpdateToggleButton();
        }

        private string GetRandomMessage()
        {
            string[] messages = {
                "bGUI is amazing! 🚀",
                "Coding with style! ✨",
                "UI magic happens here! 🎪",
                "Creative chaos! 🌈",
                "Making UIs fun again! 🎮",
                "Powered by creativity! ⚡",
                "Innovation at work! 💡",
                "Beautiful code rocks! 💎"
            };
            return messages[UnityEngine.Random.Range(0, messages.Length)];
        }

        private void StopAllAnimations()
        {
            foreach (var animation in _animations)
            {
                animation?.Reset();
            }
            _animations.Clear();

            if (_openSequence != null)
            {
                _openSequence.Stop();
            }
        }

        public override void OnUpdate()
        {
            // Update animations
            if (_animationsEnabled.Value)
            {
                float deltaTime = Time.deltaTime;
                foreach (var animation in _animations)
                {
                    animation?.Update(deltaTime);
                }

                _openSequence?.Update(deltaTime);
            }

            // Toggle menu with F1 key
            if (Input.GetKeyDown(KeyCode.F1))
            {
                _isMenuVisible = !_isMenuVisible;
                
                if (_isMenuVisible)
                {
                    EnsureUI();
                    _menuOpenTime = Time.time;
                    
                    if (_animationsEnabled.Value)
                    {
                        StartOpenAnimation();
                    }
                }
                
                if (_menuPanel != null)
                {
                    _menuPanel.GameObject.SetActive(_isMenuVisible);
                }
            }
        }

        public override void OnDeinitializeMelon()
        {
            // Clean up UI when mod is unloaded
            DestroyUI();
            
            // Unsubscribe from events
            MelonEvents.OnSceneWasLoaded.Unsubscribe(OnSceneLoaded);
            
            MelonLogger.Msg("👋 Creative Mod Settings Menu unloaded!");
        }
    }
} 