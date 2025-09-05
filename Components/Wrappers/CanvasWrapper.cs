using bGUI.Core.Abstractions;
using bGUI.Core.Containers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace bGUI.Components
{
    /// <summary>
    /// Wrapper for Unity's Canvas component.
    /// </summary>
    public class CanvasWrapper : UIElementBase, ICanvas
    {
        private Canvas _canvas;
        private CanvasScaler _canvasScaler;
        private GraphicRaycaster _graphicRaycaster;

        /// <summary>
        /// Gets the Canvas component.
        /// </summary>
        public Canvas CanvasComponent => _canvas;

        /// <summary>
        /// Gets the Canvas Scaler component.
        /// </summary>
        public CanvasScaler ScalerComponent => _canvasScaler;

        /// <summary>
        /// Gets the Graphic Raycaster component.
        /// </summary>
        public GraphicRaycaster RaycasterComponent => _graphicRaycaster;

        /// <summary>
        /// Initializes a new instance of the CanvasWrapper class.
        /// </summary>
        /// <param name="name">The name of the canvas</param>
        public CanvasWrapper(string name = "Canvas") 
            : base(null, name)
        {
            EnsureEventSystem();
            
            // Add Canvas component
            _canvas = _gameObject.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            
            // Add Canvas Scaler component
            _canvasScaler = _gameObject.AddComponent<CanvasScaler>();
            _canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            _canvasScaler.referenceResolution = new Vector2(1920, 1080);
            _canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            _canvasScaler.matchWidthOrHeight = 0.5f;
            
            // Add Graphic Raycaster component
            _graphicRaycaster = _gameObject.AddComponent<GraphicRaycaster>();
            
            // Don't destroy when loading new scenes
            Object.DontDestroyOnLoad(_gameObject);
        }

        /// <summary>
        /// Ensures there is an EventSystem in the scene for uGUI input.
        /// </summary>
        private static void EnsureEventSystem()
        {
            if (Object.FindObjectOfType<EventSystem>() != null)
            {
                return;
            }

            var es = new GameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();
            Object.DontDestroyOnLoad(es);
        }

        /// <summary>
        /// Sets the render mode of the canvas.
        /// </summary>
        /// <param name="renderMode">The render mode to set</param>
        public void SetRenderMode(RenderMode renderMode)
        {
            _canvas.renderMode = renderMode;
        }

        /// <summary>
        /// Sets the sorting order of the canvas.
        /// </summary>
        /// <param name="sortingOrder">The sorting order to set</param>
        public void SetSortingOrder(int sortingOrder)
        {
            _canvas.sortingOrder = sortingOrder;
        }

        /// <summary>
        /// Sets the reference resolution for the canvas scaler.
        /// </summary>
        /// <param name="width">Reference width</param>
        /// <param name="height">Reference height</param>
        public void SetReferenceResolution(float width, float height)
        {
            _canvasScaler.referenceResolution = new Vector2(width, height);
        }

        /// <summary>
        /// Sets the scale mode for the canvas scaler.
        /// </summary>
        /// <param name="scaleMode">The scale mode to set</param>
        public void SetScaleMode(CanvasScaler.ScaleMode scaleMode)
        {
            _canvasScaler.uiScaleMode = scaleMode;
        }

        /// <summary>
        /// Destroys the canvas.
        /// </summary>
        public override void Destroy()
        {
            if (_gameObject != null)
            {
                Object.Destroy(_gameObject);
            }
        }
    }
} 