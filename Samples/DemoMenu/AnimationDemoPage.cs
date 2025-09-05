using UnityEngine;
using UnityEngine.UI;
using bGUI.Components;
using bGUI.Components.Animations;
using System.Collections.Generic;
using bGUI.Core.Components;

namespace bGUI.Samples
{
    /// <summary>
    /// Demo page showcasing animation capabilities.
    /// </summary>
    public class AnimationDemoPage : DemoPageBase
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
        
        // Animation sections
        private PanelWrapper _basicAnimationsPanel = null!;
        private PanelWrapper _interactiveAnimationsPanel = null!;
        private PanelWrapper _sequencedAnimationsPanel = null!;
        
        // Animated elements
        private PanelWrapper _rotatingElement = null!;
        private PanelWrapper _pulsingElement = null!;
        private PanelWrapper _fadingElement = null!;
        private PanelWrapper _movingElement = null!;
        
        // Interactive animation elements
        private ButtonWrapper _toggleAnimationButton = null!;
        private ButtonWrapper _bounceButton = null!;
        private ButtonWrapper _shakeButton = null!;
        private ButtonWrapper _flashButton = null!;
        
        // Sequenced animation elements
        private List<PanelWrapper> _sequenceElements = new List<PanelWrapper>();
        private ButtonWrapper _playSequenceButton = null!;

        // Animation managers
        private readonly List<BaseAnimation> _basicAnimations = new List<BaseAnimation>();
        private InteractiveAnimations _interactiveAnimations;
        private SequenceAnimation _sequenceAnimation = null!;
        private bool _animationsEnabled = true;

        // Track vertical position as we build the UI
        private float _currentY = 40f;

        /// <summary>
        /// Gets the title of the page.
        /// </summary>
        public override string PageTitle => "Animations";
        
        /// <summary>
        /// Initializes a new instance of the AnimationDemoPage.
        /// </summary>
        /// <param name="parent">The parent transform.</param>
        public AnimationDemoPage(RectTransform parent) : base(parent) 
        {
            _interactiveAnimations = new InteractiveAnimations();
        }
        
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
            // We'll use top-down positioning for a more natural content flow
            _contentContainer.anchorMin = new Vector2(0, 1);
            _contentContainer.anchorMax = new Vector2(1, 1);
            _contentContainer.pivot = new Vector2(0.5f, 1);
            _contentContainer.anchoredPosition = Vector2.zero;
            _contentContainer.sizeDelta = new Vector2(0, 1200); // Initial height, will adjust later
            
            CreateHeader();
            CreateBasicAnimations();
            CreateInteractiveAnimations();
            CreateSequencedAnimations();
            CreateAnimationToggle();
            
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
            CreateSectionHeader("Animation Showcase");
            _currentY += 30f; // Add space after the main header
            
            // Description
            _currentY += HEADER_DESCRIPTION_SPACING;
            CreateDescription("Demonstrates various animation techniques using the bGUI framework.");
            
            // Space before next section
            _currentY += SECTION_SPACING;
        }

        private void CreateAnimationToggle()
        {
            // Add some spacing before the toggle button
            _currentY += SECTION_SPACING;
            
            _toggleAnimationButton = UI.Button(_contentContainer)
                .SetText("Toggle Animations")
                .SetSize(200, 50)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .SetColors(
                    new Color(0.3f, 0.6f, 0.9f, 1f),
                    new Color(0.4f, 0.7f, 1f, 1f),
                    new Color(0.2f, 0.5f, 0.8f, 1f),
                    Color.gray)
                .OnClick(ToggleAnimations)
                .Build();
                
            // Update position after adding the button
            _currentY += 50f; // Button height
        }
        
        private void CreateBasicAnimations()
        {
            // Section header
            CreateSectionHeader("Basic Animations");
            _currentY += HEADER_PANEL_SPACING; // Add space between header and panel
            
            // Panel for animations
            float panelWidth = PagePanel.RectTransform.rect.width - (SIDE_PADDING * 2);
            float panelHeight = 180f;
            
            _basicAnimationsPanel = UI.Panel(_contentContainer)
                .SetSize(panelWidth, panelHeight)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .SetBackgroundColor(new Color(0.2f, 0.2f, 0.24f, 1f))
                .WithHorizontalLayout(layout => layout
                    .SetSpacing(100)
                    .SetPadding(20, 20, 20, 20)
                    .SetChildAlignment(TextAnchor.MiddleCenter)
                    .SetChildForceExpand(false, true)
                 )
                .Build();

            CreateRotatingElement();
            CreatePulsingElement();
            CreateFadingElement();
            CreateMovingElement();
            
            // Update position after adding the panel
            _currentY += panelHeight;
            
            // Description
            _currentY += PANEL_DESCRIPTION_SPACING;
            CreateDescription("Simple animations that run continuously in the UI to create visual interest.");
            
            // Space before next section
            _currentY += SECTION_SPACING;
        }

        private void CreateRotatingElement()
        {
            _rotatingElement = CreateAnimatedElement("Rotating", new Color(0.3f, 0.6f, 0.9f, 1f));
            _basicAnimations.Add(new RotateAnimation(_rotatingElement));
        }

        private void CreatePulsingElement()
        {
            _pulsingElement = CreateAnimatedElement("Pulsing", new Color(0.9f, 0.3f, 0.5f, 1f));
            _basicAnimations.Add(new PulseAnimation(_pulsingElement));
        }

        private void CreateFadingElement()
        {
            _fadingElement = CreateAnimatedElement("Fading", new Color(0.4f, 0.8f, 0.4f, 1f));
            _basicAnimations.Add(new FadeAnimation(_fadingElement));
        }

        private void CreateMovingElement()
        {
            _movingElement = CreateAnimatedElement("Moving", new Color(0.9f, 0.7f, 0.2f, 1f));
            _basicAnimations.Add(new MoveAnimation(_movingElement));
        }

        private PanelWrapper CreateAnimatedElement(string label, Color color)
        {
            var element = UI.Panel(_basicAnimationsPanel.RectTransform)
                .SetSize(120, 120)
                .SetBackgroundColor(color)
                .Build();
            
            UI.Text(element.RectTransform)
                .SetContent(label)
                .SetFontSize(16)
                .SetFontStyle(FontStyle.Bold)
                .SetColor(Color.white)
                .SetAnchor(0.5f, 0.5f)
                .Build();

            return element;
        }
        
        private void CreateInteractiveAnimations()
        {
            // Section header
            CreateSectionHeader("Interactive Animations");
            _currentY += HEADER_PANEL_SPACING; // Add space between header and panel
            
            // Panel for interactive animations - NOT using layout group for buttons
            float panelWidth = PagePanel.RectTransform.rect.width - (SIDE_PADDING * 2);
            float panelHeight = 180f;
            
            _interactiveAnimationsPanel = UI.Panel(_contentContainer)
                .SetSize(panelWidth, panelHeight)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .SetBackgroundColor(new Color(0.2f, 0.2f, 0.24f, 1f))
                .Build();

            // Create buttons with explicit positioning
            float buttonWidth = 150f;
            float buttonSpacing = 50f;
            float totalWidth = (buttonWidth * 3) + (buttonSpacing * 2);
            float startX = -totalWidth / 2 + (buttonWidth / 2);
            
            _bounceButton = CreateInteractiveButton("Bounce Effect", 
                startX,
                new Color(0.3f, 0.6f, 0.9f, 1f),
                new Color(0.4f, 0.7f, 1f, 1f),
                new Color(0.2f, 0.5f, 0.8f, 1f),
                () => _interactiveAnimations.PlayBounceAnimation(_bounceButton));

            _shakeButton = CreateInteractiveButton("Shake Effect",
                startX + buttonWidth + buttonSpacing,
                new Color(0.9f, 0.4f, 0.3f, 1f),
                new Color(1f, 0.5f, 0.4f, 1f),
                new Color(0.8f, 0.3f, 0.2f, 1f),
                () => _interactiveAnimations.PlayShakeAnimation(_shakeButton));

            _flashButton = CreateInteractiveButton("Flash Effect",
                startX + (buttonWidth + buttonSpacing) * 2,
                new Color(0.4f, 0.8f, 0.3f, 1f),
                new Color(0.5f, 0.9f, 0.4f, 1f),
                new Color(0.3f, 0.7f, 0.2f, 1f),
                () => _interactiveAnimations.PlayFlashAnimation(_flashButton));
            
            // Update position after adding the panel
            _currentY += panelHeight;
            
            // Description
            _currentY += PANEL_DESCRIPTION_SPACING;
            CreateDescription("Animations triggered by user interaction, commonly used for feedback and emphasizing actions.");
            
            // Space before next section
            _currentY += SECTION_SPACING;
        }

        private ButtonWrapper CreateInteractiveButton(string text, float xPosition, Color normal, Color highlighted, Color pressed, System.Action onClick)
        {
            ButtonWrapper button = UI.Button(_interactiveAnimationsPanel.RectTransform)
                .SetText(text)
                .SetSize(150, 60)
                .SetAnchor(0.5f, 0.5f)
                .SetPivot(0.5f, 0.5f)
                .SetPosition(xPosition, 0)
                .SetColors(normal, highlighted, pressed, Color.gray)
                .OnClick(onClick)
                .Build();
                
            // Ensure text appears correctly
            Text buttonText = button.ButtonComponent.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.fontSize = 16;
                buttonText.alignment = TextAnchor.MiddleCenter;
                buttonText.color = Color.white;
            }
            
            return button;
        }
        
        private void CreateSequencedAnimations()
        {
            // Section header
            CreateSectionHeader("Sequenced Animations");
            _currentY += HEADER_PANEL_SPACING; // Add space between header and panel
            
            // Panel for sequenced animations
            float panelWidth = PagePanel.RectTransform.rect.width - (SIDE_PADDING * 2);
            float panelHeight = 180f;
            
            _sequencedAnimationsPanel = UI.Panel(_contentContainer)
                .SetSize(panelWidth, panelHeight)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .SetBackgroundColor(new Color(0.2f, 0.2f, 0.24f, 1f))
                .Build();
            
            CreateSequenceElements();
            
            // Update position after adding the panel
            _currentY += panelHeight;
            
            // Play button
            _currentY += 20f;
            CreateSequenceButton();
            
            // Update position after adding the button
            _currentY += 50f;
            
            // Description
            _currentY += PANEL_DESCRIPTION_SPACING - 10f;
            CreateDescription("Complex animations that play in sequence to create narrative or guided focus.");
        }

        private void CreateSequenceElements()
        {
            float panelWidth = _sequencedAnimationsPanel.RectTransform.rect.width;
            float elementSpacing = panelWidth / 6; // Divide space into 6 parts (5 elements + spacing)
            float startX = -panelWidth / 2 + elementSpacing; // Start from left edge + first spacing

            for (int i = 0; i < 5; i++)
            {
                var element = UI.Panel(_sequencedAnimationsPanel.RectTransform)
                    .SetSize(80, 80)
                    .SetAnchor(0.5f, 0.5f)
                    .SetPivot(0.5f, 0.5f)
                    .SetPosition(startX + (i * elementSpacing), 0)
                    .SetBackgroundColor(Color.HSVToRGB(i * 0.2f, 0.8f, 0.9f))
                    .Build();
                
                UI.Text(element.RectTransform)
                    .SetContent((i + 1).ToString())
                    .SetFontSize(20)
                    .SetFontStyle(FontStyle.Bold)
                    .SetColor(Color.white)
                    .SetAnchor(0.5f, 0.5f)
                    .Build();
                
                element.GameObject.AddComponent<CanvasGroup>();
                _sequenceElements.Add(element);
            }

            _sequenceAnimation = new SequenceAnimation(_sequenceElements);
            _sequenceAnimation.OnSequenceComplete = () => _playSequenceButton.ButtonComponent.interactable = true;
        }

        private void CreateSequenceButton()
        {
            _playSequenceButton = UI.Button(_contentContainer)
                .SetText("Play Sequence")
                .SetSize(200, 50)
                .SetAnchor(0.5f, 1f)
                .SetPivot(0.5f, 1f)
                .SetPosition(0, -_currentY)
                .SetColors(
                    new Color(0.5f, 0.3f, 0.8f, 1f),
                    new Color(0.6f, 0.4f, 0.9f, 1f),
                    new Color(0.4f, 0.2f, 0.7f, 1f),
                    Color.gray)
                .OnClick(() => {
                    if (_animationsEnabled && !_sequenceAnimation.IsPlaying)
                    {
                        _playSequenceButton.ButtonComponent.interactable = false;
                        _sequenceAnimation.Play();
                    }
                })
                .Build();
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
                
            // Increment current Y for the height of the description
            _currentY += 20f; // Approximately the height of the description text
        }
        
        /// <summary>
        /// Updates the animation states.
        /// </summary>
        public override void Update()
        {
            if (!IsVisible || !_animationsEnabled) return;
            
            float deltaTime = Time.deltaTime;
            
            // Update basic animations
            foreach (var animation in _basicAnimations)
            {
                animation.Update(deltaTime);
            }
            
            // Update sequence animation
            _sequenceAnimation.Update(deltaTime);
        }
        
        /// <summary>
        /// Toggles all animations on/off.
        /// </summary>
        private void ToggleAnimations()
        {
            _animationsEnabled = !_animationsEnabled;
            _toggleAnimationButton.ButtonComponent.GetComponentInChildren<Text>().text = 
                _animationsEnabled ? "Pause Animations" : "Resume Animations";
            
            // Update animation states
            foreach (var animation in _basicAnimations)
            {
                animation.SetEnabled(_animationsEnabled);
            }

            if (!_animationsEnabled)
            {
                _sequenceAnimation.Stop();
                _playSequenceButton.ButtonComponent.interactable = true;
            }
        }
    }
} 
