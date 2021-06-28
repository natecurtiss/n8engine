using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal readonly struct SpriteCell
    {
        public readonly string Color;
        public readonly Vector2 DistanceFromPivot;

        public SpriteCell(in string color, in Vector2 distanceFromPivot)
        {
            Color = color;
            DistanceFromPivot = distanceFromPivot;
        }
    }
}