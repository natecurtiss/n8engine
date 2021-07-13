using System;
using System.Runtime.InteropServices;

namespace N8Engine.Internal
{
    internal static class ConsoleText
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool GetCurrentConsoleFontEx(IntPtr standardOutputHandle, bool useMaximumWindow, ref FontInfo currentConsoleFontOutput);
        
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool SetCurrentConsoleFontEx(IntPtr standardOutputHandle, bool maximumWindow, ref FontInfo currentConsoleFontOutput);
        
        // note on below function: (you may delete this afterwards)
        // I don't know 100% what "marshalling" is, but I think I know the gist.
        // https://docs.microsoft.com/en-us/dotnet/framework/interop/marshaling-different-types-of-arrays
        // https://docs.microsoft.com/en-us/windows/console/reading-and-writing-blocks-of-characters-and-attributes
        // I used these microsoft doc pages to kinda guide me through what I should do to get this function binding to work
        
        // I've included quick example code that I've used to test and make sure this all works
        // I didn't happen to do any performance timing on the code, so I'm excited to hear what kind of framerates you get!
        
        [DllImport("kernel32.dll")]
	    public static unsafe extern bool WriteConsoleOutput(
		    IntPtr hConsoleOutput,
		    [In] CHAR_INFO[] lpBuffer, // the actual data, this can actually be a C# array, not sure how it's ok with that, but it seems to work.
		    [In] COORD dwBufferSize, // size of the entire 'lpBuffer' in 2-dimensions (so,  X * Y == lpBuffer.Length), smaller numbers will crop off btm-right corner
		    [In] COORD dwBUfferCoord, // normally should just be (0,0) This is just an offset for the 'lpBuffer' itself, so (0,0) will copy entire buffer
		    [In,Out] SMALL_RECT* lpWriteRegion); // This is the actual thing that tells you where in the console to copy the 'lpBuffer', overall size should be == 'dwBufferSize'

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
    
    // some basic struct 'bindings' for C Windows API
    
    [StructLayout(LayoutKind.Sequential)]
	public struct SMALL_RECT {
		public short left;
		public short top;
		public short right;
		public short bottom;

		public SMALL_RECT(short left, short top, short right, short bottom) {
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}
	}
    
    [StructLayout(LayoutKind.Sequential)]
	public struct COORD {
		public short X;
		public short Y;

		public COORD(short x, short y) {
			X = x;
			Y = y;
		}
	}
    
    // note about below struct: (delete this comment later)
    // Long ago when I tried figuing this stuff out, I didn't understand what a "union" was in C/C++
    // Turns out it's just a way of having a variable with "multitype representation", or somthing of the sort.
    // So I decided that "CHAR_INFO" which uses a "union" for the character data. I just decided to stick to the C# char type assuming that you'd only want to use ASCII
    // but if you wanna support unicode, then this might need to be changed
    
    [StructLayout(LayoutKind.Sequential)]
	public struct CHAR_INFO {
		public char character;
		public ushort attributes;

		public CHAR_INFO(char chr, ushort attrib) {
			character = chr;
			attributes = attrib;
		}
	}
    
    // THIS IS MY EXAMPLE TEST CODE (you can remove the 'WinAPI' bit. This code was in my main function.
    // For integrating this into your renderer, I personally reccomend do all your rendering to the C# arrray ('CHAR_INFO[] buffer')
    // So all of your rendering can be dealt with your own systems. Rather than using "WriteConsoleOutput" to render each pixel, which *may* cause more CPU overhead.
    // So do all your rendering in C#, then take the array buffer and **only call WriteConsoleOutput once per frame** so the scene is visible.
    /* 
    
    Console.WriteLine("This block of colorful characters has been rendered at (2,2) in the console buffer");
    WinAPI.CHAR_INFO[] buffer = new WinAPI.CHAR_INFO[64];
    Random rand = new Random();
    
    // generating random ASCII characters, with random FG/BG colors
    // Actually not sure what the higher 8 bits in the attribute do. Probably some interesting stuff.
    for(int i = 0; i < 64; i++) {
        buffer[i] = new WinAPI.CHAR_INFO((char)rand.Next(33, 127), (ushort)rand.Next(0x01, 0xFF));
    }

    WinAPI.COORD size = new WinAPI.COORD(8, 8);
    WinAPI.COORD coord = new WinAPI.COORD(0, 0);
    WinAPI.COORD bufferStart = new WinAPI.COORD(2, 2);

    WinAPI.SMALL_RECT writeRegion = new WinAPI.SMALL_RECT(
        bufferStart.X,
        bufferStart.Y,
        (short)(bufferStart.X + size.X - 1),
        (short)(bufferStart.Y + size.Y - 1));
	
    unsafe {
        WinAPI.WriteConsoleOutput(outHandle, buffer, size, coord, &writeRegion);
    }
    
    */
}
