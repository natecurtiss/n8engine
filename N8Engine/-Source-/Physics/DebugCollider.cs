using System.Collections.Generic;
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
                var width = (int) Size.X / Renderer.NUMBER_OF_PIXELS;
                var height = (int) Size.Y;
                const string green_color = "{Green,Green}";
                const string clear_color = "{Clear,Clear}";
                const int top_row = 0;
                const int first_pixel_in_row = 0;
                var bottomRow = height - 1;
                var lastPixelInRow = width - 1;

                var pixelData = new string[height];
                for (var i = 0; i < width; i++) 
                    pixelData[top_row] += green_color;
                
                for (var i = 0; i < width; i++) 
                    pixelData[bottomRow] += green_color;
                
                for (var line = 1; line < bottomRow; line++)
                {
                    for (var pixel = 0; pixel < width; pixel++)
                        if (pixel == first_pixel_in_row || pixel == lastPixelInRow)
                            pixelData[line] += green_color;
                        else
                            pixelData[line] += clear_color;
                    pixelData[line] += '\n';
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