using System.Collections.Generic;
using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class Sprite
    {
        public static Sprite Empty => new();

        internal IEnumerable<Pixel> NotFlipped { get; private set; }
        internal IEnumerable<Pixel> FlippedHorizontally { get; private set; }
        internal IEnumerable<Pixel> FlippedVertically { get; private set; }
        internal IEnumerable<Pixel> FlippedHorizontallyAndVertically { get; private set; }

        public Sprite(string path, Vector offset = default, Pivot pivot = Pivot.Center)
        {
            var image = new Bitmap(path);
            var pixels = image.AsPixels();
            var size = new IntegerVector(image.Width, image.Height);
            CreateSprite(pixels, size, offset, pivot);
        }

        internal Sprite(Pixel[] pixels, IntegerVector size, Vector offset, Pivot pivot) => CreateSprite(pixels, size, offset, pivot);
        private Sprite() { }

        private IEnumerable<Pixel> Flipped(IReadOnlyList<Pixel> notFlippedPixels, Flip flip)
        {
            var flippedPixels = new Pixel[notFlippedPixels.Count];
            var flipScale = flip switch
            {
                Flip.Horizontal => new IntegerVector(-1, 1),
                Flip.Vertical => new IntegerVector(1, -1),
                Flip.HorizontalAndVertical => new IntegerVector(-1, -1),
                var _ => IntegerVector.One
            };
            for (var index = 0; index < notFlippedPixels.Count; index++)
            {
                var pixel = notFlippedPixels[index];
                var flippedPixel = new Pixel(pixel.Color, pixel.Position * flipScale);
                flippedPixels[index] = flippedPixel;
            }
            return flippedPixels;
        }

        private void CreateSprite(Pixel[] pixels, IntegerVector size, IntegerVector offset, Pivot pivot)
        {
            for (var i = 0; i < pixels.Length; i++)
            {
                var position = pixels[i].Position;
                var newPosition = position.AdjustedToPivot(Pivot.BottomLeft, size, pivot);
                pixels[i].Position = newPosition + offset;
            }

            NotFlipped = pixels;
            FlippedHorizontally = Flipped(pixels, Flip.Horizontal);
            FlippedVertically = Flipped(pixels, Flip.Vertical);
            FlippedHorizontallyAndVertically = Flipped(pixels, Flip.HorizontalAndVertical);
        }
    }
}