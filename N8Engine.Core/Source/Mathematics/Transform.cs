using System.Numerics;

namespace N8Engine.Mathematics;

public sealed class Transform : Component
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public float Rotation { get; set; }
}