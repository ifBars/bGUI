using MelonLoader;
using UnityEngine;

// MelonLoader required attributes
[assembly: MelonInfo(typeof(bGUI.Samples.DemoMenuMod), "bGUI Showcase", "1.0.0", "Bars")]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace bGUI.Samples
{
    /// <summary>
    /// Main entry point for the bGUI showcase demo.
    /// </summary>
    public class DemoMenuMod : MelonMod
    {
        public static MelonLogger.Instance Logger { get; private set; } = null!;
        private DemoMenuManager? _menuManager;
        private KeyCode _toggleMenuKey = KeyCode.F1;
        private bool _isInitialized = false;

        /// <summary>
        /// Called when the mod is initialized.
        /// </summary>
        public override void OnInitializeMelon()
        {
            Logger = LoggerInstance;
            Logger.Msg("bGUI Showcase initializing...");
            
            // We'll create the menu manager when a scene is loaded
            _isInitialized = true;
            
            Logger.Msg($"bGUI Showcase initialized. Press {_toggleMenuKey} to toggle the demo menu.");
        }
        
        /// <summary>
        /// Called when a scene is loaded.
        /// </summary>
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (_isInitialized)
            {
                // Create the menu managers when a scene is loaded
                if (_menuManager == null)
                {
                    _menuManager = new DemoMenuManager();
                    Logger.Msg($"bGUI Showcase menu manager created in scene: {sceneName}");
                }
            }
        }
        
        /// <summary>
        /// Called once per frame by MelonLoader.
        /// </summary>
        public override void OnUpdate()
        {
            if (!_isInitialized)
                return;
                
            // Check for original menu toggle key press
            if (Input.GetKeyDown(_toggleMenuKey))
            {
                Logger.Msg($"{_toggleMenuKey} key pressed, toggling demo menu");
                if (_menuManager != null)
                {
                    _menuManager.ToggleMenu();
                }
            }
            
            // Let the menu managers update
            _menuManager?.Update();
        }
        
        /// <summary>
        /// Called when the mod is unloaded.
        /// </summary>
        public override void OnDeinitializeMelon()
        {
            // Clean up resources
            _menuManager?.Dispose();
            _menuManager = null;
            
            _isInitialized = false;
            Logger.Msg("bGUI Showcase unloaded.");
        }
    }
} 
