using System;

namespace N8Engine.Rendering
{
    /// <summary>
    /// Extension methods used for converting to and from <see cref="Color"/> and <see cref="ConsoleColor"/>.
    /// </summary>
    internal static class ColorExtensions
    {
        /// <summary>
        /// Returns a <see cref="ConsoleColor"/> from a <see cref="Color"/>.
        /// </summary>
        /// <param name="color"> The <see cref="Color"/> to convert. </param>
        /// <returns> A <see cref="ConsoleColor"/> that is decided by the <see cref="Color"/> passed in. </returns>
        public static ConsoleColor AsConsoleColor(this Color color)
        {
            string __colorName = Enum.GetName(typeof(Color), color);
            return (ConsoleColor) Enum.Parse(typeof(ConsoleColor), __colorName ?? "Magenta");
        }
        
        /// <summary>
        /// Returns a <see cref="Color"/> from a <see cref="ConsoleColor"/>.
        /// </summary>
        /// <param name="color"> The <see cref="Color"/> to convert. </param>
        /// <returns> A <see cref="Color"/> that is decided by the <see cref="ConsoleColor"/> passed in. </returns>
        public static Color AsColor(this ConsoleColor color)
        {
            string __consoleColorName = Enum.GetName(typeof(ConsoleColor), color);
            return (Color) Enum.Parse(typeof(Color), __consoleColorName ?? "Magenta");
        }

        /// <summary>
        /// Returns a <see cref="Color"/> from the <see cref="string"/> passed in.
        /// </summary>
        /// <param name="word"> The <see cref="string"/> passed in. </param>
        /// <returns> A <see cref="Color"/> from the <see cref="string"/> passed in. </returns>
        public static Color AsColor(this string word)
        {
            word = word.ToLower();
            return word switch
            {
                "black" => Color.Black,
                "darkblue" => Color.DarkBlue,
                "darkgreen" => Color.DarkGreen,
                "darkcyan" => Color.DarkCyan,
                "darkred" => Color.DarkRed,
                "darkmagenta" => Color.DarkMagenta,
                "darkyellow" => Color.DarkYellow,
                "gray" => Color.Gray,
                "darkgray" => Color.DarkGray,
                "blue" => Color.Blue,
                "green" => Color.Green,
                "cyan" => Color.Cyan,
                "red" => Color.Red,
                "magenta" => Color.Magenta,
                "yellow" => Color.Yellow,
                "white" => Color.White,
                _ => Color.None
            };
        }
    }
}