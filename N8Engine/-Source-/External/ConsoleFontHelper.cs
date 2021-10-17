using System;
using System.Runtime.InteropServices;

namespace N8Engine.External
{
    internal static class ConsoleFontHelper
    {
        // https://docs.microsoft.com/en-us/windows/console/getcurrentconsolefontex
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool GetCurrentConsoleFontEx(IntPtr standardOutputHandle, bool useMaximumWindow, ref FontInfo currentConsoleFontOutput);
        
        // https://docs.microsoft.com/en-us/windows/console/setcurrentconsolefontex
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool SetCurrentConsoleFontEx(IntPtr standardOutputHandle, bool useMaximumWindow, ref FontInfo currentConsoleFontOutput);
        
        public static void SetCurrentFont(string font, short fontSize = 1, bool useMaximumWindow = false)
        {
            var before = new FontInfo()
            {
                SizeInBytes = Marshal.SizeOf<FontInfo>()
            };
            GetCurrentConsoleFontEx(CommonConsoleWindowInfo.StandardOutputHandle, useMaximumWindow, ref before);
            var after = new FontInfo()
            {
                SizeInBytes = Marshal.SizeOf<FontInfo>(),
                FontIndex = 0,
                FontFamily = 54,
                FontName = font,
                FontWeight = 400,
                FontSize = fontSize > 0 ? fontSize : before.FontSize
            };
            SetCurrentConsoleFontEx(CommonConsoleWindowInfo.StandardOutputHandle, useMaximumWindow, ref after);
        }
    }

    // https://docs.microsoft.com/en-us/windows/console/console-font-infoex
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