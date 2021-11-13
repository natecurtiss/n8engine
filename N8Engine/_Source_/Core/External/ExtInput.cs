using System;
using System.Runtime.InteropServices;
using N8Engine.InputSystem;

namespace N8Engine.External
{
    static class ExtInput
    {
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeystate
        [DllImport("user32.dll")]
        static extern short GetKeyState(Key key);
        
        const int IS_KEY_PRESSED = 0x8000;
        
        // https://stackoverflow.com/questions/6331868/using-getkeystate
        public static bool IsDown(Key key) => Convert.ToBoolean(GetKeyState(key) & IS_KEY_PRESSED);
    }
}