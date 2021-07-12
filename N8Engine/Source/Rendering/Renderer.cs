using System;
using System.Collections.Generic;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{ 
    internal static class Renderer
    {
        public const int NUMBER_OF_PIXELS = 2;
        
        private static readonly Dictionary<Vector2, Pixel> _world = new();
        private static Dictionary<Vector2, Pixel> _worldLastFrame = new();

        public static void Initialize()
        {
            GameLoop.OnPreRender += OnPreRender;
            GameLoop.OnPostRender += OnPostRender;
        }

        private static void OnPreRender()
        {
            foreach ((Vector2 __position, Pixel __pixel) in _world)
            {
                if (_worldLastFrame.ContainsKey(__position))
                    _worldLastFrame[__position] = __pixel;
                else
                    _worldLastFrame.Add(__position, __pixel);
            }
            _world.Clear();
        }

        public static void Render(in GameObject gameObject)
        {
            Sprite __sprite = gameObject.Sprite;
            Vector2 __gameObjectPosition = gameObject.Position;
            
            foreach (Pixel __pixel in __sprite.Pixels)
            {
                Vector2 __pixelPosition = __pixel.Position + __gameObjectPosition;
                __pixelPosition = Window.GetWindowPositionAsWorldPosition(__pixelPosition);
                __pixelPosition = new Vector2(__pixelPosition.X.Floored(), __pixelPosition.Y.Floored());
                
                if (!__pixelPosition.IsWithinWindow()) 
                    continue;
                if (!_world.ContainsKey(__pixelPosition)) 
                    _world.Add(__pixelPosition, __pixel);
                else if (__pixel.SortingOrder > _world[__pixelPosition].SortingOrder)
                    _world[__pixelPosition] = __pixel;
            }
        }
        
        private static void OnPostRender()
        {
            foreach (Vector2 __position in _world.Keys)
            {
                Pixel __pixelToRender = _world[__position];
                if (_worldLastFrame.ContainsKey(__position) && _worldLastFrame[__position] == __pixelToRender) continue;

                Console.SetCursorPosition((int) __position.X, (int) __position.Y);
                Console.ForegroundColor = __pixelToRender.ForegroundColor;
                Console.BackgroundColor = __pixelToRender.BackgroundColor;
                for (int __i = 0; __i < NUMBER_OF_PIXELS; __i++) Console.Write('▒');
            }
            
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            foreach (Vector2 __oldPosition in _worldLastFrame.Keys)
            {
                if (_world.ContainsKey(__oldPosition) || _world.ContainsKey(__oldPosition + Vector2.Right * (NUMBER_OF_PIXELS - 1))) continue;
                _worldLastFrame.Remove(__oldPosition);
                Console.SetCursorPosition((int) __oldPosition.X, (int) __oldPosition.Y);
                for (int __i = 0; __i < NUMBER_OF_PIXELS; __i++) Console.Write(' ');
            }
        }
    }
}