using System;
using System.IO;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed partial class Sprite
    {
        public static Sprite Empty => new();
        internal Pixel[] Pixels { get; private set; }

        public Sprite(string path, Vector offset = default, bool shouldFlipHorizontally = false, bool shouldFlipVertically = false)
        {
            var fileData = File.ReadAllLines(path);
            var data = new SpriteData(fileData);
            Pixels = data.GetPixels().ToArray();
            CreateSprite(offset, shouldFlipHorizontally, shouldFlipVertically);
        }

        internal Sprite(string[] pixels)
        {
            if (pixels.Length == 0) return;
            var data = new SpriteData(pixels);
            Pixels = data.GetPixels().ToArray();
            CreateSprite();
        }
        
        private Sprite() { }

        private void CreateSprite(IntegerVector offset = default, bool shouldFlipHorizontally = false, bool shouldFlipVertically = false)
        {
            OffsetPixels(offset);
            if (shouldFlipHorizontally) FlipPixelsHorizontally();
            if (shouldFlipVertically) FlipPixelsVertically();
        }

        private void OffsetPixels(IntegerVector offset)
        {
            var pixels = new Pixel[Pixels.Length];
            for (var i = 0; i < Pixels.Length; i++)
            {
                var pixel = Pixels[i];
                pixels[i] = new Pixel(pixel.ForegroundColor, pixel.BackgroundColor, pixel.Position + new IntegerVector(offset.X, -offset.Y));
            }
            Pixels = pixels;
        }

        private void FlipPixelsHorizontally()
        {
            var pixels = new Pixel[Pixels.Length];
            for (var i = 0; i < Pixels.Length; i++)
            {
                var pixel = Pixels[i];
                pixels[i] = new Pixel(pixel.ForegroundColor, pixel.BackgroundColor, pixel.Position * new IntegerVector(-1, 1));
            }
            Pixels = pixels;
        }

        private void FlipPixelsVertically()
        {
            var pixels = new Pixel[Pixels.Length];
            for (var i = 0; i < Pixels.Length; i++)
            {
                var pixel = Pixels[i];
                pixels[i] = new Pixel(pixel.ForegroundColor, pixel.BackgroundColor, pixel.Position * new IntegerVector(1, -1));
            }
            Pixels = pixels;
        }
    }
}