using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Physics
{
    internal sealed class DebugRectangle
    {
        private Vector _size;
        private Sprite _sprite;

        public Vector Position { get; set; }
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

        private string[] Pixels
        {
            get
            {
                var x = (int) Size.X / N8SpriteFile.NUMBER_OF_PIXELS;
                var y = (int) Size.Y;
                const string color = "{Green,Green}";
                var pixels = new string[y];
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

        public DebugRectangle(Vector size, Vector position = default)
        {
            Size = size;
            Position = position;
        }
    }
}