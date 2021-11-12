using System.Drawing;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine
{
    public sealed class Sprite : IRenderable
    {
        readonly Pixel[] _pixels;
        Pixel[] IRenderable.Pixels => _pixels;

        Sprite(Bitmap image)
        {
            var size = new IntVector(image.Width, image.Height);
            _pixels = new Pixel[size.X * size.Y];

            var i = 0;
            for (var y = 0; y < size.Y; y++)
            {
                for (var x = 0; x < size.X; x++)
                {
                    var color = image.GetPixel(x, y);
                    var pos = new IntVector(x, y) + size / 2; // TODO: adjust to pivot later.
                    _pixels[i] = new Pixel(pos, color);
                    i++;
                }
            }
        }

        public static Sprite FromImage(string path) => new(new Bitmap(path));
    }
}