using System;

namespace N8Engine.Rendering
{
    public readonly struct Color
    {
        public readonly ConsoleColor ForegroundColor;
        public readonly ConsoleColor BackgroundColor;

        public Color(in ConsoleColor foregroundColor, in ConsoleColor backgroundColor)
        {
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }
    }
}