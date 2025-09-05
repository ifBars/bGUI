using System;
using System.Collections.Generic;
using bGUI.Core.Containers;
using UnityEngine;

namespace bGUI.Components.Builders
{
    /// <summary>
    /// Fluent builder for bGUI menus.
    /// </summary>
    public class MenuBuilder
    {
        private readonly MenuWrapper _menu;
        private readonly List<PageConfig> _pageConfigs = new List<PageConfig>();

        /// <summary>
        /// Configuration for a page.
        /// </summary>
        private class PageConfig
        {
            public string Title { get; }
            public string ButtonText { get; }
            public int Position { get; }
            public Color ButtonColor { get; }
            public Func<IPage> PageFactory { get; }

            public PageConfig(string title, string buttonText, int position, Color buttonColor, Func<IPage> pageFactory)
            {
                Title = title;
                ButtonText = buttonText;
                Position = position;
                ButtonColor = buttonColor;
                PageFactory = pageFactory;
            }
        }

        /// <summary>
        /// Initializes a new instance of the MenuBuilder class.
        /// </summary>
        /// <param name="name">The name for the menu</param>
        public MenuBuilder(string name = "Menu")
        {
            _menu = new MenuWrapper(name);
        }

        /// <summary>
        /// Sets the title of the menu.
        /// </summary>
        /// <param name="title">The title to set</param>
        public MenuBuilder SetTitle(string title)
        {
            _menu.GameObject.name = title;
            return this;
        }

        /// <summary>
        /// Adds a page to the menu.
        /// </summary>
        /// <param name="title">The title of the page</param>
        /// <param name="buttonText">The text for the navigation button</param>
        /// <param name="position">The position of the navigation button</param>
        /// <param name="buttonColor">The color of the navigation button</param>
        /// <param name="pageFactory">Factory function to create the page</param>
        public MenuBuilder AddPage(string title, string buttonText, int position, 
            Color buttonColor, Func<IPage> pageFactory)
        {
            _pageConfigs.Add(new PageConfig(title, buttonText, position, buttonColor, pageFactory));
            
            return this;
        }

        /// <summary>
        /// Adds an exit button to the menu.
        /// </summary>
        /// <param name="text">The text for the exit button</param>
        /// <param name="position">The position of the exit button</param>
        public MenuBuilder AddExitButton(string text, int position)
        {
            _menu.CreateNavigationButton(text, position, _menu.Toggle, true);
            return this;
        }

        /// <summary>
        /// Sets whether the menu should be visible initially.
        /// </summary>
        /// <param name="visible">Whether the menu should be visible</param>
        public MenuBuilder SetInitiallyVisible(bool visible)
        {
            if (visible)
            {
                _menu.Show();
            }
            else
            {
                _menu.Hide();
            }
            
            return this;
        }

        /// <summary>
        /// Builds and returns the menu.
        /// </summary>
        public MenuWrapper Build()
        {
            // Create all pages and buttons
            foreach (var config in _pageConfigs)
            {
                IPage page = config.PageFactory();
                _menu.AddPage(page);
                
                // Create a button for this page
                _menu.CreateNavigationButton(
                    config.ButtonText,
                    config.Position,
                    () => _menu.ShowPage(page),
                    false
                );
            }
            
            return _menu;
        }
    }
} 
