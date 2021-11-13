using System.Drawing;
using N8Engine.Mathematics;
using N8Engine.Rendering;
using static N8Engine.Mathematics.Pivot;

namespace N8Engine
{
    public sealed class Sprite : IRenderable
    {
        readonly Pixel[] _pixels;
        Pixel[] IRenderable.Pixels => _pixels;

        Sprite(Bitmap image, Pivot pivot)
        {
            var size = new IntVector(image.Width, image.Height);
            _pixels = new Pixel[size.X * size.Y];

            var i = 0;
            var yFlip = size.Y - 1;
            for (var y = 0; y < size.Y; y++)
            {
                for (var x = 0; x < size.X; x++)
                {
                    var color = image.GetPixel(x, y);
                    var pos = new IntVector(x, yFlip).PivotOff(BottomLeft, pivot, size);
                    _pixels[i] = new Pixel(pos, color);
                    i++;
                }
                yFlip--;
            }
        }

        public static Sprite FromImage(string path, Pivot pivot = Center) => new(new Bitmap(path), pivot);
    }
}