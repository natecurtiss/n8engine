using System.Drawing;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine
{
    public sealed class Sprite : IRenderable
    {
        readonly IPixel[] _pixels;
        IPixel[] IRenderable.Pixels => _pixels;

        public Sprite(string path, Pivot pivot = Pivot.Center) : this(new Bitmap(path), pivot) { }

        public Sprite(Bitmap image, Pivot pivot = Pivot.Center)
        {
            var size = new IntVector(image.Width, image.Height);
            _pixels = new IPixel[size.X * size.Y];

            var i = 0;
            for (var y = 0; y < size.Y; y++)
            {
                for (var x = 0; x < size.X; x++)
                {
                    var color = image.GetPixel(x, y);
                    var position = new IntVector(x, y).AdjustedToPivot(Pivot.BottomLeft, size, pivot);
                    _pixels[i] = new ImagePixel(color, position);
                    i++;
                }
            }
        }
    }
}