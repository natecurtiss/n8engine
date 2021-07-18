using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public struct DebugRectangle
    {
        private Vector _size;
        private Sprite _sprite;

        public Vector Position { get; set; }
        public Vector Size
        {
            get => _size;
            set
            {
                _sprite = new Sprite(new N8SpriteFile().GetPixels(Pixels).ToArray());
                _size = value;
            }
        }
        public Sprite Sprite => _sprite ??= new Sprite(new N8SpriteFile().GetPixels(Pixels).ToArray());

        internal string[] Pixels
        {
            get
            {
                const string __color = "{Green,Green}";
                string[] __pixels = new string[(int)Size.Y]; // TODO add thickness
                for (int __topRowPixel = 0; __topRowPixel < Size.X; __topRowPixel++) 
                    __pixels[0] += __color;
                for (int __bottomRowPixel = 0; __bottomRowPixel < Size.X; __bottomRowPixel++) 
                    __pixels[(int)Size.Y - 1] += __color;
                for (int __line = 1; __line < Size.Y - 1; __line++)
                {
                    for (int __pixel = 0; __pixel < Size.X; __pixel++)
                        if (__pixel == 0 || __pixel == (int)Size.X - 1)
                            __pixels[__line] += __color;
                        else
                            __pixels[__line] += "{Clear,Clear}";
                }

                return __pixels;
            }
        }

        public DebugRectangle(in Vector size, in Vector position) : this()
        {
            Size = size;
            Position = position;
        }
    }
}