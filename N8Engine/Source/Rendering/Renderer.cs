using System;
using System.Collections.Generic;
using System.Linq;
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
            _worldLastFrame = new Dictionary<Vector2, Pixel>(_world);
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
                __pixelPosition.X.Floor();
                __pixelPosition.Y.Floor();
                if (!Window.IsWithinWindow(__pixelPosition)) 
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

                Console.SetCursorPosition(__position.X.Floored(), __position.Y.Floored());
                Console.ForegroundColor = __pixelToRender.ForegroundColor;
                Console.BackgroundColor = __pixelToRender.BackgroundColor;
                for (int __i = 0; __i < NUMBER_OF_PIXELS; __i++) Console.Write('▒');
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}