using UnityEngine;
using UnityEngine.UI;
using MelonLoader;

namespace bGUI.Components.Animations
{
    /// <summary>
    /// Manages interactive animations for UI elements.
    /// </summary>
    public class InteractiveAnimations
    {
        public InteractiveAnimations()
        {
            // No need for coroutine runner anymore
        }

        public void PlayBounceAnimation(ButtonWrapper button)
        {
            button.RectTransform.localScale = new Vector3(0.8f, 1.2f, 1f);
            MelonCoroutines.Start(ResetButtonScale(button));
        }

        public void PlayShakeAnimation(ButtonWrapper button)
        {
            Vector2 originalPosition = button.RectTransform.anchoredPosition;
            Vector2 shakeOffset = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            button.RectTransform.anchoredPosition = originalPosition + shakeOffset;
            MelonCoroutines.Start(ResetButtonPosition(button, originalPosition));
        }

        public void PlayFlashAnimation(ButtonWrapper button)
        {
            ColorBlock originalColors = button.ButtonComponent.colors;
            ColorBlock flashColors = originalColors;
            flashColors.normalColor = Color.white;
            button.ButtonComponent.colors = flashColors;
            MelonCoroutines.Start(ResetButtonColors(button, originalColors));
        }

        private System.Collections.IEnumerator ResetButtonScale(ButtonWrapper button)
        {
            yield return new WaitForSeconds(0.1f);
            button.RectTransform.localScale = Vector3.one;
        }

        private System.Collections.IEnumerator ResetButtonPosition(ButtonWrapper button, Vector2 originalPosition)
        {
            yield return new WaitForSeconds(0.1f);
            button.RectTransform.anchoredPosition = originalPosition;
        }

        private System.Collections.IEnumerator ResetButtonColors(ButtonWrapper button, ColorBlock originalColors)
        {
            yield return new WaitForSeconds(0.1f);
            button.ButtonComponent.colors = originalColors;
        }
    }
} 