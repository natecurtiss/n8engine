using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Physics
{
    internal sealed class DebugCollider
    {
        private Vector _size;
        private Sprite _sprite;

        public Vector Position { get; set; }
        public Sprite Sprite => _sprite ??= new Sprite(Pixels);
        public Vector Size
        {
            get => _size;
            set
            {
                if (_size == value) return;
                _size = value;
                _sprite = new Sprite(Pixels);
            }
        }

        private Pixel[] Pixels
        {
            get
            {
                var width = (int) Size.X;
                var height = (int) Size.Y;
                const string green_color = "{Green,Green}";
                var pixelData = new string[height];
                for (var i = 0; i < width; i++) 
                    pixelData[0] += green_color;
                for (var i = 0; i < width; i++) 
                    pixelData[height - 1] += green_color;
                for (var line = 1; line < height - 1; line++)
                {
                    for (var pixel = 0; pixel < width; pixel++)
                        if (pixel == 0 || pixel == width - 1)
                            pixelData[line] += green_color;
                        else
                            pixelData[line] += "{Clear,Clear}";
                }
                var data = new N8SpriteData(pixelData);
                return data.Pixels.ToArray();
            }
        }

        public DebugCollider(Vector size, Vector position = default)
        {
            Size = size;
            Position = position;
        }
    }
}