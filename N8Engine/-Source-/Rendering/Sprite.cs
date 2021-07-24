namespace N8Engine.Rendering
{
    public sealed class Sprite
    {
        internal readonly Pixel[] Pixels;

        public Sprite(string path, SpriteRenderer spriteRenderer)
        {
            var file = new N8SpriteFile(path);
            Pixels = file.Pixels.ToArray();
            for (var i = 0; i < Pixels.Length; i++) 
                Pixels[i].SortingOrder = spriteRenderer.SortingOrder;
        }

        internal Sprite(Pixel[] pixels) => Pixels = pixels;
    }
}