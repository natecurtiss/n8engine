using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal abstract class Shape
    {
        public static implicit operator Sprite(Shape shape) => shape.Sprite;
        
        protected abstract Sprite Sprite { get; }

        protected Pixel[] GeneratePixels(Color[] colors, IntegerVector size)
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