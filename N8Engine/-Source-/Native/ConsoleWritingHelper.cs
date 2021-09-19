using System;
using System.Drawing;
using System.Text;
using N8Engine.Mathematics;

namespace N8Engine.Native
{
    internal static class ConsoleWritingHelper
    {
        private const string ANSI_ESCAPE_SEQUENCE_START = "\u001b[";
        
        public static void MoveCursorTo(this StringBuilder stringBuilder, IntegerVector position) => 
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{position.Y};{position.X}H");
        
        public static void SetConsoleForegroundColorTo(this StringBuilder stringBuilder, ConsoleColor foregroundColor) =>
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{foregroundColor.AsAnsiForegroundColor()}");
        
        public static void SetConsoleBackgroundColorTo(this StringBuilder stringBuilder, ConsoleColor backgroundColor) =>
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{backgroundColor.AsAnsiBackgroundColor()}");

        public static void SetConsoleColorTo(this StringBuilder stringBuilder, Color color)
        {
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}38;2;{color.R};{color.G};{color.B}m");
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}48;2;{color.R};{color.G};{color.B}m");
        }
    }
}