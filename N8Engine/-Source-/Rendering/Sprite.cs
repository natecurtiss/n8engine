using System.Collections.Generic;
using System.Drawing;
using N8Engine.Mathematics;
using static N8Engine.Mathematics.Flip;
using static N8Engine.Mathematics.Pivot;

namespace N8Engine.Rendering
{
    public sealed class Sprite
    {
        public static Sprite Empty => new();
        
        internal IEnumerable<Pixel> Pixels { get; }
        internal Sprite FlippedHorizontally { get; }
        internal Sprite FlippedVertically { get; }
        internal Sprite FlippedHorizontallyAndVertically { get; }
        
        public Sprite(string path, Vector offset = default, Pivot pivot = Center)
        {
            var image = new Bitmap(path);
            var pixels = image.AsPixels();
            var size = new IntegerVector(image.Width, image.Height);
            pixels.Offset(offset);
            pixels.AdjustToPivot(size, pivot);
            
            Pixels = pixels;
            FlippedHorizontally = new Sprite(pixels.Flipped(Horizontal, size));
            FlippedVertically = new Sprite(pixels.Flipped(Vertical, size));
            FlippedHorizontallyAndVertically = new Sprite(pixels.Flipped(HorizontalAndVertical, size));
        }

        internal Sprite(string[] pixels)
        {
            if (pixels.Length == 0) return;
            return;
        }

        private Sprite(IEnumerable<Pixel> pixels) => Pixels = pixels;
        
        private Sprite() { }
    }
}