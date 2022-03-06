using System.Numerics;

namespace N8Engine.Mathematics;

public sealed class Transform : Component
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public float Rotation { get; set; }
}