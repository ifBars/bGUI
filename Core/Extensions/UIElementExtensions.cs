using UnityEngine;
using bGUI.Core.Abstractions;
using bGUI.Core.Enums;
using System.Collections;

namespace bGUI.Core.Extensions
{
    /// <summary>
    /// Extension methods for UI elements to enhance functionality.
    /// </summary>
    public static class UIElementExtensions
    {
        /// <summary>
        /// Sets the anchor preset for the UI element.
        /// </summary>
        /// <param name="element">The UI element to modify</param>
        /// <param name="preset">The anchor preset to apply</param>
        /// <returns>The UI element for method chaining</returns>
        public static T SetAnchorPreset<T>(this T element, AnchorPreset preset) where T : IUIElement
        {
            element.RectTransform.SetAnchorPreset(preset);
            return element;
        }

        /// <summary>
        /// Sets the size of the UI element.
        /// </summary>
        /// <param name="element">The UI element to modify</param>
        /// <param name="width">The width to set</param>
        /// <param name="height">The height to set</param>
        /// <returns>The UI element for method chaining</returns>
        public static T SetSize<T>(this T element, float width, float height) where T : IUIElement
        {
            element.RectTransform.SetSize(width, height);
            return element;
        }

        /// <summary>
        /// Sets the position of the UI element.
        /// </summary>
        /// <param name="element">The UI element to modify</param>
        /// <param name="x">The x position</param>
        /// <param name="y">The y position</param>
        /// <returns>The UI element for method chaining</returns>
        public static T SetPosition<T>(this T element, float x, float y) where T : IUIElement
        {
            element.RectTransform.SetPosition(x, y);
            return element;
        }

        /// <summary>
        /// Sets the UI element to fill its parent.
        /// </summary>
        /// <param name="element">The UI element to modify</param>
        /// <param name="padding">Optional uniform padding</param>
        /// <returns>The UI element for method chaining</returns>
        public static T FillParent<T>(this T element, float padding = 0f) where T : IUIElement
        {
            element.RectTransform.FillParent(padding);
            return element;
        }

        /// <summary>
        /// Centers the UI element in its parent.
        /// </summary>
        /// <param name="element">The UI element to modify</param>
        /// <returns>The UI element for method chaining</returns>
        public static T CenterInParent<T>(this T element) where T : IUIElement
        {
            element.RectTransform.CenterInParent();
            return element;
        }

        /// <summary>
        /// Docks the UI element to a specific side of its parent.
        /// </summary>
        /// <param name="element">The UI element to modify</param>
        /// <param name="side">The side to dock to</param>
        /// <param name="size">The size along the docking axis</param>
        /// <param name="margin">Optional margin from edges</param>
        /// <returns>The UI element for method chaining</returns>
        public static T Dock<T>(this T element, DockSide side, float size, float margin = 0f) where T : class, IUIElement
        {
            switch (side)
            {
                case DockSide.Top:
                    element.RectTransform.DockTop(size, margin);
                    break;
                case DockSide.Bottom:
                    element.RectTransform.DockBottom(size, margin);
                    break;
                case DockSide.Left:
                    element.RectTransform.DockLeft(size, margin);
                    break;
                case DockSide.Right:
                    element.RectTransform.DockRight(size, margin);
                    break;
            }
            return element;
        }

        /// <summary>
        /// Fades the UI element in over time.
        /// </summary>
        /// <param name="element">The UI element to fade</param>
        /// <param name="duration">The duration of the fade</param>
        /// <param name="onComplete">Optional callback when fade is complete</param>
        /// <returns>The UI element for method chaining</returns>
        public static T FadeIn<T>(this T element, float duration = 0.3f, System.Action? onComplete = null) where T : IUIElement
        {
            var canvasGroup = GetOrAddCanvasGroup(element.GameObject);
            canvasGroup.alpha = 0f;
            element.GameObject.SetActive(true);
            
            // Simple coroutine-like fade using MonoBehaviour if available
            var fadeBehaviour = element.GameObject.GetComponent<MonoBehaviour>() ?? element.GameObject.AddComponent<SimpleMonoBehaviour>();
            fadeBehaviour.StartCoroutine(FadeCoroutine(canvasGroup, 0f, 1f, duration, onComplete));
            
            return element;
        }

        /// <summary>
        /// Fades the UI element out over time.
        /// </summary>
        /// <param name="element">The UI element to fade</param>
        /// <param name="duration">The duration of the fade</param>
        /// <param name="hideOnComplete">Whether to hide the element when fade is complete</param>
        /// <param name="onComplete">Optional callback when fade is complete</param>
        /// <returns>The UI element for method chaining</returns>
        public static T FadeOut<T>(this T element, float duration = 0.3f, bool hideOnComplete = true, System.Action? onComplete = null) where T : IUIElement
        {
            var canvasGroup = GetOrAddCanvasGroup(element.GameObject);
            
            var fadeBehaviour = element.GameObject.GetComponent<MonoBehaviour>() ?? element.GameObject.AddComponent<SimpleMonoBehaviour>();
            fadeBehaviour.StartCoroutine(FadeCoroutine(canvasGroup, canvasGroup.alpha, 0f, duration, () =>
            {
                if (hideOnComplete)
                    element.GameObject.SetActive(false);
                onComplete?.Invoke();
            }));
            
            return element;
        }

        /// <summary>
        /// Sets the alpha of the UI element.
        /// </summary>
        /// <param name="element">The UI element to modify</param>
        /// <param name="alpha">The alpha value (0-1)</param>
        /// <returns>The UI element for method chaining</returns>
        public static T SetAlpha<T>(this T element, float alpha) where T : IUIElement
        {
            var canvasGroup = GetOrAddCanvasGroup(element.GameObject);
            canvasGroup.alpha = Mathf.Clamp01(alpha);
            return element;
        }

        /// <summary>
        /// Sets whether the UI element can be interacted with.
        /// </summary>
        /// <param name="element">The UI element to modify</param>
        /// <param name="interactable">Whether the element is interactable</param>
        /// <returns>The UI element for method chaining</returns>
        public static T SetInteractable<T>(this T element, bool interactable) where T : IUIElement
        {
            var canvasGroup = GetOrAddCanvasGroup(element.GameObject);
            canvasGroup.interactable = interactable;
            return element;
        }

        /// <summary>
        /// Sets whether the UI element blocks raycasts.
        /// </summary>
        /// <param name="element">The UI element to modify</param>
        /// <param name="blocksRaycasts">Whether the element blocks raycasts</param>
        /// <returns>The UI element for method chaining</returns>
        public static T SetBlocksRaycasts<T>(this T element, bool blocksRaycasts) where T : IUIElement
        {
            var canvasGroup = GetOrAddCanvasGroup(element.GameObject);
            canvasGroup.blocksRaycasts = blocksRaycasts;
            return element;
        }

        /// <summary>
        /// Gets or adds a CanvasGroup component to the GameObject.
        /// </summary>
        /// <param name="gameObject">The GameObject to modify</param>
        /// <returns>The CanvasGroup component</returns>
        private static CanvasGroup GetOrAddCanvasGroup(GameObject gameObject)
        {
            return gameObject.GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
        }

        /// <summary>
        /// Coroutine for fading UI elements.
        /// </summary>
        private static IEnumerator FadeCoroutine(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration, System.Action onComplete)
        {
            float elapsedTime = 0f;
            
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
                yield return null;
            }
            
            canvasGroup.alpha = endAlpha;
            onComplete?.Invoke();
        }
    }

    /// <summary>
    /// Simple MonoBehaviour for running coroutines.
    /// </summary>
    internal class SimpleMonoBehaviour : MonoBehaviour
    {
        // Empty MonoBehaviour to enable coroutines
    }

    /// <summary>
    /// Defines sides for docking UI elements.
    /// </summary>
    public enum DockSide
    {
        Top,
        Bottom,
        Left,
        Right
    }
} 
