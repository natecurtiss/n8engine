using System;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal struct Pixel
    {
        public readonly ConsoleColor ForegroundColor;
        public readonly ConsoleColor BackgroundColor;
        
        public Vector2 Position { get; set; }
        public int SortingOrder { get; set; }

        public Pixel(in ConsoleColor foregroundColor, in ConsoleColor backgroundColor, in Vector2 position)
        {
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            Position = position;
            SortingOrder = 0;
        }
    }
}