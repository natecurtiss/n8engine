using System;
using System.Collections.Generic;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{ 
    internal static class Renderer
    {
        private static Dictionary<Vector2, Pixel?> _screen = new();

        public static void Initialize()
        {
            
        }

        public static void Render(in GameObject gameObject)
        {
            Sprite __sprite = gameObject.Sprite;
            Vector2 __position = gameObject.Position;
        }

        private static void RefreshScreen()
        {
            Vector2 __startingPosition = Window.BottomLeft;
            Vector2 __endingPosition = Window.TopRight;

            for (int __y = __startingPosition.Y.Rounded(); __y < __endingPosition.Y.Rounded(); __y++)
                for (int __x = __startingPosition.X.Rounded(); __x < __endingPosition.X.Rounded(); __x++)
                    _screen.Add(new Vector2(__x, __y), null);
        }
    }
}