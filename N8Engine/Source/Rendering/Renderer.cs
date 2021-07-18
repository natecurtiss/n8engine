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
            foreach (var (position, pixel) in _pixelsToRender)
            {
                if (_pixelsToRenderLastFrame.ContainsKey(position))
                    _pixelsToRenderLastFrame[position] = pixel;
                else
                    _pixelsToRenderLastFrame.Add(position, pixel);
            }

            _pixelsToRender.Clear();
        }

        public static void Render(Sprite sprite, Vector position)
        {
            foreach (var pixel in sprite.Pixels)
            {
                var pixelPosition = pixel.Position + position;
                pixelPosition = Window.GetWindowPositionAsWorldPosition(pixelPosition);
                pixelPosition = new Vector((int) pixelPosition.X, (int) pixelPosition.Y);

                var isPixelIsOutsideOfWindow = !pixelPosition.IsWithinWindow();
                var isNoPixelInPosition = !_pixelsToRender.ContainsKey(pixelPosition);

                if (isPixelIsOutsideOfWindow) 
                    continue;
                if (isNoPixelInPosition)
                    _pixelsToRender.Add(pixelPosition, pixel);
                else if (pixel.SortingOrder > _pixelsToRender[pixelPosition].SortingOrder || _pixelsToRender[pixelPosition].IsBackground)
                        _pixelsToRender[pixelPosition] = pixel;
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