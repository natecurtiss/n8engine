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
                Pivot.Center => pos,
                Pivot.Left => pos + right,
                Pivot.Right => pos + left,
                Pivot.Bottom => pos + up,
                Pivot.Top => pos + down,
                Pivot.BottomLeft => pos + up + right,
                Pivot.BottomRight => pos + up + left,
                Pivot.TopLeft => pos + down + right,
                Pivot.TopRight => pos + down + left,

                var _ => pos
            };
            var center = withCenterPivot;
            
            var newPos = to switch
            {
                Pivot.Center => center,
                Pivot.Top => center + up,
                Pivot.Bottom => center + down,
                Pivot.Right => center + right,
                Pivot.Left => center + left,
                Pivot.TopRight => center + right + up,
                Pivot.TopLeft => center + left + up,
                Pivot.BottomRight => center + right + down,
                Pivot.BottomLeft => center + left + down,
                var _ => center
            };
            return newPos;
        }
    }
}