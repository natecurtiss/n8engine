namespace N8Engine.Mathematics
{
    /// <summary>
    /// A set of extension methods for the <see cref="Direction"/> enum.
    /// </summary>
    public static class DirectionExtensions
    {
        /// <summary>
        /// Returns the <see cref="Vector"/> equivalent for the <see cref="Direction"/> passed in - 
        /// <see cref="Direction.Right">Direction.Right</see> returns a <see cref="Vector.Right">Vector of (1, 0)</see>,
        /// <see cref="Direction.Down">Direction.Down</see> returns a <see cref="Vector.Down">Vector of (0, -1)</see>, etc.
        /// </summary>
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