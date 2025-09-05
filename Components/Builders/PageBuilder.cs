using System;
using UnityEngine;

namespace bGUI.Components.Builders
{
    /// <summary>
    /// Fluent builder for bGUI pages.
    /// </summary>
    public class PageBuilder
    {
        private readonly PageWrapper _page;
        private Action<PageWrapper>? _setupAction;

        /// <summary>
        /// Initializes a new instance of the PageBuilder class.
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="title">The title of the page</param>
        public PageBuilder(Transform parent, string title)
        {
            _page = new PageWrapper(parent, title);
        }

        /// <summary>
        /// Sets the background color of the page panel.
        /// </summary>
        /// <param name="color">The color to set</param>
        public PageBuilder SetBackgroundColor(Color color)
        {
            _page.PagePanel.SetBackgroundColor(color);
            return this;
        }

        /// <summary>
        /// Sets the setup action for the page.
        /// </summary>
        /// <param name="setupAction">The action to execute during setup</param>
        public PageBuilder SetupUI(Action<PageWrapper> setupAction)
        {
            _setupAction = setupAction;
            return this;
        }

        /// <summary>
        /// Adds a header to the page.
        /// </summary>
        /// <param name="text">The header text</param>
        /// <param name="yPosition">The Y position of the header</param>
        public PageBuilder AddHeader(string text, float yPosition)
        {
            _page.CreateSectionHeader(text, yPosition);
            return this;
        }

        /// <summary>
        /// Adds a description to the page.
        /// </summary>
        /// <param name="text">The description text</param>
        /// <param name="yPosition">The Y position of the description</param>
        public PageBuilder AddDescription(string text, float yPosition)
        {
            _page.CreateDescription(text, yPosition);
            return this;
        }

        /// <summary>
        /// Sets whether the page should be visible initially.
        /// </summary>
        /// <param name="visible">Whether the page should be visible</param>
        public PageBuilder SetInitiallyVisible(bool visible)
        {
            if (visible)
            {
                _page.Show();
            }
            
            return this;
        }

        /// <summary>
        /// Builds and returns the page.
        /// </summary>
        public PageWrapper Build()
        {
            // Run the setup action if provided
            _setupAction?.Invoke(_page);
            
            return _page;
        }
    }
} 
