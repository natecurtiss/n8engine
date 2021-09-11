using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed partial class Sprite
    {
        public static Sprite Empty => new();
        
        internal IEnumerable<Pixel> Pixels { get; private set; }
        internal Sprite FlippedHorizontally { get; private set; }
        internal Sprite FlippedVertically { get; private set; }
        internal Sprite FlippedBoth { get; private set; }

        public Sprite(string path, Vector offset = default)
        {
            var image = new Bitmap(path);
            var pixels = image.AsPixels();
            pixels.Offset(offset);
            pixels.Center(new IntegerVector(image.Width, image.Height));
            Pixels = pixels;
        }

        internal Sprite(string[] pixels)
        {
            if (pixels.Length == 0) return;
            return;
        }

        private Sprite(Pixel[] pixels) => Pixels = pixels;
        
        private Sprite() { }
    }
}