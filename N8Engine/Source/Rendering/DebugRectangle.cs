using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal sealed class DebugRectangle
    {
        private readonly IMoveable _moveable;
        private Vector _size;
        private Sprite _sprite;

        public Vector Position => _moveable.Position;
        public Vector Size
        {
            get => _size;
            set
            {
                if (_size == value) return;
                _sprite = new Sprite(new N8SpriteFile().GetPixels(Pixels).ToArray());
                _size = value;
            }
        }
        public Sprite Sprite => _sprite ??= new Sprite(new N8SpriteFile().GetPixels(Pixels).ToArray());

        internal string[] Pixels
        {
            get
            {
                const string color = "{Green,Green}";
                var pixels = new string[(int) Size.Y]; // TODO add thickness
                for (var topRowPixelIndex = 0; topRowPixelIndex < Size.X; topRowPixelIndex++) 
                    pixels[0] += color;
                for (var bottomRowPixelIndex = 0; bottomRowPixelIndex < Size.X; bottomRowPixelIndex++) 
                    pixels[(int)Size.Y - 1] += color;
                for (var lineIndex = 1; lineIndex < Size.Y - 1; lineIndex++)
                {
                    for (var pixel = 0; pixel < Size.X; pixel++)
                        if (pixel == 0 || pixel == (int) Size.X - 1)
                            pixels[lineIndex] += color;
                        else
                            pixels[lineIndex] += "{Clear,Clear}";
                }

                return pixels;
            }
        }

        public DebugRectangle(Vector size, IMoveable moveable)
        {
            Size = size;
            _moveable = moveable;
        }
    }
}