using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public abstract class Shape
    {
        public static implicit operator Sprite(Shape shape) => shape.Sprite;

        private Sprite _sprite;
        
        protected abstract Color[] Colors { get; }
        protected abstract IntegerVector Size { get; }
        protected abstract Vector Offset { get; }
        protected abstract Pivot Pivot { get; }

        private Sprite Sprite => _sprite ??= new Sprite(GeneratePixels(Colors, Size), Size, Offset, Pivot);

        private Pixel[] GeneratePixels(IReadOnlyList<Color> colors, IntegerVector size)
        {
            var pixels = new Pixel[size.X * size.Y];
            var i = 0;
            
            for (var y = 0; y < size.Y; y++)
            {
                for (var x = 0; x < size.X; x++)
                {
                    pixels[i] = new Pixel(colors[i], new IntegerVector(x, y));
                    i++;
                }
            }
            return pixels;
        }
    }
}