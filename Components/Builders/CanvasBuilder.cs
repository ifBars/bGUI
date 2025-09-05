using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Components.Builders
{
    /// <summary>
    /// Fluent builder for bGUI canvas.
    /// </summary>
    public class CanvasBuilder
    {
        private readonly CanvasWrapper _canvas;

        /// <summary>
        /// Initializes a new instance of the CanvasBuilder class.
        /// </summary>
        /// <param name="name">The name for the canvas</param>
        public CanvasBuilder(string name = "Canvas")
        {
            _canvas = new CanvasWrapper(name);
        }

        /// <summary>
        /// Sets the render mode of the canvas.
        /// </summary>
        /// <param name="renderMode">The render mode to set</param>
        public CanvasBuilder SetRenderMode(RenderMode renderMode)
        {
            _canvas.SetRenderMode(renderMode);
            return this;
        }

        /// <summary>
        /// Sets the sorting order of the canvas.
        /// </summary>
        /// <param name="sortingOrder">The sorting order to set</param>
        public CanvasBuilder SetSortingOrder(int sortingOrder)
        {
            _canvas.SetSortingOrder(sortingOrder);
            return this;
        }

        /// <summary>
        /// Sets the reference resolution for the canvas scaler.
        /// </summary>
        /// <param name="width">Reference width</param>
        /// <param name="height">Reference height</param>
        public CanvasBuilder SetReferenceResolution(float width, float height)
        {
            _canvas.SetReferenceResolution(width, height);
            return this;
        }

        /// <summary>
        /// Sets the scale mode for the canvas scaler.
        /// </summary>
        /// <param name="scaleMode">The scale mode to set</param>
        public CanvasBuilder SetScaleMode(CanvasScaler.ScaleMode scaleMode)
        {
            _canvas.SetScaleMode(scaleMode);
            return this;
        }

        /// <summary>
        /// Sets the match width or height value for the canvas scaler.
        /// </summary>
        /// <param name="match">The match value (0 = width, 1 = height)</param>
        public CanvasBuilder SetMatchWidthOrHeight(float match)
        {
            _canvas.ScalerComponent.matchWidthOrHeight = match;
            return this;
        }

        /// <summary>
        /// Sets whether to preserve the aspect ratio.
        /// </summary>
        /// <param name="preserve">Whether to preserve the aspect ratio</param>
        public CanvasBuilder SetPreserveAspect(bool preserve)
        {
            _canvas.ScalerComponent.screenMatchMode = preserve 
                ? CanvasScaler.ScreenMatchMode.MatchWidthOrHeight 
                : CanvasScaler.ScreenMatchMode.Expand;
            return this;
        }

        /// <summary>
        /// Sets whether the canvas should persist between scene changes.
        /// </summary>
        /// <param name="dontDestroy">Whether the canvas should persist</param>
        public CanvasBuilder SetDontDestroyOnLoad(bool dontDestroy)
        {
            if (dontDestroy)
            {
                Object.DontDestroyOnLoad(_canvas.GameObject);
            }
            return this;
        }
        
        /// <summary>
        /// Builds and returns the canvas.
        /// </summary>
        public CanvasWrapper? Build()
        {
            return _canvas;
        }
    }
} 
