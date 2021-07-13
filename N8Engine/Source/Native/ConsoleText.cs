using System;
using System.Runtime.InteropServices;

namespace N8Engine.Native
{
    internal static class ConsoleText
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool GetCurrentConsoleFontEx(IntPtr standardOutputHandle, bool useMaximumWindow, ref FontInfo currentConsoleFontOutput);
        
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool SetCurrentConsoleFontEx(IntPtr standardOutputHandle, bool maximumWindow, ref FontInfo currentConsoleFontOutput);

        public static void SetCurrentFont(in string font, in short fontSize = 1, in bool useMaximumWindow = false)
        {
            FontInfo __before = new()
            {
                SizeInBytes = Marshal.SizeOf<FontInfo>()
            };
            GetCurrentConsoleFontEx(ConsoleWindow.StandardOutputHandle, useMaximumWindow, ref __before);
            FontInfo __after = new()
            {
                SizeInBytes = Marshal.SizeOf<FontInfo>(),
                FontIndex = 0,
                FontFamily = 54,
                FontName = font,
                FontWeight = 400,
                FontSize = fontSize > 0 ? fontSize : __before.FontSize
            };
            SetCurrentConsoleFontEx(ConsoleWindow.StandardOutputHandle, useMaximumWindow, ref __after);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct FontInfo
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
}