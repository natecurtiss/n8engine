using System;
using System.Runtime.InteropServices;

namespace N8Engine.Native
{
    internal static class ConsoleQuickEditMode
    {
        /// <summary>
        /// The current enabled state of quick-edit mode in the <see cref="Console"/>.
        /// </summary>
        private const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        /// <summary>
        /// The current enabled state of extended flags in the <see cref="Console"/>.
        /// </summary>
        private const uint ENABLE_EXTENDED_FLAGS = 0x0080;

        /// <summary>
        /// Retrieves the current input or output mode of the <see cref="Console"/>.
        /// </summary>
        /// <param name="consoleHandle"> A handle to the <see cref="Console"/> input buffer (input) or <see cref="Console"/> screen buffer (output). </param>
        /// <param name="modeOutput"> A variable that receives the current mode of the specified buffer. </param>
        /// <returns> True if the function succeeds, false if not. </returns>
        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr consoleHandle, out uint modeOutput);
        
        /// <summary>
        /// Sets the input or output mode of the <see cref="Console"/>.
        /// </summary>
        /// <param name="consoleHandle"> A handle to the <see cref="Console"/> input buffer (input) or <see cref="Console"/> screen buffer (output). </param>
        /// <param name="mode"> The input or output mode to set. </param>
        /// <returns> True if the function succeeds, false if not. </returns>
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr consoleHandle, uint mode);

        /// <summary>
        /// True when quick-edit mode is enabled.
        /// </summary>
        public static bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                GetConsoleMode(ConsoleWindow.StandardInputHandle, out var consoleMode);
                if (value)
                    consoleMode |= ENABLE_QUICK_EDIT_MODE;
                else
                    consoleMode &= ~ENABLE_QUICK_EDIT_MODE;
                consoleMode |= ENABLE_EXTENDED_FLAGS;
                SetConsoleMode(ConsoleWindow.StandardInputHandle, consoleMode);
                _isEnabled = value;
            }
        }

        /// <summary>
        /// Backing field for <see cref="IsEnabled"/>.
        /// </summary>
        private static bool _isEnabled = true;
    }
}