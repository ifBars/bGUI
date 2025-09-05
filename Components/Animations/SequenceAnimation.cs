using System.Collections.Generic;
using UnityEngine;

namespace bGUI.Components.Animations
{
    /// <summary>
    /// Manages a sequence of animations that play one after another.
    /// </summary>
    public class SequenceAnimation
    {
        private readonly List<PanelWrapper> _elements;
        private readonly float _stepDuration;
        private bool _isPlaying;
        private float _timer;
        private int _currentStep;

        public bool IsPlaying => _isPlaying;
        public System.Action? OnSequenceComplete { get; set; } = null;

        public SequenceAnimation(List<PanelWrapper> elements, float stepDuration = 0.2f)
        {
            _elements = elements;
            _stepDuration = stepDuration;
        }

        public void Update(float deltaTime)
        {
            if (!_isPlaying) return;

            _timer += deltaTime;
            if (_timer >= _stepDuration)
            {
                _timer = 0f;
                _currentStep++;

                if (_currentStep < _elements.Count)
                {
                    AnimateCurrentStep();
                }
                else
                {
                    Stop();
                    OnSequenceComplete?.Invoke();
                }
            }
        }

        public void Play()
        {
            if (_isPlaying) return;

            _isPlaying = true;
            _currentStep = -1;
            _timer = _stepDuration; // Trigger first step immediately
            ResetAllElements();
        }

        public void Stop()
        {
            _isPlaying = false;
            _currentStep = 0;
            _timer = 0f;
            ResetAllElements();
        }

        private void AnimateCurrentStep()
        {
            // Scale up current element
            _elements[_currentStep].RectTransform.localScale = new Vector3(1.5f, 1.5f, 1f);

            // Reset previous element if it exists
            if (_currentStep > 0)
            {
                ResetElement(_elements[_currentStep - 1]);
            }
        }

        private void ResetAllElements()
        {
            foreach (var element in _elements)
            {
                ResetElement(element);
            }
        }

        private void ResetElement(PanelWrapper element)
        {
            element.RectTransform.localScale = Vector3.one;
        }
    }
} 
