using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class Sprite
    {
        internal readonly Pixel[] Pixels;

        public Sprite(string path, SpriteRenderer spriteRenderer, Vector offset = default, bool shouldFlipHorizontally = false, bool shouldFlipVertically = false)
        {
            var file = new N8SpriteFile(path);
            Pixels = file.Pixels.ToArray();
            
            {
                var pixels = new Pixel[Pixels.Length];
                for (var i = 0; i < Pixels.Length; i++)
                {
                    var pixel = Pixels[i];
                    pixels[i] = new Pixel(pixel.ForegroundColor, pixel.BackgroundColor, pixel.Position + new Vector(offset.X, -offset.Y), pixel.IsBackground);
                }
                Pixels = pixels;
            }
            
            if (shouldFlipHorizontally)
            {
                var pixels = new Pixel[Pixels.Length];
                for (var i = 0; i < Pixels.Length; i++)
                {
                    var pixel = Pixels[i];
                    pixels[i] = new Pixel(pixel.ForegroundColor, pixel.BackgroundColor, pixel.Position * new Vector(-1f, 1f), pixel.IsBackground);
                }
                Pixels = pixels;
            }
            
            if (shouldFlipVertically)
            {
                var pixels = new Pixel[Pixels.Length];
                for (var i = 0; i < Pixels.Length; i++)
                {
                    var pixel = Pixels[i];
                    pixels[i] = new Pixel(pixel.ForegroundColor, pixel.BackgroundColor, pixel.Position * new Vector(1f, -1f), pixel.IsBackground);
                }
                Pixels = pixels;
            }
            
            for (var i = 0; i < Pixels.Length; i++) 
                Pixels[i].SortingOrder = spriteRenderer.SortingOrder;
        }

        internal Sprite(Pixel[] pixels) => Pixels = pixels;
    }
}