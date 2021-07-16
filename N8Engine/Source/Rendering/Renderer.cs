using System;
using System.Collections.Generic;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{ 
    internal static class Renderer
    {
        private static readonly Dictionary<Vector, Pixel> _pixelsToRender = new();
        private static readonly Dictionary<Vector, Pixel> _pixelsToRenderLastFrame = new();

        public static void Initialize()
        {
            GameLoop.OnPreRender += OnPreRender;
            GameLoop.OnPostRender += OnPostRender;
        }

        private static void OnPreRender()
        {
            foreach ((Vector __position, Pixel __pixel) in _pixelsToRender)
            {
                if (_pixelsToRenderLastFrame.ContainsKey(__position))
                    _pixelsToRenderLastFrame[__position] = __pixel;
                else
                    _pixelsToRenderLastFrame.Add(__position, __pixel);
            }
            _pixelsToRender.Clear();
        }

        public static void Render(in GameObject gameObject)
        {
            Sprite __sprite = gameObject.SpriteRenderer.Sprite;
            Vector __gameObjectPosition = gameObject.Position;
            
            foreach (Pixel __pixel in __sprite.Pixels)
            {
                Vector __pixelPosition = __pixel.Position + __gameObjectPosition;
                __pixelPosition = Window.GetWindowPositionAsWorldPosition(__pixelPosition);
                __pixelPosition = new Vector((int) __pixelPosition.X, (int) __pixelPosition.Y);

                bool __pixelIsOutsideOfWindow = !__pixelPosition.IsWithinWindow();
                bool __noPixelInPosition = !_pixelsToRender.ContainsKey(__pixelPosition);

                if (__pixelIsOutsideOfWindow) 
                    continue;
                if (__noPixelInPosition)
                    _pixelsToRender.Add(__pixelPosition, __pixel);
                else if (__pixel.SortingOrder > _pixelsToRender[__pixelPosition].SortingOrder || _pixelsToRender[__pixelPosition].IsBackground)
                        _pixelsToRender[__pixelPosition] = __pixel;
            }
        }
        
        private static void OnPostRender()
        {
            foreach (Vector __position in _pixelsToRender.Keys)
            {
                Pixel __pixelToRender = _pixelsToRender[__position];
                bool __pixelHasNotMoved = _pixelsToRenderLastFrame.ContainsKey(__position) && _pixelsToRenderLastFrame[__position] == __pixelToRender;
                if (__pixelHasNotMoved) continue;

                Console.SetCursorPosition((int) __position.X, (int) __position.Y);
                Console.ForegroundColor = __pixelToRender.ForegroundColor;
                Console.BackgroundColor = __pixelToRender.BackgroundColor;
                Console.Write('▒');
            }
            
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            foreach (Vector __oldPosition in _pixelsToRenderLastFrame.Keys)
            {
                bool __positionHasPixel = _pixelsToRender.ContainsKey(__oldPosition);
                if (__positionHasPixel) continue;
                _pixelsToRenderLastFrame.Remove(__oldPosition);
                Console.SetCursorPosition((int) __oldPosition.X, (int) __oldPosition.Y);
                Console.Write(' ');
            }
        }
    }
}