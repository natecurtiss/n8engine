using System;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal readonly struct Pixel
    {
        public readonly Color Color;
        public readonly Vector2 Position;

        public Pixel(in Color color, in Vector2 position)
        {
            Color = color;
            Position = position;
        }
    }
}