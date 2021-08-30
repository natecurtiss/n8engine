using System.IO;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class Sprite
    {
        internal Pixel[] Pixels { get; private set; }

        public Sprite(string path, Vector offset = default, bool shouldFlipHorizontally = false, bool shouldFlipVertically = false)
        {
            var file = File.ReadAllLines(path);
            var data = new N8SpriteData(file);
            Pixels = data.Pixels.ToArray();
            CreateSprite(offset, shouldFlipHorizontally, shouldFlipVertically);
        }

        internal Sprite(Pixel[] pixels)
        {
            Pixels = pixels;
            CreateSprite();
        }

        private void CreateSprite(Vector offset = default, bool shouldFlipHorizontally = false, bool shouldFlipVertically = false)
        {
            OffsetPixels(offset);
            if (shouldFlipHorizontally) FlipPixelsHorizontally();
            if (shouldFlipVertically) FlipPixelsVertically();
        }

        private void OffsetPixels(Vector offset)
        {
            var pixels = new Pixel[Pixels.Length];
            for (var i = 0; i < Pixels.Length; i++)
            {
                var pixel = Pixels[i];
                pixels[i] = new Pixel(pixel.ForegroundColor, pixel.BackgroundColor, pixel.Position + new Vector(offset.X, -offset.Y));
            }
            Pixels = pixels;
        }

        private void FlipPixelsHorizontally()
        {
            var pixels = new Pixel[Pixels.Length];
            for (var i = 0; i < Pixels.Length; i++)
            {
                var pixel = Pixels[i];
                pixels[i] = new Pixel(pixel.ForegroundColor, pixel.BackgroundColor, pixel.Position * new Vector(-1f, 1f));
            }
            Pixels = pixels;
        }

        private void FlipPixelsVertically()
        {
            var pixels = new Pixel[Pixels.Length];
            for (var i = 0; i < Pixels.Length; i++)
            {
                var pixel = Pixels[i];
                pixels[i] = new Pixel(pixel.ForegroundColor, pixel.BackgroundColor, pixel.Position * new Vector(1f, -1f));
            }
            Pixels = pixels;
        }
    }
}