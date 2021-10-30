using System;
using System.Drawing;
using System.Text;
using N8Engine.Mathematics;

namespace N8Engine.External.Console
{
    static class ConsoleWriter
    {
        const string ANSI_ESCAPE_SEQUENCE_START = "\u001b[";
        
        public static void MoveCursorTo(this StringBuilder stringBuilder, IntVector position) => 
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{position.Y};{position.X}H");
        
        public static void SetForegroundTo(this StringBuilder stringBuilder, ConsoleColor foregroundColor) =>
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{foregroundColor.AsAnsiForegroundColor()}");
        
        public static void SetBackgroundTo(this StringBuilder stringBuilder, ConsoleColor backgroundColor) =>
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}{backgroundColor.AsAnsiBackgroundColor()}");

        public static void SetColorTo(this StringBuilder stringBuilder, Color color)
        {
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}38;2;{color.R};{color.G};{color.B}m");
            stringBuilder.Append($"{ANSI_ESCAPE_SEQUENCE_START}48;2;{color.R};{color.G};{color.B}m");
        }
    }
}