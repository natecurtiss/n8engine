using System;
using System.Runtime.InteropServices;

namespace N8Engine.External.Console
{
    static class ConsoleFont
    {
        // https://docs.microsoft.com/en-us/windows/console/getcurrentconsolefontex
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool GetCurrentConsoleFontEx(IntPtr standardOutputHandle, bool useMaximumWindow, ref FontInfo currentConsoleFontOutput);
        
        // https://docs.microsoft.com/en-us/windows/console/setcurrentconsolefontex
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool SetCurrentConsoleFontEx(IntPtr standardOutputHandle, bool useMaximumWindow, ref FontInfo currentConsoleFontOutput);
        
        public static void SetTo(string fontName, short fontSize, bool useMaximumWindow = false)
        {
            var before = new FontInfo
            {
                SizeInBytes = Marshal.SizeOf<FontInfo>()
            };
            GetCurrentConsoleFontEx(ConsoleInfo.StandardOutputHandle, useMaximumWindow, ref before);
            var after = new FontInfo
            {
                SizeInBytes = Marshal.SizeOf<FontInfo>(),
                FontIndex = 0,
                FontFamily = 54,
                FontName = fontName,
                FontWeight = 400,
                FontSize = fontSize > 0 ? fontSize : before.FontSize
            };
            SetCurrentConsoleFontEx(ConsoleInfo.StandardOutputHandle, useMaximumWindow, ref after);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    struct FontInfo
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