using System;
using N8Engine.Mathematics;

namespace N8Engine.Physics
{
    internal readonly struct BoundingBox
    {
        public Vector Extents { get; }
        public Vector Left { get; }
        public Vector Right { get; }
        public Vector Top { get; }
        public Vector Bottom { get; }
        public Vector TopLeft { get; }
        public Vector BottomRight { get; }
        public Vector Position { get; }
        public string Name { get; }
        
        public BoundingBox(string name, Vector size, Vector position = default)
        {
            Name = name;
            Extents = size / 2f;
            Left = new Vector(-Extents.X, 0f) + position;
            Right = new Vector(Extents.X, 0f) + position;
            Top = new Vector(0f, Extents.Y) + position;
            Bottom = new Vector(0f, -Extents.Y) + position;
            TopLeft = new Vector(-Extents.X, Extents.Y) + position;
            BottomRight = new Vector(Extents.X, -Extents.Y) + position;
            Position = position;
        }

        public bool IsOverlapping(BoundingBox otherBoundingBox, bool debug = false)
        {
            var oneIsToTheRight = TopLeft.X > otherBoundingBox.BottomRight.X || otherBoundingBox.TopLeft.X > BottomRight.X;
            if (oneIsToTheRight)
                return false;
            var oneIsOnTop = BottomRight.Y > otherBoundingBox.TopLeft.Y || otherBoundingBox.BottomRight.Y > TopLeft.Y;
            if (oneIsOnTop)
            {
                if (debug)
                    Debug.Log(this + " " + otherBoundingBox);
                return false;
            }
            return true;
        }

        public Direction DirectionRelativeTo(BoundingBox otherBoundingBox)
        {
            var isUnderOtherRectangle = Top.Y <= otherBoundingBox.Bottom.Y;
            var isOverOtherRectangle = Bottom.Y >= otherBoundingBox.Top.Y;
            var isToTheRightOfOtherRectangle = Left.X >= otherBoundingBox.Right.X;
            var isToTheLeftOfOtherRectangle = Right.X <= otherBoundingBox.Left.X;
            
            if (isUnderOtherRectangle) return Direction.Down;
            if (isOverOtherRectangle) return Direction.Up;
            if (isToTheRightOfOtherRectangle) return Direction.Right;
            if (isToTheLeftOfOtherRectangle) return Direction.Left;
            return Direction.None;
        }

        public override string ToString() =>
            $"\n {Name} " +
            "\n { " +
            $"\n \t center: {Position}" + 
            $"\n \t extents: {Extents}" + 
            $"\n \t left: {Left}," +
            $"\n \t right: {Right}," +
            $"\n \t top: {Top}, " +
            $"\n \t bottom {Bottom}," +
            $"\n \t top left: {TopLeft}" +
            $"\n \t bottom right: {BottomRight}" +
            "\n }";
    }
}