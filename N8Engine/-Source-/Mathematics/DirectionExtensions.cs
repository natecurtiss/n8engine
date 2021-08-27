namespace N8Engine.Mathematics
{
    public static class DirectionExtensions
    {
        public static Vector AsVector(this Direction direction) => direction switch
        {

            Direction.Left => Vector.Left,
            Direction.Right => Vector.Right,
            Direction.Up => Vector.Up,
            Direction.Down => Vector.Down,
            Direction.None => Vector.Zero,
            var _ => Vector.Zero
        };
    }
}