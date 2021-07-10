using System;
using System.Collections.Generic;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{ 
    internal static class Renderer
    {
        private static readonly Dictionary<Vector2, Pixel?> _world = new();

        public static void Initialize()
        {
            GameLoop.OnPreRender += OnPreRender;
            GameLoop.OnPostRender += OnPostRender;
        }

        private static void OnPreRender() => _world.Clear();

        public static void Render(in GameObject gameObject)
        {
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
            Console.Clear();
            foreach (Vector2 __position in _world.Keys)
            {
                if (!_world[__position].HasValue) continue;
                Pixel __pixelToRender = _world[__position].Value;
                
                if (__position.X < 0 || __position.Y < 0) continue;

                Console.SetCursorPosition(__position.X.Rounded(), __position.Y.Rounded());
                Console.ForegroundColor = __pixelToRender.ForegroundColor;
                Console.BackgroundColor = __pixelToRender.BackgroundColor;
                Console.Write("▒");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}