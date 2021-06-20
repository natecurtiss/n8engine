using System;

namespace N8Engine.Rendering
{
    public enum Color
    {
        Black,
        DarkBlue,
        DarkGreen,
        DarkCyan,
        DarkRed,
        DarkMagenta,
        DarkYellow,
        Gray,
        DarkGray,
        Blue,
        Green,
        Cyan,
        Red,
        Magenta,
        Yellow,
        White
    }

    public static class ColorExtensions
    {
        internal static ConsoleColor AsConsoleColor(this Color color)
        {
            string __colorName = Enum.GetName(typeof(Color), color);
            return (ConsoleColor) Enum.Parse(typeof(ConsoleColor), __colorName ?? "Magenta");
        }
        
        internal static Color AsColor(this ConsoleColor color)
        {
            string __consoleColorName = Enum.GetName(typeof(ConsoleColor), color);
            return (Color) Enum.Parse(typeof(Color), __consoleColorName ?? "Magenta");
        }
    }
}