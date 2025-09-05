using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Utilities
{
    /// <summary>
    /// Utility class for UI layout operations.
    /// </summary>
    public static class LayoutUtils
    {
        /// <summary>
        /// Anchors a RectTransform to fill its parent.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="padding">Optional padding from edges</param>
        public static void AnchorToFill(RectTransform rectTransform, float padding = 0f)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = new Vector2(-padding * 2f, -padding * 2f);
            rectTransform.anchoredPosition = Vector2.zero;
        }

        /// <summary>
        /// Anchors a RectTransform to the top of its parent.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="height">The height of the element</param>
        /// <param name="padding">Optional padding from edges</param>
        public static void AnchorToTop(RectTransform rectTransform, float height, float padding = 0f)
        {
            rectTransform.anchorMin = new Vector2(0f, 1f);
            rectTransform.anchorMax = new Vector2(1f, 1f);
            rectTransform.sizeDelta = new Vector2(-padding * 2f, height);
            rectTransform.anchoredPosition = new Vector2(0f, -height * 0.5f - padding);
        }

        /// <summary>
        /// Anchors a RectTransform to the bottom of its parent.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="height">The height of the element</param>
        /// <param name="padding">Optional padding from edges</param>
        public static void AnchorToBottom(RectTransform rectTransform, float height, float padding = 0f)
        {
            rectTransform.anchorMin = new Vector2(0f, 0f);
            rectTransform.anchorMax = new Vector2(1f, 0f);
            rectTransform.sizeDelta = new Vector2(-padding * 2f, height);
            rectTransform.anchoredPosition = new Vector2(0f, height * 0.5f + padding);
        }

        /// <summary>
        /// Anchors a RectTransform to the left of its parent.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="width">The width of the element</param>
        /// <param name="padding">Optional padding from edges</param>
        public static void AnchorToLeft(RectTransform rectTransform, float width, float padding = 0f)
        {
            rectTransform.anchorMin = new Vector2(0f, 0f);
            rectTransform.anchorMax = new Vector2(0f, 1f);
            rectTransform.sizeDelta = new Vector2(width, -padding * 2f);
            rectTransform.anchoredPosition = new Vector2(width * 0.5f + padding, 0f);
        }

        /// <summary>
        /// Anchors a RectTransform to the right of its parent.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="width">The width of the element</param>
        /// <param name="padding">Optional padding from edges</param>
        public static void AnchorToRight(RectTransform rectTransform, float width, float padding = 0f)
        {
            rectTransform.anchorMin = new Vector2(1f, 0f);
            rectTransform.anchorMax = new Vector2(1f, 1f);
            rectTransform.sizeDelta = new Vector2(width, -padding * 2f);
            rectTransform.anchoredPosition = new Vector2(-width * 0.5f - padding, 0f);
        }

        /// <summary>
        /// Centers a RectTransform in its parent.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to modify</param>
        /// <param name="size">The size of the element</param>
        public static void CenterInParent(RectTransform rectTransform, Vector2 size)
        {
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.sizeDelta = size;
            rectTransform.anchoredPosition = Vector2.zero;
        }

        /// <summary>
        /// Sets up a Canvas with a CanvasScaler for screen space overlay.
        /// </summary>
        /// <param name="canvas">The Canvas to set up</param>
        /// <param name="referenceResolution">The reference resolution for scaling</param>
        /// <param name="matchWidthOrHeight">Value between 0 (width) and 1 (height) determining scaling axis</param>
        public static void SetupCanvasScaler(Canvas canvas, Vector2 referenceResolution, float matchWidthOrHeight = 0.5f)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var canvasScaler = canvas.GetComponent<CanvasScaler>() ?? canvas.gameObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = referenceResolution;
            canvasScaler.matchWidthOrHeight = matchWidthOrHeight;

            var raycaster = canvas.GetComponent<GraphicRaycaster>() ?? canvas.gameObject.AddComponent<GraphicRaycaster>();
        }

        /// <summary>
        /// Adds a LayoutElement with fixed width to a GameObject. Use this to set a fixed width of an element within a layout.
        /// </summary>
        /// <param name="gameObject">The GameObject to add the LayoutElement to</param>
        /// <param name="width">The fixed width</param>
        /// <returns>The created LayoutElement</returns>
        public static LayoutElement SetFixedWidth(GameObject gameObject, float width)
        {
            var layoutElement = gameObject.GetComponent<LayoutElement>() ?? gameObject.AddComponent<LayoutElement>();
            layoutElement.minWidth = width;
            layoutElement.preferredWidth = width;
            return layoutElement;
        }

        /// <summary>
        /// Adds a LayoutElement with fixed height to a GameObject. Use this to set a fixed height of an element within a layout.
        /// </summary>
        /// <param name="gameObject">The GameObject to add the LayoutElement to</param>
        /// <param name="height">The fixed height</param>
        /// <returns>The created LayoutElement</returns>
        public static LayoutElement SetFixedHeight(GameObject gameObject, float height)
        {
            var layoutElement = gameObject.GetComponent<LayoutElement>() ?? gameObject.AddComponent<LayoutElement>();
            layoutElement.minHeight = height;
            layoutElement.preferredHeight = height;
            return layoutElement;
        }

        /// <summary>
        /// Adds a LayoutElement with fixed size to a GameObject. Use this to set a fixed size of an element within a layout.
        /// </summary>
        /// <param name="gameObject">The GameObject to add the LayoutElement to</param>
        /// <param name="width">The fixed width</param>
        /// <param name="height">The fixed height</param>
        /// <returns>The created LayoutElement</returns>
        public static LayoutElement SetFixedSize(GameObject gameObject, float width, float height)
        {
            var layoutElement = gameObject.GetComponent<LayoutElement>() ?? gameObject.AddComponent<LayoutElement>();
            layoutElement.minWidth = width;
            layoutElement.preferredWidth = width;
            layoutElement.minHeight = height;
            layoutElement.preferredHeight = height;
            return layoutElement;
        }

        /// <summary>
        /// Adds a LayoutElement with flexible width to a GameObject. Use this to set a custom flexible width of an element within a layout.
        /// </summary>
        /// <param name="gameObject">The GameObject to add the LayoutElement to</param>
        /// <param name="flexibleWidth">The flexible width value (relative to other flexible elements)</param>
        /// <returns>The created LayoutElement</returns>
        public static LayoutElement SetFlexibleWidth(GameObject gameObject, float flexibleWidth = 1f)
        {
            var layoutElement = gameObject.GetComponent<LayoutElement>() ?? gameObject.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = flexibleWidth;
            return layoutElement;
        }

        /// <summary>
        /// Adds a LayoutElement with flexible height to a GameObject. Use this to set a custom flexible height of an element within a layout.
        /// </summary>
        /// <param name="gameObject">The GameObject to add the LayoutElement to</param>
        /// <param name="flexibleHeight">The flexible height value (relative to other flexible elements)</param>
        /// <returns>The created LayoutElement</returns>
        public static LayoutElement SetFlexibleHeight(GameObject gameObject, float flexibleHeight = 1f)
        {
            var layoutElement = gameObject.GetComponent<LayoutElement>() ?? gameObject.AddComponent<LayoutElement>();
            layoutElement.flexibleHeight = flexibleHeight;
            return layoutElement;
        }

        /// <summary>
        /// Adds a LayoutElement with flexible size to a GameObject. Use this to set a custom flexible size of an element within a layout.
        /// </summary>
        /// <param name="gameObject">The GameObject to add the LayoutElement to</param>
        /// <param name="flexibleWidth">The flexible width value</param>
        /// <param name="flexibleHeight">The flexible height value</param>
        /// <returns>The created LayoutElement</returns>
        public static LayoutElement SetFlexibleSize(GameObject gameObject, float flexibleWidth = 1f, float flexibleHeight = 1f)
        {
            var layoutElement = gameObject.GetComponent<LayoutElement>() ?? gameObject.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = flexibleWidth;
            layoutElement.flexibleHeight = flexibleHeight;
            return layoutElement;
        }
    }
}