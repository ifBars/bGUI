using bGUI.Core.Abstractions;
using UnityEngine;

namespace bGUI.Core.Containers
{
    /// <summary>
    /// Interface for canvas UI elements.
    /// </summary>
    public interface ICanvas : IUIElement
    {
        /// <summary>
        /// Gets the Canvas component.
        /// </summary>
        Canvas CanvasComponent { get; }
        
        /// <summary>
        /// Gets the Canvas Scaler component.
        /// </summary>
        UnityEngine.UI.CanvasScaler ScalerComponent { get; }
        
        /// <summary>
        /// Gets the Graphic Raycaster component.
        /// </summary>
        UnityEngine.UI.GraphicRaycaster RaycasterComponent { get; }
        
        /// <summary>
        /// Sets the render mode of the canvas.
        /// </summary>
        /// <param name="renderMode">The render mode to set</param>
        void SetRenderMode(RenderMode renderMode);
        
        /// <summary>
        /// Sets the sorting order of the canvas.
        /// </summary>
        /// <param name="sortingOrder">The sorting order to set</param>
        void SetSortingOrder(int sortingOrder);
        
        /// <summary>
        /// Sets the reference resolution for the canvas scaler.
        /// </summary>
        /// <param name="width">Reference width</param>
        /// <param name="height">Reference height</param>
        void SetReferenceResolution(float width, float height);
        
        /// <summary>
        /// Sets the scale mode for the canvas scaler.
        /// </summary>
        /// <param name="scaleMode">The scale mode to set</param>
        void SetScaleMode(UnityEngine.UI.CanvasScaler.ScaleMode scaleMode);
    }
} 