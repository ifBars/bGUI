using UnityEngine;

namespace bGUI.Utilities
{
    /// <summary>
    /// Utility for generating rounded rectangle sprites at runtime.
    /// </summary>
    public static class RoundedRectGenerator
    {
        /// <summary>
        /// Creates a sprite with rounded corners.
        /// </summary>
        /// <param name="width">Width of the sprite in pixels</param>
        /// <param name="height">Height of the sprite in pixels</param>
        /// <param name="cornerRadius">Radius of the corners in pixels</param>
        /// <param name="color">Color of the sprite</param>
        /// <returns>A sprite with rounded corners</returns>
        public static Sprite CreateRoundedRectSprite(int width, int height, int cornerRadius, Color color)
        {
            // Ensure parameters are valid
            width = Mathf.Max(1, width);
            height = Mathf.Max(1, height);
            cornerRadius = Mathf.Min(cornerRadius, Mathf.Min(width / 2, height / 2));

            // Create a texture
            Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            
            // Fill with transparent pixels initially
            Color[] pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color.clear;
            }
            
            // Calculate squared radius for corner checks
            int cornerRadiusSq = cornerRadius * cornerRadius;

            // Fill in the pixels
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Check if this pixel is in one of the corners
                    bool inCorner = false;
                    float distSq = 0;
                    
                    // Top-left corner
                    if (x < cornerRadius && y < cornerRadius)
                    {
                        distSq = (cornerRadius - x) * (cornerRadius - x) + (cornerRadius - y) * (cornerRadius - y);
                        inCorner = true;
                    }
                    // Top-right corner
                    else if (x >= width - cornerRadius && y < cornerRadius)
                    {
                        distSq = (x - (width - cornerRadius - 1)) * (x - (width - cornerRadius - 1)) + 
                                 (cornerRadius - y) * (cornerRadius - y);
                        inCorner = true;
                    }
                    // Bottom-left corner
                    else if (x < cornerRadius && y >= height - cornerRadius)
                    {
                        distSq = (cornerRadius - x) * (cornerRadius - x) + 
                                 (y - (height - cornerRadius - 1)) * (y - (height - cornerRadius - 1));
                        inCorner = true;
                    }
                    // Bottom-right corner
                    else if (x >= width - cornerRadius && y >= height - cornerRadius)
                    {
                        distSq = (x - (width - cornerRadius - 1)) * (x - (width - cornerRadius - 1)) + 
                                 (y - (height - cornerRadius - 1)) * (y - (height - cornerRadius - 1));
                        inCorner = true;
                    }
                    
                    // If in corner, check if inside the rounded section
                    if (inCorner && distSq > cornerRadiusSq)
                    {
                        pixels[y * width + x] = Color.clear;
                    }
                    else
                    {
                        pixels[y * width + x] = color;
                    }
                }
            }
            
            // Apply pixels to texture
            texture.SetPixels(pixels);
            texture.Apply();
            
            // Create sprite from texture
            return Sprite.Create(
                texture, 
                new Rect(0, 0, width, height), 
                new Vector2(0.5f, 0.5f), 
                100.0f,
                0,
                SpriteMeshType.FullRect);
        }
        
        /// <summary>
        /// Creates a sliced sprite with rounded corners.
        /// </summary>
        /// <param name="width">Width of the sprite in pixels</param>
        /// <param name="height">Height of the sprite in pixels</param>
        /// <param name="cornerRadius">Radius of the corners in pixels</param>
        /// <param name="borderSize">Border size for 9-slice in pixels</param>
        /// <param name="color">Color of the sprite</param>
        /// <returns>A sprite with rounded corners that can be used with 9-slice</returns>
        public static Sprite CreateRoundedRectSprite9Slice(int width, int height, int cornerRadius, int borderSize, Color color)
        {
            // Ensure parameters are valid
            width = Mathf.Max(1, width);
            height = Mathf.Max(1, height);
            cornerRadius = Mathf.Min(cornerRadius, Mathf.Min(width / 2, height / 2));
            borderSize = Mathf.Max(1, borderSize);
            
            // Create the texture
            Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            
            // Fill with transparent pixels initially
            Color[] pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color.clear;
            }
            
            // Calculate squared radius for corner checks
            int cornerRadiusSq = cornerRadius * cornerRadius;
            
            // Fill in the pixels
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Check if this pixel is in one of the corners
                    bool inCorner = false;
                    float distSq = 0;
                    
                    // Top-left corner
                    if (x < cornerRadius && y < cornerRadius)
                    {
                        distSq = (cornerRadius - x) * (cornerRadius - x) + (cornerRadius - y) * (cornerRadius - y);
                        inCorner = true;
                    }
                    // Top-right corner
                    else if (x >= width - cornerRadius && y < cornerRadius)
                    {
                        distSq = (x - (width - cornerRadius - 1)) * (x - (width - cornerRadius - 1)) + 
                                 (cornerRadius - y) * (cornerRadius - y);
                        inCorner = true;
                    }
                    // Bottom-left corner
                    else if (x < cornerRadius && y >= height - cornerRadius)
                    {
                        distSq = (cornerRadius - x) * (cornerRadius - x) + 
                                 (y - (height - cornerRadius - 1)) * (y - (height - cornerRadius - 1));
                        inCorner = true;
                    }
                    // Bottom-right corner
                    else if (x >= width - cornerRadius && y >= height - cornerRadius)
                    {
                        distSq = (x - (width - cornerRadius - 1)) * (x - (width - cornerRadius - 1)) + 
                                 (y - (height - cornerRadius - 1)) * (y - (height - cornerRadius - 1));
                        inCorner = true;
                    }
                    
                    // If in corner, check if inside the rounded section
                    if (inCorner && distSq > cornerRadiusSq)
                    {
                        pixels[y * width + x] = Color.clear;
                    }
                    else
                    {
                        pixels[y * width + x] = color;
                    }
                }
            }
            
            // Apply pixels to texture
            texture.SetPixels(pixels);
            texture.Apply();
            
            // Create a border for 9-slice
            Vector4 border = new Vector4(borderSize, borderSize, borderSize, borderSize);
            
            // Create sprite from texture with border
            return Sprite.Create(
                texture, 
                new Rect(0, 0, width, height), 
                new Vector2(0.5f, 0.5f), 
                100.0f,
                0,
                SpriteMeshType.FullRect,
                border);
        }
    }
} 