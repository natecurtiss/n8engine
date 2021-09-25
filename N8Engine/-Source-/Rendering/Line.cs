using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class Line : BasicShape
    {
        protected override Color[] Colors { get; }
        protected override IntegerVector Size { get; }

        public Line(Color color, int length, Vector offset = default, Pivot pivot = Pivot.Center) : base(offset, pivot)
        {
            Colors = new Color[length];
            for (var i = 0; i < Colors.Length; i++)
                Colors[i] = color;
            Size = new IntegerVector(length, 1);
        }
    }
}