using System;
using UnityEngine;

namespace bGUI.Samples
{
    /// <summary>
    /// This is a legacy demo class. The new, more robust demo implementation can be found in:
    /// - DemoMenuMod.cs: Main entry point
    /// - DemoMenuManager.cs: Menu management
    /// - DemoPageBase.cs: Base class for all demo pages
    /// - ButtonsDemoPage.cs: Button showcase
    /// - LayoutDemoPage.cs: Layout showcase
    /// - StylesDemoPage.cs: Visual styles showcase
    /// - AnimationDemoPage.cs: Animation showcase
    /// </summary>
    [Obsolete("This class is deprecated. Use the new DemoMenuMod class instead.")]
    public class DemoMenu
    {
        /// <summary>
        /// Logs a message directing users to the new demo implementation.
        /// </summary>
        public static void ShowLegacyWarning()
        {
            Debug.LogWarning("The DemoMenu class is deprecated. Please use the new DemoMenuMod class located in the Samples folder.");
        }
        
        /// <summary>
        /// Static constructor to show the warning when this class is referenced.
        /// </summary>
        static DemoMenu()
        {
            ShowLegacyWarning();
        }
    }
}