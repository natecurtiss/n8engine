using N8Engine.Mathematics;

namespace N8Engine;

public sealed class Transform : Component
{
    public Vector Position { get; set; }
    public Vector Scale { get; set; }
    public float Rotation { get; set; }
}