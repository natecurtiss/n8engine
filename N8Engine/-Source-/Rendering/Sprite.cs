using System.Collections.Generic;
using System.Drawing;
using N8Engine.Mathematics;
using static N8Engine.Mathematics.Orientation;

namespace N8Engine.Rendering
{
    public sealed partial class Sprite
    {
        public static Sprite Empty => new();
        
        internal IEnumerable<Pixel> Pixels { get; private set; }
        internal Sprite FlippedHorizontally { get; private set; }
        internal Sprite FlippedVertically { get; private set; }
        internal Sprite FlippedHorizontallyAndVertically { get; private set; }

        public Sprite(string path, Vector offset = default, Pivot pivot = Pivot.Center)
        {
            var image = new Bitmap(path);
            var pixels = image.AsPixels();
            var size = new IntegerVector(image.Width, image.Height);
            CreateSprite(pixels, offset, size, pivot);
        }

        internal Sprite(string[] pixels)
        {
            if (pixels.Length == 0) return;
            return;
        }

        private Sprite(IEnumerable<Pixel> pixels) => Pixels = pixels;
        
        private Sprite() { }

        private void CreateSprite(Pixel[] pixels, IntegerVector offset, IntegerVector size, Pivot pivot)
        {
            pixels.Offset(offset);
            pixels.AdjustToPivot(new IntegerVector(size.X, size.Y), pivot);
            Pixels = pixels;
            FlippedHorizontally = new Sprite(pixels.Flipped(Horizontal));
            FlippedVertically = new Sprite(pixels.Flipped(Vertical));
            FlippedHorizontallyAndVertically = new Sprite(pixels.Flipped(HorizontalAndVertical));
        }
    }
}