using MelonLoader;
using UnityEngine;
using bGUI.Components;
using System;

[assembly: MelonInfo(typeof(bGUI.Samples.BasicExample), "Basic Example", "1.0.0", "Bars")]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace bGUI.Samples
{
    public class BasicExample : MelonMod
    {
        private bool _isMenuVisible;
        private CanvasWrapper? _canvas;
        private PanelWrapper? _menuPanel;
        private PanelWrapper? _titlePanel;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Press F1 to open the basic example");
        }

        private void DestroyUI()
        {
            _canvas?.Destroy();
            _canvas = null;
            _menuPanel = null;
            _titlePanel = null;
        }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        private void CreateUI()
        {
            try
            {
                _canvas = UI.Canvas("BasicExampleCanvas")
                    .SetRenderMode(RenderMode.ScreenSpaceOverlay)
                    .SetSortingOrder(100)
                    .SetReferenceResolution(1920, 1080)
                    .Build();
                

                _menuPanel = UI.Panel(_canvas.RectTransform)
                    .SetSize(450, 600)
                    .SetAnchor(0.5f, 0.5f)
                    .SetBackgroundColor(new Color(0.08f, 0.12f, 0.18f, 0.98f))
                    .SetRounded(15)
                    .WithVerticalLayout(layout => layout
                        .SetPadding(0)
                        .SetSpacing(0)
                        .SetChildAlignment(TextAnchor.UpperCenter))
                    .Build();

                _titlePanel = UI.Panel(_menuPanel.RectTransform)
                    .SetSize(450, 40)
                    .SetBackgroundColor(new Color(0.25f, 0.25f, 0.25f, 0.95f))
                    .SetRounded(15)
                    .WithHorizontalLayout(layout => layout
                        .SetPadding(10, 10, 5, 5)
                        .SetSpacing(175)
                        .SetChildAlignment(TextAnchor.MiddleLeft))
                    .Build();

                UI.Text(_titlePanel.RectTransform)
                    .SetContent("Basic bGUI Example")
                    .SetFontSize(14)
                    .SetColor(Color.white)
                    .SetAlignment(TextAnchor.MiddleLeft)
                    .SetSize(300, 30)
                    .Build();
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Failed to create UI: {ex}");
                DestroyUI();
            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        public override void OnUpdate()
        {
            if (!Input.GetKeyDown(KeyCode.F1)) return;
            _isMenuVisible = !_isMenuVisible;
            
            if (_isMenuVisible)
            {
                if (_canvas == null) CreateUI();
                _menuPanel?.GameObject.SetActive(true);
            }
            else
            {
                _menuPanel?.GameObject.SetActive(false);
            }
        }
    }
} 