using System;

namespace N8Engine.Native
{
    internal static class ConsoleColorExtensions
    {
        public static string AsAnsiForegroundColor(this ConsoleColor consoleColor)
        {
            var ansiColor = consoleColor switch
            {
                ConsoleColor.Black => "30",
                ConsoleColor.DarkBlue => "34",
                ConsoleColor.DarkGreen => "32",
                ConsoleColor.DarkCyan => "36",
                ConsoleColor.DarkRed => "31",
                ConsoleColor.DarkMagenta => "35",
                ConsoleColor.DarkYellow => "33",
                ConsoleColor.Gray => "37",
                ConsoleColor.DarkGray => "90",
                ConsoleColor.Blue => "94",
                ConsoleColor.Green => "92",
                ConsoleColor.Cyan => "96",
                ConsoleColor.Red => "91",
                ConsoleColor.Magenta => "95",
                ConsoleColor.Yellow => "93",
                ConsoleColor.White => "97",
                var _ => "30"
            };
            return ansiColor + "m";
        }
        
        public static string AsAnsiBackgroundColor(this ConsoleColor consoleColor)
        {
            var ansiColor = consoleColor switch
            {
                ConsoleColor.Black => "40",
                ConsoleColor.DarkBlue => "44",
                ConsoleColor.DarkGreen => "42",
                ConsoleColor.DarkCyan => "46",
                ConsoleColor.DarkRed => "41",
                ConsoleColor.DarkMagenta => "45",
                ConsoleColor.DarkYellow => "43",
                ConsoleColor.Gray => "47",
                ConsoleColor.DarkGray => "100",
                ConsoleColor.Blue => "104",
                ConsoleColor.Green => "102",
                ConsoleColor.Cyan => "106",
                ConsoleColor.Red => "101",
                ConsoleColor.Magenta => "105",
                ConsoleColor.Yellow => "103",
                ConsoleColor.White => "107",
                var _ => "40"
            };
            return ansiColor + "m";
        }
    }
}