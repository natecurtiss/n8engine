using System.Collections.Generic;
using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class Sprite
    {
        public static Sprite Empty => new();
        
        internal IEnumerable<Pixel> Pixels { get; }
        internal Sprite FlippedHorizontally { get; }
        internal Sprite FlippedVertically { get; }
        internal Sprite FlippedHorizontallyAndVertically { get; }
        
        public Sprite(string path, Vector offset = default, Pivot pivot = Pivot.Center)
        {
            var image = new Bitmap(path);
            var pixels = image.AsPixels();
            var size = new IntegerVector(image.Width, image.Height);
            for (var i = 0; i < pixels.Length; i++)
            {
                var position = pixels[i].Position;
                var newPosition = position.AdjustedToPivot(Pivot.BottomLeft, size, pivot);
                pixels[i].Position = newPosition + offset;
            }

            Pixels = pixels;
            FlippedHorizontally = Flipped(pixels, Flip.Horizontal, size);
            FlippedVertically = Flipped(pixels, Flip.Vertical, size);
            FlippedHorizontallyAndVertically = Flipped(pixels, Flip.HorizontalAndVertical, size);
        }

        internal Sprite(string[] pixels)
        {
            if (pixels.Length == 0) return;
            return;
        }

        private Sprite(IEnumerable<Pixel> pixels) => Pixels = pixels;
        
        private Sprite() { }
        
        private static Sprite Flipped(IReadOnlyList<Pixel> pixels, Flip flip, IntegerVector size)
        {
            var flippedPixels = new Pixel[pixels.Count];
            var flipScale = flip switch
            {
                Flip.Horizontal => new IntegerVector(-1, 1),
                Flip.Vertical => new IntegerVector(1, -1),
                Flip.HorizontalAndVertical => new IntegerVector(-1, -1),
                var _ => IntegerVector.One
            };
            for (var index = 0; index < pixels.Count; index++)
            {
                var pixel = pixels[index];
                var flippedPixel = new Pixel(pixel.Color, pixel.Position * flipScale);
                flippedPixels[index] = flippedPixel;
            }
            return new Sprite(flippedPixels);
        }
    }
}