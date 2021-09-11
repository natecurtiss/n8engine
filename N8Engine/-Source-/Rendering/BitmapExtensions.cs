using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal static class BitmapExtensions
    {
        public static Pixel[] AsPixels(this Bitmap image)
        {
            var width = image.Width;
            var height = image.Height;
            var pixels = new Pixel[width * height];
            var index = 0;
            var upsideDownY = height - 1;
            
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    pixels[index] = new Pixel(image.GetPixel(x, upsideDownY), new IntegerVector(x, y));
                    index++;
                }
                upsideDownY--;
            }
            return pixels;
        }
    }
}