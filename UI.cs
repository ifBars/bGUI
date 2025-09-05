using UnityEngine;
using bGUI.Components;
using bGUI.Components.Builders;
using bGUI.Components.Builders.Layout;
using bGUI.Core.Factory;

namespace bGUI
{
    /// <summary>
    /// Static entry point for fluent UI creation in bGUI.
    /// </summary>
    public static class UI
    {
        /// <summary>
        /// Gets or sets whether to use object pooling by default for all UI elements.
        /// This can improve performance by reusing existing objects instead of creating new ones.
        /// </summary>
        public static bool UsePoolingByDefault { get; set; } = false;

        // UI Element Builders - now using UIFactory for better performance
        public static ButtonBuilder Button(Transform? parent = null) => new ButtonBuilder(parent, UsePoolingByDefault);
        public static PanelBuilder Panel(Transform? parent = null) => new PanelBuilder(parent, UsePoolingByDefault);
        public static TextBuilder Text(Transform? parent = null) => new TextBuilder(parent, UsePoolingByDefault);
        public static ImageBuilder Image(Transform? parent = null) => new ImageBuilder(parent, UsePoolingByDefault);
        public static ScrollViewBuilder ScrollView(Transform? parent = null) => new ScrollViewBuilder(parent, UsePoolingByDefault);
        public static SliderBuilder Slider(Transform? parent = null) => new SliderBuilder(parent, UsePoolingByDefault);
        public static ToggleBuilder Toggle(Transform? parent = null) => new ToggleBuilder(parent, UsePoolingByDefault);
        public static DropdownBuilder Dropdown(Transform? parent = null) => new DropdownBuilder(parent, UsePoolingByDefault);
        public static ToggleGroupBuilder ToggleGroup(Transform? parent = null) => new ToggleGroupBuilder(parent, UsePoolingByDefault);

        // Layout Builders
        public static HorizontalLayoutBuilder HorizontalLayout(GameObject owner) => new HorizontalLayoutBuilder(owner);
        public static VerticalLayoutBuilder VerticalLayout(GameObject owner) => new VerticalLayoutBuilder(owner);
        public static GridLayoutBuilder GridLayout(GameObject owner) => new GridLayoutBuilder(owner);
        
        public static CanvasBuilder Canvas(string name = "Canvas") => new CanvasBuilder(name);

        /// <summary>
        /// Returns a UI element to the object pool for reuse.
        /// This helps with performance by recycling objects instead of destroying them.
        /// </summary>
        /// <param name="element">The UI element to return to the pool</param>
        public static void ReturnToPool(Core.Abstractions.IUIElement element)
        {
            UIFactory.Instance.ReturnToPool(element);
        }

        /// <summary>
        /// Enables object pooling globally for all new UI elements.
        /// </summary>
        public static void EnablePooling()
        {
            UsePoolingByDefault = true;
        }

        /// <summary>
        /// Disables object pooling globally for all new UI elements.
        /// </summary>
        public static void DisablePooling()
        {
            UsePoolingByDefault = false;
        }
    }
} 
