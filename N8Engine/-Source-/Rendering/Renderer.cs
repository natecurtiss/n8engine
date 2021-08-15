using System;
using System.Collections.Generic;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{ 
    internal static class Renderer
    {
        public const int NUMBER_OF_PIXELS = 2;
        private static readonly Dictionary<Vector, Pixel> _pixelsToRender = new();
        private static readonly Dictionary<Vector, Pixel> _pixelsToRenderLastFrame = new();

        public static void Initialize()
        {
            GameLoop.OnPreRender += OnPreRender;
            GameLoop.OnPostRender += OnPostRender;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
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

        public static void Render(Sprite sprite, Vector position, int sortingOrder)
        {
            foreach (var pixel in sprite.Pixels)
            {
                var newPixel = new Pixel(pixel.ForegroundColor, pixel.BackgroundColor, pixel.Position)
                {
                    SortingOrder = sortingOrder,
                };
                var pixelPosition = newPixel.Position + position;
                pixelPosition = pixelPosition.FromWindowPositionToWorldPosition();
                pixelPosition = new Vector((int) pixelPosition.X, (int) pixelPosition.Y);

                var isPixelOutsideOfWindow = !pixelPosition.IsWithinWindow();
                var isNoPixelInPosition = !_pixelsToRender.ContainsKey(pixelPosition);

                if (isPixelOutsideOfWindow) continue;
                if (isNoPixelInPosition)
                    _pixelsToRender.Add(pixelPosition, newPixel);
                else if (newPixel.SortingOrder > _pixelsToRender[pixelPosition].SortingOrder)
                    _pixelsToRender[pixelPosition] = newPixel;
            }
        }
        
        private static void OnPostRender()
        {
            RenderNewPixels();
            ClearOldPixels();
        }

        private static void RenderNewPixels()
        {
            var lastForegroundColor = ConsoleColor.Black;
            var lastBackgroundColor = ConsoleColor.Black;
            var lastPosition = new Vector();
            
            foreach (var position in _pixelsToRender.Keys)
            {
                var pixelToRender = _pixelsToRender[position];
                var pixelHasNotMoved = _pixelsToRenderLastFrame.ContainsKey(position) && _pixelsToRenderLastFrame[position] == pixelToRender;
                if (pixelHasNotMoved) continue;

                var pixelIsToTheRightOfPreviousPixel = new Vector
                (
                    (int) position.X, (int) position.Y) - new Vector((int) lastPosition.X, (int) lastPosition.Y
                ) == Vector.Right;
                
                if (!pixelIsToTheRightOfPreviousPixel)
                    Console.SetCursorPosition((int) position.X, (int) position.Y);
                lastPosition = position;
                
                if (lastForegroundColor != pixelToRender.ForegroundColor) 
                    Console.ForegroundColor = pixelToRender.ForegroundColor;
                if (lastBackgroundColor != pixelToRender.BackgroundColor) 
                    Console.BackgroundColor = pixelToRender.BackgroundColor;
                lastForegroundColor = pixelToRender.ForegroundColor;
                lastBackgroundColor = pixelToRender.BackgroundColor;

                Console.Write("▒");
            }
        }

        private static void ClearOldPixels()
        {
            var lastOldPosition = new Vector();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            
            foreach (var oldPosition in _pixelsToRenderLastFrame.Keys)
            {
                var positionHasPixel = _pixelsToRender.ContainsKey(oldPosition);
                if (positionHasPixel) continue;
                
                _pixelsToRenderLastFrame.Remove(oldPosition);
                
                var pixelIsToTheRightOfPreviousPixel = 
                    new Vector((int) oldPosition.X, (int) oldPosition.Y) - 
                    new Vector((int) lastOldPosition.X, (int) lastOldPosition.Y) 
                    == Vector.Right;
                lastOldPosition = oldPosition;
                
                if (!pixelIsToTheRightOfPreviousPixel)
                    Console.SetCursorPosition((int) oldPosition.X, (int) oldPosition.Y);
                
                Console.Write(" ");
            }
        }
    }
}