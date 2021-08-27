using System;
using System.Collections.Generic;
using System.Text;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{ 
    internal static class Renderer
    {
        public const int NUMBER_OF_CHARACTERS_PER_PIXEL = 2;
        private const string ANSI_ESCAPE_SEQUENCE_START = "\u001b[";
        
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
            var outputStringBuilder = new StringBuilder();

            foreach (var position in _pixelsToRender.Keys)
            {
                var pixelToRender = _pixelsToRender[position];
                var pixelHasNotMoved = _pixelsToRenderLastFrame.ContainsKey(position) && _pixelsToRenderLastFrame[position] == pixelToRender;
                if (pixelHasNotMoved) continue;

                var pixelIsToTheRightOfPreviousPixel = 
                    new Vector((int) position.X, (int) position.Y) - 
                    new Vector((int) lastPosition.X, (int) lastPosition.Y) == Vector.Right;
                
                if (!pixelIsToTheRightOfPreviousPixel)
                    outputStringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{(int) position.Y};{(int) position.X}H");
                lastPosition = position;

                if (lastForegroundColor != pixelToRender.ForegroundColor)
                    outputStringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{pixelToRender.ForegroundColor.AsAnsiForegroundColor()}");
                if (lastBackgroundColor != pixelToRender.BackgroundColor) 
                    outputStringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{pixelToRender.BackgroundColor.AsAnsiBackgroundColor()}");
                lastForegroundColor = pixelToRender.ForegroundColor;
                lastBackgroundColor = pixelToRender.BackgroundColor;

                outputStringBuilder.Append("▒");
            }
            Console.Write(outputStringBuilder.ToString());
        }
        
        private static void ClearOldPixels()
        {
            var lastOldPosition = new Vector();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            var clearedPixelsStringBuilder = new StringBuilder();
            
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
                    clearedPixelsStringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{(int) oldPosition.Y};{(int) oldPosition.X}H");
                clearedPixelsStringBuilder.Append(" ");
            }
            Console.Write(clearedPixelsStringBuilder.ToString());
        }
    }
}