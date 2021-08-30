using System;
using System.Text;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Native
{
    internal static class ConsoleWritingHelper
    {
        private const string ANSI_ESCAPE_SEQUENCE_START = "\u001b[";
        
        public static void MoveCursorTo(this StringBuilder stringBuilder, Vector position) => 
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{(int) position.Y};{(int) position.X}H");
        
        public static void SetConsoleForegroundColorTo(this StringBuilder stringBuilder, ConsoleColor foregroundColor) =>
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{foregroundColor.AsAnsiForegroundColor()}");
        
        public static void SetConsoleBackgroundColorTo(this StringBuilder stringBuilder, ConsoleColor backgroundColor) =>
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{backgroundColor.AsAnsiBackgroundColor()}");
    }
}