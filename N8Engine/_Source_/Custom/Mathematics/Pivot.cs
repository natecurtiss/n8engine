using static N8Engine.Mathematics.Pivot;

namespace N8Engine.Mathematics
{
    public enum Pivot
    {
        Center,
        Left,
        Right,
        Bottom,
        Top,
        BottomLeft,
        BottomRight,
        TopLeft,
        TopRight
    }

    public static class PivotExtensions
    {
        public static Vector PivotOff(this Vector pos, Pivot from, Pivot to, Vector objSize)
        {
            // NOTE: These are supposed to be reversed like they are.
            var left = new Vector(objSize.X, 0) / 2f;
            var right = new Vector(-objSize.X, 0) / 2f;
            var down = new Vector(0, objSize.Y) / 2f;
            var up = new Vector(0, -objSize.Y) / 2f;
            
            var withCenterPivot = from switch
            {
                Center => pos,
                Left => pos + right,
                Right => pos + left,
                Bottom => pos + up,
                Top => pos + down,
                BottomLeft => pos + up + right,
                BottomRight => pos + up + left,
                TopLeft => pos + down + right,
                TopRight => pos + down + left,

                var _ => pos
            };
            var center = withCenterPivot;
            
            var newPos = to switch
            {
                Center => center,
                Top => center + up,
                Bottom => center + down,
                Right => center + right,
                Left => center + left,
                TopRight => center + right + up,
                TopLeft => center + left + up,
                BottomRight => center + right + down,
                BottomLeft => center + left + down,
                var _ => center
            };
            return newPos;
        }

        public static IntVector PivotOff(this IntVector pos, Pivot from, Pivot to, Vector objSize) => ((Vector) pos).PivotOff(from, to, objSize);
    }
}