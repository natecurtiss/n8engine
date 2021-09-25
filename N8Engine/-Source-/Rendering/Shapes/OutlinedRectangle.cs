using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class OutlinedRectangle : FilledRectangle
    {
        public OutlinedRectangle(Color color, IntegerVector size, Vector offset = default, Pivot pivot = Pivot.Center) : base(color, size, offset, pivot)
        {
            var i = 0;
            var topRow = size.Y - 1;
            var bottomRow = 0;
            var leftSide = 0;
            var rightSide = size.X - 1;
            for (var y = 0; y < size.Y; y++)
            {
                for (var x = 0; x < size.X; x++)
                {
                    if (x != leftSide && x != rightSide && y != bottomRow && y != topRow)
                        Colors[i] = Color.Transparent;
                    i++;
                }
            }
        }
    }
}