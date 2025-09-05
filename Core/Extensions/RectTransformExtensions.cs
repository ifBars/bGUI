using UnityEngine;
using bGUI.Core.Enums;

namespace bGUI.Core.Extensions
{
    /// <summary>
    /// Extension methods for RectTransform to simplify UI layout operations.
    /// </summary>
    public static class RectTransformExtensions
    {
        /// <summary>
        /// Sets the anchor preset for the RectTransform.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="preset">The anchor preset to apply</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform SetAnchorPreset(this RectTransform rectTransform, AnchorPreset preset)
        {
            var (anchorMin, anchorMax) = AnchorPresetHelper.GetAnchors(preset);
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
            rectTransform.pivot = AnchorPresetHelper.GetDefaultPivot(preset);
            return rectTransform;
        }

        /// <summary>
        /// Sets the size of the RectTransform.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="width">The width to set</param>
        /// <param name="height">The height to set</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform SetSize(this RectTransform rectTransform, float width, float height)
        {
            rectTransform.sizeDelta = new Vector2(width, height);
            return rectTransform;
        }

        /// <summary>
        /// Sets the size of the RectTransform.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="size">The size to set</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform SetSize(this RectTransform rectTransform, Vector2 size)
        {
            rectTransform.sizeDelta = size;
            return rectTransform;
        }

        /// <summary>
        /// Sets the width of the RectTransform.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="width">The width to set</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform SetWidth(this RectTransform rectTransform, float width)
        {
            rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
            return rectTransform;
        }

        /// <summary>
        /// Sets the height of the RectTransform.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="height">The height to set</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform SetHeight(this RectTransform rectTransform, float height)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
            return rectTransform;
        }

        /// <summary>
        /// Sets the anchored position of the RectTransform.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="x">The x position</param>
        /// <param name="y">The y position</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform SetPosition(this RectTransform rectTransform, float x, float y)
        {
            rectTransform.anchoredPosition = new Vector2(x, y);
            return rectTransform;
        }

        /// <summary>
        /// Sets the anchored position of the RectTransform.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="position">The position to set</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform SetPosition(this RectTransform rectTransform, Vector2 position)
        {
            rectTransform.anchoredPosition = position;
            return rectTransform;
        }

        /// <summary>
        /// Sets the pivot of the RectTransform.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="x">The x pivot</param>
        /// <param name="y">The y pivot</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform SetPivot(this RectTransform rectTransform, float x, float y)
        {
            rectTransform.pivot = new Vector2(x, y);
            return rectTransform;
        }

        /// <summary>
        /// Sets the pivot of the RectTransform.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="pivot">The pivot to set</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform SetPivot(this RectTransform rectTransform, Vector2 pivot)
        {
            rectTransform.pivot = pivot;
            return rectTransform;
        }

        /// <summary>
        /// Sets the anchors of the RectTransform.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="anchorMin">The minimum anchor</param>
        /// <param name="anchorMax">The maximum anchor</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform SetAnchors(this RectTransform rectTransform, Vector2 anchorMin, Vector2 anchorMax)
        {
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
            return rectTransform;
        }

        /// <summary>
        /// Sets the RectTransform to fill its parent.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="padding">Optional uniform padding</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform FillParent(this RectTransform rectTransform, float padding = 0f)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = new Vector2(-padding * 2f, -padding * 2f);
            rectTransform.anchoredPosition = Vector2.zero;
            return rectTransform;
        }

        /// <summary>
        /// Sets the RectTransform to fill its parent with specific padding.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="left">Left padding</param>
        /// <param name="right">Right padding</param>
        /// <param name="top">Top padding</param>
        /// <param name="bottom">Bottom padding</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform FillParent(this RectTransform rectTransform, float left, float right, float top, float bottom)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = new Vector2(left, bottom);
            rectTransform.offsetMax = new Vector2(-right, -top);
            return rectTransform;
        }

        /// <summary>
        /// Centers the RectTransform in its parent.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform CenterInParent(this RectTransform rectTransform)
        {
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;
            return rectTransform;
        }

        /// <summary>
        /// Docks the RectTransform to the top of its parent.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="height">The height of the element</param>
        /// <param name="margin">Optional margin from edges</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform DockTop(this RectTransform rectTransform, float height, float margin = 0f)
        {
            rectTransform.anchorMin = new Vector2(0f, 1f);
            rectTransform.anchorMax = new Vector2(1f, 1f);
            rectTransform.sizeDelta = new Vector2(-margin * 2f, height);
            rectTransform.anchoredPosition = new Vector2(0f, -height * 0.5f - margin);
            return rectTransform;
        }

        /// <summary>
        /// Docks the RectTransform to the bottom of its parent.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="height">The height of the element</param>
        /// <param name="margin">Optional margin from edges</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform DockBottom(this RectTransform rectTransform, float height, float margin = 0f)
        {
            rectTransform.anchorMin = new Vector2(0f, 0f);
            rectTransform.anchorMax = new Vector2(1f, 0f);
            rectTransform.sizeDelta = new Vector2(-margin * 2f, height);
            rectTransform.anchoredPosition = new Vector2(0f, height * 0.5f + margin);
            return rectTransform;
        }

        /// <summary>
        /// Docks the RectTransform to the left of its parent.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="width">The width of the element</param>
        /// <param name="margin">Optional margin from edges</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform DockLeft(this RectTransform rectTransform, float width, float margin = 0f)
        {
            rectTransform.anchorMin = new Vector2(0f, 0f);
            rectTransform.anchorMax = new Vector2(0f, 1f);
            rectTransform.sizeDelta = new Vector2(width, -margin * 2f);
            rectTransform.anchoredPosition = new Vector2(width * 0.5f + margin, 0f);
            return rectTransform;
        }

        /// <summary>
        /// Docks the RectTransform to the right of its parent.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="width">The width of the element</param>
        /// <param name="margin">Optional margin from edges</param>
        /// <returns>The RectTransform for method chaining</returns>
        public static RectTransform DockRight(this RectTransform rectTransform, float width, float margin = 0f)
        {
            rectTransform.anchorMin = new Vector2(1f, 0f);
            rectTransform.anchorMax = new Vector2(1f, 1f);
            rectTransform.sizeDelta = new Vector2(width, -margin * 2f);
            rectTransform.anchoredPosition = new Vector2(-width * 0.5f - margin, 0f);
            return rectTransform;
        }
    }
} 