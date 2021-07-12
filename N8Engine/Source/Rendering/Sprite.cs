namespace N8Engine.Rendering
{
    public sealed class Sprite
    {
        internal readonly Pixel[] Pixels;
        private int _sortingOrder;
        
        internal int SortingOrder
        {
            get => _sortingOrder;
            set
            {
                for (int __i = 0; __i < Pixels.Length; __i++)
                    Pixels[__i].SortingOrder = value;
                _sortingOrder = value;
            }
        }

        public Sprite(in string path)
        {
            N8SpriteFile __file = path;
            Pixels = __file.PixelsRelativeToCenterPixel.ToArray();
        }
    }
}