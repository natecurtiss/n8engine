using System;
using System.Runtime.InteropServices;

namespace N8Engine.Native
{
	internal static class ConsoleWriting
	{
	    private const int FOREGROUND_BLUE = 0x0001;
		private const int FOREGROUND_GREEN = 0x0002;
		private const int FOREGROUND_RED = 0x0004;
		private const int FOREGROUND_INTENSITY = 0x0008;
		private const int BACKGROUND_BLUE = 0x0010;
		private const int BACKGROUND_GREEN = 0x0020;
		private const int BACKGROUND_RED = 0x0040;
		private const int BACKGROUND_INTENSITY = 0x0080;
		private const int BLACK = 0x0000;
		
		[DllImport("kernel32.dll")]
		private static extern unsafe bool WriteConsoleOutput
		(
			IntPtr hConsoleOutput,
			[In] CharInfo[] lpBuffer,
			[In] Coordinate dwBufferSize,
			[In] Coordinate dwBufferCoordinate,
			[In,Out] SmallRectangle* lpWriteRegion
		);
		
	    public static void Write(in int width, in int height, in char[] characters, in ConsoleColor[] foregroundColors, in ConsoleColor[] backgroundColors)
	    {
		    CharInfo[] __buffer = new CharInfo[width * height];
		    for (int __i = 0; __i < width * height; __i++) 
			    __buffer[__i] = new CharInfo
			    (
				    characters[__i], 
				    (ushort)(foregroundColors[__i].AsForegroundHexadecimal() + backgroundColors[__i].AsBackgroundHexadecimal())
			    );
		    Coordinate __size = new((short) width, (short) height);
		    Coordinate __coordinate = new(0, 0);
		    Coordinate __bufferStart = new(0, 0);
		    SmallRectangle __writeRegion = new
		    (
			    __bufferStart.X,
			    __bufferStart.Y,
			    (short)(__bufferStart.X + __size.X - 1),
			    (short)(__bufferStart.Y + __size.Y - 1)
		    );
		
		    unsafe 
		    {
			    WriteConsoleOutput(ConsoleWindow.StandardOutputHandle, __buffer, __size, __coordinate, &__writeRegion);
		    }
	    }

	    private static int AsForegroundHexadecimal(this ConsoleColor color) =>
		    color switch
		    {
			    ConsoleColor.Black => BLACK,
			    ConsoleColor.DarkBlue => FOREGROUND_BLUE,
			    ConsoleColor.DarkGreen => FOREGROUND_GREEN,
			    ConsoleColor.DarkCyan => FOREGROUND_BLUE + FOREGROUND_GREEN,
			    ConsoleColor.DarkRed => FOREGROUND_RED,
			    ConsoleColor.DarkMagenta => FOREGROUND_BLUE + FOREGROUND_RED,
			    ConsoleColor.DarkYellow => FOREGROUND_RED + FOREGROUND_GREEN,
			    ConsoleColor.Gray => FOREGROUND_RED + FOREGROUND_GREEN + FOREGROUND_BLUE,
			    ConsoleColor.DarkGray => FOREGROUND_INTENSITY,
			    ConsoleColor.Blue => FOREGROUND_BLUE + FOREGROUND_INTENSITY,
			    ConsoleColor.Green => FOREGROUND_GREEN + FOREGROUND_INTENSITY,
			    ConsoleColor.Cyan => FOREGROUND_BLUE + FOREGROUND_GREEN + FOREGROUND_INTENSITY,
			    ConsoleColor.Red => FOREGROUND_RED + FOREGROUND_INTENSITY,
			    ConsoleColor.Magenta => FOREGROUND_BLUE + FOREGROUND_RED + FOREGROUND_INTENSITY,
			    ConsoleColor.Yellow => FOREGROUND_RED + FOREGROUND_GREEN + FOREGROUND_INTENSITY,
			    ConsoleColor.White => FOREGROUND_RED + FOREGROUND_GREEN + FOREGROUND_BLUE + FOREGROUND_INTENSITY,
			    _ => BLACK
		    };

	    private static int AsBackgroundHexadecimal(this ConsoleColor color) =>
		    color switch
		    {
			    ConsoleColor.Black => BLACK,
			    ConsoleColor.DarkBlue => BACKGROUND_BLUE,
			    ConsoleColor.DarkGreen => BACKGROUND_GREEN,
			    ConsoleColor.DarkCyan => BACKGROUND_BLUE + BACKGROUND_GREEN,
			    ConsoleColor.DarkRed => BACKGROUND_RED,
			    ConsoleColor.DarkMagenta => BACKGROUND_BLUE + BACKGROUND_RED,
			    ConsoleColor.DarkYellow => BACKGROUND_RED + BACKGROUND_GREEN,
			    ConsoleColor.Gray => BACKGROUND_RED + BACKGROUND_GREEN + BACKGROUND_BLUE,
			    ConsoleColor.DarkGray => BACKGROUND_INTENSITY,
			    ConsoleColor.Blue => BACKGROUND_BLUE + BACKGROUND_INTENSITY,
			    ConsoleColor.Green => BACKGROUND_GREEN + BACKGROUND_INTENSITY,
			    ConsoleColor.Cyan => BACKGROUND_BLUE + BACKGROUND_GREEN + BACKGROUND_INTENSITY,
			    ConsoleColor.Red => BACKGROUND_RED + BACKGROUND_INTENSITY,
			    ConsoleColor.Magenta => BACKGROUND_BLUE + BACKGROUND_RED + BACKGROUND_INTENSITY,
			    ConsoleColor.Yellow => BACKGROUND_RED + BACKGROUND_GREEN + BACKGROUND_INTENSITY,
			    ConsoleColor.White => BACKGROUND_RED + BACKGROUND_GREEN + BACKGROUND_BLUE + BACKGROUND_INTENSITY,
			    _ => BLACK
		    };
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal readonly struct SmallRectangle 
	{
		public readonly short Left;
		public readonly short Top;
		public readonly short Right;
		public readonly short Bottom;

		public SmallRectangle(in short left, in short top, in short right, in short bottom) 
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct Coordinate 
	{
		public readonly short X;
		public readonly short Y;

		public Coordinate(in short x, in short y) 
		{
			X = x;
			Y = y;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct CharInfo 
	{
		public readonly char Character;
		public ushort Attributes;

		public CharInfo(in char character, in ushort attribute) 
		{
			Character = character;
			Attributes = attribute;
		}
	}
}