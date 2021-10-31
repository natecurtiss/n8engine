using System;
using System.Drawing;
using System.Text;
using N8Engine.Mathematics;

namespace N8Engine.External.Console
{
    static class ConsoleWriter
    {
        const string ANSI_ESCAPE_SEQUENCE_START = "\u001b[";
        
        public static void MoveCursorTo(this StringBuilder output, IntVector position) => output.Append($"{ANSI_ESCAPE_SEQUENCE_START}{position.Y};{position.X}H");
        public static void ChangeForegroundTo(this StringBuilder output, ConsoleColor color) => output.Append($"{ANSI_ESCAPE_SEQUENCE_START}{color.AsAnsiForegroundColor()}");
        public static void ChangeForegroundTo(this StringBuilder output, Color color) => output.Append($"{ANSI_ESCAPE_SEQUENCE_START}38;2;{color.R};{color.G};{color.B}m");
        public static void ChangeBackgroundTo(this StringBuilder output, ConsoleColor color) => output.Append($"{ANSI_ESCAPE_SEQUENCE_START}{color.AsAnsiBackgroundColor()}");
        public static void ChangeBackgroundTo(this StringBuilder output, Color color) => output.Append($"{ANSI_ESCAPE_SEQUENCE_START}48;2;{color.R};{color.G};{color.B}m");
        public static void ChangeColorTo(this StringBuilder output, Color color)
        {
            output.Append($"{ANSI_ESCAPE_SEQUENCE_START}38;2;{color.R};{color.G};{color.B}m");
            output.Append($"{ANSI_ESCAPE_SEQUENCE_START}48;2;{color.R};{color.G};{color.B}m");
        }
    }
}