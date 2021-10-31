using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace N8Engine.External
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    static class CommonStructures
    {
        [StructLayout(LayoutKind.Sequential)]
        public readonly struct COORD
        {
            public readonly short X;
            public readonly short Y;
            
            public COORD(short x, short y) 
            { 
                X = x;
                Y = y;
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public readonly struct RECT
        {
            public readonly int Left;
            public readonly int Top; 
            public readonly int Right;
            public readonly int Bottom;
        }
    }
}