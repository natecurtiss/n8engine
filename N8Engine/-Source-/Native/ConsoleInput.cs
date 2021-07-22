using System;
using System.Runtime.InteropServices;
using N8Engine.Inputs;

namespace N8Engine.Native
{
    internal static class ConsoleInput
    {
        private const int KEY_PRESSED = 0x8000;
        
        [DllImport("user32.dll")]
        private static extern short GetKeyState(Key key);
        
        public static bool GetKeyDown(Key key) =>
            Convert.ToBoolean(GetKeyState(key) & KEY_PRESSED);
    }
}