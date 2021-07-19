namespace N8Engine.Mathematics
{
    internal sealed class Rectangle
    {
        private readonly IMoveable _moveable;

        public Vector Size { get; set; }
        public Vector Position => _moveable.Position;
        public Vector Extents => Size / 2f;
        public Vector Left => Vector.Left * Extents.X + Position;
        public Vector Right => Vector.Right * Extents.X + Position;
        public Vector Top => Vector.Up * Extents.Y + Position;
        public Vector Bottom => Vector.Down * Extents.Y + Position;

        public Rectangle(Vector size, IMoveable moveable)
        {
            Size = size;
            _moveable = moveable;
        }
        
        public bool IsPositionInside(Vector otherPosition) => 
            otherPosition.X.IsWithinRange(Left.X, Right.X) && 
            otherPosition.Y.IsWithinRange(Bottom.Y, Top.Y);

        public bool IsOverlapping(Rectangle otherRectangle) =>
            otherRectangle.IsPositionInside(Left) || otherRectangle.IsPositionInside(Right) || 
            otherRectangle.IsPositionInside(Top) || otherRectangle.IsPositionInside(Bottom) ||
            IsPositionInside(otherRectangle.Left) || IsPositionInside(otherRectangle.Right) || 
            IsPositionInside(otherRectangle.Top) || IsPositionInside(otherRectangle.Bottom);
    }
}