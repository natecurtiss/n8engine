using N8Engine.Mathematics;

namespace N8Engine.Physics
{
    internal struct BoundingBox
    {
        public Vector Left => Vector.Left * Extents.X + Position;
        public Vector Right => Vector.Right * Extents.X + Position;
        public Vector Top => Vector.Up * Extents.Y + Position;
        public Vector Bottom => Vector.Down * Extents.Y + Position;
        
        private Vector Position { get; }
        private Vector Size { get; }
        private Vector Extents => Size / 2f;
        private Vector TopLeft => Vector.Up * Extents.Y + Vector.Left * Extents.X + Position;
        private Vector BottomRight => Vector.Down * Extents.Y + Vector.Right * Extents.X + Position;

        public BoundingBox(Vector size, Vector position = default)
        {
            Size = size;
            Position = position;
        }
        
        public bool IsPositionInside(Vector otherPosition) => 
            otherPosition.X.IsWithinRange(Left.X, Right.X) && 
            otherPosition.Y.IsWithinRange(Bottom.Y, Top.Y);

        public bool IsOverlapping(BoundingBox otherBoundingBox)
        {
            var oneRectangleIsToTheRightOfTheOtherRectangle = TopLeft.X >= otherBoundingBox.BottomRight.X || otherBoundingBox.TopLeft.X >= BottomRight.X;
            if (oneRectangleIsToTheRightOfTheOtherRectangle)
                return false;
            var oneRectangleIsOnTopOfTheOtherRectangle = BottomRight.Y >= otherBoundingBox.TopLeft.Y || otherBoundingBox.BottomRight.Y >= TopLeft.Y;
            if (oneRectangleIsOnTopOfTheOtherRectangle)
                return false;
            return true;
        }

        public Direction DirectionRelativeTo(BoundingBox otherBoundingBox)
        {
            var isUnder = Top.Y <= otherBoundingBox.Bottom.Y;
            var isOver = Bottom.Y >= otherBoundingBox.Top.Y;
            var isToTheRight = Left.X >= otherBoundingBox.Right.X;
            var isToTheLeft = Right.X <= otherBoundingBox.Left.X;
            
            if (isUnder) return Direction.Down;
            if (isOver) return Direction.Top;
            if (isToTheRight) return Direction.Right;
            if (isToTheLeft) return Direction.Left;
            return Direction.None;
        }
    }
}