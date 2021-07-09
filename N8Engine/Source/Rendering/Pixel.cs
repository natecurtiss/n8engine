using System;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal readonly struct Pixel
    {
        public readonly ConsoleColor ForegroundColor;
        public readonly ConsoleColor BackgroundColor;
        public readonly Vector2 Position;

        public Pixel(in ConsoleColor foregroundColor, in ConsoleColor backgroundColor, in Vector2 position)
        {
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            Position = position;
        }
    }
}