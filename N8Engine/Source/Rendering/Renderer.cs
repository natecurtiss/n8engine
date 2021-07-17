using System;
using System.Collections.Generic;
using N8Engine.Mathematics;
using N8Engine.Native;

namespace N8Engine.Rendering
{ 
    internal static class Renderer
    {
        private static readonly Dictionary<Vector, Pixel> _pixelsToRender = new();
        private static readonly Dictionary<Vector, Pixel> _pixelsToRenderLastFrame = new();
        private static readonly char[] _characters = new char[(int) Window.Width * (int) Window.Height];
        private static readonly ConsoleColor[] _foregroundColors = new ConsoleColor[(int) Window.Width * (int) Window.Height];
        private static readonly ConsoleColor[] _backgroundColors = new ConsoleColor[(int) Window.Width * (int) Window.Height];

        public static void Initialize()
        {
            GameLoop.OnPreRender += OnPreRender;
            GameLoop.OnPostRender += OnPostRender;
        }

        private static void OnPreRender()
        {
            /*foreach ((Vector __position, Pixel __pixel) in _pixelsToRender)
            {
                if (_pixelsToRenderLastFrame.ContainsKey(__position))
                    _pixelsToRenderLastFrame[__position] = __pixel;
                else
                    _pixelsToRenderLastFrame.Add(__position, __pixel);
            }*/
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
            int __width = (int)Window.Width;
            int __height = (int)Window.Height;
            int __i = 0;
            for (int __y = 0; __y < __height; __y++)
            {
                for (int __x = 0; __x < __width; __x++)
                {
                    bool __isPixel = _pixelsToRender.ContainsKey(new Vector(__x, __y));
                    if (__isPixel)
                    {
                        Pixel __pixel = _pixelsToRender[new Vector(__x, __y)];
                        _characters[__i] = '▒';
                        _foregroundColors[__i] = __pixel.ForegroundColor;
                        _backgroundColors[__i] = __pixel.BackgroundColor;
                    }
                    else
                    {
                        _characters[__i] = ' ';
                        _foregroundColors[__i] = ConsoleColor.Black;
                        _backgroundColors[__i] = ConsoleColor.Black;
                    }
                    __i++;
                }
            }
            ConsoleWriting.Write(_characters, _foregroundColors, _backgroundColors);
            
            return;
            #region Old Code
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
            #endregion
        }
    }
}