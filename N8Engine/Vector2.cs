using SystemVector2 = System.Numerics.Vector2;

namespace N8Engine
{
    public struct Vector2
    {
        public static Vector2 Zero => new();
        public static Vector2 One => new(1f, 1f);
        public static Vector2 Up => new(0f, 1f);
        public static Vector2 Down => new(0f, -1f);
        public static Vector2 Right => new(1f);
        public static Vector2 Left => new (-1f);
        public static Vector2 UpRight => new(1f, 1f);
        public static Vector2 UpLeft => new(-1f, 1f);
        public static Vector2 DownRight => new(1f, -1f);
        public static Vector2 DownLeft => new(-1f, -1f);

        public float X { get; set; }
        public float Y { get; set; }

        public float Magnitude => this.AsSystemVector2().Length();
        public float SquareMagnitude => this.AsSystemVector2().LengthSquared();
        public Vector2 Normalized => SystemVector2.Normalize(this.AsSystemVector2()).AsVector2();

        public Vector2(in float x)
        {
            X = x;
            Y = 0f;
        }

        public Vector2(in float x, in float y)
        {
            X = x;
            Y = y;
        }

        public void Assign(out float x, out float y)
        {
            x = X;
            y = Y;
        }
    }

    public static class Vector2Extensions
    {
        public static SystemVector2 AsSystemVector2(this Vector2 vector2) => new(vector2.X, vector2.Y);
        public static Vector2 AsVector2(this SystemVector2 vector2) => new(vector2.X, vector2.Y);
    }
}