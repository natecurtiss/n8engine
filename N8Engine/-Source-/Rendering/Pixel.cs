using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal struct Pixel
    {
        public static bool operator ==(Pixel first, Pixel second) => first.Color == second.Color;
        public static bool operator !=(Pixel first, Pixel second) => !(first == second);

        public readonly Color Color;
        
        public IntegerVector Position { get; set; }
        public int SortingOrder { get; set; }

        public Pixel(Color color, IntegerVector position)
        {
            Color = color;
            Position = position;
            SortingOrder = 0;
        }
        
        public override bool Equals(object obj) => obj is Pixel other && this == other;

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"pixel at {Position} with color {Color}";
    }
}