using System;
using System.Collections.Generic;
using System.Linq;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{ 
    internal static class Renderer
    {
        private static readonly Dictionary<Vector2, Pixel?> _world = new();
        private static Dictionary<Vector2, Pixel?> _worldLastFrame = new();

        public static void Initialize()
        {
            GameLoop.OnPreRender += OnPreRender;
            GameLoop.OnPostRender += OnPostRender;
        }

        private static void OnPreRender()
        {
            _worldLastFrame = _world;
            _world.Clear();
        }

        public static void Render(in GameObject gameObject)
        {
            ClearWindow();
            Sprite __sprite = gameObject.Sprite;
            Vector2 __position = gameObject.Position;
            foreach (Pixel __pixel in __sprite.Pixels)
            {
                Vector2 __pixelPosition = __pixel.Position + __position;
                __pixelPosition = Window.GetWindowPositionAsWorldPosition(__pixelPosition);
                if (!_world.ContainsKey(__pixelPosition)) 
                    _world.Add(__pixelPosition, __pixel);
                else if (!_world[__pixelPosition].HasValue)
                    _world[__pixelPosition] = __pixel;
                else if (__pixel.SortingOrder > _world[__pixelPosition].Value.SortingOrder)
                    _world[__pixelPosition] = __pixel;
            }
        }
        
        private static void OnPostRender()
        {
            Console.ResetColor();
            foreach (Vector2 __position in _world.Keys)
            {
                if (!_world[__position].HasValue) continue;
                Pixel __pixelToRender = _world[__position].Value;
                
                if (__position.X < 0 || __position.Y < 0) continue;

                Console.SetCursorPosition(__position.X.Rounded(), __position.Y.Rounded());
                Console.ForegroundColor = __pixelToRender.ForegroundColor;
                Console.BackgroundColor = __pixelToRender.BackgroundColor;
                Console.Write('▒');
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private static void ClearWindow()
        {
            foreach (Vector2 __position in _worldLastFrame.Keys.Where(position => _worldLastFrame[position].HasValue).Where(position => !(position.X < 0) && !(position.Y < 0)))
            {
                Console.SetCursorPosition(__position.X.Rounded(), __position.Y.Rounded());
                Console.Write(' ');
            }
        }
    }
}