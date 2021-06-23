using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace N8Engine.Rendering
{
    /// <summary>
    /// Methods for customizing the Console window.
    /// </summary>
    internal static class ConsoleWindow
    {
        /// <summary>
        /// Native functions of the console.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        [SuppressMessage("ReSharper", "CA1069")]
        private static class NativeFunctions
        {
            /// <summary>
            /// I literally have no clue, I just copied this from stackoverflow.
            /// </summary>
            public enum StdHandle : int
            {
                STD_INPUT_HANDLE = -10,
                STD_OUTPUT_HANDLE = -11,
                STD_ERROR_HANDLE = -12,
            }

            /// <summary>
            /// I literally have no clue, I just copied this from stackoverflow.
            /// </summary>
            public enum ConsoleMode : uint
            {
                ENABLE_ECHO_INPUT = 0x0004,
                ENABLE_EXTENDED_FLAGS = 0x0080,
                ENABLE_INSERT_MODE = 0x0020,
                ENABLE_LINE_INPUT = 0x0002,
                ENABLE_MOUSE_INPUT = 0x0010,
                ENABLE_PROCESSED_INPUT = 0x0001,
                ENABLE_QUICK_EDIT_MODE = 0x0040,
                ENABLE_WINDOW_INPUT = 0x0008,
                ENABLE_VIRTUAL_TERMINAL_INPUT = 0x0200,
                
                ENABLE_PROCESSED_OUTPUT = 0x0001,
                ENABLE_WRAP_AT_EOL_OUTPUT = 0x0002,
                ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004,
                DISABLE_NEWLINE_AUTO_RETURN = 0x0008,
                ENABLE_LVB_GRID_WORLDWIDE = 0x0010
            }
            
            /// <summary>
            /// I literally have no clue, I just copied this from stackoverflow.
            /// </summary>
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr GetStdHandle(int nStdHandle);
            
            /// <summary>
            /// I literally have no clue, I just copied this from stackoverflow.
            /// </summary>
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

            /// <summary>
            /// I literally have no clue, I just copied this from stackoverflow.
            /// </summary>
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        }

        /// <summary>
        /// Enables quick-edit mode on the console if true.
        /// </summary>
        public static bool QuickEditMode
        {
            get => _quickEditMode;
            set
            {
                IntPtr __consoleHandle = NativeFunctions.GetStdHandle((int) NativeFunctions.StdHandle.STD_INPUT_HANDLE);

                NativeFunctions.GetConsoleMode(__consoleHandle, out uint __consoleMode);
                if (value)
                    __consoleMode |= (uint) NativeFunctions.ConsoleMode.ENABLE_QUICK_EDIT_MODE;
                else
                    __consoleMode &= ~(uint) NativeFunctions.ConsoleMode.ENABLE_QUICK_EDIT_MODE;

                __consoleMode |= (uint) NativeFunctions.ConsoleMode.ENABLE_EXTENDED_FLAGS;

                NativeFunctions.SetConsoleMode(__consoleHandle, __consoleMode);
                _quickEditMode = value;
            }
        }
        /// <summary>
        /// Backing field for the QuickEditMode property.
        /// </summary>
        private static bool _quickEditMode = true;
    }
}