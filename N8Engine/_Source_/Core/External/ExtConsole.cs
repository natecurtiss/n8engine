using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using N8Engine.Mathematics;
using static N8Engine.External.ExtStructs;
using SysConsole = System.Console;

namespace N8Engine.External
{
    static class ExtConsole
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct CONSOLE_FONT_INFOEX
        {
            public int SizeInBytes;
            public int FontIndex;
            public short FontWidth;
            public short FontSize;
            public int FontFamily;
            public int FontWeight;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string FontName;
        }
        
        // https://docs.microsoft.com/en-us/windows/console/getstdhandle
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);
        
        // https://docs.microsoft.com/en-us/windows/console/getconsolewindow
        [DllImport("kernel32.dll", ExactSpelling = true)]
        static extern IntPtr GetConsoleWindow();
        
        // https://docs.microsoft.com/en-us/windows/console/getconsolemode
        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr consoleHandle, out uint lpMode);
        // https://docs.microsoft.com/en-us/windows/console/setconsolemode
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr consoleHandle, uint dwMode);
        
        // https://docs.microsoft.com/en-us/windows/console/getcurrentconsolefontex
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool GetCurrentConsoleFontEx(IntPtr standardOutputHandle, bool useMaximumWindow, ref CONSOLE_FONT_INFOEX currentConsoleFontOutput);
        
        // https://docs.microsoft.com/en-us/windows/console/setcurrentconsolefontex
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool SetCurrentConsoleFontEx(IntPtr standardOutputHandle, bool useMaximumWindow, ref CONSOLE_FONT_INFOEX currentConsoleFontOutput);

        // https://docs.microsoft.com/en-us/windows/console/writeconsoleoutputcharacter
        // https://www.pinvoke.net/default.aspx/kernel32/writeconsole.html?diff=y
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool WriteConsoleOutputCharacter(IntPtr hConsoleOutput, StringBuilder lpCharacter, uint nLength, COORD dwWriteCoord);
            
        const int STANDARD_INPUT_HANDLE_NUMBER = -10;
        const int STANDARD_OUTPUT_HANDLE_NUMBER = -11;
        const int STANDARD_ERROR_HANDLE_NUMBER = -12;

        public static readonly IntPtr StandardInputHandle = GetStdHandle(STANDARD_INPUT_HANDLE_NUMBER);
        public static readonly IntPtr StandardOutputHandle = GetStdHandle(STANDARD_OUTPUT_HANDLE_NUMBER);
        public static readonly IntPtr StandardErrorHandle = GetStdHandle(STANDARD_ERROR_HANDLE_NUMBER);
        public static readonly IntPtr Handle = GetConsoleWindow();

        const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        const uint ENABLE_EXTENDED_FLAGS = 0x0080;
        
        const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;
        
        const string ESCAPE_SEQUENCE = "\u001b[";

        // https://stackoverflow.com/questions/13656846/how-to-programmatic-disable-c-sharp-console-applications-quick-edit-modey
        public static void EnableQuickEditMode()
        {
            GetConsoleMode(StandardInputHandle, out var consoleMode);
            consoleMode |= ENABLE_QUICK_EDIT_MODE;
            consoleMode |= ENABLE_EXTENDED_FLAGS;
            SetConsoleMode(StandardInputHandle, consoleMode);
        }

        public static void DisableQuickEditMode()
        {
            GetConsoleMode(StandardInputHandle, out var consoleMode);
            consoleMode &= ~ENABLE_QUICK_EDIT_MODE;
            consoleMode |= ENABLE_EXTENDED_FLAGS;
            SetConsoleMode(StandardInputHandle, consoleMode);
        }

        public static void HideCursor() => SysConsole.CursorVisible = false;
        public static void RemoveScrollbar() => SysConsole.SetBufferSize(SysConsole.WindowWidth, SysConsole.WindowHeight);

        public static void SetFont(string fontName, short fontSize, bool useMaximumWindow = false)
        {
            var before = new CONSOLE_FONT_INFOEX
            {
                SizeInBytes = Marshal.SizeOf<CONSOLE_FONT_INFOEX>()
            };
            GetCurrentConsoleFontEx(StandardOutputHandle, useMaximumWindow, ref before);
            var after = new CONSOLE_FONT_INFOEX
            {
                SizeInBytes = Marshal.SizeOf<CONSOLE_FONT_INFOEX>(),
                FontIndex = 0,
                FontFamily = 54,
                FontName = fontName,
                FontWeight = 400,
                FontSize = fontSize > 0 ? fontSize : before.FontSize
            };
            SetCurrentConsoleFontEx(StandardOutputHandle, useMaximumWindow, ref after);
        }
        
        // https://www.jerriepelser.com/blog/using-ansi-color-codes-in-net-console-apps/
        public static void EnableAnsiEscapeSequences()
        {
            GetConsoleMode(StandardOutputHandle, out var consoleMode);
            consoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;
            SetConsoleMode(StandardOutputHandle, consoleMode);
        }
        
        // https://docs.microsoft.com/en-us/windows/console/console-virtual-terminal-sequences
        public static string MoveCursor(IntVector position) => $"{ESCAPE_SEQUENCE}{position.Y};{position.X}H";
        public static string SetForeground(ConsoleColor color) => $"{ESCAPE_SEQUENCE}{color.AsAnsiForeground()}";
        public static string SetForeground(Color color) => $"{ESCAPE_SEQUENCE}38;2;{color.R};{color.G};{color.B}m";
        public static string SetBackground(ConsoleColor color) => $"{ESCAPE_SEQUENCE}{color.AsAnsiBackground()}";
        public static string SetBackground(Color color) => $"{ESCAPE_SEQUENCE}48;2;{color.R};{color.G};{color.B}m";
        public static string SetColor(Color color) =>
            $"{ESCAPE_SEQUENCE}38;2;{color.R};{color.G};{color.B}m" +
            $"{ESCAPE_SEQUENCE}48;2;{color.R};{color.G};{color.B}m";
        
        // https://ss64.com/nt/syntax-ansi.html
        public static string AsAnsiForeground(this ConsoleColor consoleColor)
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
        
        // https://ss64.com/nt/syntax-ansi.html
        public static string AsAnsiBackground(this ConsoleColor consoleColor)
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

        public static void Write(StringBuilder text, uint numberOfChars, IntVector cursorPos) => 
            WriteConsoleOutputCharacter
            (
                StandardOutputHandle, 
                text, 
                numberOfChars, 
                new COORD((short) cursorPos.X, (short) cursorPos.Y)
            );
    }
}