using N8Engine.Mathematics;

namespace N8Engine.Physics
{
    internal readonly struct BoundingBox
    {
        private Vector LeftSide { get; }
        private Vector RightSide { get; }
        private Vector TopSide { get; }
        private Vector BottomSide { get; }
        private Vector TopLeft { get; }
        private Vector BottomRight { get; }
        
        public BoundingBox(Vector size, Vector position = default)
        {
            var extents = size / 2f;
            LeftSide = new Vector(-extents.X, 0f) + position;
            RightSide = new Vector(extents.X, 0f) + position;
            TopSide = new Vector(0f, extents.Y) + position;
            BottomSide = new Vector(0f, -extents.Y) + position;
            TopLeft = new Vector(-extents.X, extents.Y) + position;
            BottomRight = new Vector(extents.X, -extents.Y) + position;
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
            var isUnderOtherRectangle = TopSide.Y <= otherBoundingBox.BottomSide.Y;
            var isOverOtherRectangle = BottomSide.Y >= otherBoundingBox.TopSide.Y;
            var isToTheRightOfOtherRectangle = LeftSide.X >= otherBoundingBox.RightSide.X;
            var isToTheLeftOfOtherRectangle = RightSide.X <= otherBoundingBox.LeftSide.X;
            
            if (isUnderOtherRectangle) return Direction.Down;
            if (isOverOtherRectangle) return Direction.Up;
            if (isToTheRightOfOtherRectangle) return Direction.Right;
            if (isToTheLeftOfOtherRectangle) return Direction.Left;
            return Direction.None;
        }
    }
}