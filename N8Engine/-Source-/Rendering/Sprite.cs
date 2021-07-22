namespace N8Engine.Rendering
{
    public sealed class Sprite
    {
        internal readonly Pixel[] Pixels;

        public Sprite(string path, SpriteRenderer spriteRenderer)
        {
            N8SpriteFile file = path;
            Pixels = file.GetPixels().ToArray();
            for (var i = 0; i < Pixels.Length; i++) 
                Pixels[i].SortingOrder = spriteRenderer.SortingOrder;
        }

        internal Sprite(Pixel[] pixels) => Pixels = pixels;
    }
}