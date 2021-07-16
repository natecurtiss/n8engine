namespace N8Engine.Rendering
{
    public sealed class Sprite
    {
        internal readonly Pixel[] Pixels;

        public Sprite(in string path, in SpriteRenderer spriteRenderer)
        {
            N8SpriteFile __file = path;
            Pixels = __file.PixelsRelativeToCenterPixel.ToArray();
            for (int __i = 0; __i < Pixels.Length; __i++)
            {
                Pixels[__i].SortingOrder = spriteRenderer.SortingOrder;
            }
        }
    }
}