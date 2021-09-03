using System;
using System.Collections.Generic;
using System.Text;
using N8Engine.Mathematics;
using N8Engine.Native;

namespace N8Engine.Rendering
{
    internal static class Renderer
    {
        private const int NUMBER_OF_CHARACTERS_PER_PIXEL = 2;
        private static readonly string _pixelCharacter = new('▒', NUMBER_OF_CHARACTERS_PER_PIXEL);
        private static readonly string _deleteCharacter = new(' ', NUMBER_OF_CHARACTERS_PER_PIXEL);
        private static readonly Dictionary<IntegerVector, Pixel> _pixelsToRender = new();
        private static readonly Dictionary<IntegerVector, Pixel> _pixelsToRenderLastFrame = new();

        public static void Initialize()
        {
            GameLoop.OnPreRender += OnPreRender;
            GameLoop.OnPostRender += OnPostRender;
        }

        private static void OnPreRender() => UpdatePixelsToRenderLastFrame();

        public static void Render(Sprite sprite, Vector spritePosition, int sortingOrder, string name)
        {
            foreach (var pixel in sprite.Pixels)
            {
                var worldPosition = pixel.Position + spritePosition;
                var windowPosition = worldPosition.FromWorldPositionToWindowPosition();
                var sortedPixel = pixel.WithSortingOrder(sortingOrder);
                
                if (windowPosition.IsOutsideOfTheWorld()) continue;
                if (windowPosition.DoesNotHaveAPixel())
                {
                    _pixelsToRender.Add(windowPosition, sortedPixel);
                }
                else
                {
                    var oldPixel = _pixelsToRender[windowPosition];
                    if (sortedPixel.IsOnTopOf(oldPixel))
                        _pixelsToRender[windowPosition] = sortedPixel;
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
            var lastPosition = new IntegerVector();
            var output = new StringBuilder();

            foreach (var (position, pixel) in _pixelsToRender)
            {
                if (!HasPixelMovedSinceLastFrame(position, pixel)) continue;
                if (!position.IsToTheRightOf(lastPosition))
                    output.MoveCursorTo(position * new IntegerVector(NUMBER_OF_CHARACTERS_PER_PIXEL, 1));
                lastPosition = position;
                
                if (pixel.ForegroundColor != lastForegroundColor)
                    output.SetConsoleForegroundColorTo(pixel.ForegroundColor);
                
                if (pixel.BackgroundColor != lastBackgroundColor) 
                    output.SetConsoleBackgroundColorTo(pixel.BackgroundColor);
                
                lastForegroundColor = pixel.ForegroundColor;
                lastBackgroundColor = pixel.BackgroundColor;
                
                output.Append(_pixelCharacter);
            }
            Console.Write(output.ToString());
        }

        private static void ClearOldPixels()
        {
            Console.ResetColor(); 
            var lastPosition = new IntegerVector();
            var output = new StringBuilder();
            
            foreach (var (position, _) in _pixelsToRenderLastFrame)
            {
                if (position.HasAPixel()) continue;
                if (!position.IsToTheRightOf(lastPosition))
                    output.MoveCursorTo(position * new IntegerVector(NUMBER_OF_CHARACTERS_PER_PIXEL, 1));
                lastPosition = position;
                output.Append(_deleteCharacter);
            }
            _pixelsToRenderLastFrame.Clear();
            Console.Write(output.ToString());
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

        private static bool HasPixelMovedSinceLastFrame(IntegerVector position, Pixel pixel) => 
            !(_pixelsToRenderLastFrame.ContainsKey(position) && _pixelsToRenderLastFrame[position] == pixel);

        private static bool IsToTheRightOf(this IntegerVector currentPosition, IntegerVector lastPosition) => 
            currentPosition - lastPosition == IntegerVector.Right;

    }
}