using System.Diagnostics.Contracts;

namespace N8Engine.Mathematics
{
    public struct Rectangle
    {
        public Vector Size { get; set; }
        public Vector Position { get; set; }

        public Vector Extents => Size / 2f;
        public Vector Left => Vector.Left * Extents.X + Position;
        public Vector Right => Vector.Right * Extents.X + Position;
        public Vector Top => Vector.Up * Extents.Y + Position;
        public Vector Bottom => Vector.Down * Extents.Y + Position;

        public Rectangle(in Vector size, in Vector position = default) : this()
        {
            Size = size;
            Position = position;
        }

        [Pure]
        public bool IsPositionInside(in Vector otherPosition) => 
            otherPosition.X.IsWithinRange(Left.X, Right.X) && 
            otherPosition.Y.IsWithinRange(Bottom.Y, Top.Y);

        public bool IsOverlapping(in Rectangle otherRectangle) =>
            otherRectangle.IsPositionInside(Left) || otherRectangle.IsPositionInside(Right) || 
            otherRectangle.IsPositionInside(Top) || otherRectangle.IsPositionInside(Bottom) ||
            IsPositionInside(otherRectangle.Left) || IsPositionInside(otherRectangle.Right) || 
            IsPositionInside(otherRectangle.Top) || IsPositionInside(otherRectangle.Bottom);
    }
}