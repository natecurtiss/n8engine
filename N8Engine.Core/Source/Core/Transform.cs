using System.Numerics;

namespace N8Engine;

public sealed class Transform : Component
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public float Rotation { get; set; }

    public Transform() { }
    public Transform(Vector2 position) => Position = position;
    public Transform(Vector2 position, Vector2 scale) : this(position) => Scale = scale;
    public Transform(Vector2 position, Vector2 scale, float rotation) : this(position, scale) => Rotation = rotation;
}