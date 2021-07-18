using System;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal struct Pixel
    {
        public static bool operator ==(in Pixel first, in Pixel second) => 
            first.ForegroundColor == second.ForegroundColor && 
            first.BackgroundColor == second.BackgroundColor;

        public static bool operator !=(Pixel first, Pixel second) => !(first == second);

        public readonly ConsoleColor ForegroundColor;
        public readonly ConsoleColor BackgroundColor;
        
        public Vector Position { get; set; }
        public int SortingOrder { get; set; }
        public bool IsBackground { get; }

        public Pixel(in ConsoleColor foregroundColor, in ConsoleColor backgroundColor, in Vector position, in bool isBackground = false)
        {
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            Position = position;
            SortingOrder = 0;
            IsBackground = isBackground;
        }
        
        public override bool Equals(object obj) => obj is Pixel other && this == other;

        public override int GetHashCode() => base.GetHashCode();
    }
}