namespace N8Engine.Mathematics
{
    internal sealed class StaticObject : IMoveable
    {
        public static implicit operator StaticObject(Vector vector) => new(vector);

        public Vector Position { get; }

        public StaticObject(Vector position) => Position = position;
    }
}