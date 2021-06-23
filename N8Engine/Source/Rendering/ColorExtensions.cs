using System;

namespace N8Engine.Rendering
{
    /// <summary>
    /// Extension methods used for converting to and from ConsoleColor and Color.
    /// </summary>
    internal static class ColorExtensions
    {
        /// <summary>
        /// Returns a ConsoleColor from a color.
        /// </summary>
        /// <param name="color"> The color to convert. </param>
        /// <returns> A ConsoleColor that is decided by the color passed in. </returns>
        public static ConsoleColor AsConsoleColor(this Color color)
        {
            string __colorName = Enum.GetName(typeof(Color), color);
            return (ConsoleColor) Enum.Parse(typeof(ConsoleColor), __colorName ?? "Magenta");
        }
        
        /// <summary>
        /// Returns a color from a ConsoleColor.
        /// </summary>
        /// <param name="color"> The color to convert. </param>
        /// <returns> A color that is decided by the ConsoleColor passed in. </returns>
        public static Color AsColor(this ConsoleColor color)
        {
            string __consoleColorName = Enum.GetName(typeof(ConsoleColor), color);
            return (Color) Enum.Parse(typeof(Color), __consoleColorName ?? "Magenta");
        }
    }
}