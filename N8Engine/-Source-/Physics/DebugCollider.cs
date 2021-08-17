using System.Collections.Generic;
using System.Text;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Physics
{
    internal sealed class DebugCollider
    {
        private readonly Collider _collider;

        private Vector _size;
        private Sprite _sprite;

        public Vector Position => _collider.Position;
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
                const string clear_color = "{Clear,Clear}";

                var pixelData = new string[height];
                var stringBuilder = new StringBuilder();
                
                for (var i = 0; i < width; i++)
                    stringBuilder.Append(green_color);
                pixelData[0] = stringBuilder.ToString();
                stringBuilder.Clear();

                for (var i = 0; i < width; i++)
                    stringBuilder.Append(green_color);
                pixelData[height - 1] = stringBuilder.ToString();
                stringBuilder.Clear();

                for (var line = 1; line < height - 1; line++)
                {
                    for (var pixel = 0; pixel < width; pixel++)
                        if (pixel == 0 || pixel == width - 1)
                            stringBuilder.Append(green_color);
                        else
                            stringBuilder.Append(clear_color);
                    pixelData[line] = stringBuilder.ToString();
                    stringBuilder.Clear();
                }
                
                var data = new N8SpriteData(pixelData);
                var sortedPixels = new List<Pixel>();
                foreach (var pixel in data.Pixels)
                {
                    var sortedPixel = pixel;
                    sortedPixel.SortingOrder = 1;
                    sortedPixels.Add(sortedPixel);
                }
                return sortedPixels.ToArray();
            }
        }

        public DebugCollider(Collider collider)
        {
            _collider = collider;
            Size = _collider.Size;
        }
    }
}