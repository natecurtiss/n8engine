using N8Engine.Debugging;
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
                _size = value;
                _sprite = new Sprite(new N8SpriteFile().GetPixels(Pixels).ToArray());
            }
        }
        public Sprite Sprite => _sprite ??= new Sprite(new N8SpriteFile().GetPixels(Pixels).ToArray());

        internal string[] Pixels
        {
            get
            {
                var x = (int) Size.X / N8SpriteFile.NUMBER_OF_PIXELS;
                var y = (int) Size.Y;
                const string color = "{Green,Green}";
                var pixels = new string[y]; // TODO add thickness
                for (var topRowPixelIndex = 0; topRowPixelIndex < x; topRowPixelIndex++) 
                    pixels[0] += color;
                for (var bottomRowPixelIndex = 0; bottomRowPixelIndex < x; bottomRowPixelIndex++) 
                    pixels[y - 1] += color;
                for (var lineIndex = 1; lineIndex < y - 1; lineIndex++)
                {
                    for (var pixel = 0; pixel < x; pixel++)
                        if (pixel == 0 || pixel == x - 1)
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