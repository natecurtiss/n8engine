using System.Text;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Physics
{
    internal sealed class ColliderVisualization
    {
        private IntegerVector _size;
        private Sprite _sprite;
        
        public Sprite Sprite => _sprite ??= new Sprite(GeneratePixelData());

        public void Redraw(IntegerVector size)
        {
            _size = size;
            if (_size.X == 0 || _size.Y == 0) return;
            Debug.Log(_size);
            _sprite = new Sprite(GeneratePixelData());
        }
        
        private string[] GeneratePixelData()
        {
            var width = _size.X;
            var height = _size.Y;

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
            return pixelData;
        }

        public ColliderVisualization(Collider collider) => Redraw(collider.Size);
    }
}