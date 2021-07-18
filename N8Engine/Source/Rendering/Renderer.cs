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

        public static void Render(in Sprite sprite, in Vector position)
        {
            foreach (Pixel __pixel in sprite.Pixels)
            {
                Vector __pixelPosition = __pixel.Position + position;
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
            var lastForegroundColor = ConsoleColor.Black;
            var lastBackgroundColor = ConsoleColor.Black;
            var lastPosition = new Vector();
            foreach (var position in _pixelsToRender.Keys)
            {
                var pixelToRender = _pixelsToRender[position];
                var pixelHasNotMoved = _pixelsToRenderLastFrame.ContainsKey(position) && _pixelsToRenderLastFrame[position] == pixelToRender;
                if (pixelHasNotMoved) continue;

                if (new Vector((int) position.X, (int) position.Y) - new Vector((int)lastPosition.X, (int)lastPosition.Y) != Vector.Right)
                    Console.SetCursorPosition((int) position.X, (int) position.Y);
                if (lastForegroundColor != pixelToRender.ForegroundColor) Console.ForegroundColor = pixelToRender.ForegroundColor;
                if (lastBackgroundColor != pixelToRender.BackgroundColor) Console.BackgroundColor = pixelToRender.BackgroundColor;
                Console.Write('▒');
                lastForegroundColor = pixelToRender.ForegroundColor;
                lastBackgroundColor = pixelToRender.BackgroundColor;
            }
            
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            foreach (var oldPosition in _pixelsToRenderLastFrame.Keys)
            {
                var positionHasPixel = _pixelsToRender.ContainsKey(oldPosition);
                if (positionHasPixel) continue;
                _pixelsToRenderLastFrame.Remove(oldPosition);
                Console.SetCursorPosition((int) oldPosition.X, (int) oldPosition.Y);
                Console.Write(' ');
            }
        }
    }
}