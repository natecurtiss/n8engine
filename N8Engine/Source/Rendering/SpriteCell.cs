using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public readonly struct SpriteCell
    {
        public readonly string Color;
        public readonly Vector2 LocalPosition;

        public SpriteCell(in string color, in Vector2 localPosition)
        {
            Color = color;
            LocalPosition = localPosition;
        }
    }
}