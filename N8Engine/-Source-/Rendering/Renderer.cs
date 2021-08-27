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
        }

        private static void OnPreRender() => UpdatePixelsToRenderLastFrame();

        public static void Render(Sprite sprite, Vector spritePosition, int sortingOrder)
        {
            foreach (var pixel in sprite.Pixels)
            {
                var windowPosition = pixel.Position + spritePosition;
                var worldPosition = windowPosition.FromWindowPositionToWorldPosition();
                var sortedPixel = pixel.WithSortingOrder(sortingOrder);
                
                if (worldPosition.IsOutsideOfTheWorld()) continue;
                if (worldPosition.DoesNotHaveAPixel())
                {
                    _pixelsToRender.Add(worldPosition, sortedPixel);
                }
                else
                {
                    var oldPixel = _pixelsToRender[worldPosition];
                    if (sortedPixel.IsOnTopOf(oldPixel))
                        _pixelsToRender[worldPosition] = sortedPixel;
                }
            }
        }
        
        private static void OnPostRender()
        {
            RenderNewPixels();
            ClearOldPixels();
        }

        private static void RenderNewPixels()
        {
            var lastForegroundColor = Console.ForegroundColor;
            var lastBackgroundColor = Console.BackgroundColor;
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
        
        private static void UpdatePixelsToRenderLastFrame()
        {
            foreach (var (position, pixel) in _pixelsToRender)
                if (_pixelsToRenderLastFrame.ContainsKey(position))
                    _pixelsToRenderLastFrame[position] = pixel;
                else
                    _pixelsToRenderLastFrame.Add(position, pixel);
            _pixelsToRender.Clear();
        }

        private static Pixel WithSortingOrder(this Pixel pixel, int sortingOrder)
        {
            var newPixel = pixel;
            newPixel.SortingOrder = sortingOrder;
            return newPixel;
        }

        private static bool HasAPixel(this IntegerVector position) => _pixelsToRender.ContainsKey(position);

        private static bool DoesNotHaveAPixel(this IntegerVector position) => !position.HasAPixel();

        private static bool IsOnTopOf(this Pixel newPixel, Pixel oldPixel) => newPixel.SortingOrder > oldPixel.SortingOrder;
    }
}