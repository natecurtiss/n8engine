using N8Engine.Mathematics;

namespace N8Engine.Physics
{
    internal readonly struct BoundingBox
    {
        public Vector Left { get; }
        public Vector Right { get; }
        public Vector Top { get; }
        public Vector BottomSide { get; }
        public Vector TopLeft { get; }
        public Vector BottomRight { get; }
        public Vector Position { get; }
        
        public BoundingBox(Vector size, Vector position = default)
        {
            var extents = size / 2f;
            Left = new Vector(-extents.X, 0f) + position;
            Right = new Vector(extents.X, 0f) + position;
            Top = new Vector(0f, extents.Y) + position;
            BottomSide = new Vector(0f, -extents.Y) + position;
            TopLeft = new Vector(-extents.X, extents.Y) + position;
            BottomRight = new Vector(extents.X, -extents.Y) + position;
            Position = position;
        }

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
            var isUnderOtherRectangle = Top.Y <= otherBoundingBox.BottomSide.Y;
            var isOverOtherRectangle = BottomSide.Y >= otherBoundingBox.Top.Y;
            var isToTheRightOfOtherRectangle = Left.X >= otherBoundingBox.Right.X;
            var isToTheLeftOfOtherRectangle = Right.X <= otherBoundingBox.Left.X;
            
            if (isUnderOtherRectangle) return Direction.Down;
            if (isOverOtherRectangle) return Direction.Up;
            if (isToTheRightOfOtherRectangle) return Direction.Right;
            if (isToTheLeftOfOtherRectangle) return Direction.Left;
            return Direction.None;
        }
    }
}