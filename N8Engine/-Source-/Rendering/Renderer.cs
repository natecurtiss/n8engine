﻿using System;
using System.Collections.Generic;
using System.Text;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{ 
    internal static class Renderer
    {
        public const int NUMBER_OF_CHARACTERS_PER_PIXEL = 2;
        private const string ANSI_ESCAPE_SEQUENCE_START = "\u001b[";
        private const string PIXEL_CHARACTER = "▒";
        private const string DELETE_CHARACTER = " ";

        private static readonly Dictionary<IntegerVector, Pixel> _pixelsToRender = new();
        private static readonly Dictionary<IntegerVector, Pixel> _pixelsToRenderLastFrame = new();

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
            var lastPosition = new IntegerVector();
            var output = new StringBuilder();

            foreach (var (position, pixel) in _pixelsToRender)
            {
                if (HasPixelNotMovedSinceLastFrame(position, pixel)) continue;
                if (position.IsNotToTheRightOf(lastPosition))
                    output.MoveCursorTo(position);
                lastPosition = position;

                var currentForegroundColor = pixel.ForegroundColor;
                if (currentForegroundColor.IsDifferentThan(lastForegroundColor))
                    output.SetConsoleForegroundColorTo(currentForegroundColor);
                
                var currentBackgroundColor = pixel.BackgroundColor;
                if (currentBackgroundColor.IsDifferentThan(lastBackgroundColor)) 
                    output.SetConsoleBackgroundColorTo(currentBackgroundColor);
                
                lastForegroundColor = pixel.ForegroundColor;
                lastBackgroundColor = pixel.BackgroundColor;

                output.Append(PIXEL_CHARACTER);
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
                if (position.IsNotToTheRightOf(lastPosition))
                    output.MoveCursorTo(position);
                lastPosition = position;
                output.Append(DELETE_CHARACTER);
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

        private static bool HasPixelNotMovedSinceLastFrame(Vector position, Pixel pixel) => 
            _pixelsToRenderLastFrame.ContainsKey(position) && _pixelsToRenderLastFrame[position] == pixel;
        
        private static bool IsNotToTheRightOf(this IntegerVector currentPosition, IntegerVector lastPosition) => 
            currentPosition - lastPosition != IntegerVector.Right;

        private static void MoveCursorTo(this StringBuilder stringBuilder, Vector position) => 
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{(int) position.Y};{(int) position.X}H");

        private static bool IsDifferentThan(this ConsoleColor first, ConsoleColor second) => first != second;
        
        private static void SetConsoleForegroundColorTo(this StringBuilder stringBuilder, ConsoleColor foregroundColor) =>
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{foregroundColor.AsAnsiForegroundColor()}");

        private static void SetConsoleBackgroundColorTo(this StringBuilder stringBuilder, ConsoleColor backgroundColor) =>
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{backgroundColor.AsAnsiBackgroundColor()}");
    }
}